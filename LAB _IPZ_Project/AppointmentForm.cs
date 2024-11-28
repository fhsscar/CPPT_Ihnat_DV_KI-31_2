using System;
using System.Windows.Forms;

public partial class AppointmentForm : Form
{
    private int userId; // Додаємо поле для збереження userId

    private AppointmentService appointmentService;
    private ProcedureService procedureService;
    private string currentUser;
    private ComboBox comboBoxProcedures;
    private DateTimePicker datePicker;
    private NumericUpDown hourPicker;
    private NumericUpDown minutePicker;
    private Button btnMakeAppointment;

    public AppointmentForm(int userId, string username, AppointmentService appointmentService, ProcedureService procedureService)
    {
        InitializeComponent();
        this.appointmentService = appointmentService;
        this.procedureService = procedureService;
        this.currentUser = username;
        this.userId = userId; // Додаємо ініціалізацію userId

        comboBoxProcedures.DataSource = procedureService.GetAvailableProcedures();
        datePicker.MinDate = DateTime.Today; // Обмеження вибору минулих дат
    }


    private async void btnMakeAppointment_Click(object sender, EventArgs e)
    {
        // Отримуємо вибрану дату з datePicker
        DateTime selectedDate = datePicker.Value.Date; // Тільки дата без часу

        // Отримуємо вибрані годину та хвилину
        int selectedHour = (int)hourPicker.Value;
        int selectedMinute = (int)minutePicker.Value;

        // Формуємо повну дату, включаючи час
        DateTime appointmentDate = new DateTime(
            selectedDate.Year, selectedDate.Month, selectedDate.Day,
            selectedHour, selectedMinute, 0); // Додаємо час: година, хвилина, секунда

        // Отримуємо вибрану процедуру з comboBox
        string selectedProcedure = comboBoxProcedures.SelectedItem.ToString();

        // Отримуємо дані користувача
        int userId = 1; // Приклад, треба використовувати актуальний userId
        string username = "User"; // Приклад, треба використовувати актуальне ім'я користувача

        // Викликаємо асинхронний метод для запису
        bool success = await appointmentService.MakeAppointmentAsync(userId, username, selectedProcedure, appointmentDate);

        // Якщо запис пройшов успішно, повідомляємо користувача
        if (success)
        {
            MessageBox.Show("Запис успішно створено!");
        }
        else
        {
            MessageBox.Show("Не вдалося створити запис. Можливо, виник конфлікт з іншими запитами на ту ж дату.");
        }
    }




    private void InitializeComponent()
    {
            this.comboBoxProcedures = new System.Windows.Forms.ComboBox();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.hourPicker = new System.Windows.Forms.NumericUpDown();
            this.minutePicker = new System.Windows.Forms.NumericUpDown();
            this.btnMakeAppointment = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.hourPicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minutePicker)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxProcedures
            // 
            this.comboBoxProcedures.FormattingEnabled = true;
            this.comboBoxProcedures.Location = new System.Drawing.Point(234, 29);
            this.comboBoxProcedures.Name = "comboBoxProcedures";
            this.comboBoxProcedures.Size = new System.Drawing.Size(121, 21);
            this.comboBoxProcedures.TabIndex = 0;
            // 
            // datePicker
            // 
            this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePicker.Location = new System.Drawing.Point(234, 82);
            this.datePicker.MinDate = new System.DateTime(2024, 11, 25, 0, 0, 0, 0);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(121, 20);
            this.datePicker.TabIndex = 1;
            this.datePicker.ValueChanged += new System.EventHandler(this.datePicker_ValueChanged);
            // 
            // hourPicker
            // 
            this.hourPicker.Location = new System.Drawing.Point(234, 120);
            this.hourPicker.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.hourPicker.Name = "hourPicker";
            this.hourPicker.Size = new System.Drawing.Size(50, 20);
            this.hourPicker.TabIndex = 2;
            this.hourPicker.ValueChanged += new System.EventHandler(this.hourPicker_ValueChanged);
            // 
            // minutePicker
            // 
            this.minutePicker.Location = new System.Drawing.Point(305, 120);
            this.minutePicker.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.minutePicker.Name = "minutePicker";
            this.minutePicker.Size = new System.Drawing.Size(50, 20);
            this.minutePicker.TabIndex = 3;
            // 
            // btnMakeAppointment
            // 
            this.btnMakeAppointment.Location = new System.Drawing.Point(235, 160);
            this.btnMakeAppointment.Name = "btnMakeAppointment";
            this.btnMakeAppointment.Size = new System.Drawing.Size(120, 30);
            this.btnMakeAppointment.TabIndex = 4;
            this.btnMakeAppointment.Text = "Записатися";
            this.btnMakeAppointment.UseVisualStyleBackColor = true;
            this.btnMakeAppointment.Click += new System.EventHandler(this.btnMakeAppointment_Click);
            // 
            // AppointmentForm
            // 
            this.ClientSize = new System.Drawing.Size(592, 240);
            this.Controls.Add(this.comboBoxProcedures);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.hourPicker);
            this.Controls.Add(this.minutePicker);
            this.Controls.Add(this.btnMakeAppointment);
            this.Name = "AppointmentForm";
            this.Load += new System.EventHandler(this.AppointmentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.hourPicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minutePicker)).EndInit();
            this.ResumeLayout(false);

    }

    private void AppointmentForm_Load(object sender, EventArgs e)
    {
     
    }

    private void datePicker_ValueChanged(object sender, EventArgs e)
    {

    }

    private void hourPicker_ValueChanged(object sender, EventArgs e)
    {

    }
}
