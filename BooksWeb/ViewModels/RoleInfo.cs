using ConceptArchitect.BookManagement.ModelValidators;

namespace BooksWeb
{
    public class RolesInfo
    {
        public string Email { get;  set; }

        [ExistingRoles]
        public List<string> Roles { get; set; } = new List<string>();
    }
}