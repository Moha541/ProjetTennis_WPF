using ProjetTennis.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTennis.DAO
{
    internal class SuperTieBreaksDAO
    {
        private string connectionString;
        public SuperTieBreaksDAO()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Tonda_Mansour_Project"].ConnectionString;
        }
        public List<SuperTieBreak> GetSuperTieBreaks()
        {
            List<SuperTieBreak> SuperTieBreaks = new List<SuperTieBreak>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                SqlCommand cmd = new SqlCommand("SELECT * " +
                   "FROM SuperTieBreaks ", connection);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SuperTieBreak SuperTieBreak = new SuperTieBreak();
                        SuperTieBreak.Id_Set= reader.GetInt32("Id_Set");
                   
                        SuperTieBreaks.Add(SuperTieBreak);
                    }
                }
            }

            return SuperTieBreaks;
        }
      
    }
}
