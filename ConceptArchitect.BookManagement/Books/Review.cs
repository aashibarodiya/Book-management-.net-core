using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class Review
    {
        public int Id { get; set; }

        //public virtual Book Book { get; set; }

        //optionally also add a foreign key field.
        //Notice ID is all caps. It is convention for Foreign Key in EF
        public string BookId { get; set; }

        public string Email { get; set; }

        [Required]
        public string ReviewerName { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Details { get; set; }

        [Required,Range(1,5)]
        public double Rating { get; set; }


    }
}
