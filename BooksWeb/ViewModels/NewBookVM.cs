using ConceptArchitect.BookManagement;
using ConceptArchitect.BookManagement.ModelValidators;
using System.ComponentModel.DataAnnotations;

namespace BooksWeb.ViewModels
{
    public class NewBookVM 
    {
        public string Id { get; set; }

        public string Title { get; set; }

        [ExistingAuthorId]
        public string AuthorId { get; set; }

        [Range(0, 5000)]
        public int Price { get; set; }

        [Range(1, 5)]
        public double Rating { get; set; }

        [StringLength(5000, MinimumLength = 50)]
        public string Description { get; set; }

        public string CoverUrl { get; set; }

        public string Tags { get; set; }

    }
}
