using ProjetTennis.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
            connectionString = ConfigurationManager.ConnectionStrings["Tonda_Mansour_Project"].ConnectionString;
        }
        public List<Match> GetMatchs()
        {
            List<Match> Matchs = new List<Match>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * " +
                           "FROM matchs  " + connection);
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
        public bool InsertMatch(Match m)
         {
             bool succes = false;

             using (SqlConnection connection = new SqlConnection(connectionString))
             {
                 SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.matchs(date_match,duration,round,Id_Person,Id_Court,Id_Schedule,Id_Opponent1,Id_Opponent2) VALUES(@Date,@Duration,@Round,@Id_Person,@Id_Court,@Id_Schedule,@Id_Opponent1,@Id_Opponent2)", connection);
                cmd.Parameters.AddWithValue("Date", m.DateMatch);
                cmd.Parameters.AddWithValue("Duration", m.Duration);
                cmd.Parameters.AddWithValue("Round", m.Round);     
                cmd.Parameters.AddWithValue("Id_Schedule", m.Schedule.Id_Schedule);
                cmd.Parameters.AddWithValue("Id_Person", m.Referee.Id_Person);

                Court court=m.Court;
                cmd.Parameters.AddWithValue("Id_Court", court.Id_Court);
                Opponent op1 = m.Opponent1;
                Opponent op2 = m.Opponent2;
                cmd.Parameters.AddWithValue("Id_Opponent1", op1.Id_Opponent);
                cmd.Parameters.AddWithValue("Id_Opponent2", op2.Id_Opponent);
                connection.Open();
                 int res = cmd.ExecuteNonQuery();
                 succes = res > 0;
             }
             return succes;
         }
        public int GetIdMatch(Match m)
        {
            int lastInsertedMatchId = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT TOP 1 Id_Match FROM dbo.matchs ORDER BY Id_Match DESC";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lastInsertedMatchId = reader.GetInt32(0);
                        }
                    }
                }
            }

            return lastInsertedMatchId;
        }

        public void UpdateMatch(Match m)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE dbo.matchs SET date_match = @Date, duration = @Duration, round = @Round, Id_Person = @Id_Person, Id_Court = @Id_Court, Id_Schedule = @Id_Schedule WHERE Id_Match = @Id_Match", connection);
                cmd.Parameters.AddWithValue("Date", m.DateMatch);
                cmd.Parameters.AddWithValue("Duration", m.Duration);
                cmd.Parameters.AddWithValue("Round", m.Round);
                cmd.Parameters.AddWithValue("Id_Person", m.Referee.Id_Person);
                cmd.Parameters.AddWithValue("Id_Court", m.Court.Id_Court);
                cmd.Parameters.AddWithValue("Id_Schedule", m.Schedule.Id_Schedule);
                cmd.Parameters.AddWithValue("Id_Match", m.Id_Match);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
