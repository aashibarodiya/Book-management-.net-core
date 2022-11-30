using BooksWeb.Utils;
using BooksWeb.ViewModels;
using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksWeb.Controllers
{
    [ApiController]
    [Route("/api/books")]
    [ExceptionMapper(ExceptionType =typeof(InvalidIdException),StatusCode =404)]
    public class BookApiController:Controller
    {
        IBookService bookService;
        public BookApiController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await bookService.GetAllBooks());
        }

        [HttpGet("{id}",Name ="GetBookById")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await bookService.GetBookById(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddBook([FromBody] NewBookVM vm)
        {
            var book = new Book()
            {
                Id = vm.Id,
                Title = vm.Title,
                Price = vm.Price,
                Rating = vm.Rating,
                Description = vm.Description,
                Tags = vm.Tags,
                CoverUrl = vm.CoverUrl,
                AuthorId = vm.AuthorId,
                
            };

            var result = await bookService.AddBook(book);

            return Created(Url.Link("GetBookById", new { Id = book.Id }),  result );

        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(string id, [FromBody] NewBookVM vm)
        {
            var book = new Book()
            {
                Id = vm.Id,
                Title = vm.Title,
                Price = vm.Price,
                Rating = vm.Rating,
                Description = vm.Description,
                Tags = vm.Tags,
                CoverUrl = vm.CoverUrl,
                AuthorId = vm.AuthorId,

            };
          

            if (id != book.Id)
                return BadRequest(new
                {
                    message = "current author, url mistmatch",
                    url = Url.Link("GetBookById", new { id = id }),
                    correctUrl = Url.Link("GetBookById", new { id = book.Id }),
                    authorId = book.Id,

                });
            await bookService.UpdateBook(book);
            return Accepted(book);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            await bookService.DeleteBook(id);
            return NoContent();
        }



        [HttpPost("{bookId}/reviews")]
        public async Task<IActionResult> AddReview(string bookId, [FromBody] Review review)
        {
           
            var updatedReview = await bookService.AddReview(review);

            return Created("",updatedReview);          


        }

    
        [HttpGet("{bookId}/reviews")]
        public async Task<IActionResult> GetAllReviews(string bookId)
        {
            var book=await bookService.GetBookById(bookId);

            return Ok(book.Reviews);
        }
    
    }
}
