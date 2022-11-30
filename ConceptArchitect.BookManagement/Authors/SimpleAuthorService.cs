using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class SimpleAuthorService : IAuthorService
    {
        IRepository<Author, string> repository;
        public SimpleAuthorService(IRepository<Author,string> repository)
        {
            this.repository = repository;
        }

        public async Task<Author> AddAuthor(Author author)
        {
            if (string.IsNullOrEmpty(author.Id))
                author.Id = author.Name.ToLower().Replace(' ', '-');
            await repository.Add(author);
            return author;
        }

        public async Task DeleteAuthor(string id)
        {
            await repository.Remove(id);
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await repository.GetAll();
        }

        public async Task<Author> GetAuthorById(string id)
        {
            return await repository.GetById(id);
        }

        public async Task UpdateAuthor(Author author)
        {

            await repository.Update(author, (oldAuthor, newAuthor) =>
            {
                //oldAuthor.Id = newAuthor.Id; //---> id should not be changed

                oldAuthor.Name = newAuthor.Name;
                oldAuthor.Biography = newAuthor.Biography;
                oldAuthor.PhotoUrl = newAuthor.PhotoUrl;
                oldAuthor.Social = newAuthor.Social;
                oldAuthor.BirthDate = newAuthor.BirthDate;
                oldAuthor.DeathDate = newAuthor.DeathDate;
                oldAuthor.Tags = newAuthor.Tags;
                
                
            });
        }
    }
}
