using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

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
        string username = txtUsername.Text;
        string password = txtPassword.Text;
        string email = txtEmail.Text;

        // Перевірка на порожні поля
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
        {
            MessageBox.Show("Будь ласка, заповніть всі поля.");
            return;
        }

        // Перевірка формату email
        if (!IsValidEmail(email))
        {
            MessageBox.Show("Введіть коректний email.");
            return;
        }

        // Реєстрація користувача
        int userId = -1;
        if (userService.RegisterUser(username, password, email))
        {
            MessageBox.Show("Реєстрація успішна!");

            // Отримуємо userId після реєстрації (можна інтегрувати це в RegisterUser)
            userId = userService.GetUserIdByUsername(username);
            MessageBox.Show($"Ваш userId: {userId}");

            // Тут можете вивести додаткову інформацію або перенаправити на іншу форму

            // Закриваємо вікно реєстрації
            this.Close();
        }
        else
        {
            MessageBox.Show("Користувач або email вже існують.");
        }
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
