using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class retire_request : System.Web.UI.UserControl
{
    string dtformat = "dd-MON-yyyy hh:mi AM";
    string oracle_dtformat_notime = "dd-MON-yyyy";
    private void show_posting_to_user()
    {
        string status = Session["Status"].ToString();
        string empid = Session["EmpId"].ToString();
        string sql = string.Empty;
        OraDBconnection oracn = new OraDBconnection();

        sql = "SELECT * FROM (SELECT cr.oonum || ' / ' || to_char(cr.oodate,'dd-Mon-yyyy') as \"Office Order\", " +
                "pshr.get_desg(ecr.olddesgcode) || ' at ' || cadre.get_org_plants(cr.oldloccode) \"Present Loc\", " +
                "(select eref from pshr.mast_event where eventcode=cr.eventcode) as \"Retirement Type\", " +
                "to_char(eventdate,'" + oracle_dtformat_notime + "') as \"Retirement Date\", " +
                "to_char(date_rel_req,'" + dtformat + "') as \"Relieve Request Date\", " +
                "pshr.get_fullname(REP_OFF_REL) || ' (' || REP_OFF_REL || ')' as \"Relieving Officer\", " +
                "to_char(date_rel_accept,'" + dtformat + "') as \"Relieve Accept Date\" " +
                "FROM CADRE.chargereport cr LEFT OUTER JOIN pshr.empperso e1 ON e1.empid = cr.empid WHERE " +
                "eventcode in (11, 12, 13, 14, 15, 16, 89) and ";
        if (status == "None")
        {
            sql += "(status is null or status = 'RRS') and ";
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
            "pshr.empperso e left outer join pshr.empaddr ea on e.empid =ea.empid " +
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
            //check for status is null in case of Retirement Events
            //update if -  
            //1) status is null -- first time request submission
            //2) status = RRS -- resubmission of request (in case of submission to wrong officer etc.)
            sql = "update cadre.chargereport set " +
            " rep_off_rel = " + repofficer + ", " +
            " status = 'RRS', " +
            " date_rel_req = sysdate" +
            " where (status = 'RRS' or status is null) and empid = " + empid;
            oracn.ExecQry(sql);
            show_posting_to_user();
            lblMsg.Text = "Retirement Relieve Request Submitted Successfully";
        }
    }
    protected void bLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("login.aspx");
    }
}