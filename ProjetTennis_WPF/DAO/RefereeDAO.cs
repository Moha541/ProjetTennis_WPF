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
    internal class RefereeDAO
    {
        private string connectionString;
        public RefereeDAO()
        {
            connectionString = ConfigurationManager.ConnectionStrings["TennisProjet"].ConnectionString;
        }
        public List<Referee> GetReferees()
        {
            List<Referee> Referees = new List<Referee>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
             

                SqlCommand cmd = new SqlCommand("SELECT P.Id_Person, P.firstname, P.lastname, P.nationality, R.Available " +
                   "FROM Person P " + "JOIN Referee R ON P.Id_Person = R.Id_Person", connection);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Referee referee = new Referee();
                        referee.Id_Person = reader.GetInt32("id_person");
                        referee.Firstname = reader.GetString("Firstname");
                        referee.Lastname = reader.GetString("Lastname");
                        referee.Nationality = reader.GetString("Nationality");
                        referee.IsAvailable = reader.GetBoolean("Available");
                        Referees.Add(referee);
                        
                    }
                }
            }

            return Referees;
        }
        public void Update(Referee referee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                SqlCommand cmd = new SqlCommand("UPDATE referee set available=@Available "
                   + "where id_person=@RefereeId", connection);

                connection.Open();
                using (SqlCommand updateCmd = cmd)
                {
                    // Ajouter les paramètres
                    updateCmd.Parameters.AddWithValue("@Available", referee.IsAvailable);
                    updateCmd.Parameters.AddWithValue("@RefereeId", referee.Id_Person);

                    // Exécuter la commande UPDATE
                    int rowsAffected = updateCmd.ExecuteNonQuery();

                    // Vérifier si la mise à jour a réussi
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Mise à jour réussie.");
                    }
                    else
                    {
                        Console.WriteLine("Échec de la mise à jour. Aucune ligne affectée.");
                    }
                }
            }
           
        }
     
    }
}
