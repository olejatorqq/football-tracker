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
        public static class Tokens
        {
            public static string tokenAPI = "Paste your Api key here";
            public static int tokenEPL = 1204;
            public static int tokenLaLiga = 1399;
            public static int tokenSerieA = 1269;
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


        /// <summary>
        /// Логика работы таблицы Апл
        /// </summary>
        private async void StandingsLeague(int id)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://data.football-api.com/v3/standings/{id}?Authorization={Tokens.tokenAPI}"),
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
                MessageBox.Show(teamList.Length.ToString());


            }
            
            dgMain.ItemsSource = teamList;


           

            /*foreach (TeamList team in dgMain.ItemsSource)
            {
                var row = dgMain.ItemContainerGenerator.ContainerFromItem(team) as DataGridRow;
                if (team.teamPosition == "1")
                {
                    MessageBox.Show(row.ToString());
                    row.Background = Brushes.Green;
                }
            }*/



        }







        private async void MatchesFromDate(int id)
        {

            /// <summary>
            /// Логика работы расписания матчей
            /// </summary>

            DateTime yesterday = DateTime.Today.AddDays(-1);
            DateTime endDate = DateTime.Today.AddDays(7);
            var client1 = new HttpClient();
            var request1 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://data.football-api.com/v3/matches?comp_id={id}&from_date={yesterday.ToString("d")}&to_date={endDate.ToString("d")}&Authorization={Tokens.tokenAPI}"),
            };
            int count = 0;
            

            using (var response = await client1.SendAsync(request1))
            {

                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                List<DeserializeSheduleClass.Root> tableShedule = JsonConvert.DeserializeObject<List<DeserializeSheduleClass.Root>>(body);

                string score = null;
                int i = 0;

                foreach (var item in tableShedule)
                {
                    foreach (var item1 in item.localteam_name)
                    {
                        count++;
                    }
                }

                SheduleList[] sheduleList = new SheduleList[count];

                foreach (var shedule in tableShedule)
                {

                    if (shedule.localteam_score != "?" && shedule.visitorteam_score != "?")
                    {
                        score = shedule.localteam_score + " " + shedule.visitorteam_score;
                    }

                    sheduleList[i] = new SheduleList { matchTime = shedule.time + " " + shedule.formatted_date, team1SheduleName = shedule.localteam_name, matchScore = score, team2SheduleName = shedule.visitorteam_name };
                    i++;
                }
                //Need to fix bug with error in shedule
                dgMain_Shedule.ItemsSource = sheduleList;



            }
        }

        private void MainWindow_LoadedAsync(object sender, RoutedEventArgs e)
        {

            StandingsLeague(Tokens.tokenEPL);
            MatchesFromDate(Tokens.tokenEPL);
            LeagueLogo.Source = new BitmapImage(new Uri(@"/Resources/premier-league-logo.png", UriKind.Relative));
        }

        private void LaLigueButton_Click(object sender, RoutedEventArgs e)
        {
            StandingsLeague(Tokens.tokenLaLiga);
            MatchesFromDate(Tokens.tokenLaLiga);
            LeagueLogo.Source = new BitmapImage(new Uri(@"/Resources/la-liga-logo.png", UriKind.Relative));
        }

        private void SerieAButton_Click(object sender, RoutedEventArgs e)
        {
            StandingsLeague(Tokens.tokenSerieA);
            MatchesFromDate(Tokens.tokenSerieA);
            
            LeagueLogo.Source = new BitmapImage(new Uri(@"/Resources/serie-a-logo.png", UriKind.Relative));
        }

    }
}
