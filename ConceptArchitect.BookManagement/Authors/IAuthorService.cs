using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public interface IAuthorService
    {
        public Task<Author> AddAuthor(Author author);
        public Task<Author> GetAuthorById(string id);
        public Task<List<Author>> GetAllAuthors();

        public Task DeleteAuthor(string id);

        public Task UpdateAuthor(Author author);
    }
}
