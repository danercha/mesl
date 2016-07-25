using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamBuilder.STATIC
{
    public class Class1
    {
    }

    public class Local_player
    {
        public int ID { get; set; }
        public string FNAME { get; set; }
        public string LNAME { get; set; }
        public DateTime DOB { get; set; }
        public double AGE { get; set; }
        public double AGESTART { get; set; }
        public bool GENDER { get; set; }
        public string NOTES { get; set; }
        public int SEASONSPLAYED { get; set; }
        public int TEAMID { get; set; }
    }
    public class Local_Team
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string COACH { get; set; }
        public string PLAYER1 { get; set; }
        public string PLAYER2 { get; set; }
        public string PLAYER3 { get; set; }
        public string PLAYER4 { get; set; }
        public string PLAYER5 { get; set; }
        public string PLAYER6 { get; set; }
        public double AGE { get; set; }
        public double GENDER { get; set; }
    }
}
