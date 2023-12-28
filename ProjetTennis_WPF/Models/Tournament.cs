using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTennis.Models
{
     public class Tournament
    {
        public int Id_Tournament { get; set; }
        public string Name { get; set; }
        public List<Court> courts { get; set; }
        public List<RefereeTournament> referees { get; set; }
        public List<Schedule> schedules { get; set; }
        public void Play()
        {
            Name = "Wimbledon";
            Schedule schedule = new Schedule { Tournament=this};
            //schedule.Play();
        }
    }
}
