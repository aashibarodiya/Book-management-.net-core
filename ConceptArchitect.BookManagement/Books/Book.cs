using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConceptArchitect.BookManagement
{
    [Table("Books")]
    public class Book
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public virtual Author Author { get; set; }

        //optionally add a foreign key to author
        public string AuthorId { get; set; }

        public virtual List<Review> Reviews { get; set; } = new List<Review>();

        

        [Range(0,5000)]
        public int Price { get; set; }

        [Range(1,5)]
        public double Rating { get; set; }

        [StringLength(5000,MinimumLength =50)]
        public string Description { get; set; }

        public string CoverUrl { get; set; }

        public string Tags { get; set; }

        public List<string> TagList => Tags != null ? Tags.Split(',').ToList() : new List<string>();
    }
}