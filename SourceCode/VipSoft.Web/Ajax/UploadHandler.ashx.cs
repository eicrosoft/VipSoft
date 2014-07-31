using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Spring.Threading;

namespace VipSoft.Web.Ajax
{
    /// <summary>
    /// Summary description for UploadHandler
    /// </summary>
    public class UploadHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            var objId = context.Request.QueryString["id"];
            if (objId != null)
            {
                ShowThumbnail(context);
            }
            else
            {
                UploadThumbnail(context);
            }       
        }

        public void UploadThumbnail(HttpContext context)
        {
            System.Drawing.Image thumbnail_image = null;
            System.Drawing.Image original_image = null;
            System.Drawing.Bitmap final_image = null;
            System.Drawing.Graphics graphic = null;
            MemoryStream ms = null;
            try
            {
                // Get the data
                HttpPostedFile jpeg_image_upload = context.Request.Files["Filedata"];
                context.Session["OriginalImage"] = jpeg_image_upload;
                // Retrieve the uploaded image
                original_image = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);

                // Calculate the new width and height
                int width = original_image.Width;
                int height = original_image.Height;
                int target_width = 190;
                int target_height = 225;
                int new_width, new_height;

                float target_ratio = (float)target_width / (float)target_height;
                float image_ratio = (float)width / (float)height;

                if (target_ratio > image_ratio)
                {
                    new_height = target_height;
                    new_width = (int)Math.Floor(image_ratio * (float)target_height);
                }
                else
                {
                    new_height = (int)Math.Floor((float)target_width / image_ratio);
                    new_width = target_width;
                }

                new_width = new_width > target_width ? target_width : new_width;
                new_height = new_height > target_height ? target_height : new_height;


                // Create the thumbnail

                // Old way
                //thumbnail_image = original_image.GetThumbnailImage(new_width, new_height, null, System.IntPtr.Zero);
                // We don't have to create a Thumbnail since the DrawImage method will resize, but the GetThumbnailImage looks better
                // I've read about a problem with GetThumbnailImage. If a jpeg has an embedded thumbnail it will use and resize it which
                //  can result in a tiny 40x40 thumbnail being stretch up to our target size


                final_image = new System.Drawing.Bitmap(target_width, target_height);
                graphic = System.Drawing.Graphics.FromImage(final_image);
                //加了就有黑边
                //graphic.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), new System.Drawing.Rectangle(0, 0, target_width, target_height));
                int paste_x = (target_width - new_width) / 2;
                int paste_y = (target_height - new_height) / 2;
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; /* new way */
                //graphic.DrawImage(thumbnail_image, paste_x, paste_y, new_width, new_height);
                graphic.DrawImage(original_image, paste_x, paste_y, new_width, new_height);

                // Store the thumbnail in the session (Note: this is bad, it will take a lot of memory, but this is just a demo)
                ms = new MemoryStream();
                final_image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                // Store the data in my custom Thumbnail object
                string thumbnail_id = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                Thumbnail thumb = new Thumbnail(thumbnail_id, ms.GetBuffer());

                // Put it all in the Session (initialize the session if necessary)			
                List<Thumbnail> thumbnails = context.Session["file_info"] as List<Thumbnail>;
                if (thumbnails == null)
                {
                    thumbnails = new List<Thumbnail>();
                    context.Session["file_info"] = thumbnails;
                }
                thumbnails.Add(thumb);

                context.Response.StatusCode = 200;
                context.Response.Write(thumbnail_id);
            }
            catch
            {
                // If any kind of error occurs return a 500 Internal Server error
                context.Response.StatusCode = 500;
                context.Response.Write("An error occured");
                context.Response.End();
            }
            finally
            {
                // Clean up
                if (final_image != null) final_image.Dispose();
                if (graphic != null) graphic.Dispose();
                if (original_image != null) original_image.Dispose();
                if (thumbnail_image != null) thumbnail_image.Dispose();
                if (ms != null) ms.Close();
                context.Response.End();
            }

            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
        }

        private void ShowThumbnail(HttpContext context)
        {
            string id = context.Request.QueryString["id"] as string;
            if (id == null)
            {
                context.Response.StatusCode = 404;
                context.Response.Write("Not Found");
                context.Response.End();
                return;
            }

            List<Thumbnail> thumbnails = context.Session["file_info"] as List<Thumbnail>;

            if (thumbnails == null)
            {
                context.Response.StatusCode = 404;
                context.Response.Write("Not Found");
                context.Response.End();
                return;
            }

            foreach (Thumbnail thumb in thumbnails)
            {
                if (thumb.ID == id)
                {
                    context.Response.ContentType = "image/jpeg";
                    context.Response.BinaryWrite(thumb.Data);
                    context.Response.End();
                    return;
                }
            }

            // If we reach here then we didn't find the file id so return 404
            context.Response.StatusCode = 404;
            context.Response.Write("Not Found");
            context.Response.End();
        }


        /*
         
          public void ProcessRequest1(HttpContext context)
        {
            //取得处事类型
            string action = "";// DTRequest.GetQueryString("action");

            switch (action)
            {
                case "SingleFile": //单文件
                    SingleFile(context);
                    break;
                case "MultipleFile": //多文件
                    MultipleFile(context);
                    break;
                case "AttachFile": //附件
                    AttachFile(context);
                    break;
                case "EditorFile": //编辑器文件
                    EditorFile(context);
                    break;
                case "ManagerFile": //管理文件
                    ManagerFile(context);
                    break;
            }

        }
  
        #region 上传单文件处理===================================
        private void SingleFile(HttpContext context)
        {
            string _refilepath = DTRequest.GetQueryString("ReFilePath"); //取得返回的对象名称
            string _upfilepath = DTRequest.GetQueryString("UpFilePath"); //取得上传的对象名称
            string _delfile = DTRequest.GetString(_refilepath);
            HttpPostedFile _upfile = context.Request.Files[_upfilepath];
            bool _iswater = false; //默认不打水印
            bool _isthumbnail = false; //默认不生成缩略图
            bool _isimage = false;

            if (DTRequest.GetQueryString("IsWater") == "1")
                _iswater = true;
            if (DTRequest.GetQueryString("IsThumbnail") == "1")
                _isthumbnail = true;
            if (DTRequest.GetQueryString("IsImage") == "1")
                _isimage = true;

            if (_upfile == null)
            {
                context.Response.Write("{msg: 0, msgbox: \"请选择要上传文件！\"}");
                return;
            }
            UpLoad upFiles = new UpLoad();
            string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater, _isimage);
            //删除已存在的旧文件
            Utils.DeleteUpFile(_delfile);
            //返回成功信息
            context.Response.Write(msg);
            context.Response.End();
        }
        #endregion

        #region 上传多文件处理===================================
        private void MultipleFile(HttpContext context)
        {
            string _upfilepath = context.Request.QueryString["UpFilePath"]; //取得上传的对象名称
            HttpPostedFile _upfile = context.Request.Files[_upfilepath];
            bool _iswater = false; //默认不打水印
            bool _isthumbnail = false; //默认不生成缩略图

            if (context.Request.QueryString["IsWater"] == "1")
                _iswater = true;
            if (context.Request.QueryString["IsThumbnail"] == "1")
                _isthumbnail = true;

            if (_upfile == null)
            {
                context.Response.Write("{msg: 0, msgbox: \"请选择要上传文件！\"}");
                return;
            }
            UpLoad upFiles = new UpLoad();
            string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater);
            //返回成功信息
            context.Response.Write(msg);
            context.Response.End();
        }
        #endregion

        #region 上传附件处理=====================================
        private void AttachFile(HttpContext context)
        {
            string _upfilepath = context.Request.QueryString["UpFilePath"]; //取得上传的对象名称
            HttpPostedFile _upfile = context.Request.Files[_upfilepath];
            bool _iswater = false; //默认不打水印
            bool _isthumbnail = false; //默认不生成缩略图

            if (_upfile == null)
            {
                context.Response.Write("{msg: 0, msgbox: \"请选择要上传文件！\"}");
                return;
            }
            UpLoad upFiles = new UpLoad();
            string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater, false, true);
            //返回成功信息
            context.Response.Write(msg);
            context.Response.End();
        }
        #endregion

        #region 编辑器上传处理===================================
        private void EditorFile(HttpContext context)
        {
            bool _iswater = false; //默认不打水印
            if (context.Request.QueryString["IsWater"] == "1")
                _iswater = true;
            HttpPostedFile imgFile = context.Request.Files["imgFile"];
            if (imgFile == null)
            {
                showError(context, "请选择要上传文件！");
                return;
            }
            UpLoad upFiles = new UpLoad();
            string remsg = upFiles.fileSaveAs(imgFile, false, _iswater);
            string pattern = @"^{\s*msg:\s*(.*)\s*,\s*msgbox:\s*\""(.*)\""\s*}$"; //键名前和键值前后都允许出现空白字符
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase); //正则表达式实例，不区分大小写
            Match m = r.Match(remsg); //搜索匹配项
            string msg = m.Groups[1].Value; //msg的值，正则表达式中第1个圆括号捕获的值
            string msgbox = m.Groups[2].Value; //msgbox的值，正则表达式中第2个圆括号捕获的值 
            if (msg == "0")
            {
                showError(context, msgbox);
                return;
            }
            Hashtable hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = msgbox;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(hash));
            context.Response.End();

        }
        //显示错误
        private void showError(HttpContext context, string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(hash));
            context.Response.End();
        }
        #endregion

        #region 浏览文件处理=====================================
        private void ManagerFile(HttpContext context)
        {
            Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(Utils.GetXmlMapPath("Configpath"));
            //String aspxUrl = context.Request.Path.Substring(0, context.Request.Path.LastIndexOf("/") + 1);

            //根目录路径，相对路径
            String rootPath = siteConfig.webpath + siteConfig.attachpath + "/"; //站点目录+上传目录
            //根目录URL，可以指定绝对路径，比如 http://www.yoursite.com/attached/
            String rootUrl = siteConfig.webpath + siteConfig.attachpath + "/";
            //图片扩展名
            String fileTypes = "gif,jpg,jpeg,png,bmp";

            String currentPath = "";
            String currentUrl = "";
            String currentDirPath = "";
            String moveupDirPath = "";

            String dirPath = Utils.GetMapPath(rootPath);
            String dirName = context.Request.QueryString["dir"];
            //if (!String.IsNullOrEmpty(dirName))
            //{
            //    if (Array.IndexOf("image,flash,media,file".Split(','), dirName) == -1)
            //    {
            //        context.Response.Write("Invalid Directory name.");
            //        context.Response.End();
            //    }
            //    dirPath += dirName + "/";
            //    rootUrl += dirName + "/";
            //    if (!Directory.Exists(dirPath))
            //    {
            //        Directory.CreateDirectory(dirPath);
            //    }
            //}

            //根据path参数，设置各路径和URL
            String path = context.Request.QueryString["path"];
            path = String.IsNullOrEmpty(path) ? "" : path;
            if (path == "")
            {
                currentPath = dirPath;
                currentUrl = rootUrl;
                currentDirPath = "";
                moveupDirPath = "";
            }
            else
            {
                currentPath = dirPath + path;
                currentUrl = rootUrl + path;
                currentDirPath = path;
                moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
            }

            //排序形式，name or size or type
            String order = context.Request.QueryString["order"];
            order = String.IsNullOrEmpty(order) ? "" : order.ToLower();

            //不允许使用..移动到上一级目录
            if (Regex.IsMatch(path, @"\.\."))
            {
                context.Response.Write("Access is not allowed.");
                context.Response.End();
            }
            //最后一个字符不是/
            if (path != "" && !path.EndsWith("/"))
            {
                context.Response.Write("Parameter is not valid.");
                context.Response.End();
            }
            //目录不存在或不是目录
            if (!Directory.Exists(currentPath))
            {
                context.Response.Write("Directory does not exist.");
                context.Response.End();
            }

            //遍历目录取得文件信息
            string[] dirList = Directory.GetDirectories(currentPath);
            string[] fileList = Directory.GetFiles(currentPath);

            switch (order)
            {
                case "size":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new SizeSorter());
                    break;
                case "type":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new TypeSorter());
                    break;
                case "name":
                default:
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new NameSorter());
                    break;
            }

            Hashtable result = new Hashtable();
            result["moveup_dir_path"] = moveupDirPath;
            result["current_dir_path"] = currentDirPath;
            result["current_url"] = currentUrl;
            result["total_count"] = dirList.Length + fileList.Length;
            List<Hashtable> dirFileList = new List<Hashtable>();
            result["file_list"] = dirFileList;
            for (int i = 0; i < dirList.Length; i++)
            {
                DirectoryInfo dir = new DirectoryInfo(dirList[i]);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = true;
                hash["has_file"] = (dir.GetFileSystemInfos().Length > 0);
                hash["filesize"] = 0;
                hash["is_photo"] = false;
                hash["filetype"] = "";
                hash["filename"] = dir.Name;
                hash["datetime"] = dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            for (int i = 0; i < fileList.Length; i++)
            {
                FileInfo file = new FileInfo(fileList[i]);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = false;
                hash["has_file"] = false;
                hash["filesize"] = file.Length;
                hash["is_photo"] = (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) >= 0);
                hash["filetype"] = file.Extension.Substring(1);
                hash["filename"] = file.Name;
                hash["datetime"] = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            context.Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(result));
            context.Response.End();
        }

        #region Helper
        public class NameSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.FullName.CompareTo(yInfo.FullName);
            }
        }

        public class SizeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Length.CompareTo(yInfo.Length);
            }
        }

        public class TypeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Extension.CompareTo(yInfo.Extension);
            }
        }
        #endregion
        #endregion
        */

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class Thumbnail
    {
        public Thumbnail(string id, byte[] data)
        {
            this.ID = id;
            this.Data = data;
        }

        public string ID { get; set; }

        public byte[] Data { get; set; }

    }

}