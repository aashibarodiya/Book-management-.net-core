using ConceptArchitect.BookManagement.ModelValidators;
using ConceptArchitect.Utils.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ConceptArchitect.BookManagement
{
    [AuthorAgeRange(Minimum =10, Maximum =120)]
    //[DataContract]
    public class Author
    {
        [Required]
        //[UniqueAuthorId]
        
        public virtual string Id { get; set; }
       
        
        public virtual List<Book> Books { get; set; } = new List<Book>();
        
        public virtual SocialInfo Social { get; set; } = new SocialInfo();

        [Required]
        public string Name { get; set; }

        [StringLength(2000,MinimumLength =50)]
        [WordCount(Minimum=50, ErrorMessage ="There must be at least 50 words in biography")]
        public string Biography { get; set; }

        [Column("Photograph")]
        public string PhotoUrl { get; set; }
               

        [Required]
        [PastDate(DaysInPast = 3652)]
        
        public DateTime BirthDate { get; set; }

        //"?" indicates optional field
        public DateTime? DeathDate { get; set; }

        public String Tags { get; set; }

        public List<string> TagList => Tags!=null ?Tags.Split(',').ToList(): new List<string>();

        
        
            

    }
}