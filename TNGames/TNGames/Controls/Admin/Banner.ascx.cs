using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DM = TNGames.Core.Domain;
using TNGames.Core;
using TNGames.Core.Helper;

namespace TNGames.Controls.Admin
{
    public partial class Banner : System.Web.UI.UserControl
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
                    pagerList.CurrentIndex = index;
                }

                return pagerList.CurrentIndex;
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
                    pagerList.PageSize = size;
                }

                return pagerList.PageSize;
            }
        }

        DM.ContentType contentType;
        public DM.ContentType ContentType
        {
            get
            {
                if (contentType == null)
                {
                    string typeId = Page.RouteData.Values["id"] as string;
                    int id = 0;
                    int.TryParse(typeId, out id);

                    contentType = DomainManager.GetObject<DM.ContentType>(id);
                }

                return contentType;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }

        protected void LoadData()
        {
            if (ContentType == null)
            {
                Utils.ShowMessage(lblMsg, "Dữ liệu cung cấp chưa đúng. Bạn kiểm tra lại");
                return;
            }

            litType.Text = ContentType.ContentTypeName;

            List<DM.Content> lst = TNHelper.GetContentsByType(ContentType.Id);
            int totalRow = 0;
            if (lst != null)
            {
                totalRow = lst.Count;
                lst = lst.OrderByDescending(p => p.Id)
                         .Skip((PageIndex - 1) * PageSize)
                         .Take(PageSize).ToList();
            }

            if (lst != null)
            {
                rptList.DataSource = lst;
                rptList.DataBind();
            }

            pagerList.ItemCount = totalRow;
            pagerList.Visible = (pagerList.PageCount > 1);

            if (totalRow == 0)
                Utils.ShowMessage(lblMsg, "Không tìm thấy dữ liệu banner.");
        }

        public void pager_Command(object sender, CommandEventArgs e)
        {
            int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
            pagerList.CurrentIndex = currnetPageIndx;
            LoadData();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                DM.Content content = new DM.Content();
                content.ContentText = TextInputUtil.GetSafeInput(txtContent.Text);
                content.Active = radYes.Checked;
                content.ContentType = ContentType;
                DomainManager.Insert(content);

                txtContent.Text = string.Empty;
                radYes.Checked = true;
                radNo.Checked = false;

                LoadData();
                Utils.ShowMessage(lblMsg, "Lưu banner thành công");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rptList.Items)
            {
                HiddenField hfContentID = item.FindControl("hfContentID") as HiddenField;
                TextBox txtContent = item.FindControl("txtContent") as TextBox;
                CheckBox chkActive = item.FindControl("chkActive") as CheckBox;

                int id = 0;
                if (hfContentID != null)
                    int.TryParse(hfContentID.Value, out id);

                DM.Content content = DomainManager.GetObject<DM.Content>(id);
                if (content != null)
                {
                    if (txtContent != null)
                        content.ContentText = TextInputUtil.GetSafeInput(txtContent.Text);

                    if (chkActive != null)
                        content.Active = chkActive.Checked;

                    DomainManager.Update(content);
                }
            }
            Utils.ShowMessage(lblMsg, "Cập nhật dữ liệu thành công");

        }

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (string.Compare(e.CommandName, "delete", true) == 0)
            {
                int id = 0;
                int.TryParse(e.CommandArgument.ToString(), out id);

                DM.Content content = DomainManager.GetObject<DM.Content>(id);
                if (content != null)
                {
                    DomainManager.Delete(content);
                    LoadData();
                    Utils.ShowMessage(lblMsg, "Xóa banner thành công");
                }
            }
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
           
        }
    }
}