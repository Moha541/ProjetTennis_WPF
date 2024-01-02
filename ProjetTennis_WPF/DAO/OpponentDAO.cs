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
            connectionString = ConfigurationManager.ConnectionStrings["Tonda_Mansour_Project"].ConnectionString;
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
                        Opponent.Player1.Id_Person = reader.GetInt32("Id_Person1");
                        Opponent.Player2.Id_Person = reader.GetInt32("Id_Person2");



                        Opponents.Add(Opponent);
                    }
                }
            }

            return Opponents;
        }
        public bool InsertOpponentSingle(Opponent p)
        {
             bool succes = false;

             using (SqlConnection connection = new SqlConnection(connectionString))
            {
                 SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Opponent(Id_Person1) VALUES(@Id_Person1)", connection);
                 cmd.Parameters.AddWithValue("Id_Person1", p.Player1.Id_Person);
                connection.Open();
                 int res = cmd.ExecuteNonQuery();
                 succes = res > 0;
             }
             return succes;
         }
         public bool InsertOpponentDouble(Opponent p)
         {
             bool succes = false;

             using (SqlConnection connection = new SqlConnection(connectionString))
             {
                 SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Opponent(Id_Person1,Id_Person2) VALUES(@Id_Person1,@Id_Person2)", connection);
                 cmd.Parameters.AddWithValue("Id_Person1", p.Player1.Id_Person);
                cmd.Parameters.AddWithValue("Id_Person2", p.Player2.Id_Person);
                connection.Open();
                 int res = cmd.ExecuteNonQuery();
                 succes = res > 0;
             }
             return succes;
         }
        public int GetIdOpponentSingle(Opponent p)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT Id_Opponent FROM dbo.Opponent WHERE Id_Person1 = @Id_Person1", connection);
                cmd.Parameters.AddWithValue("Id_Person1", p.Player1.Id_Person);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32("Id_Opponent");
                    }
                }
            }
            return id;
        }
        public int GetIdOpponentDouble(Opponent p)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT Id_Opponent FROM dbo.Opponent WHERE Id_Person1 = @Id_Person1 AND Id_Person2 = @Id_Person2", connection);
                cmd.Parameters.AddWithValue("Id_Person1", p.Player1.Id_Person);
                cmd.Parameters.AddWithValue("Id_Person2", p.Player2.Id_Person);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32("Id_Opponent");
                    }
                }
            }
            return id;
        }
    }
    
}
