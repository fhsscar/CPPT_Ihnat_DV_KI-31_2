using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class ProcedureService
{
    private string connectionString = "Data Source=localhost;Initial Catalog=BeautySalonDB;Integrated Security=True;";

    // Повертає список доступних процедур із бази даних
    public List<string> GetAvailableProcedures()
    {
        List<string> procedures = new List<string>();

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ProcedureName FROM Procedures"; // SQL-запит для отримання процедур

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string procedureName = reader["ProcedureName"].ToString(); // Отримуємо назву процедури
                            procedures.Add(procedureName); // Додаємо до списку
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Можна обробити помилку, наприклад, вивести повідомлення або повернути порожній список
            Console.WriteLine("Error fetching procedures: " + ex.Message);
        }

        return procedures;
    }
}
