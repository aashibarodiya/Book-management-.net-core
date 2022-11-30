using BooksWeb.Utils;
using BooksWeb.ViewModels;
using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;

namespace BooksWeb.Controllers
{
    public class AuthorController:Controller
    {
        IAuthorService authorService;
        IDataSeeder<Author> seeder;
       
        public AuthorController(IAuthorService authorService, IDataSeeder<Author> seeder)
        {
           this.authorService = authorService;
           this.seeder = seeder;
        }

        public async Task<ActionResult> Seed()
        {
            await seeder.Seed();
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> Index()
        {
            var authors =await  authorService.GetAllAuthors();
            return View(authors);
        }

        [HttpGet]
        public ActionResult Create()
        {    
            
            return View(new NewAuthorVM()); //create a view for empty author object model
        }


        [HttpPost]
        [Submit(ModelName ="vm")]
        public async Task Create(NewAuthorVM vm)
        {
            
            var author = new Author()
            {
                Id = vm.Id,
                Name = vm.Name,
                Biography = vm.Biography,
                PhotoUrl = vm.PhotoUrl,
                BirthDate = vm.BirthDate,
                DeathDate = vm.DeathDate
            };

            await authorService.AddAuthor(author);
            
        }


        [HttpPost]
        public async Task<ActionResult> CreateV1(NewAuthorVM vm)
        {
            if(ModelState.IsValid)
            {
                var author = new Author()
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Biography = vm.Biography,
                    PhotoUrl = vm.PhotoUrl,
                    BirthDate = vm.BirthDate,
                    DeathDate = vm.DeathDate
                };
                
                await authorService.AddAuthor(author);
                return RedirectToAction("Details", new { Id = vm.Id });

            }
            else
            {
                //we have wrong values
                HttpContext.Response.StatusCode = 400;
                return View(vm);
            }
        }

        public async Task<ActionResult> Add3(Author author)
        {
            await authorService.AddAuthor(author); 
            return RedirectToAction("Details", new {Id=author.Id});
        }

        public async Task<ActionResult> Add2(string id, string name, string bio, 
            DateTime dob,DateTime?deathDate, string photoUrl, string? email, string? website)
        {
            var author = new Author()
            {
                Id = id,
                Name = name,
                Biography = bio,
                BirthDate = dob,
                DeathDate = deathDate,
                PhotoUrl = photoUrl,
                Social = new SocialInfo()
                {
                    Email = email,
                    WebSite = website
                }
            };
            await authorService.AddAuthor(author);
            return RedirectToAction("Details", new { id = author.Id });
        }

        public ActionResult Add1()
        {
            var name= HttpContext.Request.Form["name"];
            var birthDate =DateTime.Parse( HttpContext.Request.Form["dob"].ToString());

            return Content($"Author Added\nName={name}\tBirthDate={birthDate}");
        }

        [NullModelIs404]
        public async Task<ActionResult> Details(string id)
        {
            var author = await authorService.TryGetAuthorById(id);

            
            return View(author);
        }
        public async Task<ActionResult> Edit(string id)
        {
            var author = await authorService.TryGetAuthorById(id);
            return View(author);
        }

        [HttpPost]
        [Submit]
        public async Task Edit(Author author)
        {
             await authorService.UpdateAuthor(author);
        }

        public async Task< ActionResult> Delete(string id)
        {
            var author= await authorService.GetAuthorById(id);
            return View(author);
        }

        public async Task<ActionResult> ConfirmDelete(string id)
        {
            await authorService.DeleteAuthor(id);
            return RedirectToAction("Index");
        }
    }
}
