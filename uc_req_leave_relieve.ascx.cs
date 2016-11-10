using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class leave_request : System.Web.UI.UserControl
{
    string dtformat = "dd-MON-yyyy hh:mi AM";
    string dtformat_notime = "dd-MON-yyyy";
    string evntDate = string.Empty;
    private void show_posting_to_user()
    {
        string status = Session["Status"].ToString();
        string empid = Session["EmpId"].ToString();
        string sql = string.Empty;
        OraDBconnection oracn = new OraDBconnection();
        //string strLeaveEvents = "1,2,3,4,5,6,7,8,9,62,63,86,98";

        sql = "SELECT * FROM (SELECT cr.oonum || ' / ' || to_char(cr.oodate,'dd-Mon-yyyy') as \"Office Order\", " +
                "pshr.get_desg(cr.olddesgcode) || ' at ' || cadre.get_org_plants(cr.oldloccode) as \"Present Loc\", " +
                "'ON LEAVE' \"New Loc\", " +
                "to_char(eventdate,'" + dtformat_notime + "') as \"Leave Date\", " +
                "to_char(date_rel_req,'" + dtformat + "') as \"Relieving Request Date\", " +
                "pshr.get_fullname(REP_OFF_REL) || ' (' || REP_OFF_REL || ')' as \"Relieving Officer\", " +
                "to_char(date_rel_accept,'" + dtformat + "') as \"Relieving Accept Date\", " +
                "rel_off_comment " +
                "FROM CADRE.chargereport cr LEFT OUTER JOIN pshr.empperso e1 ON e1.empid = cr.empid " +
                "WHERE cr.eventcode in (1,2,3,4,5,6,7,8,9,62,63,86,98) and cr.empid = " + empid + " and ";
        if (status == "None")
        {
            //in the start when request is yet to be submitted
            //hide comments panel and show request panel
            panRelReq0.Visible = false;
            panRelReq.Visible = true;
            sql += "(cr.status IS NULL or status = 'RRS') ";
        }
        else if (status == "RRS")
        {
            //if request submitted then
            //show comments panel and hide request panel
            panRelReq0.Visible = true;
            panRelReq.Visible = false;
            sql += "cr.status = 'RRS' ";
        }
        else if (status == "RRA")
        {
            //if leave request is accepted then
            //hide both comments panel and request panel
            panRelReq.Visible = false;
            panRelReq0.Visible = false;
            sql += "cr.status = 'RRA' ";
        }
        sql += "ORDER BY cr.oodate DESC) WHERE rownum = 1";
        System.Data.DataSet ds = new System.Data.DataSet();
        oracn.FillData(sql, ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtROComment.Text = ds.Tables[0].Rows[0]["rel_off_comment"].ToString();
            oracn.fillgrid(ref gvPosting, ref ds);
            evntDate = ds.Tables[0].Rows[0]["Leave Date"].ToString();
        }
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

        //request date should be at most one day earlier than leave date
        if (evntDate != string.Empty)
        {
            DateTime dtEvntDate = DateTime.ParseExact(evntDate, "dd-MMM-yyyy", null);
            int nDays = (dtEvntDate - new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)).Days;
            if (nDays != 0 && nDays != 1)
            {
                lblMsg.Text = "You can only apply for your chargereport on or one day before sanctioned leave date";
                return;
            }
        }
        else
        {
            lblMsg.Text = "Invalid leave date";
            return;
        }

        //submit request if status = None or status = 'RRS' (required if wrong submission in first case)
        if (status == "None" || status == "RRS")
        {
            sql = "update cadre.chargereport set " +
            " rep_off_rel = " + repofficer + ", " +
            " status = 'RRS', " +
            " date_rel_req = sysdate" +
            " where (status is null or status = 'RRS') and " +
            " eventcode in (1,2,3,4,5,6,7,8,9,62,63,86,98) and " +
            " empid = " + empid;
            oracn.ExecQry(sql);
            lblMsg.Text = "Relieve Request Submitted Successfully";
            show_posting_to_user();
        }
    }
    protected void bLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("login.aspx");
    }
}