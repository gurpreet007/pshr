using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChargeReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["ucontrol"] = "uc_acc_tnp.ascx";
        String str_uc = Session["ucontrol"].ToString();
        UserControl uc = (UserControl)Page.LoadControl(str_uc);
        PlaceHolder1.Controls.Add(uc);
    }
}