using ConceptArchitect.BookManagement;
using ConceptArchitect.BookManagement.Repositories.Ado;
using ConceptArchitect.Utils;
using System.Data.Common;
using System.Data.SqlClient;

namespace BooksWeb.Config
{
    public static class AdoRepositoryConfiguration
    {
        public static IServiceCollection AddAdoRepository(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Author, string>, AdoAuthorRepository>();
            services.AddTransient<DbManager>(); //required by AdoAuthorRepository

            services.AddTransient<Func<DbConnection>>(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                var connectionString = configuration["ConnectionStrings:BooksWebAdo"];

                return () =>
                {
                    DbConnection connection = new SqlConnection();
                    connection.ConnectionString = connectionString;
                    return connection;
                };
            });

            return services;
        }
    }
}
