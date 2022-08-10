using System;
using System.Collections.Generic;
using System.Linq;
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
        internal class Team
        {
            public int teamPosition { get; set; }
            public string teamName { get; set; }
            public int teamPoints { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            connectionToken auth_token = new connectionToken();
            Team[] team = new Team[1];
            team[0] = new Team() { teamPosition = 1, teamName = "Liverpoool", teamPoints = 100 };
            dgMain.ItemsSource = team;
            
        }
    }
}
