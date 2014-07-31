using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VipSoft.WebControls
{
    public delegate void PageChangedEventHandler(object sender, PageChangedEventArgs e);


    public class VirtualRecordCount
    {
        public int PageCount;
        public int RecordCount;
        public int RecordsInLastPage;
    }

    public enum CacheMode
    {
        Cached,
        NonCached
    }

    public enum PagerMode
    {
        logistic,
        physical
    }
    public class PageChangedEventArgs : EventArgs
    {
        public int NewPageIndex;
        public int OldPageIndex;
    }

    [DefaultEvent("PageIndexChanged"), Localizable(true), ToolboxData("<{0}:Pagination runat=\"server\" />")]
    public class Pagination : System.Web.UI.WebControls.WebControl, INamingContainer
    {
        private Control controlToPaginate;
        private object dataBindSource;
        private object dataSource;
        private PagedDataSource pds = new PagedDataSource();

        public event PageChangedEventHandler PageIndexChanged;

        public Pagination()
        {
            this.CacheMode = CacheMode.Cached;
            this.PagerMode = PagerMode.physical;
            this.PageIndex = 0;
            this.PageSize = 10;
            this.CacheDuration = 60;
            this.ShowFirstButton = true;
            this.ShowNextButton = true;
            this.ShowPreviousButton = true;
            this.ShowLastButton = true;
            this.ShowJumpPage = true;
            this.ShowStat = true;
        }

        private void BuildControlHierarchy()
        {
            string str = string.IsNullOrEmpty(this.CssClass) ? "Pagination" : this.CssClass;
            Panel panel4 = new Panel();
            panel4.CssClass = str;
            Panel child = panel4;
            Panel panel5 = new Panel();
            panel5.CssClass = "PageLabel";
            Panel cell = panel5;
            Panel panel6 = new Panel();
            panel6.CssClass = " PagerButton";
            Panel panel3 = panel6;
            if (this.ShowStat)
            {
                this.BuildCurrentPage(cell);
                child.Controls.Add(cell);
            }
            this.BuildNextPrevUI(panel3);
            if (this.ShowJumpPage)
            {
                this.BuildDropDownUI(panel3);
            }
            child.Controls.Add(panel3);
            this.Controls.Add(child);
        }

        private void BuildCurrentPage(Control cell)
        {
            if ((this.PageIndex >= 0) && (this.PageIndex <= this.PageCount))
            {
                Label label2 = new Label();
                label2.Text = string.Format(GetResource("InfoText"), this.RecordCount, this.PageIndex + 1, this.PageCount);
                Label child = label2;
                cell.Controls.Add(child);
            }
        }

        private void BuildDropDownUI(Control cell)
        {
            DropDownList list2 = new DropDownList();
            list2.ID = "PageList";
            list2.SkinID = "PageList";
            list2.AutoPostBack = true;
            DropDownList child = list2;
            child.SelectedIndexChanged += new EventHandler(this.PageList_Click);
            child.Font.Name = this.Font.Name;
            child.Font.Size = this.Font.Size;
            child.ForeColor = this.ForeColor;
            if ((this.PageCount <= 0) || (this.PageIndex == -1))
            {
                child.Items.Add("");
                child.Enabled = false;
                child.SelectedIndex = 0;
            }
            else
            {
                child.Enabled = true;
                for (int i = 1; i <= this.PageCount; i++)
                {
                    int num2 = i - 1;
                    ListItem item = new ListItem(i.ToString(), num2.ToString());
                    child.Items.Add(item);
                }
                child.SelectedIndex = this.PageIndex;
            }
            cell.Controls.Add(child);
        }

        private void BuildNextPrevUI(Control cell)
        {
            bool flag = (this.PageIndex >= 0) && (this.PageIndex <= (this.PageCount - 1));
            bool flag2 = this.PageIndex > 0;
            bool flag3 = this.PageIndex < (this.PageCount - 1);
            if (this.ShowFirstButton)
            {
                LinkButton button2 = new LinkButton();
                button2.ID = "First";
                button2.CausesValidation = false;
                LinkButton child = button2;
                child.Click += new EventHandler(this.first_Click);
                child.Text = GetResource("FirstText");
                child.Enabled = flag && flag2;
                cell.Controls.Add(child);
            }
            if (this.ShowPreviousButton)
            {
                LinkButton button4 = new LinkButton();
                button4.ID = "Prev";
                button4.CausesValidation = false;
                LinkButton button3 = button4;
                button3.Click += new EventHandler(this.prev_Click);
                button3.Text = GetResource("PreviousText");
                button3.Enabled = flag && flag2;
                cell.Controls.Add(button3);
            }
            if (this.ShowNextButton)
            {
                LinkButton button6 = new LinkButton();
                button6.ID = "Next";
                button6.CausesValidation = false;
                LinkButton button5 = button6;
                button5.Click += new EventHandler(this.next_Click);
                button5.Text = GetResource("NextText");
                button5.Enabled = flag && flag3;
                cell.Controls.Add(button5);
            }
            if (this.ShowLastButton)
            {
                LinkButton button8 = new LinkButton();
                button8.ID = "Last";
                button8.CausesValidation = false;
                LinkButton button7 = button8;
                button7.Click += new EventHandler(this.last_Click);
                button7.Text = GetResource("LastText");
                button7.Enabled = flag && flag3;
                cell.Controls.Add(button7);
            }
        }

        public void ClearCache()
        {
            if (this.CacheMode == CacheMode.Cached)
            {
                this.Page.Cache.Remove(this.CacheKeyName);
            }
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            base.ClearChildViewState();
            this.BuildControlHierarchy();
        }

        public override void DataBind()
        {
            base.DataBind();
            base.ChildControlsCreated = false;
            if (this.ControlToPaginate != "")
            {
                this.controlToPaginate = this.Parent.FindControl(this.ControlToPaginate);
                if ((this.controlToPaginate != null) && (((this.controlToPaginate is BaseDataList) || (this.controlToPaginate is ListControl)) || ((this.controlToPaginate is Repeater) || (this.controlToPaginate is CompositeDataBoundControl))))
                {
                    this.FetchAllData();
                    if (this.controlToPaginate is BaseDataList)
                    {
                        ((BaseDataList)this.controlToPaginate).DataSource = this.dataBindSource;
                    }
                    else if (this.controlToPaginate is CompositeDataBoundControl)
                    {
                        ((CompositeDataBoundControl)this.controlToPaginate).DataSource = this.dataBindSource;
                    }
                    else if (this.controlToPaginate is ListControl)
                    {
                        ((ListControl)this.controlToPaginate).Items.Clear();
                        ((ListControl)this.controlToPaginate).DataSource = this.dataBindSource;
                    }
                    else if (this.controlToPaginate is Repeater)
                    {
                        ((Repeater)this.controlToPaginate).DataSource = this.dataBindSource;
                    }
                    this.controlToPaginate.DataBind();
                }
            }
        }

        private void FetchAllData()
        {
            if (this.dataSource != null)
            {
                if (this.dataSource is IEnumerable)
                {
                    this.pds.DataSource = (IEnumerable)this.dataSource;
                }
                if (this.dataSource is IListSource)
                {
                    this.pds.DataSource = ((IListSource)this.dataSource).GetList();
                }
                if (this.PagerMode == PagerMode.logistic)
                {
                    this.pds.AllowPaging = true;
                    this.pds.PageSize = this.PageSize;
                    this.RecordCount = this.pds.DataSourceCount;
                    int num = PageSize == 0 ? 0 : ((this.RecordCount + this.PageSize) - 1) / this.PageSize;
                    this.PageCount = num;
                    if (this.pds.PageCount > num)
                    {
                        if (this.PageIndex > 0)
                        {
                            this.pds.CurrentPageIndex = this.PageIndex - 1;
                        }
                    }
                    else
                    {
                        this.pds.CurrentPageIndex = this.PageIndex;
                    }
                    this.dataBindSource = this.pds;
                }
                else
                {
                    this.dataBindSource = this.dataSource;
                }
            }
        }

        private void first_Click(object sender, EventArgs e)
        {
            this.GoToPage(0);
        }

        private static string GetResource(string key)
        {
            return VipSoft.WebControls.WebControls.ResourceManager.GetString(key);
        }

        private void GoToPage(int _pageIndex)
        {
            PageChangedEventArgs args2 = new PageChangedEventArgs();
            args2.OldPageIndex = this.PageIndex;
            args2.NewPageIndex = _pageIndex;
            PageChangedEventArgs e = args2;
            this.PageIndex = _pageIndex;
            this.OnPageIndexChanged(e);
            this.DataBind();
        }

        private void last_Click(object sender, EventArgs e)
        {
            this.GoToPage(this.PageCount - 1);
        }

        private void next_Click(object sender, EventArgs e)
        {
            if (this.PageIndex == (this.PageCount - 1))
            {
                this.GoToPage(this.PageIndex);
            }
            else
            {
                this.GoToPage(this.PageIndex + 1);
            }
        }

        protected virtual void OnPageIndexChanged(PageChangedEventArgs e)
        {
            if (this.PageIndexChanged != null)
            {
                this.PageIndexChanged(this, e);
            }
        }

        private void PageList_Click(object sender, EventArgs e)
        {
            DropDownList list = (DropDownList)sender;
            int num = Convert.ToInt32(list.SelectedItem.Value);
            this.GoToPage(num);
        }

        private void prev_Click(object sender, EventArgs e)
        {
            if (this.PageIndex == 0)
            {
                this.GoToPage(this.PageIndex);
            }
            else
            {
                this.GoToPage(this.PageIndex - 1);
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            if ((base.Site != null) && base.Site.DesignMode)
            {
                this.CreateChildControls();
            }
            base.Render(output);
        }

        [Description("CacheDuration")]
        public int CacheDuration
        {
            get
            {
                return Convert.ToInt32(this.ViewState["CacheDuration"]);
            }
            set
            {
                this.ViewState["CacheDuration"] = value;
            }
        }

        private string CacheKeyName
        {
            get
            {
                return (this.Page.Request.FilePath + "_" + this.UniqueID + "_Data");
            }
        }

        [Description("Cache mode ,Options are: Cached, NonCached")]
        public CacheMode CacheMode
        {
            get
            {
                return (CacheMode)ViewState["CacheMode"];
            }
            set
            {
                ViewState["CacheMode"] = value;
            }
        }
        [Description(" Contorl's ID which Pagination ")]
        public string ControlToPaginate
        {
            get
            {
                return Convert.ToString(this.ViewState["ControlToPaginate"]);
            }
            set
            {
                this.ViewState["ControlToPaginate"] = value;
            }
        }

        [DefaultValue((string)null), Bindable(true), Themeable(false)]
        public virtual object DataSource
        {
            get
            {
                return this.dataSource;
            }
            set
            {
                if ((value != null) && ((value is IListSource) || (value is IEnumerable)))
                {
                    this.dataSource = value;
                }
            }
        }

        [Browsable(false)]
        public int PageCount
        {
            get
            {
                return PageSize == 0 ? 0 : (((this.RecordCount + this.PageSize) - 1) / this.PageSize);
            }
            set
            {
                this.ViewState["PageCount"] = value;
            }
        }

        [Description("Number of current page")]
        public int PageIndex
        {
            get
            {
                return Convert.ToInt32(this.ViewState["PageIndex"]);
            }
            set
            {
                this.ViewState["PageIndex"] = value;
            }
        }

        [Description("Pager Mode, Options are: logistic,physical")]
        public PagerMode PagerMode
        {
            get
            {
                return (PagerMode)this.ViewState["PagerMode"];
            }
            set
            {
                this.ViewState["PagerMode"] = value;
            }
        }

        [Description("what record is display on each of page ")]
        public int PageSize
        {
            get
            {
                return Convert.ToInt32(this.ViewState["PageSize"]);
            }
            set
            {
                this.ViewState["PageSize"] = value;
            }
        }

        [Browsable(false)]
        public int RecordCount
        {
            get
            {
                return Convert.ToInt32(this.ViewState["RecordCount"]);
            }
            set
            {
                this.ViewState["RecordCount"] = value;
            }
        }

        [Description("Show First Button")]
        public bool ShowFirstButton
        {
            get
            {
                return Convert.ToBoolean(this.ViewState["ShowFirstButton"]);
            }
            set
            {
                this.ViewState["ShowFirstButton"] = value;
            }
        }

        [Description("Show a DropDownList for Jump Page")]
        public bool ShowJumpPage
        {
            get
            {
                return Convert.ToBoolean(this.ViewState["ShowJumpPage"]);
            }
            set
            {
                this.ViewState["ShowJumpPage"] = value;
            }
        }

        [Description("Show Last Button")]
        public bool ShowLastButton
        {
            get
            {
                return Convert.ToBoolean(this.ViewState["ShowStat"]);
            }
            set
            {
                this.ViewState["ShowStat"] = value;
            }
        }

        [Description("Show Next Button")]
        public bool ShowNextButton
        {
            get
            {
                return Convert.ToBoolean(this.ViewState["ShowNextButton"]);
            }
            set
            {
                this.ViewState["ShowNextButton"] = value;
            }
        }

        [Description("Show  Previous Button")]
        public bool ShowPreviousButton
        {
            get
            {
                return Convert.ToBoolean(this.ViewState["ShowPreviousButton"]);
            }
            set
            {
                this.ViewState["ShowPreviousButton"] = value;
            }
        }

        [Description("Show Stat information")]
        public bool ShowStat
        {
            get
            {
                return Convert.ToBoolean(this.ViewState["ShowStat"]);
            }
            set
            {
                this.ViewState["ShowStat"] = value;
            }
        }
    }
}

