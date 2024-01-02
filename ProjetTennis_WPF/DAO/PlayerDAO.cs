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
    internal class PlayerDAO
    {
        private string connectionString;
        public PlayerDAO()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Tonda_Mansour_Project"].ConnectionString;
        }

        public List<Player> GetPlayers()
        {
            List<Player> Players = new List<Player>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT P.Id_Person, P.firstname, P.lastname,P.nationality, PL.ranking, PL.gender " +
                           "FROM Person P " +
                           "JOIN Player PL ON P.Id_Person = PL.Id_Person", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Player player = new Player();
                        player.Id_Person = reader.GetInt32("id_person");
                        player.Gender = reader.GetString("Gender");
                        player.Ranking = reader.GetInt32("Ranking");
                        player.Firstname = reader.GetString("Firstname");
                        player.Lastname = reader.GetString("Lastname");
                        player.Nationality = reader.GetString("Nationality");
                        Players.Add(player);
                    }
                }
            }

            return Players;
        }


        public List<Player> GetPlayers128Male()
        {
            List<Player> Players = new List<Player>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP (128) P.Id_Person, P.firstname, P.lastname,P.nationality, PL.ranking, PL.gender " +
                    "FROM Person P " +
                    "JOIN Player PL ON P.Id_Person = PL.Id_Person  "+
                    "where gender = 'Male'", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Player player = new Player();
                        player.Id_Person = reader.GetInt32("id_person");
                        player.Gender = reader.GetString("Gender");
                        player.Ranking = reader.GetInt32("Ranking");
                        player.Firstname = reader.GetString("Firstname");
                        player.Lastname = reader.GetString("Lastname");
                        player.Nationality = reader.GetString("Nationality");
                        Players.Add(player);
                    }
                }
            }

            return Players;
        }


        public List<Player> GetPlayers128Female()
        {
            List<Player> Players = new List<Player>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP (128) P.Id_Person, P.firstname, P.lastname,P.nationality, PL.ranking, PL.gender " +
                    "FROM Person P " +
                    "JOIN Player PL ON P.Id_Person = PL.Id_Person " +
                    "where gender = 'Female'", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Player player = new Player();
                        player.Id_Person = reader.GetInt32("id_person");
                        player.Gender = reader.GetString("Gender");
                        player.Ranking = reader.GetInt32("Ranking");
                        player.Firstname = reader.GetString("Firstname");
                        player.Lastname = reader.GetString("Lastname");
                        player.Nationality = reader.GetString("Nationality");
                        Players.Add(player);
                    }
                }
            }

            return Players;
        }

        public List<Player> GetPlayers128Mixed()
        {
            List<Player> Players = new List<Player>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP (64) P.Id_Person, P.firstname, P.lastname,P.nationality, PL.ranking, PL.gender " +
                    "FROM Person P " +
                    "JOIN Player PL ON P.Id_Person = PL.Id_Person " +
                    "where gender = 'Male' "+
                    "UNION "+
                    "SELECT TOP (64) P.Id_Person, P.firstname, P.lastname,P.nationality, PL.ranking, PL.gender " +
                    "FROM Person P " +
                    "JOIN Player PL ON P.Id_Person = PL.Id_Person " +
                    "where gender = 'Female'"
                    , connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Player player = new Player();
                        player.Id_Person = reader.GetInt32("id_person");
                        player.Gender = reader.GetString("Gender");
                        player.Ranking = reader.GetInt32("Ranking");
                        player.Firstname = reader.GetString("Firstname");
                        player.Lastname = reader.GetString("Lastname");
                        player.Nationality = reader.GetString("Nationality");
                        Players.Add(player);
                    }
                }
            }

            return Players;
        }


        public List<Player> GetPlayers64Male()
        {
            List<Player> Players = new List<Player>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP (64) P.Id_Person, P.firstname, P.lastname,P.nationality, PL.ranking, PL.gender " +
                    "FROM Person P " +
                    "JOIN Player PL ON P.Id_Person = PL.Id_Person " +
                    "where gender = 'Male'", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Player player = new Player();
                        player.Id_Person = reader.GetInt32("id_person");
                        player.Gender = reader.GetString("Gender");
                        player.Ranking = reader.GetInt32("Ranking");
                        player.Firstname = reader.GetString("Firstname");
                        player.Lastname = reader.GetString("Lastname");
                        player.Nationality = reader.GetString("Nationality");
                        Players.Add(player);
                    }
                }
            }

            return Players;
        }


        public List<Player> GetPlayers64Female()
        {
            List<Player> Players = new List<Player>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP (64) P.Id_Person, P.firstname, P.lastname,P.nationality, PL.ranking, PL.gender " +
                    "FROM Person P " +
                    "JOIN Player PL ON P.Id_Person = PL.Id_Person " +
                    "where gender = 'Female'", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Player player = new Player();
                        player.Id_Person = reader.GetInt32("id_person");
                        player.Gender = reader.GetString("Gender");
                        player.Ranking = reader.GetInt32("Ranking");
                        player.Firstname = reader.GetString("Firstname");
                        player.Lastname = reader.GetString("Lastname");
                        player.Nationality = reader.GetString("Nationality");
                        Players.Add(player);
                    }
                }
            }

            return Players;
        }


        public bool InsertPlayer(Player p)
        {
            bool succes = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Players(Lastname) VALUES(@Lastname)", connection);
                cmd.Parameters.AddWithValue("Lastname", p.Lastname);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                succes = res > 0;
            }
            return succes;
        }
    }
}
