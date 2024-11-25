using System;
using System.Windows.Forms;

public partial class LoginForm : Form
{
    private UserService userService;
    private TextBox txtUsername;
    private TextBox txtPassword;
    private Button btnLogin;
    private Button btnRegister;
    private Label lblUsername; // Лейбл для логіну
    private Label lblPassword; // Лейбл для паролю

    public LoginForm()
    {
        InitializeComponent();
        userService = new UserService();
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text;
        string password = txtPassword.Text;

        // Перевірка на порожні поля
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show("Будь ласка, введіть логін та пароль.");
            return;
        }

        int userId;
        try
        {
            // Аутентифікація користувача
            if (userService.AuthenticateUser(username, password, out userId))
            {
                MessageBox.Show("Успішна авторизація!");
                // Перехід на головну форму
                MainForm mainForm = new MainForm(username, userId, new AppointmentService(), new ProcedureService());
                mainForm.Show();
                this.Close();  // Закриваємо форму авторизації після переходу на головну
            }
            else
            {
                MessageBox.Show("Невірний логін або пароль.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Помилка при аутентифікації: {ex.Message}");
        }
    }

    private void btnRegister_Click(object sender, EventArgs e)
    {
        // Відкриття форми реєстрації
        RegisterForm registerForm = new RegisterForm(userService);
        registerForm.FormClosed += (s, args) => this.Show();  // Показати вікно авторизації після закриття вікна реєстрації
        this.Hide();  // Сховати вікно авторизації
        registerForm.Show();
    }

    private void InitializeComponent()
    {
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(130, 50);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(120, 20);
            this.txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(130, 90);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(120, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(130, 130);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(120, 30);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Увійти";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(130, 170);
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
            this.lblUsername.Location = new System.Drawing.Point(50, 50);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(37, 13);
            this.lblUsername.TabIndex = 4;
            this.lblUsername.Text = "Логін:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(50, 90);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(48, 13);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "Пароль:";
            // 
            // LoginForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnRegister);
            this.Name = "LoginForm";
            this.Text = "Авторизація";
            this.Load += new System.EventHandler(this.LoginForm_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    // Виклик при завантаженні форми (якщо потрібно, можна додати логіку)
    private void LoginForm_Load(object sender, EventArgs e)
    {
    }

    private void LoginForm_Load_1(object sender, EventArgs e)
    {

    }
}
