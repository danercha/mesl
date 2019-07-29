using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

using TeamBuilder.DAL;
using TeamBuilder.STATIC;

namespace TeamBuilder.Divisions
{
    public partial class U12 : System.Web.UI.Page
    {
        public List<Local_Team> _teams = new List<Local_Team>();
        public List<Local_player> _players = new List<Local_player>();
        public List<COACH> _coaches = new List<COACH>();
        public string _division = "U12";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (MESLEntities txt = new MESLEntities())
                {
                    var _plrs = (from p in txt.PLAYERs
                                 where p.DIVISION.Trim() == _division
                                 select p).ToList();
                    DateTime seasonstart = new DateTime(DateTime.Now.Year, 09, 01);
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
                                where t.ID != 1 && t.DIVISION.Trim() == _division
                                select t).ToList();

                    foreach (TEAM _t in _tms)
                    {
                        var _dobs = (from i in txt.PLAYERs where i.TEAMID == _t.ID select i.DOB).ToList();
                        var _gen = (from i in txt.PLAYERs where i.TEAMID == _t.ID select i.GENDER).ToList();

                        List<double> _age = new List<double>();
                        foreach (var d in _dobs)
                        {
                            _age.Add(((DateTime.Now - DateTime.Parse(d.ToString())).TotalDays) / 365);
                        }
                        List<int> _genders = new List<int>();
                        foreach (var g in _gen)
                        {
                            if (_gen != null)
                            {
                                _genders.Add((bool.Parse(g.ToString())) ? 0 : 1);
                            }
                        }

                        double gensum = _genders.Sum();
                        double count = _genders.Count();
                        double gendersum = 0;
                        if (count > 0)
                        {
                            gendersum = (gensum / count);
                        }

                        var _ass = (from a in txt.ASSISTANTs where a.TEAMID == _t.ID select new { name = "[" + a.FNAME.Trim() + " " + a.LNAME.Trim() + "]" }).FirstOrDefault();
                        string assname = string.Empty;
                        if (_ass != null)
                        {
                            assname = _ass.name;
                        }

                        _teams.Add(new Local_Team
                        {
                            ID = _t.ID,
                            NAME = _t.NAME.Trim(),
                            COACH = _t.COACHes.Where(z => z.TEAMID == _t.ID).First().FNAME.Trim() + " " + _t.COACHes.Where(z => z.TEAMID == _t.ID).First().LNAME.Trim() + " " + assname,
                            PLAYER1 = (!string.IsNullOrEmpty(_t.PLAYER1.ToString())) ? (((from p in _players where p.ID == _t.PLAYER1 select p).First().GENDER == true) ? "(m)" : "(f)") + " " + (from p in _players where p.ID == _t.PLAYER1 select p).First().FNAME.Trim() + " " + (from p in _players where p.ID == _t.PLAYER1 select p).First().LNAME.Trim() + " [ " + (from p in _players where p.ID == _t.PLAYER1 select p).First().AGE.ToString("#.##") + " ]" : "",
                            PLAYER2 = (!string.IsNullOrEmpty(_t.PLAYER2.ToString())) ? (((from p in _players where p.ID == _t.PLAYER2 select p).First().GENDER == true) ? "(m)" : "(f)") + " " + (from p in _players where p.ID == _t.PLAYER2 select p).First().FNAME.Trim() + " " + (from p in _players where p.ID == _t.PLAYER2 select p).First().LNAME.Trim() + " [ " + (from p in _players where p.ID == _t.PLAYER2 select p).First().AGE.ToString("#.##") + " ]" : "",
                            PLAYER3 = (!string.IsNullOrEmpty(_t.PLAYER3.ToString())) ? (((from p in _players where p.ID == _t.PLAYER3 select p).First().GENDER == true) ? "(m)" : "(f)") + " " + (from p in _players where p.ID == _t.PLAYER3 select p).First().FNAME.Trim() + " " + (from p in _players where p.ID == _t.PLAYER3 select p).First().LNAME.Trim() + " [ " + (from p in _players where p.ID == _t.PLAYER3 select p).First().AGE.ToString("#.##") + " ]" : "",
                            PLAYER4 = (!string.IsNullOrEmpty(_t.PLAYER4.ToString())) ? (((from p in _players where p.ID == _t.PLAYER4 select p).First().GENDER == true) ? "(m)" : "(f)") + " " + (from p in _players where p.ID == _t.PLAYER4 select p).First().FNAME.Trim() + " " + (from p in _players where p.ID == _t.PLAYER4 select p).First().LNAME.Trim() + " [ " + (from p in _players where p.ID == _t.PLAYER4 select p).First().AGE.ToString("#.##") + " ]" : "",
                            PLAYER5 = (!string.IsNullOrEmpty(_t.PLAYER5.ToString())) ? (((from p in _players where p.ID == _t.PLAYER5 select p).First().GENDER == true) ? "(m)" : "(f)") + " " + (from p in _players where p.ID == _t.PLAYER5 select p).First().FNAME.Trim() + " " + (from p in _players where p.ID == _t.PLAYER5 select p).First().LNAME.Trim() + " [ " + (from p in _players where p.ID == _t.PLAYER5 select p).First().AGE.ToString("#.##") + " ]" : "",
                            PLAYER6 = (!string.IsNullOrEmpty(_t.PLAYER6.ToString())) ? (((from p in _players where p.ID == _t.PLAYER6 select p).First().GENDER == true) ? "(m)" : "(f)") + " " + (from p in _players where p.ID == _t.PLAYER6 select p).First().FNAME.Trim() + " " + (from p in _players where p.ID == _t.PLAYER6 select p).First().LNAME.Trim() + " [ " + (from p in _players where p.ID == _t.PLAYER6 select p).First().AGE.ToString("#.##") + " ]" : "",
                            PLAYER7 = (!string.IsNullOrEmpty(_t.PLAYER7.ToString())) ? (((from p in _players where p.ID == _t.PLAYER7 select p).First().GENDER == true) ? "(m)" : "(f)") + " " + (from p in _players where p.ID == _t.PLAYER7 select p).First().FNAME.Trim() + " " + (from p in _players where p.ID == _t.PLAYER7 select p).First().LNAME.Trim() + " [ " + (from p in _players where p.ID == _t.PLAYER7 select p).First().AGE.ToString("#.##") + " ]" : "",
                            PLAYER8 = (!string.IsNullOrEmpty(_t.PLAYER8.ToString())) ? (((from p in _players where p.ID == _t.PLAYER8 select p).First().GENDER == true) ? "(m)" : "(f)") + " " + (from p in _players where p.ID == _t.PLAYER8 select p).First().FNAME.Trim() + " " + (from p in _players where p.ID == _t.PLAYER8 select p).First().LNAME.Trim() + " [ " + (from p in _players where p.ID == _t.PLAYER8 select p).First().AGE.ToString("#.##") + " ]" : "",
                            PLAYER9 = (!string.IsNullOrEmpty(_t.PLAYER9.ToString())) ? (((from p in _players where p.ID == _t.PLAYER9 select p).First().GENDER == true) ? "(m)" : "(f)") + " " + (from p in _players where p.ID == _t.PLAYER9 select p).First().FNAME.Trim() + " " + (from p in _players where p.ID == _t.PLAYER9 select p).First().LNAME.Trim() + " [ " + (from p in _players where p.ID == _t.PLAYER9 select p).First().AGE.ToString("#.##") + " ]" : "",
                            PLAYER10 = (!string.IsNullOrEmpty(_t.PLAYER10.ToString())) ? (((from p in _players where p.ID == _t.PLAYER10 select p).First().GENDER == true) ? "(m)" : "(f)") + " " + (from p in _players where p.ID == _t.PLAYER10 select p).First().FNAME.Trim() + " " + (from p in _players where p.ID == _t.PLAYER10 select p).First().LNAME.Trim() + " [ " + (from p in _players where p.ID == _t.PLAYER10 select p).First().AGE.ToString("#.##") + " ]" : "",
                            PLAYER11 = (!string.IsNullOrEmpty(_t.PLAYER11.ToString())) ? (((from p in _players where p.ID == _t.PLAYER11 select p).First().GENDER == true) ? "(m)" : "(f)") + " " + (from p in _players where p.ID == _t.PLAYER11 select p).First().FNAME.Trim() + " " + (from p in _players where p.ID == _t.PLAYER11 select p).First().LNAME.Trim() + " [ " + (from p in _players where p.ID == _t.PLAYER11 select p).First().AGE.ToString("#.##") + " ]" : "",
                            PLAYER12 = (!string.IsNullOrEmpty(_t.PLAYER12.ToString())) ? (((from p in _players where p.ID == _t.PLAYER12 select p).First().GENDER == true) ? "(m)" : "(f)") + " " + (from p in _players where p.ID == _t.PLAYER12 select p).First().FNAME.Trim() + " " + (from p in _players where p.ID == _t.PLAYER12 select p).First().LNAME.Trim() + " [ " + (from p in _players where p.ID == _t.PLAYER12 select p).First().AGE.ToString("#.##") + " ]" : "",
                            PLAYER13 = (!string.IsNullOrEmpty(_t.PLAYER13.ToString())) ? (((from p in _players where p.ID == _t.PLAYER13 select p).First().GENDER == true) ? "(m)" : "(f)") + " " + (from p in _players where p.ID == _t.PLAYER13 select p).First().FNAME.Trim() + " " + (from p in _players where p.ID == _t.PLAYER13 select p).First().LNAME.Trim() + " [ " + (from p in _players where p.ID == _t.PLAYER13 select p).First().AGE.ToString("#.##") + " ]" : "",
                            PLAYER14 = (!string.IsNullOrEmpty(_t.PLAYER14.ToString())) ? (((from p in _players where p.ID == _t.PLAYER14 select p).First().GENDER == true) ? "(m)" : "(f)") + " " + (from p in _players where p.ID == _t.PLAYER14 select p).First().FNAME.Trim() + " " + (from p in _players where p.ID == _t.PLAYER14 select p).First().LNAME.Trim() + " [ " + (from p in _players where p.ID == _t.PLAYER14 select p).First().AGE.ToString("#.##") + " ]" : "",
                            AGE = (_age.Count() > 0) ? _age.Average() : 0,
                            GENDER = gendersum * 100

                        });
                    }

