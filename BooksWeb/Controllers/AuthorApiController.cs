using BooksWeb.Utils;
using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksWeb.Controllers
{

    [ApiController]
    [Route("/api/authors")]
    [ExceptionMapper(ExceptionType = typeof(InvalidIdException), StatusCode = 404)]
    public class AuthorApiController : Controller
    {
        IAuthorService _authorService;
        public AuthorApiController(IAuthorService authorService)
        {
            this._authorService = authorService;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] Author author)
        {

            await _authorService.AddAuthor(author);

            return Created(
                           Url.Link("GetAuthorByIdRoute", new { id = author.Id }),  //where GET request for this resource can be made
                           author);
        }

        [HttpGet("{id}", Name = "GetAuthorByIdRoute")]

        public async Task<IActionResult> GetAuthorById(string id)
        {
            var author = await _authorService.GetAuthorById(id);
            return Ok(author);
        }



        [HttpGet("{id}/v2", Name = "GetAuthorByIdRouteV2")]
        public async Task<IActionResult> GetAuthorByIdV2(string id)
        {
            try
            {
                var author = await _authorService.GetAuthorById(id);
                return Ok(author);

            }
            catch (InvalidIdException ex)
            {
                return NotFound(new { Message = ex.Message, Id = ex.Id });
            }
        }



        [HttpGet("{id}/v1")]
        public async Task<IActionResult> GetAuthorByIdV1(string id)
        {
            var author = await _authorService.GetAuthorById(id);
            if (author != null)
                return Ok(author);
            else
                return NotFound(new { Message = "Author Not Found", Id = id });
        }



        //[HttpGet]
        //public async Task<List<Author>> GetAllAuthors()
        //{
        //   return await _authorService.GetAllAuthors();
        //}


        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAuthors();

            return Ok(from author in authors
                      select new { Id = author.Id, Name = author.Name, PhotoUrl = author.PhotoUrl, TagList = author.TagList });
        }



        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            await _authorService.DeleteAuthor(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(string id, Author author)
        {
            if (id != author.Id)
                return BadRequest(new
                {
                    message = "current author, url mistmatch",
                    url = Url.Link("GetAuthorByIdRoute", new { id = id }),
                    correctUrl = Url.Link("GetAuthorByIdRoute", new { id = author.Id }),
                    authorId = author.Id,

                });
            await _authorService.UpdateAuthor(author);
            return Accepted(author);
        }




    }
}
