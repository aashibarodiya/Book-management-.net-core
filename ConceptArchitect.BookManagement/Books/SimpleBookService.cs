using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class SimpleBookService : IBookService
    {
        IRepository<Book, string> repository;

        public SimpleBookService(IRepository<Book,string> repository)
        {
            this.repository = repository;
        }
        public async Task<Book> AddBook(Book book)
        {
            return await repository.Add(book);
        }

        public async Task<Review> AddReview(Review review)
        {
            var book=await GetBookById(review.BookId); 
            book.Reviews.Add(review);
            await repository.Save();
            return review;
        }

        public async Task DeleteBook(string id)
        {
            await repository.Remove(id);
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await repository.GetAll();
        }

        public async Task<Book> GetBookById(string id)
        {
            return await repository.GetById(id);
        }

        public async Task UpdateBook(Book book)
        {
            await repository.Update(book, (o, n) =>
            {
                o.Title = n.Title;
                o.CoverUrl = n.CoverUrl;
                o.Price = n.Price;
                o.Tags = n.Tags;
                o.Description = n.Description;
                o.Rating = n.Rating;
            });
        }
    }
}
