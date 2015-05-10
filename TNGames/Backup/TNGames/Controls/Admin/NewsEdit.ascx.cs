using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGames.Core;
using TNGames.Core.Domain;
using TNGames.Core.Helper;
using System.IO;


namespace TNGames.Controls.Admin
{
    public partial class NewsEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadDefaultData();
                LoadEditData();
            }
        }

        private void LoadDefaultData()
        {
            IList<Category> lst = DomainManager.GetAll<Category>();
            if (lst != null)
            {
                ddlCategories.DataSource = lst;
                ddlCategories.DataTextField = "CategoryName";
                ddlCategories.DataValueField = "Id";
                ddlCategories.DataBind();
            }

            txtTitle.Attributes.Add("onblur", string.Format("AutoGenerateAlias(this.value, '{0}', false);", txtAlias.ClientID));
        }

        private void LoadEditData()
        {
            string strId = Page.RouteData.Values["id"] as string;
            int id = 0;
            int.TryParse(strId, out id);

            New obj = DomainManager.GetObject<New>(id);
            if (obj != null)
            {
                txtTitle.Text = obj.NewsTitle;
                txtAlias.Text = obj.NewsAlias;
                txtSummary.Text = obj.Summary;
                txtContent.Text = obj.NewsContent;

                if (obj.Category != null)
                {
                    ListItem item = ddlCategories.Items.FindByValue(obj.Category.Id.ToString());
                    if (item != null)
                        item.Selected = true;
                }

                if (!string.IsNullOrEmpty(obj.Photo))
                {
                    imgPhoto.ImageUrl = TNHelper.GetNewsPhoto(obj.Photo);
                    imgPhoto.Visible = true;
                }
                btnDelete.Visible = true;
            }
            else
            {
                imgPhoto.Visible = false;
                btnDelete.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string strId = Page.RouteData.Values["id"] as string;
                int id = 0;
                int.TryParse(strId, out id);

                New obj = DomainManager.GetObject<New>(id);
                if (obj == null)
                    obj = new New();

                obj.NewsTitle = TextInputUtil.GetSafeInput(txtTitle.Text.Trim());
                obj.NewsAlias = TextInputUtil.GetSafeInput(txtAlias.Text.Trim());
                obj.Summary = TextInputUtil.GetSafeInput(txtSummary.Text.Trim());
                obj.NewsContent = TextInputUtil.GetSafeInput(txtContent.Text);
                obj.Active = true;

                int catId = 0;
                int.TryParse(ddlCategories.SelectedValue, out catId);
                obj.Category = DomainManager.GetObject<Category>(catId);

                if (obj.Id > 0)
                {
                    obj.ModifedDate = DateTime.Now;
                    DomainManager.Update(obj);
                }
                else
                {
                    obj.NewsAlias = Utils.GenerateAlias(obj.NewsTitle, true);
                    obj.CreatedDate = DateTime.Now;
                    obj.ModifedDate = DateTime.Now;
                    DomainManager.Insert(obj);
                }

                if (fuPhoto.HasFile)
                {
                    string path = string.Empty;
                    // delete exist photo
                    if (!string.IsNullOrEmpty(obj.Photo))
                    {
                        path = Page.Server.MapPath(string.Format("~/Userfiles/News/{0}", obj.Photo));
                        if (File.Exists(path))
                        {
                            try { File.Delete(path); }
                            catch { }
                        }
                    }


                    string photoName = string.Format("{0}{1}", obj.Id, Path.GetExtension(fuPhoto.FileName));
                    path = Page.Server.MapPath(string.Format("~/Userfiles/News/{0}", photoName));
                    fuPhoto.SaveAs(path);

                    obj.Photo = photoName;
                    DomainManager.Update(obj);
                }

                string msg = Page.Server.UrlEncode("Lưu dữ liệu thành công");
                Page.Response.Redirect(string.Format("/admincp/news-list?msg={0}", msg), true);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string strId = Page.RouteData.Values["id"] as string;
            int id = 0;
            int.TryParse(strId, out id);

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
                Page.Response.Redirect("/admincp/news-list", true);
            }
        }
    }
}