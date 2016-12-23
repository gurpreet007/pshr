using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_acc_tnp : System.Web.UI.UserControl
{
    string oracle_dtformat = "dd-MON-yyyy hh:mi:ss AM";
    string dnet_dtformat = "dd-MMM-yyyy hh:mm:ss tt";
    private void show_posting_to_officer()
    {
        string sql = string.Empty;
        //Session["Status"] = "RRS";
        string status = Session["Status"].ToString();
        string empid = Session["EmpId"].ToString();
        OraDBconnection oracn = new OraDBconnection();

        sql = "SELECT cr.empid as Empid, pshr.get_fullname(cr.empid) as Name, " +
                "cr.oonum || ' / ' || to_char(cr.oodate,'dd-Mon-yyyy') as \"Office Order\", " +
                "pshr.get_desg(cr.olddesgcode) || ' at ' || cadre.get_org_plants(cr.oldloccode) \"Present Loc\"," +
                "pshr.get_desg(cr.desgcode)  || ' at ' || cadre.get_org_plants(cr.loccode) \"NEW Loc\"," +
            //"nvl2(cr.postjoin, " +
            //"pshr.get_desg(c2.desgcode)    || ' at '    || cadre.get_org_plants(c2.loccode), " +
            //"pshr.get_desg(cr.desgcode)    || ' at '    || cadre.get_org_plants(cr.loccode) " +
            //") \"NEW Loc\", " +
                "CASE WHEN rel_skip='1' then 'N/A' ELSE pshr.get_fullname(REP_OFF_REL) || ' (' || REP_OFF_REL || ')' END as \"Relieving Officer\", " +
                "CASE WHEN rel_skip='1' then 'N/A' ELSE to_char(date_rel_req,'" + oracle_dtformat + "') END as \"Relieving Request\", " +
                "CASE WHEN rel_skip='1' then 'N/A' ELSE to_char(date_rel_accept,'" + oracle_dtformat + "') END as \"Relieving Accept\", " +
                "pshr.get_fullname(REP_OFF_JOIN) || ' (' || REP_OFF_JOIN || ')' as \"Joining Officer\", " +
                "to_char(date_join_req,'" + oracle_dtformat + "') as \"Joining Request\", " +
                "to_char(date_join_accept,'" + oracle_dtformat + "') as \"Joining Accept\" " +
                "FROM CADRE.chargereport cr LEFT OUTER JOIN pshr.empperso e1 ON e1.empid = cr.empid " +
                "LEFT OUTER JOIN cadre.cadr c2 ON c2.rowno = cr.postjoin ";

        if (status == "RRS")
        {
            lblROComment.Visible = true;
            txtROComment.Visible = true;
            sql += string.Format("WHERE status = 'RRS' and rep_off_rel={0} ", empid);
        }
        else if (status == "JRS")
        {
            lblROComment.Visible = false;
            txtROComment.Visible = false;
            lblJOComment.Visible = true;
            txtJOComment.Visible = true;

            lblROComment.Enabled = false;
            txtROComment.Enabled = false;

            sql += string.Format("WHERE status = 'JRS' and rep_off_join={0} ", empid);
        }

        sql += "ORDER BY cr.oodate DESC";

        System.Data.DataSet ds = new System.Data.DataSet();
        oracn.FillData(sql, ref ds);
        oracn.fillgrid(ref gvRequests, ref ds);
    }
    private void show_details(string empid)
    {
        string sql;
        btnAcceptReq.Enabled = true;
        System.Data.DataSet ds = new System.Data.DataSet();
        OraDBconnection orcn = new OraDBconnection();

        if (empid == "" || empid.Length != 6)
        {
            return;
        }

        sql = "select pshr.get_fullname(e.empid) as name, pshr.get_desg(e.cdesgcode) as desg, " +
            "cadre.get_org_plants(e.cloccode) as loc,ea.phonecell as cell, i.photo2 as photo, " +
            "c.rel_off_comment as relcomm, c.join_off_comment as joincomm,c.status " +
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
        lblMsg.Text = string.Empty;
        lblRRName.Text = ds.Tables[0].Rows[0]["name"].ToString();
        lblRRLoc.Text = ds.Tables[0].Rows[0]["loc"].ToString();
        lblRRDesg.Text = ds.Tables[0].Rows[0]["desg"].ToString();
        lblRRMob.Text = ds.Tables[0].Rows[0]["cell"].ToString();
        txtROComment.Text = ds.Tables[0].Rows[0]["relcomm"].ToString();
        txtJOComment.Text = ds.Tables[0].Rows[0]["joincomm"].ToString();
        string st=ds.Tables[0].Rows[0]["status"].ToString();

             if (st == "RRS")
            {
                //txtJOComment.Enabled = false;
                txtROComment.Enabled = true;
                txtROComment.Visible = true;
                 lblROComment.Visible = true;

                txtJOComment.Visible = false;
                lblJOComment.Visible = false;
                
                
            }
             else if (st == "JRS")
             {
                 txtJOComment.Enabled = true;
                 txtJOComment.Visible = true;
                 lblJOComment.Visible = true;
                 txtROComment.Visible = false;
                 lblROComment.Visible = false;
             }
        //load image
        if (!Convert.IsDBNull(ds.Tables[0].Rows[0]["photo"]))
        {
            byte[] barr = (byte[])ds.Tables[0].Rows[0]["photo"];
            string base64str = Convert.ToBase64String(barr);
            imgEmpPhoto.ImageUrl = string.Format("data:image/gif;base64,{0}", base64str);
        }

        ds.Clear();
        ds.Dispose();
        panRelReq.Visible = true;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblJOComment.Visible = false;
        lblROComment.Visible = false;
        txtJOComment.Visible = false;
        txtROComment.Visible = false;
        if (!IsPostBack)
        {
            show_posting_to_officer();
        }
    }

    private bool IsReportingOfficer(string empid, string oodate, string oonum)
    {
        string status = Session["Status"].ToString();
        string repOfficerId = Session["EmpId"].ToString();
        OraDBconnection oracn = new OraDBconnection();
        string sql = string.Empty;
        if (status == "RRS")
        {
            sql = "Select rep_off_rel from cadre.chargereport where empid= " + empid + " and oonum= '" + oonum + "' and to_char(oodate,'dd-Mon-yyyy')='"
                + oodate + "' and rep_off_rel= " + repOfficerId;
                
        }
        else if (status == "JRS")
        {
            sql = "Select rep_off_join from cadre.chargereport where empid= " + empid + " and oonum= '" + oonum + "' and to_char(oodate,'dd-Mon-yyyy')='" + oodate + "' and rep_off_join= " + repOfficerId;
        }

        System.Data.DataSet ds = new System.Data.DataSet();
        oracn.FillData(sql, ref ds);
        return ds.Tables[0].Rows.Count > 0;
    }

    private bool ChangeTables(string empid)
    {
        string sql;
        string oonum, postrel, postjoin, eventcode, loccode, desgcode;
        string jloccode, jdesgcode, jindx;
        DateTime odate, fromdate;

        OraDBconnection orcn = new OraDBconnection();
        System.Data.DataSet ds = new System.Data.DataSet();
        System.Data.DataRow drow;

        //get values
        sql = "select * from cadre.chargereport where "+
            "status = 'JRS' and empid = " + empid + 
            " and oodate = (select max(oodate) from cadre.chargereport "+
            "where status='JRS' and empid = " + empid + ") order by oodate desc";
        orcn.FillData(sql, ref ds);

        if (ds.Tables[0].Rows.Count != 1)
        {
            //lMsg0.Text = "No Pending Row";
            return false;
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
            return false;
        }
        //todate will be sysdate
        //fromdate will be relieving date
        if (!Convert.IsDBNull(drow["date_rel_accept"].ToString()))
        {
            fromdate = (DateTime)drow["date_rel_accept"];
        }
        else
        {
            lblMsg.Text = "Invalid Event Date";
            return false;
        }
        postrel = drow["postrel"].ToString();
        postjoin = drow["postjoin"].ToString();
        eventcode = drow["eventcode"].ToString();
        loccode = drow["loccode"].ToString();
        desgcode = drow["desgcode"].ToString();

        ds.Clear();

        //postjoin is empty it means we are dealing with a special location
        if (string.IsNullOrEmpty(postjoin))
        {
            jloccode = string.Empty;
            jdesgcode = string.Empty;
            jindx = string.Empty;

            //delete entry from cadrmap
            sql = "delete from cadre.cadrmap where empid = " + empid;
            orcn.ExecQry(sql);
        }
        else
        {
            //get the trifecta from newrowno
            sql = "select loccode, desgcode, indx from cadre.cadr where rowno =" + postjoin;
            orcn.FillData(sql, ref ds);

            if (ds.Tables[0].Rows.Count != 1)
            {
                lMsg0.Text = "Unable to get join row in cadre";
                return false;
            }
            drow = ds.Tables[0].Rows[0];
            jloccode = drow["loccode"].ToString();
            jdesgcode = drow["desgcode"].ToString();
            jindx = drow["indx"].ToString();

            //stop if post is not empty
            sql = "select empid, rowno, pshr.get_fullname(empid) as name from cadre.cadrmap where rowno = " + postjoin;
            System.Data.DataSet ds2 = new System.Data.DataSet();
            orcn.FillData(sql, ref ds2);
            //if (ds2.Tables[0].Rows.Count > 0)
            //{
            //    lMsg0.Text = String.Format("The Joining Location is already occupied by Er. {0} (ID {1}). Aborting", 
            //        ds2.Tables[0].Rows[0]["name"], ds2.Tables[0].Rows[0]["empid"]);
            //    return false;
            //}

            //update cadrmap
            sql = "delete from cadre.cadrmap where empid = " + empid + " or rowno = " + postjoin;
            orcn.ExecQry(sql);
            sql = string.Format("insert into cadre.cadrmap(empid,rowno) values ({0},{1})", empid, postjoin);
            orcn.ExecQry(sql);
        }

        //update empperso
        sql = string.Format("update pshr.empperso set cloccode = {0}, cdesgcode = {1} where empid = {2}",
            loccode, desgcode, empid);
        orcn.ExecQry(sql);

        //insert new row in emphistory
        sql = string.Format("insert into pshr.emphistory(empid,eventcode,desgcode,loccode,rowno," +
                    "eventhistoryid, pcloccode,sancdesg,sancindx,oonum,odate,status,fromdate,todate) values " +
                    "({0},{1},{2},{3},(select nvl(max(rowno),0)+1 from pshr.emphistory where empid={0})," +
                    "(select max(eventhistoryid)+1 from pshr.emphistory),'{4}','{5}','{6}','{7}',to_date('{8}'," +
                    "'DD-MON-YYYY hh:mi:ss AM'),1,to_date('{9}','DD-MON-YYYY hh:mi:ss AM'), sysdate)",
                    empid, eventcode, desgcode, loccode, jloccode, jdesgcode, jindx, oonum,
                    odate.ToString(dnet_dtformat), fromdate.ToString(dnet_dtformat));
        orcn.ExecQry(sql);
        return true;
    }
    protected void gvRequests_SelectedIndexChanged(object sender, EventArgs e)
    {
        string empid = gvRequests.SelectedRow.Cells[1].Text.Trim();
        string oonum_date = gvRequests.SelectedRow.Cells[3].Text.Trim();
        string oonum = oonum_date.Substring(0,oonum_date.LastIndexOf('/')).Trim();
        string oodate = oonum_date.Substring(oonum_date.LastIndexOf('/') + 1).Trim();
            
        panAccept.Visible = true;
        show_details(empid);
    }
    protected void btnAcceptReq_Click(object sender, EventArgs e)
    {
        string sql;
        string empid = lblEmpID.Text;
        OraDBconnection orcn = new OraDBconnection();
        string status = Session["Status"].ToString();
        System.Data.DataSet ds = new System.Data.DataSet();
        string phonecell;
        string msg;

        if (lblEmpID.Text == "")
        {
            
            return;
        }
        string oonum_date = gvRequests.SelectedRow.Cells[3].Text.Trim();
        string oonum = oonum_date.Substring(0, oonum_date.LastIndexOf('/')).Trim();
        string oodate = oonum_date.Substring(oonum_date.LastIndexOf('/') + 1).Trim();

        if (!IsReportingOfficer(lblEmpID.Text, oodate, oonum))
        {
            show_posting_to_officer();
            gvRequests.SelectedIndex = -1;
            lMsg0.Text = "Relieving/joining officer has been changed by employee " + empid;
            panRelReq.Visible = false;
            return;
        }
        if (drpAccept.SelectedValue == "YES")
        {
            //txtJOComment.Text = "";
            //txtROComment.Text = "";
            if (status == "RRS")
            {
                //ChangeTables(empid);
                
                //remove mapping
                orcn.ExecQry("delete from cadre.cadrmap where empid = " + empid);

                sql = string.Format("UPDATE cadre.chargereport SET status='RRA', "+
                    "date_rel_accept=sysdate, rel_off_comment='{0}', savedon=sysdate "+
                    "WHERE status='RRS' AND empid={1} AND "+
                    "oodate=(SELECT max(oodate) FROM cadre.chargereport WHERE empid = {1} AND status = 'RRS')", 
                    txtROComment.Text.Trim(), empid);
                //sql = "UPDATE cadre.chargereport set status = 'RRA', " +
                //    "date_rel_accept = sysdate, rel_off_comment = '" + txtROComment.Text +
                //    "', savedon=sysdate where status='RRS' and empid = " + empid;
                orcn.ExecQry(sql);
                lblMsg.Text = "Relieve Request Accepted for empid " + empid;
            }
            else if (status == "JRS")
            {
                if (! ChangeTables(empid))
                {
                    return;
                }
                sql = string.Format("UPDATE cadre.chargereport SET status = 'JRA', "+
                    "date_join_accept = sysdate, join_off_comment='{0}' " +
                    "WHERE status='JRS' AND empid={1} AND " +
                    "oodate=(select max(oodate) FROM cadre.chargereport WHERE empid={1} AND status = 'JRS')",
                    txtJOComment.Text, empid);
                //sql = "UPDATE cadre.chargereport set status = 'JRA', " +
                //    "date_join_accept = sysdate, join_off_comment = '" + txtJOComment.Text +
                //    "' where status='JRS' and empid = " + empid;
                orcn.ExecQry(sql);
                lblMsg.Text = "Join Request Accepted for empid " + empid;
            }
            if (status == "RRS" || status == "JRS")
            {
                orcn.FillData("select nvl(phonecell,'0') from pshr.EMPADDR where empid = " + empid, ref ds);
                phonecell = ds.Tables[0].Rows[0][0].ToString();
                msg = "Your relieving/joining request is accepted. Please visit HR Portal";
                if (System.Environment.MachineName.ToUpper().Contains("SERVER") && phonecell != "0" && libSMSPbGovt.SMS.SendSMS(phonecell, msg))
                {
                    orcn.ExecQry(string.Format("insert into cadre.smslog values('{0}','{1}',sysdate)", phonecell, msg));
                }
            }
        }
        else
        {
            if (status == "RRS")
            {
                if (String.IsNullOrWhiteSpace(txtROComment.Text))
                {
                    txtROComment.Enabled = true;
                    txtROComment.Visible = true;
                    lblROComment.Visible = true;

                    txtJOComment.Visible = false;
                    lblJOComment.Visible = false;
                
                    lblMsg.Text = "Please enter comments for choosing No option";
                    return;
                }
                sql = string.Format("UPDATE cadre.chargereport SET rel_off_comment='{0}' "+
                    "WHERE status='RRS' AND empid={1} AND "+
                    "oodate=(SELECT max(oodate) FROM cadre.chargereport WHERE empid = {1} AND status = 'RRS')", 
                    txtROComment.Text.Trim(), empid);
                //sql = "UPDATE cadre.chargereport set rel_off_comment = '" + txtROComment.Text +
                //    "' where status='RRS' and empid = " + empid;
                orcn.ExecQry(sql);
                lblMsg.Text = "Your change is saved for empid " + empid;
            }
            else if (status == "JRS")
            {
                if (String.IsNullOrWhiteSpace(txtJOComment.Text))
                {
                   txtJOComment.Enabled = true;
                 txtJOComment.Visible = true;
                 lblJOComment.Visible = true;
                 txtROComment.Visible = false;
                 lblROComment.Visible = false;
                    
                    lblMsg.Text = "Please enter comments for choosing No option";
                    return;
                }
                sql = string.Format("UPDATE cadre.chargereport SET join_off_comment='{0}' " +
                    "WHERE status='JRS' AND empid={1} AND " +
                    "oodate=(select max(oodate) FROM cadre.chargereport WHERE empid={1} AND status = 'JRS')",txtJOComment.Text, empid);
                //sql = "UPDATE cadre.chargereport set join_off_comment = '" + txtJOComment.Text +
                //    "' where status='JRS' and empid = " + empid;
                orcn.ExecQry(sql);
                lblMsg.Text = "Your change is saved for empid " + empid;
            }
        }
        btnAcceptReq.Enabled = false;
        show_posting_to_officer();
    }
    protected void bLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("login.aspx");
    }
    protected void drpAccept_SelectedIndexChanged(object sender, EventArgs e)
    {
       
            string status = Session["Status"].ToString();
            if (status == "RRS")
            {
                //txtJOComment.Enabled = false;
                txtROComment.Enabled = true;

                txtJOComment.Visible = false;
                lblJOComment.Visible = false;
                txtROComment.Visible = true;
                lblROComment.Visible = true;
            }
            else if (status == "JRS")
            {
                txtJOComment.Enabled = true;
                //txtROComment.Enabled = false;

                txtJOComment.Visible = true;
                lblJOComment.Visible = true;
                txtROComment.Visible = false;
                lblROComment.Visible = false;
            }
        
    }
}