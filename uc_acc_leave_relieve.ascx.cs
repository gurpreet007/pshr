using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_acc_leave_relieve : System.Web.UI.UserControl
{
    string oracle_dtformat = "dd-MON-yyyy hh:mi:ss AM";
    string dnet_dtformat = "dd-MMM-yyyy hh:mm:ss tt";
    string oracle_dtformat_notime = "dd-MON-yyyy";
    private void show_posting_to_officer()
    {
        string status = Session["Status"].ToString();
        string sql = string.Empty;

        string empid = Session["EmpId"].ToString();
        OraDBconnection oracn = new OraDBconnection();

        sql = "SELECT cr.empid as Empid, pshr.get_fullname(cr.empid) as Name, " +
                "cr.oonum || ' / ' || to_char(cr.oodate,'dd-Mon-yyyy') as \"Office Order\", " +
                "pshr.get_desg(cr..olddesgcode) || ' at ' || cadre.get_org_plants(cr.oldloccode) \"Present Loc\"," +
                "'ON LEAVE' \"New Loc\"," +
                "(SELECT event FROM pshr.mast_event WHERE eventcode = cr.eventcode) AS \"Leave Type\", " +
                "to_char(eventdate,'" + oracle_dtformat_notime + "') as \"Leave Date\", " +
                "pshr.get_fullname(REP_OFF_REL) || ' (' || REP_OFF_REL || ')' as \"Relieving Officer\", " +
                "to_char(date_rel_req,'" + oracle_dtformat + "') as \"Relieving Request\", " +
                "to_char(date_rel_accept,'" + oracle_dtformat + "') as \"Relieving Accept\" " +
                "FROM CADRE.chargereport cr LEFT OUTER JOIN pshr.empperso e1 ON e1.empid = cr.empid " +
                "WHERE eventcode in (1,2,3,4,5,6,7,8,9,62,63,86,98) ";

        if (status == "RRS")
        {
            //if only relieve request is submitted yet and not approved
            //then show comments row
            lblROComment.Visible = true;
            txtROComment.Visible = true;
            sql += string.Format("AND status = 'RRS' AND rep_off_rel={0} ", empid);
        }

        sql += "ORDER BY cr.oodate DESC";

        System.Data.DataSet ds = new System.Data.DataSet();
        oracn.FillData(sql, ref ds);
        oracn.fillgrid(ref gvRequests, ref ds);
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
            "cadre.get_org_plants(e.cloccode) as loc,ea.phonecell as cell, i.photo2 as photo, " +
            "c.rel_off_comment as relcomm " +
            "from pshr.empperso e left outer join pshr.empaddr ea on e.empid =ea.empid " +
            "left outer join img_pshr.img i on e.empid=i.empid " +
            "LEFT OUTER JOIN CADRE.chargereport c ON e.empid = c.empid " +
            "where recstatus=10 and e.empid=" + empid + " order by c.oodate desc";
        orcn.FillData(sql, ref ds);
        if (ds.Tables[0].Rows.Count < 1)
        {
            return;
        }
        lblEmpID.Text = empid;
        lblRRName.Text = ds.Tables[0].Rows[0]["name"].ToString();
        lblRRLoc.Text = ds.Tables[0].Rows[0]["loc"].ToString();
        lblRRDesg.Text = ds.Tables[0].Rows[0]["desg"].ToString();
        lblRRMob.Text = ds.Tables[0].Rows[0]["cell"].ToString();
        txtROComment.Text = ds.Tables[0].Rows[0]["relcomm"].ToString();
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
        lblROComment.Visible = false;
        txtROComment.Visible = false;

        show_posting_to_officer();
    }
    private void ChangeTables(string empid)
    {
        string sql;
        string oonum, postrel, eventcode, loccode, desgcode;
        string offempid = Session["Empid"].ToString();

        DateTime odate;

        OraDBconnection orcn = new OraDBconnection();
        System.Data.DataSet ds = new System.Data.DataSet();
        System.Data.DataRow drow;

        //get values
        sql = "select * from cadre.chargereport where " +
            " status = 'RRS' and " +
            " eventcode in (1,2,3,4,5,6,7,8,9,62,63,86,98) and " +
            " rep_off_rel = " + offempid + " and " +
            " empid = " + empid +
            " order by oodate desc";
        orcn.FillData(sql, ref ds);

        if (ds.Tables[0].Rows.Count != 1)
        {
            lMsg0.Text = "No Pending Row";
            return;
        }
        drow = ds.Tables[0].Rows[0];

        oonum = drow["oonum"].ToString();
        //check and get o/o date
        if (!Convert.IsDBNull(drow["oodate"].ToString()))
        {
            odate = (DateTime)drow["oodate"];
        }
        else
        {
            lblMsg.Text = "Invalid O/o Date";
            return;
        }

        //fromdate will be sysdate

        postrel = drow["postrel"].ToString();
        eventcode = drow["eventcode"].ToString();
        loccode = "999999999";  //loccode = "ON LEAVE"
        desgcode = "8888";  //desgcode = (null)

        ds.Clear();

        //delete entry from cadrmap
        sql = "delete from cadre.cadrmap where empid = " + empid + " or rowno = '" + postrel + "'";
        orcn.ExecQry(sql);

        //update empperso
        sql = string.Format("update pshr.empperso set cloccode = {0}, cdesgcode = {1} where empid = {2}",
            loccode, desgcode, empid);
        orcn.ExecQry(sql);

        //insert new row in emphistory
        sql = string.Format("insert into pshr.emphistory(empid,eventcode,desgcode,loccode,rowno," +
                    "eventhistoryid, pcloccode,sancdesg,sancindx,oonum,odate,status,fromdate) values " +
                    "({0},{1},{2},{3},(select nvl(max(rowno),0)+1 from pshr.emphistory where empid={0})," +
                    "(select max(eventhistoryid)+1 from pshr.emphistory),{4},{5},{6},'{7}',to_date('{8}'," +
                    "'DD-MON-YYYY hh:mi:ss AM'),1,sysdate)",
                    empid, eventcode, desgcode, loccode, "999999999", "8888", "0", oonum,
                    odate.ToString(dnet_dtformat));
        orcn.ExecQry(sql);
    }
    private string GetSelectedEmpid()
    {
        string empid = gvRequests.SelectedRow.Cells[1].Text.Trim();
        return empid;
    }
    protected void gvRequests_SelectedIndexChanged(object sender, EventArgs e)
    {
        string empid = GetSelectedEmpid();
        panAccept.Visible = true;
        show_details(empid);
    }
    protected void btnAcceptReq_Click(object sender, EventArgs e)
    {
        string sql;
        string empid = lblEmpID.Text;
        string offempid = Session["Empid"].ToString();
        string status = Session["Status"].ToString();
        OraDBconnection orcn = new OraDBconnection();

        if (lblEmpID.Text == "")
        {
            return;
        }
        if (status == "RRS")
        {
            if (drpAccept.SelectedValue == "YES")
            {
                txtROComment.Text = "";
                ChangeTables(empid);
                sql = "UPDATE cadre.chargereport set status = 'RRA', " +
                    "date_rel_accept = sysdate, rel_off_comment = '', savedon = sysdate " +
                    "where eventcode in (1,2,3,4,5,6,7,8,9,62,63,86,98) " +
                    "and status='RRS' and empid = " + empid + " and rep_off_rel = " + offempid;
                orcn.ExecQry(sql);
                lblMsg.Text = "Leave Request Accepted for empid " + empid;
            }
            else
            {
                sql = "UPDATE cadre.chargereport set rel_off_comment = '" + txtROComment.Text + "' " +
                    "where eventcode in (1,2,3,4,5,6,7,8,9,62,63,86,98) " +
                    "and status='RRS' and empid = " + empid + " and rep_off_rel = " + offempid;
                orcn.ExecQry(sql);
                lblMsg.Text = "Your change is saved for empid " + empid;
            }
        }
        show_posting_to_officer();
    }
    protected void bLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("login.aspx");
    }
    protected void drpAccept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpAccept.SelectedValue == "YES")
        {
            txtROComment.Enabled = false;
        }
        else
        {
            string status = Session["Status"].ToString();
            if (status == "RRS")
            {
                txtROComment.Enabled = true;
                txtROComment.Visible = true;
                lblROComment.Visible = true;
            }
        }
    }
}