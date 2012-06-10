using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core.Domain;
using TNGames.Core;
using System.IO;

namespace TNGames.Controls.Admin
{
    public partial class NewsList : System.Web.UI.UserControl
    {
        public int PageIndex
        {
            get
            {
                int index = 1;
                if (Page.Request.QueryString["PageIndex"] != null)
                {
                    int.TryParse(Page.Request.QueryString["PageIndex"], out index);
                    index = index > 0 ? index : 1;
                    pager.CurrentIndex = index;
                }

                return pager.CurrentIndex;
            }
        }

        public int PageSize
        {
            get
            {
                int size = 10;
                if (Page.Request.QueryString["PageSize"] != null)
                {
                    int.TryParse(Page.Request.QueryString["PageSize"], out size);
                    size = size > 0 ? size : 10;
                    pager.PageSize = size;
                }

                return pager.PageSize;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            IList<New> lst = DomainManager.GetAll<New>();

            int totalRow = 0;
            if (lst != null)
            {
                totalRow = lst.Count;
                lst = lst.OrderByDescending(p=>p.CreatedDate)
                         .OrderBy(p=> p.NewsTitle)
                         .Skip((PageIndex - 1) * PageSize)
                         .Take(PageSize).ToList();
            }

            if (lst != null)
            {
                rptNews.DataSource = lst;
                rptNews.DataBind();
            }

            pager.ItemCount = totalRow;
            pager.Visible = (pager.PageCount > 1);
        }

        protected void rptNews_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (string.Compare(e.CommandName, "delete", true) == 0)
            {
                int id = int.Parse(e.CommandArgument.ToString());
                New obj = DomainManager.GetObject<New>(id);

                if (obj != null)
                {
                    // delete photo
                    if (!string.IsNullOrEmpty(obj.Photo))
                    {
                        string path = Page.Server.MapPath(string.Format("~/Userfiles/News/{0}", obj.Photo));
                        if (File.Exists(path))
                        {
                            try { File.Delete(path); }
                            catch { }
                        }
                    }
                    DomainManager.Delete(obj);
                    lblMsg.Text = "Xóa thành công.";
                    LoadData();
                }
            }
        }

        protected void pager_Command(object sender, CommandEventArgs e)
        {
            int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
            pager.CurrentIndex = currnetPageIndx;
            LoadData();
        }
    }
}