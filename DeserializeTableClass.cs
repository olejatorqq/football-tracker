using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeserializeTableClass
{
    public class Root
    {
        public string comp_id { get; set; }
        public string season { get; set; }
        public string round { get; set; }
        public string stage_id { get; set; }
        public object comp_group { get; set; }
        public string country { get; set; }
        public string team_id { get; set; }
        public string team_name { get; set; }
        public string status { get; set; }
        public string recent_form { get; set; }
        public string position { get; set; }
        public string overall_gp { get; set; }
        public string overall_w { get; set; }
        public string overall_d { get; set; }
        public string overall_l { get; set; }
        public string overall_gs { get; set; }
        public string overall_ga { get; set; }
        public string home_gp { get; set; }
        public string home_w { get; set; }
        public string home_d { get; set; }
        public string home_l { get; set; }
        public string home_gs { get; set; }
        public string home_ga { get; set; }
        public string away_gp { get; set; }
        public string away_w { get; set; }
        public string away_d { get; set; }
        public string away_l { get; set; }
        public string away_gs { get; set; }
        public string away_ga { get; set; }
        public string gd { get; set; }
        public string points { get; set; }
        public string description { get; set; }
    }
}
