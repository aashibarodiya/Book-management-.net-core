using Microsoft.EntityFrameworkCore;

namespace ConceptArchitect.BookManagement.Repositories.EF
{
   
    public class BMSContext : DbContext
    {
        public BMSContext(DbContextOptions opt):base(opt)
        {   
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }


        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> BookList { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<FavoriteAuthor> FavoriteAuthors { get; set; }

        public DbSet<FavoriteBook> FavoriteBooks { get; set; }

    }
}