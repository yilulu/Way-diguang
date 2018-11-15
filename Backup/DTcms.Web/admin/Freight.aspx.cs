using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using System.Data;

namespace DTcms.Web.admin
{
    public partial class Freight : Web.UI.ManagePage
    {
        Ltf.Model.Freight mod;
        Ltf.BLL.Freight bllf = new Ltf.BLL.Freight();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
          
        }
        #region 保存
        void LoadData()
        {
            mod = bllf.GetModel(1);
            if (mod.typID == 1)
            {
                RadOne.Checked = true;
            }
            if (mod.typID == 2)
            {
                RadTwo.Checked = true;
                txtTotal.Text = mod.TotalPrice.ToString();
                txtLow.Text = mod.spec.ToString();
                txtFee.Text = mod.Fee.ToString();
            }
            if (mod.typID == 3)
            {
                Rad3.Checked = true;
                txtMasterFee.Text = mod.Fee.ToString();
            }
        }
        #endregion

        #region 保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            mod = new Ltf.Model.Freight();
            if (RadOne.Checked == true)
            {
                mod.typID = int.Parse(RadOne.Text);
                mod.TotalPrice = decimal.Parse("0");
                mod.spec = "0";
                mod.Fee = decimal.Parse("0");
            }
            if (RadTwo.Checked == true)
            {
                mod.typID = int.Parse(RadTwo.Text);
                mod.TotalPrice = decimal.Parse(txtTotal.Text.Trim());
                mod.spec = txtLow.Text.Trim();
                mod.Fee = decimal.Parse(txtFee.Text.Trim());
            }
            if (Rad3.Checked == true)
            {
                mod.typID = int.Parse(Rad3.Text);
                mod.Fee = decimal.Parse(txtMasterFee.Text.Trim());
                mod.TotalPrice = decimal.Parse("0");
                mod.spec = "0";
            }
            mod.ID = 1;
            if (bllf.Update(mod))
            {
                JscriptMsg("修改成功！", "Freight.aspx", "Success");
            }
        }

        #endregion
    }
}