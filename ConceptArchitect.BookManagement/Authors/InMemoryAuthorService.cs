using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class InMemoryAuthorService : IAuthorService
    {
        List<Author> authors=new List<Author>();
        public async Task<Author> AddAuthor(Author author)
        {
            await Task.Factory.StartNew(() => authors.Add(author));
            return author;
        }

        public async Task DeleteAuthor(string id)
        {
            var author = await GetAuthorById(id);
            authors.Remove(author);
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await Task.Factory.StartNew(() => authors);
        }

        public async Task<Author> GetAuthorById(string id)
        {
            await Task.CompletedTask;
            var author= authors.FirstOrDefault(a => a.Id == id);
            return author ??  throw new InvalidIdException(id);
        }

        public async Task UpdateAuthor(Author author)
        {
            await Task.CompletedTask;
        }
    }
}
