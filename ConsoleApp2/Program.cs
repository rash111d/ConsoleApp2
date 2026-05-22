using System;
using System.Data.SqlClient;

namespace LibraryApp
{
    class Program
    {
        static string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=LibraryDB;Trusted_Connection=True;";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n===== БИБЛИОТЕКА =====");
                Console.WriteLine("1 - Показать книги");
                Console.WriteLine("2 - Добавить книгу");
                Console.WriteLine("3 - Найти книгу");
                Console.WriteLine("4 - Удалить книгу");
                Console.WriteLine("0 - Выход");

                Console.Write("Выберите пункт: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowBooks();
                        break;

                    case "2":
                        AddBook();
                        break;

                    case "3":
                        FindBook();
                        break;

                    case "4":
                        DeleteBook();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }

        static void ShowBooks()
        {
            try
            {
                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Books";

                    SqlCommand command =
                        new SqlCommand(query, connection);

                    SqlDataReader reader =
                        command.ExecuteReader();

                    Console.WriteLine("\nСПИСОК КНИГ:\n");

                    while (reader.Read())
                    {
                        Console.WriteLine(
                            reader["Id"] + " | " +
                            reader["Title"] + " | " +
                            reader["Author"] + " | " +
                            reader["PublishYear"] + " | Кол-во: " +
                            reader["Quantity"]);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        static void AddBook()
        {
            try
            {
                Console.Write("Название: ");
                string title = Console.ReadLine();

                Console.Write("Автор: ");
                string author = Console.ReadLine();

                Console.Write("Год: ");
                int year = Convert.ToInt32(Console.ReadLine());

                Console.Write("Количество: ");
                int quantity = Convert.ToInt32(Console.ReadLine());

                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        "INSERT INTO Books " +
                        "(Title, Author, PublishYear, Quantity) " +
                        "VALUES (@title, @author, @year, @quantity)";

                    SqlCommand command =
                        new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@author", author);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@quantity", quantity);

                    command.ExecuteNonQuery();

                    Console.WriteLine("Книга добавлена!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        static void FindBook()
        {
            try
            {
                Console.Write("Введите название: ");
                string title = Console.ReadLine();

                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        "SELECT * FROM Books WHERE Title LIKE @title";

                    SqlCommand command =
                        new SqlCommand(query, connection);

                    command.Parameters.AddWithValue(
                        "@title",
                        "%" + title + "%");

                    SqlDataReader reader =
                        command.ExecuteReader();

                    Console.WriteLine("\nРезультаты:\n");

                    while (reader.Read())
                    {
                        Console.WriteLine(
                            reader["Id"] + " | " +
                            reader["Title"] + " | " +
                            reader["Author"] + " | " +
                            reader["PublishYear"] + " | Кол-во: " +
                            reader["Quantity"]);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        static void DeleteBook()
        {
            try
            {
                Console.Write("Введите ID: ");
                int id = Convert.ToInt32(Console.ReadLine());

                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        "DELETE FROM Books WHERE Id=@id";

                    SqlCommand command =
                        new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@id", id);

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                        Console.WriteLine("Книга удалена!");
                    else
                        Console.WriteLine("Книга не найдена!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
    }
}