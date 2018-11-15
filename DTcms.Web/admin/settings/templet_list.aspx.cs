using System;
using System.IO;
using System.Data;
using System.Xml;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
{
    public partial class templet_list : DTcms.Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_templet", DTEnums.ActionEnum.View.ToString()); //檢查許可權
                RptBind();
            }
        }

        #region 數據綁定===============================================
        private void RptBind()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("skinname", Type.GetType("System.String"));
            dt.Columns.Add("name", Type.GetType("System.String"));
            dt.Columns.Add("img", Type.GetType("System.String"));
            dt.Columns.Add("author", Type.GetType("System.String"));
            dt.Columns.Add("createdate", Type.GetType("System.String"));
            dt.Columns.Add("version", Type.GetType("System.String"));
            dt.Columns.Add("fordntver", Type.GetType("System.String"));

            DirectoryInfo dirInfo = new DirectoryInfo(Utils.GetMapPath("../../templates/"));
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                DataRow dr = dt.NewRow();
                Model.template model = GetInfo(dir.FullName);
                if (model != null)
                {
                    dr["skinname"] = dir.Name;  //資料夾名稱
                    dr["name"] = model.name;    // 範本名稱
                    dr["img"] = "../../templates/" + dir.Name + "/about.png";   // 範本圖片
                    dr["author"] = model.author;    //作者
                    dr["createdate"] = model.createdate;    //創建日期
                    dr["version"] = model.version;  //範本版本
                    dr["fordntver"] = model.fordntver;  //適用的版本
                    dt.Rows.Add(dr);
                }
            }
            this.rptList.DataSource = dt;
            this.rptList.DataBind();
        }
        #endregion

        #region 讀取範本配置資訊========================================
        /// <summary>
        /// 從範本說明文件中獲得範本說明資訊
        /// </summary>
        /// <param name="xmlPath">範本路徑(不包含檔案名)</param>
        /// <returns>範本說明資訊</returns>
        private Model.template GetInfo(string xmlPath)
        {
            Model.template model = new Model.template();
            ///存放關於資訊的檔 about.xml是否存在,不存在返回空串
            if (!File.Exists(xmlPath + @"\about.xml"))
            {
                return null;
            }
            try
            {
                XmlNodeList xnList = XmlHelper.ReadNodes(xmlPath + @"\about.xml", "about");
                foreach (XmlNode n in xnList)
                {
                    if (n.NodeType != XmlNodeType.Comment && n.Name.ToLower() == "template")
                    {
                        model.name = n.Attributes["name"] != null ? n.Attributes["name"].Value.ToString() : "";
                        model.author = n.Attributes["author"] != null ? n.Attributes["author"].Value.ToString() : "";
                        model.createdate = n.Attributes["createdate"] != null ? n.Attributes["createdate"].Value.ToString() : "";
                        model.version = n.Attributes["version"] != null ? n.Attributes["version"].Value.ToString() : "";
                        model.fordntver = n.Attributes["fordntver"] != null ? n.Attributes["fordntver"].Value.ToString() : "";
                    }
                }
            }
            catch
            {
                return null;
            }
            return model;
        }
        #endregion

        #region 全部生成範本============================================
        /// <summary>
        /// 生成全部範本
        /// </summary>
        private void MarkTemplates(string skinName)
        {
            //取得ASP目錄下的所有檔
            DirectoryInfo dirFile = new DirectoryInfo(Utils.GetMapPath(siteConfig.webpath + "aspx/"));
            //獲得URL映射列表
            BLL.url_rewrite bll = new BLL.url_rewrite();
            List<Model.url_rewrite> ls = bll.GetList("");
            //刪除不屬於URL映射表裡的檔，防止冗餘
            foreach (FileInfo file in dirFile.GetFiles())
            {
                //檢查檔
                Model.url_rewrite model2 = ls.Find(p => p.page.ToLower() == file.Name.ToLower());
                if (model2 == null)
                {
                    file.Delete();
                }
            }
            //遍歷網站目錄的templates資料夾下的範本檔
            DirectoryInfo dirInfo = new DirectoryInfo(Utils.GetMapPath(siteConfig.webpath + @"templates/" + skinName));
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                if (!file.Name.StartsWith("_") && file.Name.EndsWith(".html"))
                {
                    //查找相對應的繼承類名
                    foreach (Model.url_rewrite model in ls)
                    {
                        if (file.Name.ToLower()==model.templet && !string.IsNullOrEmpty(model.inherit))
                        {
                            //檢查頻道ID
                            int channelId = Utils.StrToInt(model.channel, 0);
                            //生成範本檔
                            PageTemplate.GetTemplate(siteConfig.webpath, "templates", skinName, model.templet, model.page, model.inherit, channelId, 1);
                        }
                    }
                }
            }
        }
        #endregion

        //啟用範本
        protected void lbtnStart_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_templet", DTEnums.ActionEnum.Add.ToString()); //檢查許可權
            BLL.siteconfig bll = new BLL.siteconfig();
            Model.siteconfig model = siteConfig;
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                string skinName = ((HiddenField)rptList.Items[i].FindControl("hideSkinName")).Value;
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    //判是否當前範本
                    if (skinName.ToLower() == siteConfig.templateskin)
                    {
                        JscriptMsg("該範本已是當前範本！", "back", "Warning");
                        return;
                    }
                    model.templateskin = skinName.ToLower();
                    //修改設定檔
                    bll.saveConifg(model, Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
                    //重新生成範本
                    MarkTemplates(skinName);
                    JscriptMsg("範本啟用並全部生成成功！", "templet_list.aspx", "Success");
                    return;
                }
            }
        }

        //生成範本
        protected void lbtnRemark_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_templet", DTEnums.ActionEnum.Edit.ToString()); //檢查許可權
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                string skinName = ((HiddenField)rptList.Items[i].FindControl("hideSkinName")).Value;
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    //判是否當前範本
                    if (skinName.ToLower() != siteConfig.templateskin)
                    {
                        JscriptMsg("該範本不是當前範本，生成失敗！", "back", "Error");
                        return;
                    }
                    //重新生成範本
                    MarkTemplates(skinName);
                    JscriptMsg("範本已全部重新生成！", "templet_list.aspx", "Success");
                    return;
                }
            }
        }

        //管理範本
        protected void lbtnManage_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                string skinName = ((HiddenField)rptList.Items[i].FindControl("hideSkinName")).Value;
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    Response.Redirect("templet_file_list.aspx?skin=" + Utils.UrlEncode(skinName));
                }
            }
        }
    }
}
