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
    internal class TournamentsDAO
    {
        private string connectionString;
        public TournamentsDAO()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Tonda_Mansour_Project"].ConnectionString;
        }
        public List<Tournament> GetTournaments()
        {
            List<Tournament> Tournaments = new List<Tournament>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                SqlCommand cmd = new SqlCommand("SELECT * " +
                   "FROM Tournament ", connection);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tournament Tournament = new Tournament();
                        Tournament.Id_Tournament = reader.GetInt32("Id_Tournament");
                        Tournament.Name = reader.GetString("Name");

                        Tournaments.Add(Tournament);
                    }
                }
            }

            return Tournaments;
        }
        /*   public bool InsertTournaments(Tournaments p)
           {
               bool succes = false;

               using (SqlConnection connection = new SqlConnection(connectionString))
               {
                   SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Tournamentss(Lastname) VALUES(@Lastname)", connection);
                   cmd.Parameters.AddWithValue("Lastname", p.Lastname);
                   connection.Open();
                   int res = cmd.ExecuteNonQuery();
                   succes = res > 0;
               }
               return succes;
           }*/
    }
    
}
