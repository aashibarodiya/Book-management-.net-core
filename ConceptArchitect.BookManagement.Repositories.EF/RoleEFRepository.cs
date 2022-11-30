using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Repositories.EF
{
    public class RoleEFRepository : IRepository<Role, string>
    {
        BMSContext context;
        public RoleEFRepository(BMSContext context)
        {
            this.context = context;
        }
        public async Task<Role> Add(Role entity)
        {
            context.Roles.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Role>> GetAll()
        {
           
            return await Task.Factory.StartNew(()=>context.Roles.ToList()); 
        }

        public async Task<Role> GetById(string id)
        {
            var role = await context.Roles.FindAsync(id);

            return role ?? throw new InvalidIdException($"No Such Role: {id}");
        }

        public async Task Remove(string id)
        {
            var role = await GetById(id);
            context.Roles.Remove(role);

        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(Role entity, Action<Role, Role> mergeOldNew)
        {
            var existing = await GetById(entity.Name);
            mergeOldNew(existing, entity);
            await context.SaveChangesAsync();
        }
    }
}