                    var _coch = (from c in txt.COACHes
                                 where c.DIVISION.Trim() == _division
                                 select c).ToList();

                    foreach (COACH _c in _coch)
                    {
                        _coaches.Add(_c);
                    }

                    rptPlayer.DataSource = _players.Where(x => x.TEAMID == 1); 
                    rptPlayer.DataBind();
                    lblPlayerCount.Text = "[ " + _players.Where(x => x.TEAMID == 1).Count().ToString() + " ]";

                    rptAllPlayer.DataSource = _players;
                    rptAllPlayer.DataBind(); 
                    lblAllPlayerCount.Text = "[ " + _players.Count().ToString() + " ]";

                    rptTeams.DataSource = _teams;
                    rptTeams.DataBind();

                    rptCoaches.DataSource = _coaches;
                    rptCoaches.DataBind();


                }
            }

        }

        protected void ddlteam_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            int teamid = int.Parse(ddl.SelectedValue);
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



                var t = (from x in ptx.TEAMs
                         where x.ID == teamid
                         select x).FirstOrDefault();
                if (string.IsNullOrEmpty(t.PLAYER1.ToString()))
                {
                    t.PLAYER1 = playerid;
                    p.TEAMID = teamid;
                    ptx.SaveChanges();
                    ptx.Dispose();
                    Response.Redirect("U12.aspx");
                }
                else if (string.IsNullOrEmpty(t.PLAYER2.ToString()))
                {
                    t.PLAYER2 = playerid;
                    p.TEAMID = teamid;
                    ptx.SaveChanges();
                    ptx.Dispose();
                    Response.Redirect("U12.aspx");
                }
                else if (string.IsNullOrEmpty(t.PLAYER3.ToString()))
                {
                    t.PLAYER3 = playerid;
                    p.TEAMID = teamid;
                    ptx.SaveChanges();
                    ptx.Dispose();
                    Response.Redirect("U12.aspx");
                }
                else if (string.IsNullOrEmpty(t.PLAYER4.ToString()))
                {
                    t.PLAYER4 = playerid;
                    p.TEAMID = teamid;
                    ptx.SaveChanges();
                    ptx.Dispose();
                    Response.Redirect("U12.aspx");
                }
                else if (string.IsNullOrEmpty(t.PLAYER5.ToString()))
                {
                    t.PLAYER5 = playerid;
                    p.TEAMID = teamid;
                    ptx.SaveChanges();
                    ptx.Dispose();
                    Response.Redirect("U12.aspx");
                }
                else if (string.IsNullOrEmpty(t.PLAYER6.ToString()))
                {
                    t.PLAYER6 = playerid;
                    p.TEAMID = teamid;
                    ptx.SaveChanges();
                    ptx.Dispose();
                    Response.Redirect("U12.aspx");
                }
                else if (string.IsNullOrEmpty(t.PLAYER7.ToString()))
                {
                    t.PLAYER7 = playerid;
                    p.TEAMID = teamid;
                    ptx.SaveChanges();
                    ptx.Dispose();
                    Response.Redirect("U12.aspx");
                }
                else if (string.IsNullOrEmpty(t.PLAYER8.ToString()))
                {
                    t.PLAYER8 = playerid;
                    p.TEAMID = teamid;
                    ptx.SaveChanges();
                    ptx.Dispose();
                    Response.Redirect("U12.aspx");
                }
                else if (string.IsNullOrEmpty(t.PLAYER9.ToString()))
                {
                    t.PLAYER9 = playerid;
                    p.TEAMID = teamid;
                    ptx.SaveChanges();
                    ptx.Dispose();
                    Response.Redirect("U12.aspx");
                }
                else if (string.IsNullOrEmpty(t.PLAYER10.ToString()))
                {
                    t.PLAYER10 = playerid;
                    p.TEAMID = teamid;
                    ptx.SaveChanges();
                    ptx.Dispose();
                    Response.Redirect("U12.aspx");
                }
                else if (string.IsNullOrEmpty(t.PLAYER11.ToString()))
                {
                    t.PLAYER11 = playerid;
                    p.TEAMID = teamid;
                    ptx.SaveChanges();
                    ptx.Dispose();
                    Response.Redirect("U12.aspx");
                }
                else if (string.IsNullOrEmpty(t.PLAYER12.ToString()))
                {
                    t.PLAYER12 = playerid;
                    p.TEAMID = teamid;
                    ptx.SaveChanges();
                    ptx.Dispose();
                    Response.Redirect("U12.aspx");
                }
                else if (string.IsNullOrEmpty(t.PLAYER13.ToString()))
                {
                    t.PLAYER13 = playerid;
                    p.TEAMID = teamid;
                    ptx.SaveChanges();
                    ptx.Dispose();
                    Response.Redirect("U12.aspx");
                }
                else if (string.IsNullOrEmpty(t.PLAYER14.ToString()))
                {
                    t.PLAYER14 = playerid;
                    p.TEAMID = teamid;
                    ptx.SaveChanges();
                    ptx.Dispose();
                    Response.Redirect("U12.aspx");
                }
                else
                {
                    lblError.Text = "Team is full";
                    lblError.Visible = true;
                }

            }



        }

        protected void rptPlayer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                ((DropDownList)e.Item.FindControl("ddlteam")).DataSource = _teams;
                ((DropDownList)e.Item.FindControl("ddlteam")).DataTextField = "NAME";
                ((DropDownList)e.Item.FindControl("ddlteam")).DataValueField = "ID";
                ((DropDownList)e.Item.FindControl("ddlteam")).DataBind();

            }
        }

        protected void rptTeams_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    var teamplacenumber = e.CommandArgument.ToString().Split('_');
                    string teamname = teamplacenumber[0].ToString().Trim();
                    int teammembernumber = int.Parse(teamplacenumber[1].ToString());

                    using (MESLEntities ttx = new MESLEntities())
                    {
                        var _t = (from t in ttx.TEAMs
                                  where t.NAME == teamname && t.DIVISION == _division
                                  select t).FirstOrDefault();

                        var _p = (from p in ttx.PLAYERs where p.ID == _t.PLAYER1 select p).First();

                        switch (teammembernumber)
                        {
                            case 1:
                                _p = (from p in ttx.PLAYERs where p.ID == _t.PLAYER1 select p).First();
                                _p.TEAMID = 1;
                                _t.PLAYER1 = null;
                                break;
                            case 2:
                                _p = (from p in ttx.PLAYERs where p.ID == _t.PLAYER2 select p).First();
                                _p.TEAMID = 1;
                                _t.PLAYER2 = null;
                                break;
                            case 3:
                                _p = (from p in ttx.PLAYERs where p.ID == _t.PLAYER3 select p).First();
                                _p.TEAMID = 1;
                                _t.PLAYER3 = null;
                                break;
                            case 4:
                                _p = (from p in ttx.PLAYERs where p.ID == _t.PLAYER4 select p).First();
                                _p.TEAMID = 1;
                                _t.PLAYER4 = null;
                                break;
                            case 5:
                                _p = (from p in ttx.PLAYERs where p.ID == _t.PLAYER5 select p).First();
                                _p.TEAMID = 1;
                                _t.PLAYER5 = null;
                                break;
                            case 6:
                                _p = (from p in ttx.PLAYERs where p.ID == _t.PLAYER6 select p).First();
                                _p.TEAMID = 1;
                                _t.PLAYER6 = null;
                                break;
                            case 7:
                                _p = (from p in ttx.PLAYERs where p.ID == _t.PLAYER7 select p).First();
                                _p.TEAMID = 1;
                                _t.PLAYER7 = null;
                                break;
                            case 8:
                                _p = (from p in ttx.PLAYERs where p.ID == _t.PLAYER8 select p).First();
                                _p.TEAMID = 1;
                                _t.PLAYER8 = null;
                                break;
                            case 9:
                                _p = (from p in ttx.PLAYERs where p.ID == _t.PLAYER9 select p).First();
                                _p.TEAMID = 1;
                                _t.PLAYER9 = null;
                                break;
                            case 10:
                                _p = (from p in ttx.PLAYERs where p.ID == _t.PLAYER10 select p).First();
                                _p.TEAMID = 1;
                                _t.PLAYER10 = null;
                                break;
                            case 11:
                                _p = (from p in ttx.PLAYERs where p.ID == _t.PLAYER11 select p).First();
                                _p.TEAMID = 1;
                                _t.PLAYER11 = null;
                                break;
                            case 12:
                                _p = (from p in ttx.PLAYERs where p.ID == _t.PLAYER12 select p).First();
                                _p.TEAMID = 1;
                                _t.PLAYER12 = null;
                                break;
                            case 13:
                                _p = (from p in ttx.PLAYERs where p.ID == _t.PLAYER13 select p).First();
                                _p.TEAMID = 1;
                                _t.PLAYER13 = null;
                                break;
                            case 14:
                                _p = (from p in ttx.PLAYERs where p.ID == _t.PLAYER14 select p).First();
                                _p.TEAMID = 1;
                                _t.PLAYER14 = null;
                                break;
                        }
                        ttx.SaveChanges();
                    }

                    break;
            }
            Response.Redirect("U12.aspx");
        }
    }
}