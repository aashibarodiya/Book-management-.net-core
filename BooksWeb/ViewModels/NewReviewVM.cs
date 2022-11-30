using ConceptArchitect.BookManagement.ModelValidators;
using System.ComponentModel.DataAnnotations;

namespace BooksWeb.ViewModels
{
    public class NewReviewVM
    {
        public int Id { get; set; }

        [ExistingBookId]
        public string BookId { get; set; }

        public string Email { get; set; }

        [Required]
        public string ReviewerName { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Details { get; set; }

        [Required, Range(1, 5)]
        public double Rating { get; set; }

    }
}
