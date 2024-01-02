using ProjetTennis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static ProjetTennis.Models.Schedule;

namespace ProjetTennis_WPF
{
    /// <summary>
    /// Logique d'interaction pour TournamentType.xaml
    /// </summary>
    public partial class TournamentType : Window
    {
        Schedule schedule = new Schedule();
        public TournamentType(Tournament tournament)
        {
            InitializeComponent();
            schedule.Tournament = tournament;
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            Application.Current.Shutdown();//Stop le programme quand on appuie sur le X 
        }
        private void ChoiceType(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            Enum.TryParse(clickedButton.Content.ToString(), out Schedule.ScheduleType result);//le result va prendre la valeur du contenu du boutton 
            
            schedule.Type = result;
            

          
            

            if (schedule.Type == ScheduleType.GentlemanSingle || schedule.Type == ScheduleType.LadiesSingle)
            {
                //ici on envoi bien le bon schedule type
                PlayTournament playTournament = new PlayTournament(schedule);
                playTournament.Show();
                this.Hide();
                
            }
            else if (schedule.Type == ScheduleType.LadiesDouble || schedule.Type == ScheduleType.GentlemanDouble || schedule.Type == ScheduleType.MixedDouble)
            {
                //ici on envoi bien le bon schedule type
                PlayTournamentDouble playTournamentDouble = new PlayTournamentDouble(schedule);
                playTournamentDouble.Show();
                this.Hide();
            }

       


        }


    }
}
