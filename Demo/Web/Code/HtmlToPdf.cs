using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpertPdf.HtmlToPdf;

namespace Demo.Code
{
    public class HtmlToPdf : PdfConverter
    {
        public HtmlToPdf()
        {
            base.PdfDocumentOptions.PdfPageSize = PdfPageSize.Letter;
            base.PdfDocumentOptions.AutoSizePdfPage = true;
            base.PdfDocumentOptions.FitWidth = true;
            base.PdfDocumentOptions.TopMargin = 50;
            base.PdfDocumentOptions.LeftMargin = 50;
            base.PdfDocumentOptions.RightMargin = 50;
            base.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
            base.PdfDocumentOptions.ShowHeader = false;
            base.PdfDocumentOptions.ShowFooter = false;
        }

        /// <summary>
        /// Convert html to pdf and output the pdf file
        /// For addidional doc: http://www.html-to-pdf.net/docs/html-to-pdf-library/html/26d5208a-eac4-bf15-f805-f03a4d5bdeb9.htm
        /// </summary>
        /// <param name="downloadName">File extension .pdf will be added if absent</param>
        /// <param name="HtmlCodeToOutput">Rendered HTML code from original aspx page</param>
        /// <param name="UrlToOutput">Original page url for CSS and images referencing</param>
        public virtual void OutputAsPdf(string DownloadName, string HtmlCodeToOutput, string UrlToOutput)
        {
            //add ".pdf" is absent
            if (DownloadName.ToLower().IndexOf(".pdf") == -1)
            {
                DownloadName += ".pdf";
            }
   
            byte[] downloadBytes = base.GetPdfBytesFromHtmlString(HtmlCodeToOutput, UrlToOutput);

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Clear();
            response.AddHeader("Content-Type", "binary/octet-stream");
            response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}; size={1}", DownloadName, downloadBytes.Length.ToString()));
            response.Flush();
            response.BinaryWrite(downloadBytes);
            response.Flush();
            response.End();
        }
    }
}