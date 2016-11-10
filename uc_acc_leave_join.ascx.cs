using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_acc_leave_join : System.Web.UI.UserControl
{
    string oracle_dtformat = "dd-MON-yyyy hh:mi:ss AM";
    string dnet_dtformat = "dd-MMM-yyyy hh:mm:ss tt";
    private void show_posting_to_officer()
    {
        string status = Session["Status"].ToString();
        string sql = string.Empty;

        string empid = Session["EmpId"].ToString();
        OraDBconnection oracn = new OraDBconnection();

        sql = "SELECT cr.empid as Empid, pshr.get_fullname(cr.empid) as Name, " +
                "cr.oonum || ' / ' || to_char(cr.oodate,'dd-Mon-yyyy') as \"Office Order\", " +
                "'ON LEAVE' \"Present Loc\", " +
                "pshr.get_desg(cr.desgcode)  || ' at ' || cadre.get_org_plants(cr.loccode) \"NEW Loc\"," +
                //"nvl2(cr.postjoin, " +
                //"pshr.get_desg(c2.desgcode)    || ' at '    || cadre.get_org_plants(c2.loccode), " +
                //"pshr.get_desg(cr.desgcode)    || ' at '    || cadre.get_org_plants(cr.loccode) " +
                //") \"NEW Loc\", " +
                "pshr.get_fullname(REP_OFF_JOIN) || ' (' || REP_OFF_JOIN || ')' as \"Joining Officer\", " +
                "to_char(date_join_req,'" + oracle_dtformat + "') as \"Joining Request\", " +
                "to_char(date_join_accept,'" + oracle_dtformat + "') as \"Joining Accept\" " +
                "FROM CADRE.chargereport cr LEFT OUTER JOIN cadre.cadr c2 ON c2.rowno = cr.postjoin " +
                "WHERE eventcode = 10 and status in ('JRS','JRA') and rep_off_join=" + empid +
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
        string oonum, postjoin, eventcode, loccode, desgcode;
        string jloccode, jdesgcode, jindx;
        DateTime odate;
        //DateTime todate;

        OraDBconnection orcn = new OraDBconnection();
        System.Data.DataSet ds = new System.Data.DataSet();
        System.Data.DataRow drow;

        //get values
        //only for LJON (10)
        sql = "select * from cadre.chargereport where status = 'JRS' and eventcode = 10 and empid = " + empid + " order by oodate desc";
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

        ////todate will be eventdate
        //if (!Convert.IsDBNull(drow["eventdate"].ToString()))
        //{
        //    todate = (DateTime)drow["eventdate"];
        //}
        //else
        //{
        //    lblMsg.Text = "Invalid Event Date";
        //    return;
        //}

        //todate will be sysdate
        postjoin = drow["postjoin"].ToString();
        eventcode = "10";   //only for LJON (10)
        loccode = drow["loccode"].ToString();
        desgcode = drow["desgcode"].ToString();

        ds.Clear();

        //get the trifecta from newrowno
        sql = "select loccode, desgcode, indx from cadre.cadr where rowno =" + postjoin;
        orcn.FillData(sql, ref ds);

        if (ds.Tables[0].Rows.Count != 1)
        {
            lMsg0.Text = "Unable to get join row in cadre";
            return;
        }
        drow = ds.Tables[0].Rows[0];
        jloccode = drow["loccode"].ToString();
        jdesgcode = drow["desgcode"].ToString();
        jindx = drow["indx"].ToString();

        //update cadrmap
        sql = "delete from cadre.cadrmap where empid = " + empid + " or rowno = " + postjoin;
        orcn.ExecQry(sql);
        sql = string.Format("insert into cadre.cadrmap(empid,rowno) values ({0},{1})", empid, postjoin);
        orcn.ExecQry(sql);

        //update empperso
        sql = string.Format("update pshr.empperso set cloccode = {0}, cdesgcode = {1} where empid = {2}",
            loccode, desgcode, empid);
        orcn.ExecQry(sql);

        //insert new row in emphistory
        sql = string.Format("insert into pshr.emphistory(empid,eventcode,desgcode,loccode,rowno," +
                    "eventhistoryid, pcloccode,sancdesg,sancindx,oonum,odate,status,todate) values " +
                    "({0},{1},{2},{3},(select nvl(max(rowno),0)+1 from pshr.emphistory where empid={0})," +
                    "(select max(eventhistoryid)+1 from pshr.emphistory),{4},{5},{6},'{7}',to_date('{8}'," +
                    "'DD-MON-YYYY hh:mi:ss AM'),1,sysdate)",
                    empid, eventcode, desgcode, loccode, jloccode, jdesgcode, jindx, oonum,
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
        if (status == "JRS")
        {
            ChangeTables(empid);
            sql = "UPDATE cadre.chargereport SET status = 'JRA', " +
                "date_join_accept = sysdate, savedon=sysdate WHERE " +
                "status='JRS' " +
                "AND eventcode = 10 " +
                "AND empid = " + empid + " " +
                "AND rep_off_join = " + offempid;
            orcn.ExecQry(sql);
            lblMsg.Text = "Join Request Accepted for empid " + empid;
        }

        show_posting_to_officer();
    }
    protected void bLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("login.aspx");
    }
}