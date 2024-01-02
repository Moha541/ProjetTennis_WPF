using ProjetTennis.DAO;
using ProjetTennis_WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTennis.Models
{
     public class Tournament
    {
        public int Id_Tournament { get; set; }
        public string Name { get; set; }
        public List<Court> courts { get; set; }
        public List<Schedule> schedules { get; set; }
        public Queue<Referee> availableReferees = new Queue<Referee>();
        public Queue<Court> availableCourts = new Queue<Court>();

        public Tournament()
        {
            
        }
        public void Play()
        {
            availableCourts = new Queue<Court>(Court.GetCourts());
            availableReferees = new Queue<Referee>(Referee.GetReferees());
            TournamentsDAO tournamentsDAO = new TournamentsDAO();
            Tournament tournament = new Tournament();
            tournament = tournamentsDAO.GetTournaments()[0];
            this.Name = tournament.Name;
            this.Id_Tournament = tournament.Id_Tournament;
            TournamentType tournamentType = new TournamentType(this);
            tournamentType.Show();
        }
    }
}
