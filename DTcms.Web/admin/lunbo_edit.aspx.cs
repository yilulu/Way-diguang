using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin
{
    public partial class lunbo_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作類型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改類型
                this.id = DTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("傳輸參數不正確！", "back", "Error");
                    return;
                }

            }
            if (!Page.IsPostBack)
            {
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
                else
                {

                }
            }
        }



        #region 賦值操作=================================
        private void ShowInfo(int _id)
        {
            DAL.imagedal bll = new DAL.imagedal();
            Model.image model = bll.GetModel(_id);
            txtTitle.Text = model.title;
            txtSortId.Text = model.sort.ToString();
            txtlink_url.Text = model.link_url;
            ViewState["file"] = model.img_url;
            ddlType.SelectedValue = model.Typeid.ToString();
            // LitAlbumList.Text = GetAlbumHtml(model.albums, model.img_url);


        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = true;
            Model.image model = new Model.image();
            DAL.imagedal bll = new DAL.imagedal();


            model.title = txtTitle.Text.Trim();
            model.sort = Convert.ToInt32(txtSortId.Text);
            model.link_url = txtlink_url.Text;
            model.Typeid =Convert.ToInt32(ddlType.SelectedValue);
            //儲存相冊
            string[] albumArr = Request.Form.GetValues("hide_photo_name");
            string[] remarkArr = Request.Form.GetValues("hide_photo_remark");

            if (albumArr != null && albumArr.Length >= 0)
            {
                string[] imgArr = albumArr[0].Split('|');
                if (imgArr != null && imgArr.Length >= 1)
                {
                    model.img_url = imgArr[1]; //focus_photo.Value;
                }
            }
            if (fileUpImage.HasFile)
            {
                string extendName = fileUpImage.FileName.Substring(fileUpImage.FileName.LastIndexOf('.'));
                string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + extendName;
                if (!System.IO.Directory.Exists(Server.MapPath("../upload/logo/")))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("../upload/logo/"));
                }

                fileUpImage.SaveAs(Server.MapPath("../upload/logo/" + filename));
                string url = HttpContext.Current.Request.Url.Host;
                model.img_url = "http://" + url + "/upload/logo/" + filename;
            }
            if (bll.Add(model) < 1)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = true;
            DAL.imagedal bll = new DAL.imagedal();
            Model.image model = bll.GetModel(_id);

            model.title = txtTitle.Text.Trim();
            model.sort = Convert.ToInt32(txtSortId.Text);
            model.link_url = txtlink_url.Text;
            model.Typeid = Convert.ToInt32(ddlType.SelectedValue);

            if (fileUpImage.HasFile)
            {
                string extendName = fileUpImage.FileName.Substring(fileUpImage.FileName.LastIndexOf('.'));
                string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + extendName;
                if (!System.IO.Directory.Exists(Server.MapPath("../upload/logo/")))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("../upload/logo/"));
                }

                fileUpImage.SaveAs(Server.MapPath("../upload/logo/" + filename));
                string url = HttpContext.Current.Request.Url.Host;
                model.img_url = "http://" + url + "/upload/logo/" + filename;
            }
            else
            {
                model.img_url = ViewState["file"] == null ? "" : ViewState["file"].ToString();
            }
            if (!bll.Update(model))
            {
                result = false;
            }

            return result;
        }
        #endregion

        //儲存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                //ChkAdminLevel("sys_model", DTEnums.ActionEnum.Edit.ToString()); //檢查許可權
                if (!DoEdit(this.id))
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("修改成功！", "lunbo_list.aspx", "Success", "parent.loadChannelTree");
            }
            else //添加
            {
                //ChkAdminLevel("sys_model", DTEnums.ActionEnum.Add.ToString()); //檢查許可權
                if (!DoAdd())
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("添加成功！", "lunbo_list.aspx", "Success", "parent.loadChannelTree");
            }
        }
    }
}

