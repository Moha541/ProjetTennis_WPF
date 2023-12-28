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
            connectionString = ConfigurationManager.ConnectionStrings["TennisProjet"].ConnectionString;
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
        /*   public bool InsertSets(Sets p)
           {
               bool succes = false;

               using (SqlConnection connection = new SqlConnection(connectionString))
               {
                   SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Setss(Lastname) VALUES(@Lastname)", connection);
                   cmd.Parameters.AddWithValue("Lastname", p.Lastname);
                   connection.Open();
                   int res = cmd.ExecuteNonQuery();
                   succes = res > 0;
               }
               return succes;
           }*/
    }
}
