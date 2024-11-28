using System;
using System.Windows.Forms;

namespace IPZ_lab2
{
    public static class Program
    {
        [STAThread] // This attribute is required for Windows Forms applications
        static void Main()
        {
            // This initializes the application and opens the LoginForm
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm()); // Change LoginForm to your starting form
        }
    }
}
