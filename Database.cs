using Microsoft.Data.Sqlite;

namespace ConsoleApp1;

public class Database
{
    public SqliteConnection Connection;
    public void CheckDatabase()
    {
        if (!File.Exists("./database.sqlite3"))
        {
            File.Create("database.sqlite3");
            Console.WriteLine("Database Created...");
        }
        else if(File.Exists("database.sqlite3"))
        {
            Console.WriteLine("Database Already Exists...");
        }
    }

    public void CreateTable()
    {
        Console.WriteLine("Creating Table....");
            
        string databaseCreateQuery =
            "CREATE TABLE people (id int, firstname text, lastname text)";
            
        SqliteCommand createCommand = new SqliteCommand(databaseCreateQuery, Connection);

        createCommand.ExecuteNonQuery();

        Console.WriteLine("Table Created...");
            
        Console.WriteLine();
    }

    public void AddNames()
    {
        Console.Write("Please enter a first name: ");
        string? first = Console.ReadLine();

        Console.Write("Please enter a last name: ");
        string? last = Console.ReadLine();
            
        string databaseAddQuery = "INSERT INTO people ('firstname', 'lastname') VALUES (@firstname, @lastname)";
        SqliteCommand addCommand = new SqliteCommand(databaseAddQuery, Connection);

        addCommand.Parameters.AddWithValue("@firstname", first); //Adds first name
        addCommand.Parameters.AddWithValue("@lastname", last); //Adds last name
        addCommand.ExecuteNonQuery();

        Console.WriteLine();
    }

    public void GetData()
    {

        string databaseGetQuery = "SELECT * FROM people";

        SqliteCommand getCommand = new SqliteCommand(databaseGetQuery, Connection);
            
        SqliteDataReader getDatabaseResults= getCommand.ExecuteReader();
        if (getDatabaseResults.HasRows)
        {
            Console.WriteLine("People Table: ");
            while (getDatabaseResults.Read())
            {
                Console.WriteLine("First Name: {0} - Last Name: {1}", getDatabaseResults["firstname"], getDatabaseResults["lastname"]);
            }
        }

        Console.WriteLine();
    }

    public void Logic()
    {
        CheckDatabase();
        
        Connection = new SqliteConnection("Data Source=database.sqlite3");
        
        Connection.Open();
        
        Console.WriteLine();
        Console.WriteLine("[1] Create a new table\n" + "[2] Add a name to database\n" + "[3] Return database results\n" + "[4] Exit\n" + "Would you like to: ");
        
        int answer = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine();
        
        if (answer == 1)
        {
            CreateTable();
            Console.WriteLine();
            Logic();
            Console.WriteLine();
        }
        if (answer == 2)
        {
            AddNames();
            Console.WriteLine();
            Logic();
            Console.WriteLine();
        }
        else if (answer == 3)
        {
            GetData();
            Console.WriteLine();
            Logic();
            Console.WriteLine();
        }
        else
        {
            Connection.Close();
            Environment.Exit(0);
        }
    }
}