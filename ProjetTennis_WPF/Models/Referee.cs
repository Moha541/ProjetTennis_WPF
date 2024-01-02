using ProjetTennis.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTennis.Models
{
    public class Referee : Person
    {
        //public List<Match> Matches { get; set; }
        public bool IsAvailable { get; set; }
        public bool Available()
        {
            return IsAvailable;
        }
        public static Queue<Referee> GetReferees()
        {
            RefereeDAO refereeDAO = new RefereeDAO();
            return refereeDAO.GetReferees();
        }

        public override string ToString()
        {
            return $"{this.Firstname} {this.Lastname}, {this.Nationality}";
        }
        public void Release()
        {
            IsAvailable = true;
            RefereeDAO refereeDAO = new RefereeDAO();
            refereeDAO.Update(this);
        }
    }

}


