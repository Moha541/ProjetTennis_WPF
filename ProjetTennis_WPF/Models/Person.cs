using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTennis.Models
{
    public class Person
    {
        public int Id_Person { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nationality { get; set; }
        List<Referee> Referees { get; set; }
        List<Player> Players { get; set; }

       
    }
}
