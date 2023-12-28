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
    internal class OpponentDAO
    {

        private string connectionString;
        public OpponentDAO()
        {
            connectionString = ConfigurationManager.ConnectionStrings["TennisProjet"].ConnectionString;
        }
        public List<Opponent> GetOpponents()
        {
            List<Opponent> Opponents = new List<Opponent>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * " +
                           "FROM Opponent " + connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Opponent Opponent = new Opponent();
                        
                    

                        Opponents.Add(Opponent);
                    }
                }
            }

            return Opponents;
        }
        /* public bool InsertOpponent(Opponent p)
         {
             bool succes = false;

             using (SqlConnection connection = new SqlConnection(connectionString))
             {
                 SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Opponents(Lastname) VALUES(@Lastname)", connection);
                 cmd.Parameters.AddWithValue("Lastname", p.Lastname);
                 connection.Open();
                 int res = cmd.ExecuteNonQuery();
                 succes = res > 0;
             }
             return succes;
         }
     }*/
    }
}
