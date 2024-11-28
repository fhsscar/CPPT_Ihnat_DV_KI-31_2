using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

public partial class RegisterForm : Form
{
    private UserService userService;
    private TextBox txtUsername;
    private TextBox txtPassword;
    private TextBox txtEmail;
    private Button btnRegister;
    private Label lblUsername;
    private Label lblPassword;
    private Label lblEmail;

    public RegisterForm(UserService userService)
    {
        InitializeComponent();
        this.userService = userService;
    }

    private void btnRegister_Click(object sender, EventArgs e)
    {
        // Зчитуємо дані з текстових полів
        string username = txtUsername.Text;
        string password = txtPassword.Text;
        string email = txtEmail.Text;

        // Перевірка, чи заповнені обов'язкові поля
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show("Ім'я користувача та пароль обов'язкові.");
            return;
        }

        // Рядок підключення до бази даних
        string connectionString = "Server=LAPTOP-M19B2MQO\\SQLEXPRESS01;Database=BeautySalon;Trusted_Connection=True";

        // Запис користувача в базу даних
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Перевірка, чи існує користувач з таким ім'ям або email
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username OR Email = @Email";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Username", username);
                    checkCommand.Parameters.AddWithValue("@Email", email);

                    int userExists = (int)checkCommand.ExecuteScalar();
                    if (userExists > 0)
                    {
                        MessageBox.Show("Користувач з таким ім'ям або email вже існує.");
                        return;
                    }
                }

                // Додаємо нового користувача, якщо не існує дублюючих записів
                string insertQuery = "INSERT INTO Users (Username, Password, Email) VALUES (@Username, @Password, @Email)";
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@Username", username);
                    insertCommand.Parameters.AddWithValue("@Password", password);
                    insertCommand.Parameters.AddWithValue("@Email", email);

                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Реєстрація успішна!");
                        this.Close(); // Закриває форму реєстрації
                    }
                    else
                    {
                        MessageBox.Show("Не вдалося зареєструвати користувача.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        LoginForm loginForm = new LoginForm();
        loginForm.Show();
        this.Hide();


    }

    private bool IsValidEmail(string email)
    {
        var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        return emailRegex.IsMatch(email);
    }

    private void InitializeComponent()
    {
        this.txtUsername = new System.Windows.Forms.TextBox();
        this.txtPassword = new System.Windows.Forms.TextBox();
        this.txtEmail = new System.Windows.Forms.TextBox();
        this.btnRegister = new System.Windows.Forms.Button();
        this.lblUsername = new System.Windows.Forms.Label();
        this.lblPassword = new System.Windows.Forms.Label();
        this.lblEmail = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // txtUsername
        // 
        this.txtUsername.Location = new System.Drawing.Point(130, 30);
        this.txtUsername.Name = "txtUsername";
        this.txtUsername.Size = new System.Drawing.Size(120, 20);
        this.txtUsername.TabIndex = 0;
        // 
        // txtPassword
        // 
        this.txtPassword.Location = new System.Drawing.Point(130, 70);
        this.txtPassword.Name = "txtPassword";
        this.txtPassword.Size = new System.Drawing.Size(120, 20);
        this.txtPassword.TabIndex = 1;
        this.txtPassword.UseSystemPasswordChar = true;
        // 
        // txtEmail
        // 
        this.txtEmail.Location = new System.Drawing.Point(130, 110);
        this.txtEmail.Name = "txtEmail";
        this.txtEmail.Size = new System.Drawing.Size(120, 20);
        this.txtEmail.TabIndex = 2;
        // 
        // btnRegister
        // 
        this.btnRegister.Location = new System.Drawing.Point(130, 150);
        this.btnRegister.Name = "btnRegister";
        this.btnRegister.Size = new System.Drawing.Size(120, 30);
        this.btnRegister.TabIndex = 3;
        this.btnRegister.Text = "Зареєструватися";
        this.btnRegister.UseVisualStyleBackColor = true;
        this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
        // 
        // lblUsername
        // 
        this.lblUsername.AutoSize = true;
        this.lblUsername.Location = new System.Drawing.Point(30, 30);
        this.lblUsername.Name = "lblUsername";
        this.lblUsername.Size = new System.Drawing.Size(37, 13);
        this.lblUsername.TabIndex = 4;
        this.lblUsername.Text = "Логін:";
        // 
        // lblPassword
        // 
        this.lblPassword.AutoSize = true;
        this.lblPassword.Location = new System.Drawing.Point(30, 70);
        this.lblPassword.Name = "lblPassword";
        this.lblPassword.Size = new System.Drawing.Size(48, 13);
        this.lblPassword.TabIndex = 5;
        this.lblPassword.Text = "Пароль:";
        // 
        // lblEmail
        // 
        this.lblEmail.AutoSize = true;
        this.lblEmail.Location = new System.Drawing.Point(30, 110);
        this.lblEmail.Name = "lblEmail";
        this.lblEmail.Size = new System.Drawing.Size(35, 13);
        this.lblEmail.TabIndex = 6;
        this.lblEmail.Text = "Email:";
        // 
        // RegisterForm
        // 
        this.ClientSize = new System.Drawing.Size(284, 261);
        this.Controls.Add(this.lblEmail);
        this.Controls.Add(this.lblPassword);
        this.Controls.Add(this.lblUsername);
        this.Controls.Add(this.btnRegister);
        this.Controls.Add(this.txtEmail);
        this.Controls.Add(this.txtPassword);
        this.Controls.Add(this.txtUsername);
        this.Name = "RegisterForm";
        this.Text = "Реєстрація";
        this.Load += new System.EventHandler(this.RegisterForm_Load);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private void RegisterForm_Load(object sender, EventArgs e)
    {
    }
}
