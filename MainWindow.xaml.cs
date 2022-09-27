using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DeserializeTableClass;
using Newtonsoft.Json;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public static class connectionToken
        {
            public static string token = "Paste your Api key here"
        }
        internal class TeamList
        {
            public string teamPosition { get; set; }
            public string teamName { get; set; }
            public string matchPlayed { get; set; }
            public string winMatches { get; set; }
            
            public string drawMatches { get; set; }
            public string loseMatches { get; set; }
            public string teamPoints { get; set; }
            public string formTeams { get; set; }
        }
        internal class SheduleList
        {
            public string matchTime { get; set; }
            public string team1SheduleName { get; set; }
            public string matchScore { get; set; }
            public string team2SheduleName { get; set;}
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        

        private async void MainWindow_LoadedAsync(object sender, RoutedEventArgs e)
        {

            /// <summary>
            /// Логика работы таблицы Апл
            /// </summary>

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://data.football-api.com/v3/standings/1204?Authorization={connectionToken.token}"),
            };

            TeamList[] teamList = new TeamList[20];

            using (var response = await client.SendAsync(request))
            {
                
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                List<DeserializeTableClass.Root> table = JsonConvert.DeserializeObject<List<DeserializeTableClass.Root>>(body);
                
                foreach (var nameTeam in table)
                {
                    teamList[int.Parse(nameTeam.position) - 1] = new TeamList
                    {
                        teamPosition = nameTeam.position,
                        teamName = nameTeam.team_name,
                        matchPlayed = nameTeam.round,
                        winMatches = nameTeam.overall_w,
                        drawMatches = nameTeam.overall_d,
                        loseMatches = nameTeam.overall_l,
                        teamPoints = nameTeam.points,
                        formTeams = nameTeam.recent_form
                    };
                }

                 


                //MessageBox.Show(table.team_name, "zxc");
            }

            dgMain.ItemsSource = teamList;




        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime thisDay = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(7);
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://data.football-api.com/v3/matches?comp_id=1204&from_date={thisDay.ToString("d")}&to_date={endDate.ToString("d")}&Authorization={connectionToken.token}"),
            };
            
            SheduleList[] sheduleList = new SheduleList[10];


            using (var response = await client.SendAsync(request))
            {

                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                List<DeserializeSheduleClass.Root> tableShedule = JsonConvert.DeserializeObject<List<DeserializeSheduleClass.Root>>(body);

                string score = null;
                int i = 0;


                foreach (var shedule in tableShedule)
                {
                    if (shedule.localteam_score == "?" && shedule.visitorteam_score == "?")
                    {
                        score = "    0:0";
                    }
                    else
                    {
                        score = shedule.localteam_score + " " + shedule.visitorteam_score;
                    }
                    sheduleList[i] = new SheduleList { matchTime = shedule.time + " " + shedule.formatted_date, team1SheduleName = shedule.localteam_name, matchScore = score , team2SheduleName = shedule.visitorteam_name};
                    i++;
                }

                /*for (int i = 0; i < sheduleList.Length; i++)
                {
                    foreach (var team in tableShedule)
                    {
                        if (team.localteam_score == null && team.visitorteam_score == null)
                        {
                            score = "0:0";
                        }
                        else
                        {
                            score = team.localteam_score + " " + team.visitorteam_score;     
                        }
                        sheduleList[i] = new SheduleList { matchTime = team.time + " " + team.formatted_date, team1SheduleName = team.localteam_name, matchScore = score };
                        break;
                    }
                }*/


                dgMain_Shedule.ItemsSource = sheduleList;

                //MessageBox.Show(table.team_name, "zxc");
            }

        }
    }

}
