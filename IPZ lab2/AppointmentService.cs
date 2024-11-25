using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class AppointmentService
{
    private string connectionString = "your_connection_string_here";

    // Запис на процедуру
    public bool MakeAppointment(string username, string procedure, DateTime date)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Перевірка конфлікту запису
            string checkQuery = "SELECT COUNT(*) FROM Appointments WHERE ProcedureName = @Procedure AND AppointmentDate = @Date";
            using (SqlCommand command = new SqlCommand(checkQuery, connection))
            {
                command.Parameters.AddWithValue("@Procedure", procedure);
                command.Parameters.AddWithValue("@Date", date);
                int count = (int)command.ExecuteScalar();
                if (count > 0)
                {
                    return false; // Конфлікт
                }
            }

            // Додавання запису з отриманням AppointmentId
            string insertQuery = "INSERT INTO Appointments (UserName, ProcedureName, AppointmentDate) OUTPUT INSERTED.AppointmentId VALUES (@UserName, @Procedure, @Date)";
            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@UserName", username);
                command.Parameters.AddWithValue("@Procedure", procedure);
                command.Parameters.AddWithValue("@Date", date);

                // Отримуємо новий AppointmentId
                int newAppointmentId = (int)command.ExecuteScalar();

                // Створюємо об'єкт Appointment з отриманим AppointmentId
                Appointment newAppointment = new Appointment
                {
                    AppointmentId = newAppointmentId,
                    UserName = username,
                    ProcedureName = procedure,
                    AppointmentDate = date
                };

                return true; // Повертаємо true, якщо запис успішно доданий
            }
        }
    }
    public List<Appointment> GetAppointmentsByUserId(int userId)
    {
        List<Appointment> appointments = new List<Appointment>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string selectQuery = "SELECT AppointmentId, ProcedureName, AppointmentDate FROM Appointments WHERE UserId = @UserId";
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        appointments.Add(new Appointment
                        {
                            AppointmentId = reader.GetInt32(0),
                            UserId = userId,
                            ProcedureName = reader.GetString(1),
                            AppointmentDate = reader.GetDateTime(2)
                        });
                    }
                }
            }
        }

        return appointments;
    }


    // Отримання записів користувача
    public List<Appointment> GetAppointments(string username)
    {
        List<Appointment> appointments = new List<Appointment>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string selectQuery = "SELECT AppointmentId, ProcedureName, AppointmentDate FROM Appointments WHERE UserName = @UserName";
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@UserName", username);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        appointments.Add(new Appointment
                        {
                            AppointmentId = reader.GetInt32(0), // Отримуємо AppointmentId
                            UserName = username,
                            ProcedureName = reader.GetString(1),
                            AppointmentDate = reader.GetDateTime(2)
                        });
                    }
                }
            }
        }

        return appointments;
    }

    // Скасування запису
    public bool CancelAppointment(Appointment appointment)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string deleteQuery = "DELETE FROM Appointments WHERE AppointmentId = @AppointmentId";
            using (SqlCommand command = new SqlCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@AppointmentId", appointment.AppointmentId); // Використовуємо AppointmentId
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
