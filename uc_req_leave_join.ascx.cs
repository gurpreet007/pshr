using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class leave_join_request : System.Web.UI.UserControl
{
    string dtformat = "dd-MON-yyyy hh:mi AM";
    private void show_posting_to_user()
    {
        string status = Session["Status"].ToString();
        string empid = Session["EmpId"].ToString();
        string sql = string.Empty;
        OraDBconnection oracn = new OraDBconnection();

        sql = "SELECT * FROM (SELECT cr.oonum || ' / ' || to_char(cr.oodate,'dd-Mon-yyyy') as \"Office Order\", " +
                "'ON LEAVE' \"Present Loc\", " +
                "pshr.get_desg(cr.desgcode)  || ' at ' || cadre.get_org_plants(cr.loccode) \"NEW Loc\"," +
                //"nvl2(cr.postjoin, " +
                //"pshr.get_desg(c2.desgcode)    || ' at '    || cadre.get_org_plants(c2.loccode), " +
                //"pshr.get_desg(cr.desgcode)    || ' at '    || cadre.get_org_plants(cr.loccode) " +
                //") \"NEW Loc\", " +
                "to_char(date_join_req,'" + dtformat + "') as \"Joining Request Date\", " +
                "pshr.get_fullname(REP_OFF_JOIN) || ' (' || REP_OFF_JOIN || ')' as \"Joining Officer\", " +
                "to_char(date_join_accept,'" + dtformat + "') as \"Joining Accept Date\" " +
                "FROM CADRE.chargereport cr LEFT OUTER JOIN cadre.cadr c2 ON c2.rowno = cr.postjoin WHERE " +
                "eventcode = 10 and";
        if (status == "None")
        {
            sql += "(status is null or status = 'JRS') and ";
            panRelReq.Visible = true;
        }
        else
        {
            panRelReq.Visible = false;
        }

        sql += " cr.empid = " + empid + " ORDER BY cr.oodate DESC) WHERE rownum = 1";
        System.Data.DataSet ds = new System.Data.DataSet();
        oracn.FillData(sql, ref ds);
        oracn.fillgrid(ref gvPosting, ref ds);
    }
    private void show_details(string empid)
    {
        string sql;

        System.Data.DataSet ds = new System.Data.DataSet();
        OraDBconnection orcn = new OraDBconnection();

        if (empid == "" || empid.Length != 6)
        {
            return;
        }

        sql = "select pshr.get_fullname(e.empid) as name, pshr.get_desg(e.cdesgcode) as desg, " +
            "cadre.get_org_plants(e.cloccode) as loc,ea.phonecell as cell, i.photo2 as photo from " +
            "empperso e left outer join empaddr ea on e.empid =ea.empid " +
            "left outer join img_pshr.img i on e.empid=i.empid " +
            "where recstatus=10 and e.empid=" + empid;
        orcn.FillData(sql, ref ds);
        if (ds.Tables[0].Rows.Count != 1)
        {
            return;
        }
        txtRREmpid.Text = empid;
        lblRRName.Text = ds.Tables[0].Rows[0]["name"].ToString();
        lblRRLoc.Text = ds.Tables[0].Rows[0]["loc"].ToString();
        lblRRDesg.Text = ds.Tables[0].Rows[0]["desg"].ToString();
        lblRRMob.Text = ds.Tables[0].Rows[0]["cell"].ToString();

        //load image
        if (!Convert.IsDBNull(ds.Tables[0].Rows[0]["photo"]))
        {
            byte[] barr = (byte[])ds.Tables[0].Rows[0]["photo"];
            string base64str = Convert.ToBase64String(barr);
            imgEmpPhoto.ImageUrl = string.Format("data:image/gif;base64,{0}", base64str);
        }

        ds.Clear();
        ds.Dispose();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        show_posting_to_user();
    }
    protected void txtRREmpid_TextChanged(object sender, EventArgs e)
    {
        string empid = Session["EmpId"].ToString();
        if (empid == txtRREmpid.Text.Trim())
        {
            lblMsg.Text = "You cannot enter your own EmpID";
            txtRREmpid.Text = string.Empty;
            return;
        }
        lblMsg.Text = string.Empty;
        show_details(txtRREmpid.Text);
    }
    protected void btnSubReq_Click(object sender, EventArgs e)
    {
        string sql;
        string empid = Session["EmpId"].ToString();
        string repofficer = txtRREmpid.Text;
        OraDBconnection oracn = new OraDBconnection();
        string status = Session["Status"].ToString();

        if (string.IsNullOrWhiteSpace(repofficer))
            return;

        if (empid == "" || empid.Length != 6) return;

        if (status == "None")
        {
            //check for status is null in case of LJON (10)
            //update if -  
            //1) status is null -- first time request submission
            //2) status = JRS -- resubmission of request (in case of submission to wrong officer etc.)
            sql = "update cadre.chargereport set " +
            " rep_off_join = " + repofficer + ", " +
            " status = 'JRS', " +
            " date_join_req = sysdate" +
            " where (status = 'JRS' or status is null) and empid = " + empid;
            oracn.ExecQry(sql);
            show_posting_to_user();
            lblMsg.Text = "Joining Request Submitted Successfully";
        }
    }
    protected void bLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("login.aspx");
    }
}