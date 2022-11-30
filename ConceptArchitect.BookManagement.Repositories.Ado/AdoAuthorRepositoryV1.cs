using ConceptArchitect.Utils;
using System.Data.Common;

namespace ConceptArchitect.BookManagement.Repositories.Ado
{
    public class AdoAuthorRepositoryV1 : IRepository<Author, string>
    {
        Func<DbConnection> connectionFactory;

        public AdoAuthorRepositoryV1(Func<DbConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<Author> Add(Author author)
        {
            //insert
            DbConnection connection = null;
            try
            {
                connection = connectionFactory();
                connection.Open();
                var command=connection.CreateCommand();
                command.CommandText = $"insert into authors(id,name,biography,photograph,birthdate,deathdate,email,web)" +
                                     $"value('{author.Id}','{author.Name}','{author.Biography}'," +
                                     $"'{author.PhotoUrl}','{author.BirthDate}','{author.DeathDate}','{author.Social.Email}','{author.Social.WebSite}'";

               
                command.ExecuteNonQuery();

                await Task.CompletedTask;
                return author;

            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<List<Author>> GetAll()
        {
            //select
            DbConnection connection = null;
            try
            {
                connection=connectionFactory();
                connection.Open();

                var command=connection.CreateCommand();

                command.CommandText = "select * from authors";
                var reader = command.ExecuteReader();


                var authors = new List<Author>();
                while(reader.Read())
                {
                    var author=new Author();

                    author.Id = reader["id"].ToString();
                    author.Name = reader["name"].ToString();
                    author.Biography = reader["biography"].ToString();
                    author.BirthDate = (DateTime)reader["birthDate"];
                    author.DeathDate = (DateTime?)reader["deathDate"];
                    author.Social.Email = reader["email"].ToString();
                    author.Social.WebSite = reader["web"].ToString();
                    author.PhotoUrl = reader["photograph"].ToString();

                    authors.Add(author);
                }

                await Task.CompletedTask;
                return authors;

            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<Author> GetById(string id)
        {
            //select
            DbConnection connection = null;
            try
            {
                connection = connectionFactory();
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = $"select * from authors where id='{id}'";
                var reader = command.ExecuteReader();

                await Task.CompletedTask;

                if (reader.Read())
                {
                    var author = new Author();

                    author.Id = reader["id"].ToString();
                    author.Name = reader["name"].ToString();
                    author.Biography = reader["biography"].ToString();
                    author.BirthDate = (DateTime)reader["birthDate"];
                    author.DeathDate = (DateTime?)reader["deathDate"];
                    author.Social.Email = reader["email"].ToString();
                    author.Social.WebSite = reader["web"].ToString();
                    author.PhotoUrl = reader["photograph"].ToString();

                    return author;
                }
                else
                {
                    return null;
                }
                
                

            }
            finally
            {
                connection.Close();
            }
        }

        public async Task Remove(string id)
        {
            //delete
            DbConnection connection = null;
            try
            {
                connection=connectionFactory();
                connection.Open();

                var command=connection.CreateCommand();

                await Task.CompletedTask;
                //run delete command
            }
            finally
            {
                connection.Close();
            }
            
        }

        public async Task Update(Author entity, Action<Author, Author> mergeOldNew)
        {
            //update
            DbConnection connection = null;
            try
            {
                connection = connectionFactory();
                connection.Open();

                var command = connection.CreateCommand();
                await Task.CompletedTask;
                //run update command
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task Save()
        {
            await Task.CompletedTask;
        }
    }
}