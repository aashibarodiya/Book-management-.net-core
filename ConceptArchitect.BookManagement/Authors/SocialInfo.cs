using DataAnnotationsExtensions;

namespace ConceptArchitect.BookManagement
{
    public class SocialInfo
    {
        public int Id { get; set; }

        [Email]
        public string? Email { get; set; }

        [Url]
        public string? WebSite { get; set; }

        
    }
}