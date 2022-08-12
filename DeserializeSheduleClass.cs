using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeserializeSheduleClass
{
    public class Area
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string flag { get; set; }
    }

    public class AwayTeam
    {
        public int id { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string tla { get; set; }
        public string crest { get; set; }
    }

    public class Competition
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string type { get; set; }
        public string emblem { get; set; }
    }

    public class Filters
    {
        public string season { get; set; }
    }

    public class FullTime
    {
        public int? home { get; set; }
        public int? away { get; set; }
    }

    public class HalfTime
    {
        public int? home { get; set; }
        public int? away { get; set; }
    }

    public class HomeTeam
    {
        public int id { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string tla { get; set; }
        public string crest { get; set; }
    }

    public class Match
    {
        public Area area { get; set; }
        public Competition competition { get; set; }
        public Season season { get; set; }
        public int id { get; set; }
        public DateTime utcDate { get; set; }
        public string status { get; set; }
        public int matchday { get; set; }
        public string stage { get; set; }
        public object group { get; set; }
        public DateTime lastUpdated { get; set; }
        public HomeTeam homeTeam { get; set; }
        public AwayTeam awayTeam { get; set; }
        public Score score { get; set; }
        public Odds odds { get; set; }
        public List<Referee> referees { get; set; }
    }

    public class Odds
    {
        public string msg { get; set; }
    }

    public class Referee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string nationality { get; set; }
    }

    public class ResultSet
    {
        public int count { get; set; }
        public string first { get; set; }
        public string last { get; set; }
        public int played { get; set; }
    }

    public class Root
    {
        public Filters filters { get; set; }
        public ResultSet resultSet { get; set; }
        public Competition competition { get; set; }
        public List<Match> matches { get; set; }
    }

    public class Score
    {
        public string winner { get; set; }
        public string duration { get; set; }
        public FullTime fullTime { get; set; }
        public HalfTime halfTime { get; set; }
    }

    public class Season
    {
        public int id { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int currentMatchday { get; set; }
        public object winner { get; set; }
    }
}
