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
    internal class SetsDAO
    {
        private string connectionString;
        public SetsDAO()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Tonda_Mansour_Project"].ConnectionString;
        }
        public List<Sets> GetSets()
        {
            List<Sets> Sets = new List<Sets>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                SqlCommand cmd = new SqlCommand("SELECT * " +
                   "FROM Sets " , connection);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Sets Set = new Sets();
                        Set.Id_Set = reader.GetInt32("id_set");
                        Set.ScoreOp1 = reader.GetInt32("ScoreOp1");
                        Set.ScoreOp2 = reader.GetInt32("ScoreOp2");
                        Set.Match.Id_Match = reader.GetInt32("Id_Match");
                        Sets.Add(Set);
                    }
                }
            }

            return Sets;
        }
          public bool InsertSets(Sets s)
           {
               bool succes = false;

               using (SqlConnection connection = new SqlConnection(connectionString))
               {
                   SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Sets(ScoreOp1,ScoreOp2,winner,Id_Match) VALUES(@ScoreOp1,@ScoreOp2,@Winner,@Id_Match)", connection);
                   cmd.Parameters.AddWithValue("ScoreOp1", s.ScoreOp1); 
                   cmd.Parameters.AddWithValue("ScoreOp2", s.ScoreOp2);
                   cmd.Parameters.AddWithValue("Winner", s.WinnerOpponent.Id_Opponent);
                   cmd.Parameters.AddWithValue("Id_Match", s.Match.Id_Match);

                connection.Open();
                   int res = cmd.ExecuteNonQuery();
                   succes = res > 0;
               }
               return succes;
           }

        public int GetIdSet(Sets s)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT Id_Set FROM Sets WHERE Id_Set = @Id_Set", connection);
                cmd.Parameters.AddWithValue("Id_Set", s.Id_Set);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32("Id_Set");
                    }
                }
            }
            return id;
        }
    }
}
