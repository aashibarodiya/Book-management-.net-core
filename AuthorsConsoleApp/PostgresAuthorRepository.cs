using ConceptArchitect.BookManagement;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorsConsoleApp
{
    public  class PostgresAuthorRepository
    {
        Func<DbConnection> connectionFactory;

        public PostgresAuthorRepository(Func<DbConnection> connectionFactory)
        {
            this.connectionFactory=connectionFactory;
        }

        public List<Author> GetAllAuthors()
        {
            DbConnection connection = null;

            try
            {
                connection = connectionFactory();
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "select * from authors";

                var reader = command.ExecuteReader();

                var authors = new List<Author>();
                while (reader.Read())
                {
                    var author = new Author();

                    author.Id = (string)reader["id"];
                    author.Name = reader["name"].ToString();
                    author.Biography = reader["biography"].ToString();
                    author.Social.WebSite = reader["social"].ToString();
                    author.BirthDate = (DateTime)reader["birthdate"];
                    author.PhotoUrl = reader["photograph"].ToString();

                    authors.Add(author);

                }
                return authors;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
