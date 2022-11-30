using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public enum ReadingStatus { WantToRead, Reading, Completed}

    [Table("FavoriteBooks")]
    public class FavoriteBook
    {
        public int Id { get; set; }

        public virtual  Book? Book { get; set; }

        

        public ReadingStatus ReadingStatus { get; set; }

        public string? Notes { get; set; }

        //optional foreign keys
        public string BookId { get; set; }
        public string UserEmail { get; set; }

    }
}
