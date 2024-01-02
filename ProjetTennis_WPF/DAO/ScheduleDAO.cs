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
    internal class ScheduleDAO
    {

        private string connectionString;
        public ScheduleDAO()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Tonda_Mansour_Project"].ConnectionString;
        }
        public List<Schedule> GetSchedules()
        {
            List<Schedule> Schedules = new List<Schedule>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Id_Schedule,Type,ActualRound,Id_Tournament FROM Schedule", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Schedule Schedule = new Schedule();
                        Schedule.Tournament = new Tournament(); // Initialize Tournament before setting its property
                        Schedule.Id_Schedule = reader.GetInt32("Id_Schedule");
                        Schedule.Type = (Schedule.ScheduleType)reader.GetByte("Type");
                        Schedule.ActualRound = reader.GetInt32("ActualRound");
                        Schedule.Tournament.Id_Tournament = reader.GetInt32("Id_Tournament");
                        Schedules.Add(Schedule);
                    }
                }
            }

            return Schedules;
        }
        public bool InsertSchedule(Schedule s)
        {
            bool succes = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Schedule(Type,actualRound,Id_Tournament) VALUES(@Type,@actualRound,@Id_Tournament)", connection);
                cmd.Parameters.AddWithValue("Type", s.Type);
                cmd.Parameters.AddWithValue("actualRound", s.ActualRound);
                cmd.Parameters.AddWithValue("Id_Tournament", s.Tournament.Id_Tournament);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                succes = res > 0;
            }
            return succes;
        }

        public int GetScheduleId(Schedule s)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT Id_Schedule FROM dbo.Schedule WHERE Type = @Type AND ActualRound = @ActualRound AND Id_Tournament = @Id_Tournament", connection);
                cmd.Parameters.AddWithValue("Type", s.Type);
                cmd.Parameters.AddWithValue("ActualRound", s.ActualRound);
                cmd.Parameters.AddWithValue("Id_Tournament", s.Tournament.Id_Tournament);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32("Id_Schedule");
                    }
                }
            }
            return id;
        }
        public void UpdateSchedule(Schedule s)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE dbo.Schedule SET Type = @Type, ActualRound = @ActualRound, Id_Tournament = @Id_Tournament WHERE Id_Schedule = @Id_Schedule", connection);
                cmd.Parameters.AddWithValue("Type", s.Type);
                cmd.Parameters.AddWithValue("ActualRound", s.ActualRound);
                cmd.Parameters.AddWithValue("Id_Tournament", s.Tournament.Id_Tournament);
                cmd.Parameters.AddWithValue("Id_Schedule", s.Id_Schedule);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
