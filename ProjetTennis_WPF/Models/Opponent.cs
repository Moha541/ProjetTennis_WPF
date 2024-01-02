using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTennis.Models
{
    public class Opponent
    {
        public int Id_Opponent { get; set; }
        public Player Player1 { get; set; }
        public Player? Player2 { get; set; }

    }
}
