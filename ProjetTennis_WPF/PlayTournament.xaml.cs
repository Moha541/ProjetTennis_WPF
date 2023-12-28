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

        public Schedule.ScheduleType TournamentType { get; set; }
        public ObservableCollection<Match> Matches { get; set; }

        private Schedule schedule;

        public PlayTournament(Schedule.ScheduleType tournamentType1)
        {
            InitializeComponent();
            TournamentType = tournamentType1; // On utilise la propriété de classe au lieu de déclarer une nouvelle variable locale
            InitializeParticipants();
            this.DataContext = this; // Définir le contexte de données
        }

        //private void InitializeParticipants()
        //{
        //    Matches = new ObservableCollection<Match>();
        //    Schedule schedule = new Schedule();
        //    schedule.CreateMatch(TournamentType);
        //    List<Match> matches = schedule.Matches;
        //    for (int i = 0; i < matches.Count(); i++)
        //    {
        //        Matches.Add(matches[i]);
        //    }
        //    ParticipantsList.ItemsSource = Matches;

        //}




        //------------------------------------------TEST---------------------------------------------------------------//

        private void InitializeParticipants()
        {
            Matches = new ObservableCollection<Match>();

            // Utilisez la variable de classe au lieu d'en déclarer une nouvelle
            schedule = new Schedule();
            schedule.Play(TournamentType);
            List<Match> matches = schedule.Matches;

            for (int i = 0; i < matches.Count(); i++)
            {
                Matches.Add(matches[i]);
            }
            ParticipantsList.ItemsSource = Matches;
        }

        public void PlayNextRound(object sender, RoutedEventArgs e)
        {
            schedule.Play(TournamentType); 
            UpdateMatchesList();

            if (schedule.ActualRound == 6)
            {
                ShowWinner();
                IdTxtButton.Visibility = Visibility.Hidden;
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



    }
}

