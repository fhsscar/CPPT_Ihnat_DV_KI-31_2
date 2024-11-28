using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

public class AppointmentService
{
    private string connectionString = "Server=LAPTOP-M19B2MQO\\SQLEXPRESS01;Database=BeautySalon;Trusted_Connection=True";

    // Запис на процедуру
    public async Task<bool> MakeAppointmentAsync(int userId, string username, string procedure, DateTime appointmentDate)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            // Перевірка на наявність конфлікту в записах
            string checkQuery = "SELECT COUNT(*) FROM Appointments WHERE ProcedureName = @Procedure AND AppointmentDate = @Date";
            using (SqlCommand command = new SqlCommand(checkQuery, connection))
            {
                command.Parameters.AddWithValue("@Procedure", procedure);
                command.Parameters.AddWithValue("@Date", appointmentDate);
                int count = (int)await command.ExecuteScalarAsync();
                if (count > 0)
                {
                    return false; // Конфлікт (ця процедура вже зареєстрована на цю дату)
                }
            }

            // Отримання ProcedureId за назвою процедури
            string getProcedureIdQuery = "SELECT ProcedureId FROM Procedures WHERE ProcedureName = @Procedure";
            int procedureId = 0; // Ідентифікатор процедури
            using (SqlCommand command = new SqlCommand(getProcedureIdQuery, connection))
            {
                command.Parameters.AddWithValue("@Procedure", procedure);
                object result = await command.ExecuteScalarAsync();
                if (result != null)
                {
                    procedureId = (int)result; // Присвоюємо отриманий ProcedureId
                }
                else
                {
                    return false; // Якщо процедура не знайдена
                }
            }

            // SQL запит на додавання запису в таблицю Appointments
            string insertQuery = "INSERT INTO Appointments (UserId, UserName, ProcedureName, ProcedureId, AppointmentDate) OUTPUT INSERTED.AppointmentId VALUES (@UserId, @UserName, @Procedure, @ProcedureId, @Date)";
            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                // Параметри запиту
                command.Parameters.AddWithValue("@UserId", userId); // UserId поточного користувача
                command.Parameters.AddWithValue("@UserName", username); // UserName поточного користувача
                command.Parameters.AddWithValue("@Procedure", procedure); // Назва процедури
                command.Parameters.AddWithValue("@ProcedureId", procedureId); // ProcedureId для процедури
                command.Parameters.AddWithValue("@Date", appointmentDate); // Повна дата (включаючи час)

                // Виконання запиту і отримання нової ID запису
                int newAppointmentId = (int)await command.ExecuteScalarAsync();
                return true; // Успішно додано запис
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
