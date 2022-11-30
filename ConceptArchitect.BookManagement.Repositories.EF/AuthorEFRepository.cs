using ConceptArchitect.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Repositories.EF
{
    public class AuthorEFRepository : IRepository<Author, string>
    {
        BMSContext context;
        public AuthorEFRepository(BMSContext context)
        {
            this.context = context; 
        }


        public async Task<Author> Add(Author entity)
        {
            await context.Authors.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Author>> GetAll()
        {
            await Task.CompletedTask;
            return context.Authors.ToList();
        }


        public async Task<Author> GetById(string id)
        {
            
            var author= await context
                            .Authors
                         //   .Include(a => a.Social)   //include the referenced object details
                            .FirstOrDefaultAsync(a => a.Id == id);

            //if (author == null)
            //    throw new InvalidIdException(id, $"No Author with Id: {id}");
            //else
            //    return author;

            return author ?? throw new InvalidIdException(id, $"No Author with Id:{id}");

        }

        public async Task Remove(string id)
        {
            var author = await GetById(id);
            if (author != null)
            {
                context.Authors.Remove(author);
                await context.SaveChangesAsync();
            }

        }

     

        public async Task Update(Author entity, Action<Author, Author> mergeOldNew)
        {
            var author = await GetById(entity.Id);
            if (author != null)
            {
                mergeOldNew(author, entity);
                await context.SaveChangesAsync();

            }
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
