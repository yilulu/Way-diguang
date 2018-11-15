using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

namespace Common
{
    /// <summary>
    /// 綁定主文件數據
    /// </summary>
    public class MyDropDownList : DropDownList
    {
        SQLHelper SQlHelper = new SQLHelper();


        private string where;

        public string Where
        {
            get { return where; }
            set { where = value; }
        }

        private string _Table;
        /// <summary>
        /// 表名-Value-顯示欄位
        /// </summary>
        public string Table_ID_Name
        {
            get
            {
                return _Table;
            }
            set
            {
                _Table = value;
                Bind();
            }
        }

        private string _SelectedVlueID;
        public string SelectedVlueID
        {
            get
            {
                return _SelectedVlueID;
            }
            set
            {
                _SelectedVlueID = value;
            }
        }
        public MyDropDownList()
        {

        }

        private void Bind()
        {
            try
            {
                string sqlWhere = "";
                string[] arry = Table_ID_Name.Split('*'); //Table_ID_Name
                if (!string.IsNullOrEmpty(Where))
                {
                    sqlWhere = " where " + Where;
                }
                string sql = string.Format("SELECT * FROM {0} {1}", arry[0], sqlWhere);
                var table = SQlHelper.GetDataTable(sql, System.Data.CommandType.Text);
                this.DataSource = table;
                this.DataValueField = arry[1];
                this.DataTextField = arry[2];
                this.DataBind();
                if (SelectedVlueID != "" && SelectedVlueID != null)
                {
                    this.SelectedValue = SelectedVlueID;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
