using System;

public class Appointment
{
    public int AppointmentId { get; set; }  // Ідентифікатор запису
    public int UserId { get; set; }          // Зовнішній ключ для користувача
    public int ProcedureId { get; set; }     // Зовнішній ключ для процедури
    public DateTime AppointmentDate { get; set; }

    // Можна зберігати ці значення при вибірці для відображення
    public string ProcedureName { get; set; }  // Назва процедури для відображення
    public string UserName { get; set; }       // Ім'я користувача для відображення

    public override string ToString()
    {
        return $"{ProcedureName} для {UserName} на {AppointmentDate.ToString("dd/MM/yyyy HH:mm")}";
    }
}
