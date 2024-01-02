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
    public partial class PlayTournament : Window
    {

        public ObservableCollection<Match> Matches { get; set; }

        private Schedule schedule;

        public PlayTournament(Schedule schedule1)
        {
            InitializeComponent();
            schedule = schedule1; // On utilise la propriété de classe au lieu de déclarer une nouvelle variable locale
            InitializeParticipants();
            this.DataContext = this; // Définir le contexte de données
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
            schedule.Play(schedule); 
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
                string winnerName = winners[0].Player1.Lastname;

                WinnerTextBlock.Text = $"Le vainqueur du tournoi est {winnerName} !";
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

