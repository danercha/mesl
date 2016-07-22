using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TeamBuilder.DAL;
using TeamBuilder.STATIC;

namespace TeamBuilder
{
    public partial class _Default : Page
    {
        public List<TEAM> _teams = new List<TEAM>();
        public List<Local_player> _players = new List<Local_player>();
        public List<COACH> _coaches = new List<COACH>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (MESLEntities txt = new MESLEntities())
                {
                    var _plrs = (from p in txt.PLAYERs
                                 where p.TEAMID == 1
                                 select p).ToList();
                    DateTime seasonstart = new DateTime(2016, 09, 01);
                    foreach (PLAYER _p in _plrs)
                    {
                        _players.Add(new Local_player
                        {
                            ID = _p.ID,
                            FNAME = _p.FNAME.Trim(),
                            LNAME = _p.LNAME.Trim(),
                            DOB = DateTime.Parse(_p.DOB.ToString()),
                            AGE = ((DateTime.Now - DateTime.Parse(_p.DOB.ToString())).TotalDays) / 365,
                            AGESTART = ((seasonstart - DateTime.Parse(_p.DOB.ToString())).TotalDays) / 365,
                            GENDER = bool.Parse(_p.GENDER.ToString()),
                            NOTES = _p.ROSTERNOTE + " " + _p.PLAYERNOTE,
                            SEASONSPLAYED = int.Parse(_p.SEASONSPLAYED.ToString()),
                            TEAMID = int.Parse(_p.TEAMID.ToString())
                        });
                    }

                    var _tms = (from t in txt.TEAMs
                                where t.ID != 1
                                select t).ToList();

                    foreach (TEAM _t in _tms)
                    {
                        _teams.Add(_t);
                    }

                    var _coch = (from c in txt.COACHes
                                 select c).ToList();

                    foreach (COACH _c in _coch)
                    {
                        _coaches.Add(_c);
                    }

                    rptPlayer.DataSource = _players;
                    rptPlayer.DataBind();



                }
            }

        }

        protected void ddlteam_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            string selectedValue = ddl.SelectedValue;
            int playerid = 0;

            RepeaterItem item = (RepeaterItem)ddl.NamingContainer;
            if (item != null)
            {
                HiddenField list = (HiddenField)item.FindControl("hdnID");
                if (list != null)
                {
                    playerid = int.Parse(list.Value);
                }
            }

            using (MESLEntities ptx = new MESLEntities())
            {
                var p = (from s in ptx.PLAYERs
                         where s.ID == playerid
                         select s).FirstOrDefault();

                p.TEAMID = int.Parse(selectedValue);
                ptx.SaveChanges();
            }

            Response.Redirect("Default.aspx");

        }

        protected void rptPlayer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                ((DropDownList)e.Item.FindControl("ddlteam")).DataSource = _teams;
                ((DropDownList)e.Item.FindControl("ddlteam")).DataTextField = "Name";
                ((DropDownList)e.Item.FindControl("ddlteam")).DataValueField = "ID";
                ((DropDownList)e.Item.FindControl("ddlteam")).DataBind();

            }
        }
    }
}