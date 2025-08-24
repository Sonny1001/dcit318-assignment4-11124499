using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static string connStr = "Server=localhost,1433;Database=PharmacyDB;User Id=sa;Password=MyStrongPass123!;TrustServerCertificate=True;";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n--- Pharmacy Inventory & Sales System ---");
            Console.WriteLine("1. Add Medicine");
            Console.WriteLine("2. View All Medicines");
            Console.WriteLine("3. Search Medicine");
            Console.WriteLine("4. Update Stock");
            Console.WriteLine("5. Record Sale");
            Console.WriteLine("6. Exit");
            Console.Write("Choose option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddMedicine(); break;
                case "2": ViewAllMedicines(); break;
                case "3": SearchMedicine(); break;
                case "4": UpdateStock(); break;
                case "5": RecordSale(); break;
                case "6": return;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    static void AddMedicine()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Category: ");
        string category = Console.ReadLine();
        Console.Write("Price: ");
        decimal price = decimal.Parse(Console.ReadLine());
        Console.Write("Quantity: ");
        int quantity = int.Parse(Console.ReadLine());

        using var con = new SqlConnection(connStr);
        con.Open();
        SqlCommand cmd = new SqlCommand("AddMedicine", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Name", name);
        cmd.Parameters.AddWithValue("@Category", category);
        cmd.Parameters.AddWithValue("@Price", price);
        cmd.Parameters.AddWithValue("@Quantity", quantity);
        cmd.ExecuteNonQuery();

        Console.WriteLine("Medicine added successfully!");
    }

    static void ViewAllMedicines()
    {
        using var con = new SqlConnection(connStr);
        con.Open();
        SqlCommand cmd = new SqlCommand("GetAllMedicines", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr = cmd.ExecuteReader();
        Console.WriteLine("\n--- Medicines ---");
        while (dr.Read())
        {
            Console.WriteLine($"{dr["MedicineID"]}: {dr["Name"]} | {dr["Category"]} | {dr["Price"]} | Stock: {dr["Quantity"]}");
        }
    }

    static void SearchMedicine()
    {
        Console.Write("Enter search term: ");
        string term = Console.ReadLine();

        using var con = new SqlConnection(connStr);
        con.Open();
        SqlCommand cmd = new SqlCommand("SearchMedicine", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@SearchTerm", term);
        SqlDataReader dr = cmd.ExecuteReader();
        Console.WriteLine("\n--- Search Results ---");
        while (dr.Read())
        {
            Console.WriteLine($"{dr["MedicineID"]}: {dr["Name"]} | {dr["Category"]} | {dr["Price"]} | Stock: {dr["Quantity"]}");
        }
    }

    static void UpdateStock()
    {
        Console.Write("Medicine ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Quantity to add: ");
        int qty = int.Parse(Console.ReadLine());

        using var con = new SqlConnection(connStr);
        con.Open();
        SqlCommand cmd = new SqlCommand("UpdateStock", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@MedicineID", id);
        cmd.Parameters.AddWithValue("@Quantity", qty);
        cmd.ExecuteNonQuery();

        Console.WriteLine("Stock updated successfully!");
    }

    static void RecordSale()
    {
        Console.Write("Medicine ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Quantity Sold: ");
        int qty = int.Parse(Console.ReadLine());

        using var con = new SqlConnection(connStr);
        con.Open();
        SqlCommand cmd = new SqlCommand("RecordSale", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@MedicineID", id);
        cmd.Parameters.AddWithValue("@QuantitySold", qty);
        cmd.ExecuteNonQuery();

        Console.WriteLine("Sale recorded successfully!");
    }
}
