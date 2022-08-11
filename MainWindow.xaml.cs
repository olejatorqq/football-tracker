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
using DeserializeClass;
using Newtonsoft.Json;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        internal class connectionToken
        {
            public string token = "***";
        }
        internal class TeamList
        {
            public int teamPosition { get; set; }
            public string teamName { get; set; }
            public int teamPoints { get; set; }
            public int goalDifference { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MainWindow_LoadedAsync(object sender, RoutedEventArgs e)
        {
            connectionToken auth_token = new connectionToken();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.football-data.org/v4/competitions/PL/standings"),
                Headers =
                {
                    { "X-Auth-Token", auth_token.token },
                },
            };

            TeamList[] teamList = new TeamList[20];

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                DeserializeClass.Root stats = JsonConvert.DeserializeObject<DeserializeClass.Root>(body);


                foreach (var stand in stats.standings)
                {
                    foreach (var nameTeams in stand.table)
                    {
                        
                        teamList[nameTeams.position - 1] = new TeamList { teamPosition = nameTeams.position, teamName = nameTeams.team.shortName, teamPoints = nameTeams.points, goalDifference = nameTeams.goalDifference };
                        
                    }
                }

            }

            dgMain.ItemsSource = teamList;

            /*Team[] team = new Team[1];
            team[0] = new Team() { teamPosition = 1, teamName = "Liverpoool", teamPoints = 100 };
            dgMain.ItemsSource = team;

            DeserializeClass.Root statsTeams = new DeserializeClass.Root();*/



        }
    }

}
