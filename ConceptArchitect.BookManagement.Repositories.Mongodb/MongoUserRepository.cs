using BooksWeb.Config;
using ConceptArchitect.Utils;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ConceptArchitect.BookManagement.Repositories.Mongodb
{
    public class MongoUserRepository : IRepository<User, string>
    {
        private readonly IMongoCollection<User> _users;

        public MongoUserRepository(IOptions<MongoConfig> mongoConfig)
        {
            Console.WriteLine($"Connection String: {mongoConfig.Value.ConnectionString}");
            var mongoClient = new MongoClient(mongoConfig.Value.ConnectionString);

            var db = mongoClient.GetDatabase(mongoConfig.Value.DatabaseName);
            _users = db.GetCollection<User>(mongoConfig.Value.UsersCollectionName);
        }

        public async Task<User> Add(User entity)
        {
            await _users.InsertOneAsync(entity);
            return await GetById(entity.Email);
        }

        public async Task<List<User>> GetAll()
        {
            return await _users.Find(_ => true).ToListAsync();
        }

        public async Task<User> GetById(string id)
        {
            var user = await _users.Find(u => u.Email == id).FirstOrDefaultAsync();

            return user ?? throw new InvalidIdException(id, $"Email is not registered: {id} ");

        }

        public async Task Remove(string id)
        {

            var result = await _users.DeleteOneAsync(user => user.Email == id);
            if (result.DeletedCount == 0)
                throw new InvalidIdException(id);
        }

        public async Task Save()
        {
            await Task.CompletedTask;
        }

        public async Task Update(User entity, Action<User, User> mergeOldNew)
        {
            var old = await GetById(entity.Email);
            mergeOldNew(old, entity);

            await _users.ReplaceOneAsync(u => u.Email == entity.Email, old);

        }
    }

}