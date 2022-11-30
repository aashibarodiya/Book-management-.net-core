using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Repositories.EF
{
    public class UserEFRepository : IRepository<User, string>
    {
        BMSContext context;

        public UserEFRepository(BMSContext context)
        {
            this.context = context;
        }


        public async Task<User> Add(User entity)
        {
            context.Users.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<User>> GetAll()
        {
            await Task.CompletedTask;
            return context.Users.ToList();
        }

        public async Task<User> GetById(string id)
        {
            var user = await context.Users.FindAsync(id);
            
            return user ?? throw new InvalidIdException($"No User with email '{id}'");
            
        }

        public async Task Remove(string id)
        {
            var user= await  context.Users.FindAsync(id);
            context.Users.Remove(user);
            await context.SaveChangesAsync();

        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(User entity, Action<User, User> mergeOldNew)
        {
            var old = await GetById(entity.Email);

            mergeOldNew(old, entity);
            context.SaveChanges();
        }
    }
}
