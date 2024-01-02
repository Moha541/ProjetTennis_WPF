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
    internal class CourtDAO
    {

        private string connectionString;
        public CourtDAO()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Tonda_Mansour_Project"].ConnectionString;
        }
        public List<Court> GetCourts()
        {
            List<Court> Courts = new List<Court>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Id_Court,nbSpectators,covered,Id_Tournament,Available FROM Court", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Court Court = new Court();
                        Court.Tournament = new Tournament(); // Initialize Tournament before setting its property
                        Court.Id_Court = reader.GetInt32("Id_Court");
                        Court.NbSpectators = reader.GetInt32("nbSpectators");
                        Court.Covered = reader.GetBoolean("covered");
                        Court.Tournament.Id_Tournament = reader.GetInt32("Id_Tournament");
                        Court.IsAvailable = reader.GetBoolean("Available");
                        Courts.Add(Court);
                    }
                }
            }

            return Courts;
        }
        public void Update(Court court)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                SqlCommand cmd = new SqlCommand("UPDATE court set nbSpectators=@nbSpectators, covered=@covered, available=@Available "
                   + "where id_court=@CourtId", connection);

                connection.Open();
                using (SqlCommand updateCmd = cmd)
                {
                    // Ajouter les paramètres
                    updateCmd.Parameters.AddWithValue("@covered", court.Covered);
                    updateCmd.Parameters.AddWithValue("@nbSpectators", court.NbSpectators);
                    updateCmd.Parameters.AddWithValue("@Available", court.IsAvailable);
                    updateCmd.Parameters.AddWithValue("@CourtId", court.Id_Court);

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
