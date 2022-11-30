using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public static  class AuthorServiceExtensions
    {
        public static async Task<Author?> TryGetAuthorById(this IAuthorService service, string id)
        {
            try
            {
                var author = await service.GetAuthorById(id);
                return author;
            }
            catch(InvalidIdException ex)
            {
                return null;
            }
        }
    }
}
