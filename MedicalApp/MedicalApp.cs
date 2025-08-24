using System;
using System.Data.SqlClient;

class Program
{
    static string connStr = "Server=localhost,1433;Database=MedicalDB;User Id=sa;Password=MyStrongPass123!;";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n--- Medical Appointment System ---");
            Console.WriteLine("1. View Doctors");
            Console.WriteLine("2. Book Appointment");
            Console.WriteLine("3. View Appointments");
            Console.WriteLine("4. Exit");
            Console.Write("Choose option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": ViewDoctors(); break;
                case "2": BookAppointment(); break;
                case "3": ViewAppointments(); break;
                case "4": return;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    static void ViewDoctors()
    {
        using var con = new SqlConnection(connStr);
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT * FROM Doctors", con);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
            Console.WriteLine($"{dr["DoctorID"]}: {dr["FullName"]} ({dr["Specialty"]}) Available: {dr["Availability"]}");
    }

    static void BookAppointment()
    {
        Console.Write("Enter DoctorID: ");
        int doctorId = int.Parse(Console.ReadLine());
        Console.Write("Enter PatientID: ");
        int patientId = int.Parse(Console.ReadLine());
        Console.Write("Enter Date (yyyy-mm-dd hh:mm): ");
        DateTime date = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter Notes: ");
        string notes = Console.ReadLine();

        using var con = new SqlConnection(connStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(
            "INSERT INTO Appointments (DoctorID, PatientID, AppointmentDate, Notes) VALUES (@d, @p, @date, @n)", con);
        cmd.Parameters.AddWithValue("@d", doctorId);
        cmd.Parameters.AddWithValue("@p", patientId);
        cmd.Parameters.AddWithValue("@date", date);
        cmd.Parameters.AddWithValue("@n", notes);

        cmd.ExecuteNonQuery();
        Console.WriteLine("Appointment booked!");
    }

    static void ViewAppointments()
    {
        using var con = new SqlConnection(connStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(
            "SELECT A.AppointmentID, D.FullName AS Doctor, P.FullName AS Patient, A.AppointmentDate, A.Notes " +
            "FROM Appointments A JOIN Doctors D ON A.DoctorID=D.DoctorID JOIN Patients P ON A.PatientID=P.PatientID", con);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
            Console.WriteLine($"#{dr["AppointmentID"]} - {dr["Doctor"]} with {dr["Patient"]} at {dr["AppointmentDate"]} ({dr["Notes"]})");
    }
}
