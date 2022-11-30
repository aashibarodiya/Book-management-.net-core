using ConceptArchitect.BookManagement;
using ConceptArchitect.BookManagement.Repositories.Mongodb;
using ConceptArchitect.Utils;

namespace BooksWeb.Config
{
    public static class MongoRepositoryConfiguration
    {
        public static IServiceCollection AddMongoRepository(this IServiceCollection collection, IConfiguration configuration)
        {
            collection
                .Configure<MongoConfig>(
                    configuration.GetSection("MongoConfig")
                );

            collection.AddTransient<IRepository<User, string>, MongoUserRepository>();

            return collection;
        }
    }
}
