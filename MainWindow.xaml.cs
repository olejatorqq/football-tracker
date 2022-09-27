using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
            public static string token = "Paste your token here";
        }
        internal class TeamList
        {
            public int teamPosition { get; set; }
            public string teamName { get; set; }
            public int matchPlayed { get; set; }
            public int winMatches { get; set; }
            
            public int drawMatches { get; set; }
            public int loseMatches { get; set; }
            public int teamPoints { get; set; }
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
                Console.WriteLine(body);
            }


            /*using (var response = await client.SendAsync(requestStanding))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                DeserializeTableClass.Root stats = JsonConvert.DeserializeObject<DeserializeTableClass.Root>(body);

                foreach (var stand in stats.standings)
                {
                    foreach (var nameTeams in stand.table)
                    {
                        teamList[nameTeams.position - 1] = new TeamList { teamPosition = nameTeams.position, teamName = nameTeams.team.shortName, matchPlayed=nameTeams.playedGames, winMatches=nameTeams.won, 
                            drawMatches=nameTeams.draw, loseMatches=nameTeams.lost, teamPoints = nameTeams.points, formTeams = nameTeams.form };
                    }
                }

            }*/

            /// <summary>
            /// Логика работы таблицы с расписанием Апл
            /// </summary>
            /// 

            //dgMain.ItemsSource = teamList;


        }

    }

}
