using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{

    [Table("FavoriteAuthors")]
    public class FavoriteAuthor
    {
        public int Id { get; set; }

        public virtual Author Author { get; set; }

        public string? Notes { get; set; }
    }
}
