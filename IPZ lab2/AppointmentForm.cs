using System;
using System.Windows.Forms;

public partial class AppointmentForm : Form
{
    private AppointmentService appointmentService;
    private ProcedureService procedureService;
    private string currentUser;
    private ComboBox comboBoxProcedures;
    private DateTimePicker datePicker;
    private NumericUpDown hourPicker;
    private NumericUpDown minutePicker;
    private Button btnMakeAppointment;

    public AppointmentForm(string username, AppointmentService appointmentService, ProcedureService procedureService)
    {
        InitializeComponent();
        this.appointmentService = appointmentService;
        this.procedureService = procedureService;
        this.currentUser = username;

       
        comboBoxProcedures.DataSource = procedureService.GetAvailableProcedures();

        
        datePicker.MinDate = DateTime.Today; // Обмеження вибору минулих дат
    }

    private void btnMakeAppointment_Click(object sender, EventArgs e)
    {
        string selectedProcedure = comboBoxProcedures.SelectedItem.ToString(); 
        DateTime selectedDate = datePicker.Value.Date; 
        int selectedHour = (int)hourPicker.Value;      
        int selectedMinute = (int)minutePicker.Value;  

       
        DateTime appointmentDateTime = selectedDate.AddHours(selectedHour).AddMinutes(selectedMinute);

        if (appointmentService.MakeAppointment(currentUser, selectedProcedure, appointmentDateTime))
        {
            MessageBox.Show("Запис на процедуру здійснено!");
        }
        else
        {
            MessageBox.Show("Процедура вже записана на цей час.");
        }
    }

    private void InitializeComponent()
    {
        this.comboBoxProcedures = new System.Windows.Forms.ComboBox();
        this.datePicker = new System.Windows.Forms.DateTimePicker();
        this.hourPicker = new System.Windows.Forms.NumericUpDown();
        this.minutePicker = new System.Windows.Forms.NumericUpDown();
        this.btnMakeAppointment = new System.Windows.Forms.Button();
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
        this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short; // Показує тільки календар
        this.datePicker.Location = new System.Drawing.Point(234, 82);
        this.datePicker.Name = "datePicker";
        this.datePicker.Size = new System.Drawing.Size(121, 20);
        this.datePicker.TabIndex = 1;
        this.datePicker.MinDate = DateTime.Today; // Обмеження вибору минулих дат

        // 
        // hourPicker
        // 
        this.hourPicker.Location = new System.Drawing.Point(234, 120);
        this.hourPicker.Maximum = 23; // Максимум 23 години
        this.hourPicker.Name = "hourPicker";
        this.hourPicker.Size = new System.Drawing.Size(50, 20);
        this.hourPicker.TabIndex = 2;

        // 
        // minutePicker
        // 
        this.minutePicker.Location = new System.Drawing.Point(305, 120);
        this.minutePicker.Maximum = 59; // Максимум 59 хвилин
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
        this.Text = "Запис на процедуру";
        this.Load += new System.EventHandler(this.AppointmentForm_Load);
        this.ResumeLayout(false);

    }

    private void AppointmentForm_Load(object sender, EventArgs e)
    {
     
    }
}
