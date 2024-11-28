using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

public partial class ViewAppointmentsForm : Form
{
    private AppointmentService appointmentService;
    private int userId; // ID користувача
    private ListBox listBoxAppointments;
    private Button btnViewAppointments;
    private Button btnDeleteAppointment;

    public ViewAppointmentsForm(int userId, AppointmentService appointmentService)
    {
        InitializeComponent();
        this.userId = userId;
        this.appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));

        // Кнопка видалення прихована до вибору запису
        btnDeleteAppointment.Visible = false;
    }

    private void btnViewAppointments_Click(object sender, EventArgs e)
    {
        RefreshAppointmentsList();
        btnDeleteAppointment.Visible = listBoxAppointments.Items.Count > 0;
    }

    private void btnDeleteAppointment_Click(object sender, EventArgs e)
    {
        if (listBoxAppointments.SelectedItem != null)
        {
            var selectedAppointmentText = listBoxAppointments.SelectedItem.ToString();

            // Отримуємо записи для обраного користувача
            var appointments = appointmentService.GetAppointmentsByUserId(userId);
            var appointment = appointments.FirstOrDefault(a => a.ToString() == selectedAppointmentText);

            if (appointment != null && appointmentService.CancelAppointment(appointment))
            {
                MessageBox.Show("Запис успішно видалено.");
                RefreshAppointmentsList();
            }
            else
            {
                MessageBox.Show("Не вдалося видалити запис.");
            }
        }
        else
        {
            MessageBox.Show("Оберіть запис для видалення.");
        }
    }

    private void RefreshAppointmentsList()
    {
        listBoxAppointments.Items.Clear();
        var appointments = appointmentService.GetAppointmentsByUserId(userId);

        foreach (var appointment in appointments)
        {
            listBoxAppointments.Items.Add(appointment.ToString());
        }
    }

    private void InitializeComponent()
    {
        this.listBoxAppointments = new System.Windows.Forms.ListBox();
        this.btnViewAppointments = new System.Windows.Forms.Button();
        this.btnDeleteAppointment = new System.Windows.Forms.Button();

        this.SuspendLayout();
        // 
        // listBoxAppointments
        // 
        this.listBoxAppointments.Location = new System.Drawing.Point(30, 30);
        this.listBoxAppointments.Name = "listBoxAppointments";
        this.listBoxAppointments.Size = new System.Drawing.Size(300, 200);
        this.listBoxAppointments.TabIndex = 0;
        // 
        // btnViewAppointments
        // 
        this.btnViewAppointments.Location = new System.Drawing.Point(30, 250);
        this.btnViewAppointments.Name = "btnViewAppointments";
        this.btnViewAppointments.Size = new System.Drawing.Size(300, 30);
        this.btnViewAppointments.TabIndex = 1;
        this.btnViewAppointments.Text = "Переглянути записи";
        this.btnViewAppointments.UseVisualStyleBackColor = true;
        this.btnViewAppointments.Click += new System.EventHandler(this.btnViewAppointments_Click);
        // 
        // btnDeleteAppointment
        // 
        this.btnDeleteAppointment.Location = new System.Drawing.Point(30, 290);
        this.btnDeleteAppointment.Name = "btnDeleteAppointment";
        this.btnDeleteAppointment.Size = new System.Drawing.Size(300, 30);
        this.btnDeleteAppointment.TabIndex = 2;
        this.btnDeleteAppointment.Text = "Видалити запис";
        this.btnDeleteAppointment.UseVisualStyleBackColor = true;
        this.btnDeleteAppointment.Visible = false;
        this.btnDeleteAppointment.Click += new System.EventHandler(this.btnDeleteAppointment_Click);

        // 
        // ViewAppointmentsForm
        // 
        this.ClientSize = new System.Drawing.Size(380, 350);
        this.Controls.Add(this.listBoxAppointments);
        this.Controls.Add(this.btnViewAppointments);
        this.Controls.Add(this.btnDeleteAppointment);
        this.Name = "ViewAppointmentsForm";
        this.Text = "Перегляд записів на процедури";
        this.Load += new System.EventHandler(this.ViewAppointmentsForm_Load);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private void ViewAppointmentsForm_Load(object sender, EventArgs e)
    {
        RefreshAppointmentsList();
    }
}
