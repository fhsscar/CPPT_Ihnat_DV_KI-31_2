using System;
using System.Windows.Forms;

public partial class MainForm : Form
{
    private string currentUser;
    private int userId; // Зберігаємо userId
    private AppointmentService appointmentService;
    private ProcedureService procedureService;
    private Button btnViewAppointments;
    private Button btnMakeAppointment;

    // Конструктор для ініціалізації
    public MainForm(string username, int userId, AppointmentService appointmentService, ProcedureService procedureService)
    {
        if (string.IsNullOrEmpty(username) || appointmentService == null || procedureService == null)
        {
            throw new ArgumentException("Некоректні параметри ініціалізації форми.");
        }

        InitializeComponent();
        this.currentUser = username;
        this.userId = userId; // Зберігаємо userId
        this.appointmentService = appointmentService;
        this.procedureService = procedureService;

        // Спробуємо автентифікувати користувача і отримати userId
        if (!AuthenticateUser(currentUser, out userId))
        {
            MessageBox.Show("Невірні дані для входу.");
            return; // Якщо автентифікація не пройшла, припиняємо ініціалізацію
        }
    }

    // Метод автентифікації користувача
    private bool AuthenticateUser(string username, out int userId)
    {
        userId = -1; // Ініціалізація значення за замовчуванням
        // Викликаємо метод UserService для автентифікації
        UserService userService = new UserService(); // Припускаємо, що цей клас вже є у вашому проєкті
        return userService.AuthenticateUser(username, "userPassword", out userId); // Використовується метод для автентифікації
    }

    // Кнопка перегляду записів
    private void btnViewAppointments_Click(object sender, EventArgs e)
    {
        if (appointmentService != null)
        {
            // Передаємо userId замість currentUser
            ViewAppointmentsForm viewAppointmentsForm = new ViewAppointmentsForm(userId, appointmentService);
            viewAppointmentsForm.Show();
        }
        else
        {
            MessageBox.Show("Не вдалося завантажити записи, спробуйте ще раз.");
        }
    }

    // Кнопка запису на процедуру
    private void btnMakeAppointment_Click(object sender, EventArgs e)
    {
        if (appointmentService != null && procedureService != null)
        {
            // Відкриваємо форму для запису на процедуру
            AppointmentForm appointmentForm = new AppointmentForm(currentUser, appointmentService, procedureService);
            appointmentForm.Show();
        }
        else
        {
            MessageBox.Show("Не вдалося завантажити необхідні сервіси для запису.");
        }
    }

    // Ініціалізація компонентів форми
    private void InitializeComponent()
    {
        this.btnViewAppointments = new Button();
        this.btnMakeAppointment = new Button();

        // 
        // btnViewAppointments
        // 
        this.btnViewAppointments.Location = new System.Drawing.Point(30, 30);
        this.btnViewAppointments.Name = "btnViewAppointments";
        this.btnViewAppointments.Size = new System.Drawing.Size(200, 30);
        this.btnViewAppointments.TabIndex = 0;
        this.btnViewAppointments.Text = "Переглянути записи";
        this.btnViewAppointments.UseVisualStyleBackColor = true;
        this.btnViewAppointments.Click += new EventHandler(this.btnViewAppointments_Click);

        // 
        // btnMakeAppointment
        // 
        this.btnMakeAppointment.Location = new System.Drawing.Point(30, 80);
        this.btnMakeAppointment.Name = "btnMakeAppointment";
        this.btnMakeAppointment.Size = new System.Drawing.Size(200, 30);
        this.btnMakeAppointment.TabIndex = 1;
        this.btnMakeAppointment.Text = "Записатись на процедуру";
        this.btnMakeAppointment.UseVisualStyleBackColor = true;
        this.btnMakeAppointment.Click += new EventHandler(this.btnMakeAppointment_Click);

        // 
        // MainForm
        // 
        this.ClientSize = new System.Drawing.Size(284, 261);
        this.Controls.Add(this.btnViewAppointments);
        this.Controls.Add(this.btnMakeAppointment);
        this.Name = "MainForm";
        this.Text = "Головна форма";
        this.Load += new System.EventHandler(this.MainForm_Load);
        this.ResumeLayout(false);
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        // Допоміжні дії при завантаженні форми (якщо потрібно)
    }
}
