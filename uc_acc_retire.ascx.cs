using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_acc_retire : System.Web.UI.UserControl
{
    string oracle_dtformat = "dd-MON-yyyy hh:mi:ss AM";
    string oracle_dtformat_notime = "dd-MON-yyyy";
    string dnet_dtformat = "dd-MMM-yyyy hh:mm:ss tt";
    private void show_posting_to_officer()
    {
        string status = Session["Status"].ToString();
        string sql = string.Empty;

        string empid = Session["EmpId"].ToString();
        OraDBconnection oracn = new OraDBconnection();

        sql = "SELECT cr.empid as Empid, pshr.get_fullname(cr.empid) as Name, " +
                "cr.oonum || ' / ' || to_char(cr.oodate,'dd-Mon-yyyy') as \"Office Order\", " +
                "pshr.get_desg(cr.olddesgcode) || ' at ' || cadre.get_org_plants(cr.oldloccode) \"Present Loc\"," +
                "(select eref from pshr.mast_event where eventcode=cr.eventcode) as \"Retirement Type\", " +
                "to_char(eventdate,'" + oracle_dtformat_notime + "') as \"Retirement Date\", " +
                "pshr.get_fullname(REP_OFF_REL) || ' (' || REP_OFF_REL || ')' as \"Relieving Officer\", " +
                "to_char(date_rel_req,'" + oracle_dtformat + "') as \"Relieving Request\", " +
                "to_char(date_rel_accept,'" + oracle_dtformat + "') as \"Relieving Accept\" " +
                "FROM CADRE.chargereport cr LEFT OUTER JOIN pshr.empperso e1 ON e1.empid = cr.empid " +
                "WHERE status in ('RRS', 'RRA') " +
                "AND eventcode in (11, 12, 13, 14, 15, 16, 89) " +
                "AND rep_off_rel=" + empid +
                " ORDER BY cr.oodate DESC";

        System.Data.DataSet ds = new System.Data.DataSet();
        oracn.FillData(sql, ref ds);
        oracn.fillgrid(ref gvRequests, ref ds);
    }
    private void show_details(string empid)
    {
        string sql;
        string status;

        System.Data.DataSet ds = new System.Data.DataSet();
        OraDBconnection orcn = new OraDBconnection();

        if (empid == "" || empid.Length != 6)
        {
            return;
        }

        sql = "select pshr.get_fullname(e.empid) as name, pshr.get_desg(e.cdesgcode) as desg, " +
            "cadre.get_org_plants(e.cloccode) as loc,ea.phonecell as cell, i.photo2 as photo, " +
            "c.rel_off_comment as relcomm, c.join_off_comment as joincomm, c.status as status " +
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
        status = ds.Tables[0].Rows[0]["status"].ToString();

        btnAcceptReq.Enabled = status != "JRA";

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
        show_posting_to_officer();
    }
    private void ChangeTables(string empid)
    {
        string sql;
        string oonum, eventcode, loccode, desgcode;
        string offempid = Session["EmpId"].ToString();

        DateTime odate, fromdate;

        OraDBconnection orcn = new OraDBconnection();
        System.Data.DataSet ds = new System.Data.DataSet();
        System.Data.DataRow drow;

        //get values
        //for retirement events (11, 12, 13, 14, 15, 16, 89)
        sql =
            "SELECT * FROM cadre.chargereport " +
            "WHERE status = 'RRS' " +
            "AND eventcode in (11, 12, 13, 14, 15, 16, 89) " +
            "AND rep_off_rel = " + offempid + " " +
            "AND empid = " + empid + " " +
            "ORDER BY oodate DESC";
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

        //fromdate will be eventdate
        if (!Convert.IsDBNull(drow["eventdate"].ToString()))
        {
            fromdate = (DateTime)drow["eventdate"];
        }
        else
        {
            lblMsg.Text = "Invalid Event Date";
            return;
        }


        eventcode = drow["eventcode"].ToString();
        loccode = "99999";  //loccode = "---DO---"
        desgcode = "8888";  //desgcode = (null)

        ds.Clear();

        //remove entry from cadrmap
        sql = "delete from cadre.cadrmap where empid = " + empid;
        orcn.ExecQry(sql);

        //update empperso -- put recstatus = 20
        sql = string.Format("update pshr.empperso set recstatus = 20 where empid = " + empid);
        orcn.ExecQry(sql);

        //insert new row in emphistory
        sql = string.Format("insert into pshr.emphistory(empid,eventcode,desgcode,loccode,rowno," +
                    "eventhistoryid, pcloccode,sancdesg,sancindx,oonum,odate,status,fromdate) values " +
                    "({0},{1},{2},{3},(select nvl(max(rowno),0)+1 from pshr.emphistory where empid={0})," +
                    "(select max(eventhistoryid)+1 from pshr.emphistory),{4},{5},{6},'{7}',to_date('{8}'," +
                    "'DD-MON-YYYY hh:mi:ss AM'),1, to_date('{9}','DD-MON-YYYY hh:mi:ss AM'))",
                    empid, eventcode, desgcode, loccode, "77777", "8888", "0", oonum,
                    odate.ToString(dnet_dtformat), fromdate.ToString(dnet_dtformat));
        orcn.ExecQry(sql);
    }
    private string GetSelectedEmpid()
    {
        string empid = gvRequests.SelectedRow.Cells[1].Text.Trim();
        return empid;
    }
    protected void gvRequests_SelectedIndexChanged(object sender, EventArgs e)
    {
        panAccept.Visible = true;
        show_details(GetSelectedEmpid());
    }
    protected void btnAcceptReq_Click(object sender, EventArgs e)
    {
        string sql;
        string empid = lblEmpID.Text;
        OraDBconnection orcn = new OraDBconnection();
        string status = Session["Status"].ToString();
        string offempid = Session["EmpId"].ToString();

        if (lblEmpID.Text == "")
        {
            return;
        }
        if (status == "RRS")
        {
            ChangeTables(empid);
            sql =
                "UPDATE cadre.chargereport SET status = 'RRA', " +
                "date_rel_accept = sysdate,savedon = sysdate WHERE " +
                "status = 'RRS' " +
                "AND eventcode IN (11, 12, 13, 14, 15, 16, 89) " +
                "AND empid = " + empid + " " +
                "AND rep_off_rel = " + offempid;
            orcn.ExecQry(sql);
            lblMsg.Text = "Retirement Request Accepted for empid " + empid;
        }

        show_posting_to_officer();
    }
    protected void bLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("login.aspx");
    }
}