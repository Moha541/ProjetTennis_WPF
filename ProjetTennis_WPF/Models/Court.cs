using ProjetTennis.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTennis.Models
{
    public class Court
    {
        public int Id_Court { get; set; }
        public int NbSpectators { get; set; }
        public bool Covered { get; set; }
        public bool IsAvailable { get; set; }
        
        public void Release()
        {
            IsAvailable = true;
            CourtDAO courtDAO = new CourtDAO();
            courtDAO.Update(this);
        }
        public static List<Court> GetCourts()
        {
            CourtDAO courtDAO = new CourtDAO();
            return courtDAO.GetCourts();
        }
        public Tournament Tournament { get; set; }
        public List<Match> Matches { get; set; }


    }
}
