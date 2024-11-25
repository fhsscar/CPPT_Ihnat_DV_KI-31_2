using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BeautySalonServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 1234;
            IPAddress ipAddress = IPAddress.Parse("127.1.0.1");

            TcpListener server = new TcpListener(ipAddress, port);
            server.Start();
            Console.WriteLine($"Сервер запущено на IP {ipAddress} і порті {port}");

            while (true)
            {
                Console.WriteLine("Очікування підключення...");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Підключення прийнято.");

                Task.Run(() => HandleClient(client));
            }
        }

        private static void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string clientMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Console.WriteLine($"Повідомлення від клієнта: {clientMessage}");
            string[] messageParts = clientMessage.Split(',');

            switch (messageParts[0])
            {
                case "REGISTER":
                    HandleRegister(messageParts, stream);
                    break;
                case "ADD_PROCEDURE":
                    HandleAddProcedure(messageParts, stream);
                    break;
                case "VIEW_PROCEDURES":
                    HandleViewProcedures(messageParts, stream);
                    break;
                case "DELETE_PROCEDURE":
                    HandleDeleteProcedure(messageParts, stream);
                    break;
                default:
                    SendResponseToClient(stream, "Невідома команда.");
                    break;
            }

            client.Close();
            Console.WriteLine("З'єднання закрито.");
        }

        private static void HandleRegister(string[] messageParts, NetworkStream stream)
        {
            if (messageParts.Length == 4)
            {
                string email = messageParts[1];
                string username = messageParts[2];
                string password = messageParts[3];

                if (RegisterUser(email, username, password))
                {
                    SendResponseToClient(stream, "Реєстрація успішна.");
                }
                else
                {
                    SendResponseToClient(stream, "Помилка реєстрації.");
                }
            }
            else
            {
                SendResponseToClient(stream, "Невірний формат даних для реєстрації.");
            }
        }

        private static void HandleAddProcedure(string[] messageParts, NetworkStream stream)
        {
            if (messageParts.Length == 4)
            {
                string username = messageParts[1];
                string procedureName = messageParts[2];
                string time = messageParts[3];

                if (AddProcedure(username, procedureName, time))
                {
                    SendResponseToClient(stream, "Запис додано успішно.");
                }
                else
                {
                    SendResponseToClient(stream, "Помилка додавання запису.");
                }
            }
            else
            {
                SendResponseToClient(stream, "Невірний формат даних для додавання запису.");
            }
        }

        private static void HandleViewProcedures(string[] messageParts, NetworkStream stream)
        {
            if (messageParts.Length == 2)
            {
                string username = messageParts[1];

                string procedures = ViewProcedures(username);
                SendResponseToClient(stream, procedures);
            }
            else
            {
                SendResponseToClient(stream, "Невірний формат даних для перегляду записів.");
            }
        }

        private static void HandleDeleteProcedure(string[] messageParts, NetworkStream stream)
        {
            if (messageParts.Length == 3)
            {
                string username = messageParts[1];
                string procedureName = messageParts[2];

                if (DeleteProcedure(username, procedureName))
                {
                    SendResponseToClient(stream, "Запис видалено успішно.");
                }
                else
                {
                    SendResponseToClient(stream, "Помилка видалення запису.");
                }
            }
            else
            {
                SendResponseToClient(stream, "Невірний формат даних для видалення запису.");
            }
        }

        private static void SendResponseToClient(NetworkStream stream, string responseMessage)
        {
            byte[] responseData = Encoding.UTF8.GetBytes(responseMessage);
            stream.Write(responseData, 0, responseData.Length);
        }

        private static bool RegisterUser(string username, string password, string email)
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-M19B2MQO\\SQLEXPRESS01;Initial Catalog=BeautySalonDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Users (Email, Username, Password) VALUES (@Email, @Username, @Password)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Email", email);
          
                
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private static bool AddProcedure(string username, string procedureName, string time)
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-M19B2MQO\\SQLEXPRESS01;Initial Catalog=BeautySalonDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Отримати UserId за username
                    string getUserIdQuery = "SELECT UserId FROM Users WHERE Username = @Username";
                    int userId = -1;
                    using (SqlCommand getUserIdCommand = new SqlCommand(getUserIdQuery, connection))
                    {
                        getUserIdCommand.Parameters.AddWithValue("@Username", username);
                        userId = (int)getUserIdCommand.ExecuteScalar();
                    }

                    // Додати запис у таблицю Appointments
                    string query = "INSERT INTO Appointments (UserId, ProcedureId, AppointmentDate) " +
                     "VALUES (@UserId, (SELECT ProcedureId FROM Procedures WHERE ProcedureName = @ProcedureName), @Time)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@ProcedureName", procedureName);
                        command.Parameters.AddWithValue("@Time", DateTime.Parse(time));
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }


        private static string ViewProcedures(string username)
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-M19B2MQO\\SQLEXPRESS01;Initial Catalog=BeautySalonDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Отримати UserId за username
                    string getUserIdQuery = "SELECT UserId FROM Users WHERE Username = @Username";
                    int userId = -1;
                    using (SqlCommand getUserIdCommand = new SqlCommand(getUserIdQuery, connection))
                    {
                        getUserIdCommand.Parameters.AddWithValue("@Username", username);
                        userId = (int)getUserIdCommand.ExecuteScalar();
                    }

                    // Отримати записи про процедури для UserId
                    string query = "SELECT ProcedureName, AppointmentDate FROM Appointments JOIN Procedures ON Appointments.ProcedureId = Procedures.ProcedureId WHERE UserId = @UserId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            StringBuilder result = new StringBuilder();
                            while (reader.Read())
                            {
                                result.AppendLine($"{reader["ProcedureName"]}, {reader["AppointmentDate"]}");
                            }
                            return result.ToString();
                        }
                    }
                }
            }
            catch
            {
                return "Помилка отримання даних.";
            }
        }


        private static bool DeleteProcedure(string username, string procedureName)
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-M19B2MQO\\SQLEXPRESS01;Initial Catalog=BeautySalonDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Видаляємо запис з таблиці Appointments, де збігаються ім'я користувача та назва процедури
                    string query = "DELETE FROM Appointments WHERE Username = @Username AND ProcedureName = @ProcedureName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@ProcedureName", procedureName);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
