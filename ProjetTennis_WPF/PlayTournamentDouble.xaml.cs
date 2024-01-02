using ProjetTennis.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace ProjetTennis_WPF
{
    /// <summary>
    /// Logique d'interaction pour PlayTournament.xaml
    /// </summary>
    public partial class PlayTournamentDouble : Window
    {
        public ObservableCollection<Match> Matches { get; set; }

        private Schedule schedule;
        public PlayTournamentDouble(Schedule schedule1)
        {
            InitializeComponent();
            //schedule = schedule1;
            schedule = schedule1;

            InitializeParticipants();
            this.DataContext = this;//définir le contexte de données
            Closing += MainWindow_Closing;
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            Application.Current.Shutdown();//Stop le programme quand on appuie sur le X 
        }

        private void InitializeParticipants()
        {
            Matches = new ObservableCollection<Match>();

            // Utilisez la variable de classe au lieu d'en déclarer une nouvelle
            schedule.Play(schedule);
            List<Match> matches = schedule.Matches;

            for (int i = 0; i < matches.Count(); i++)
            {
                Matches.Add(matches[i]);
            }
            ParticipantsList.ItemsSource = Matches;
        }

        public void PlayNextRound(object sender, RoutedEventArgs e)
        {
            schedule.Play(schedule); // Appeler schedule.Play() au lieu de PlayNextRound()
            UpdateMatchesList();

            if (schedule.ActualRound == 6)
            {
                ShowWinner();
                IdTxtButton.Visibility = Visibility.Hidden;
                HomeBtn.Visibility = Visibility.Visible;
            }
        }

        private void ShowWinner()
        {
            List<Opponent> winners = schedule.GetWinners();

            if (winners.Count > 0)
            {
                string winnerName1 = winners[0].Player1.Lastname;
                string winnerName2 = winners[0].Player2.Lastname;

                // Ajoutez ici le code pour afficher le vainqueur dans votre interface utilisateur
                WinnerTextBlock.Text = $"Les vainqueurs du tournoi sont {winnerName1} et {winnerName2} !";
                WinnerTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void UpdateMatchesList()
        {
            Matches.Clear();

            foreach (var match in schedule.Matches)
            {
                Matches.Add(match);
            }
        }
        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }
    }
}

