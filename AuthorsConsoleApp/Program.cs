// See https://aka.ms/new-console-template for more information
using AuthorsConsoleApp;
using ConceptArchitect.BookManagement;
using Npgsql;
using System.Data.Common;

class Program
{
    static void Main()
    {
        var repository = new PostgresAuthorRepository(() =>
        {
            string server = "tyke.db.elephantsql.com";
            string db = "bkpqbxlp";
            string user = "bkpqbxlp";
            string password = "ROzwtDAKulU2lnt1zqgJmV5bNeoBfV2S";
            int port;
            DbConnection connection = new NpgsqlConnection();
            connection.ConnectionString = $"Server={server};Database={db};User Id={user};Password={password};";
            return connection;
        });

        foreach(var author in repository.GetAllAuthors())
        {
            Console.WriteLine(author.Name);
        }
    }
}

