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
    internal class RefereeTournamentDAO
    {
        private string connectionString;
        public RefereeTournamentDAO()
        {
            connectionString = ConfigurationManager.ConnectionStrings["TennisProjet"].ConnectionString;
        }
        public List<RefereeTournament> GetRefereeTournaments()
        {
            List<RefereeTournament> RefereeTournaments = new List<RefereeTournament>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                SqlCommand cmd = new SqlCommand("SELECT * " +
                   "FROM RefereeTournament " , connection);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RefereeTournament RefereeTournament = new RefereeTournament();
                        RefereeTournament.Referee.Id_Person = reader.GetInt32("id_person");
                        RefereeTournament.Tournament.Id_Tournament = reader.GetInt32("Id_Tournament");
                        RefereeTournaments.Add(RefereeTournament);
                    }
                }
            }

            return RefereeTournaments;
        }
        /*   public bool InsertRefereeTournament(RefereeTournament p)
           {
               bool succes = false;

               using (SqlConnection connection = new SqlConnection(connectionString))
               {
                   SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.RefereeTournaments(Lastname) VALUES(@Lastname)", connection);
                   cmd.Parameters.AddWithValue("Lastname", p.Lastname);
                   connection.Open();
                   int res = cmd.ExecuteNonQuery();
                   succes = res > 0;
               }
               return succes;
           }*/
    }
}
