using ConceptArchitect.BookManagement;
using ConceptArchitect.BookManagement.ModelValidators;

namespace BooksWeb.ViewModels
{
    public class NewAuthorVM :Author
    {
        [UniqueAuthorId]
        public override string Id { get => base.Id; set => base.Id = value; }
    }
}
