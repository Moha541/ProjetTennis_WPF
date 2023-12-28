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
    internal class MatchDAO
    {

        private string connectionString;
        public MatchDAO()
        {
            connectionString = ConfigurationManager.ConnectionStrings["TennisProjet"].ConnectionString;
        }
        public List<Match> GetMatchs()
        {
            List<Match> Matchs = new List<Match>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * " +
                           "FROM Matchs  " + connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Match Match = new Match();
                        Match.Id_Match = reader.GetInt32("Id_match");
                        Match.DateMatch = reader.GetDateTime("date_match");
                        Match.Duration = reader.GetTimeSpan(reader.GetOrdinal("duration"));
                        Match.Round = reader.GetInt32("round");
                        Match.Referee.Id_Person = reader.GetInt32("id_person");
                        Match.Court.Id_Court = reader.GetInt32("id_court");
                        Match.Schedule.Id_Schedule = reader.GetInt32("id_schedule");

                        Matchs.Add(Match);
                    }
                }
            }

            return Matchs;
        }
        /* public bool InsertMatch(Match p)
         {
             bool succes = false;

             using (SqlConnection connection = new SqlConnection(connectionString))
             {
                 SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Matchs(Lastname) VALUES(@Lastname)", connection);
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
