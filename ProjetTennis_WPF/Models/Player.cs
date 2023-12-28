using ProjetTennis.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTennis.Models
{
    public class Player : Person
    {
        public int Ranking { get; set; }
        public string Gender { get; set; }
        public Player()
        {
            
        }
        public static List<Player> GetPlayers()
        {
            PlayerDAO dao = new PlayerDAO();
            return dao.GetPlayers();
        }
        public Player(int PersonId , string firstname)
        {
            Id_Person = PersonId;
            Firstname = firstname;
        }
        public override string ToString()
        {
            return $"{this.Firstname} {this.Lastname}, {this.Nationality}";
        }
    }
}
