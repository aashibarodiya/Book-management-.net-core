using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Repositories.EF
{
    public class BookEFRepository : IRepository<Book, string>
    {
        BMSContext context;

        public BookEFRepository(BMSContext context)
        {
            this.context = context;
        }
        public async Task<Book> Add(Book entity)
        {
            context.BookList.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Book>> GetAll()
        {
            await Task.CompletedTask;
            return context.BookList.ToList();
        }

        public async Task<Book> GetById(string id)
        {
            var book = await context.BookList.FindAsync(id);
            
            return book ?? throw new InvalidIdException(id);


        }

        public async Task Remove(string id)
        {
            var book = await GetById(id);
            context.BookList.Remove(book);
            await context.SaveChangesAsync();
        }

        public async Task Update(Book entity, Action<Book, Book> mergeOldNew)
        {
            var book = await GetById(entity.Id);
            mergeOldNew(book, entity);
            await context.SaveChangesAsync();

        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
