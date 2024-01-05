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
    
        public bool IsAvailable { get; set; }
       
        public static Queue<Referee> GetReferees()
        {
            RefereeDAO refereeDAO = new RefereeDAO();
            return refereeDAO.GetReferees();
        }

     
        public void Release()
        {
            IsAvailable = true;
            RefereeDAO refereeDAO = new RefereeDAO();
            refereeDAO.Update(this);
        }
    }

}


