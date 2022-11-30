using ConceptArchitect.Utils;
using System.Data.Common;

namespace ConceptArchitect.BookManagement.Repositories.Ado
{
    public class AdoAuthorRepository : IRepository<Author, string>
    {
        DbManager db;
        
        public AdoAuthorRepository(DbManager db)
        {
            this.db = db;
            
        }



        public async Task<Author> Add(Author author)
        {
            return await db.ExecuteCommand(async command =>
            {
                var deathDate = "";
                if (author.DeathDate != null)
                    deathDate = $"{author.DeathDate}";

                var biography = author.Biography
                                    .Replace("'", "&squote;")
                                    .Replace("\"", "&dquote;");


                command.CommandText = $"insert into authors(id,name,biography,photograph,birthdate,deathdate,email,web)" +
                                     $"values('{author.Id}','{author.Name}','{biography}'," +
                                     $"'{author.PhotoUrl}','{author.BirthDate}','{deathDate}','{author.Social.Email}','{author.Social.WebSite}')";

                Console.WriteLine($"insert command: {command.CommandText}");
                command.ExecuteNonQuery();

                await Task.CompletedTask;
                return author;
            });

        }

        public async Task<List<Author>> GetAll()
        {
            //select
            return await db.ExecuteCommand(async command =>
           {
               command.CommandText = "select * from authors";
               var reader = command.ExecuteReader();


               var authors = new List<Author>();
               while (reader.Read())
               {
                   var author = new Author();

                   author.Id = (string)reader["id"];
                   author.Name = (string)reader["name"];
                   author.Biography =(string) reader["biography"];

                   author.Biography = author.Biography.Replace("&squote;", "'").Replace("&dquote;", "\"");

                   author.BirthDate = (DateTime)reader["birthDate"];

                   var deathDate = reader["deathDate"];
                   if(deathDate!=null && !(deathDate is DBNull))
                   {
                        author.DeathDate = (DateTime)deathDate;
                   }

                   author.Social.Email = reader["email"].ToString();
                   author.Social.WebSite = reader["web"].ToString();
                   author.PhotoUrl = (string)reader["photograph"];

                   authors.Add(author);
               }

               await Task.CompletedTask;
               return authors;
           });


        }

        public async Task<Author?> GetById(string id)
        {
            return await db.ExecuteCommand(async command =>
              {

                   //select           
                   command.CommandText = $"select * from authors where id='{id}'";
                  var reader = command.ExecuteReader();

                  await Task.CompletedTask;

                  if (reader.Read())
                  {
                      var author = new Author();

                      author.Id = (string)reader["id"];
                      author.Name = (string)reader["name"];
                      author.Biography = (string)reader["biography"];
                      author.Biography = author.Biography.Replace("&squote;", "'").Replace("&dquote;", "\"");
                      author.BirthDate = (DateTime)reader["birthDate"];
                      var deathDate = reader["deathDate"];
                      if (deathDate != null && !(deathDate is DBNull))
                      {
                          author.DeathDate = (DateTime)deathDate;
                      }
                      author.Social.Email = reader["email"].ToString();
                      author.Social.WebSite = reader["web"].ToString();
                      author.PhotoUrl = (string)reader["photograph"];

                      return author;
                  }
                  else
                  {
                      return null;
                  }

              });

        }

        public async Task Remove(string id)
        {
            await db.ExecuteCommand(async command =>
            {
                command.CommandText = $"delete from authors where id='{id}'";
                await Task.CompletedTask;
                return command.ExecuteNonQuery();                
            });

        }

        public async Task Save()
        {
            await Task.CompletedTask;
        }

        public async Task Update(Author newAuthorInfo, Action<Author, Author> mergeOldNew)
        {
            var currentAuthorInfo = await GetById(newAuthorInfo.Id); //find old details
            if (currentAuthorInfo == null)
                return;

            mergeOldNew(currentAuthorInfo, newAuthorInfo);
            var a = currentAuthorInfo;
            var deathDate = "";

            if (a.DeathDate != null)
                deathDate = $"{a.DeathDate}";

            var biography = a.Biography
                                .Replace("'", "&squote;")
                                .Replace("\"", "&dquote;");
            await db.ExecuteCommand(async command =>
            {
                command.CommandText = $"update authors  " +
                                    $" set name='{a.Name}'," +
                                    $" biography='{biography}'," +
                                    $" photograph='{a.PhotoUrl}'," +
                                    $" email='{a.Social.Email}'," +
                                    $" web='{a.Social.WebSite}'," +
                                    $" birthDate='{a.BirthDate}'," +
                                    $" deathDate='{deathDate}' " +
                                    $" where id='{a.Id}'";

                var result= command.ExecuteNonQuery();
                await Task.CompletedTask;
                return result;
            });
        }
    }
}