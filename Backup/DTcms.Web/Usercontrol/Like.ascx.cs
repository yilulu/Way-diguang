using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.DAL;

namespace DTcms.Web
{
    public partial class Like : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int AreaID = 0;
            string proID = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(proID))
            {
                BLL.article bll = new BLL.article();
                Model.article_goods model = bll.GetGoodsModel(int.Parse(proID));
                if (model != null)
                {
                    AreaID = model.Areaid;
                }

            }
            DAL.article dalArticle = new article();
            repdate1.DataSource = dalArticle.GetPageindexList("", 4, " and channel_id =" + Request.QueryString["mid"] + " and id !=" + int.Parse(proID) + " and Areaid=" + AreaID);
            repdate1.DataBind();
        }
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public string ToSubstring(string obj, int Length)
        {
            try
            {
                if (obj.Length > Length)
                {
                    return obj.Substring(0, Length) + "...";
                }
                else
                {
                    return obj;
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string getSellOrHire(string price)
        {
            string ReturnHtml = string.Empty;
            if (this.Request.QueryString["mid"] == "2")
            {
                ReturnHtml = "總價:" + price + "萬";
            }
            if (Request.QueryString["mid"] == "3")
            {
                ReturnHtml = "租金:" + price + "元/月";
            }
            return ReturnHtml;
        }
    }
}