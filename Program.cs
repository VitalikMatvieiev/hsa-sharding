using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString = "my_connection";
        int totalInserts = 10000;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            for (int i = 1; i <= totalInserts; i++)
            {
                int categoryId = (i % 3) + 1;
                string query = "INSERT INTO books (id, category_id, author, title, year) " +
                               "VALUES (@Id, @CategoryId, @Author, @Title, @Year)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", i);
                    command.Parameters.AddWithValue("@CategoryId", categoryId);
                    command.Parameters.AddWithValue("@Author", "Jane Austen");
                    command.Parameters.AddWithValue("@Title", "Pride and Prejudice");
                    command.Parameters.AddWithValue("@Year", 1813);

                    command.ExecuteNonQuery();
                }

                if (i % 1000 == 0)
                {
                    Console.WriteLine($"{i} records inserted.");
                }
            }
        }

        Console.WriteLine("Insertion completed.");
    }
}
