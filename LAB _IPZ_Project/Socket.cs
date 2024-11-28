using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

public class SocketService
{
    public void SendMessage(string message)
    {
        try
        {
            // Підключення до сервера за IP і портом
            TcpClient client = new TcpClient("127.1.0.1", 1234);
            NetworkStream stream = client.GetStream();

            // Перетворення повідомлення на байти
            byte[] data = Encoding.UTF8.GetBytes(message);

            // Відправка даних
            stream.Write(data, 0, data.Length);

            // Читання відповіді від сервера
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string serverResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            MessageBox.Show($"Відповідь від сервера: {serverResponse}");

            // Закриття з'єднання
            stream.Close();
            client.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Помилка: {ex.Message}");
        }
    }
}
