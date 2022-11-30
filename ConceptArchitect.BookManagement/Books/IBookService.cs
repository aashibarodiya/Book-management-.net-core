using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public interface IBookService
    {
        Task<Book> AddBook(Book book);
        Task<Book> GetBookById(string id);
        Task<List<Book>> GetAllBooks();

        Task DeleteBook(string id);

        Task UpdateBook(Book book);
        Task<Review> AddReview(Review review);
    }
}
