using System.Data.SqlClient;
using System;

public class UserService
{
    private string connectionString = "Data Source=localhost;Initial Catalog=BeautySalonDB;Integrated Security=True;";

    // Реєстрація користувача
    public bool RegisterUser(string username, string password, string email)
    {
        // Перевірка, чи існує вже користувач або email в базі даних
        if (UserExists(username, email))
        {
            return false; // Користувач або email вже існують
        }

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (Username, Password, Email) VALUES (@Username, @Password, @Email)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Email", email);

                    return command.ExecuteNonQuery() > 0; // Повертає true, якщо користувач був доданий
                }
            }
        }
        catch (Exception ex)
        {
            // Логування помилки або додаткові дії
            Console.WriteLine("Помилка реєстрації: " + ex.Message);
            return false; // Помилка під час реєстрації
        }
    }

    // Метод для отримання userId за логіном
    public int GetUserIdByUsername(string username)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT UserId FROM Users WHERE Username = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return (int)result; // Повертаємо UserId
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Логування помилки або додаткові дії
            Console.WriteLine("Помилка отримання userId: " + ex.Message);
        }
        return -1; // Повертаємо -1, якщо користувача не знайдено
    }

    // Метод автентифікації користувача
    public bool AuthenticateUser(string username, string password, out int userId)
    {
        userId = -1; // Ініціалізуємо значення за замовчуванням
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT UserId FROM Users WHERE UserName = @Username AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        userId = (int)result; // Встановлюємо отриманий UserId
                        return true;  // Користувач автентифікований
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Логування помилки або додаткові дії
            Console.WriteLine("Помилка автентифікації: " + ex.Message);
        }
        return false; // Користувач не знайдений або невірний пароль
    }

    // Перевірка чи існує користувач або email в базі даних
    private bool UserExists(string username, string email)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username OR Email = @Email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Email", email);

                    int count = (int)command.ExecuteScalar();
                    return count > 0; // Повертає true, якщо користувач або email вже існують
                }
            }
        }
        catch (Exception ex)
        {
            // Логування помилки або додаткові дії
            Console.WriteLine("Помилка перевірки існування користувача або email: " + ex.Message);
            return true; // Якщо сталася помилка, вважаємо, що користувач існує
        }
    }
}
