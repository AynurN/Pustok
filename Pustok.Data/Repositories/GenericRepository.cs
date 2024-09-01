using Microsoft.EntityFrameworkCore;
using Pustok.Core.IRepositories;
using Pustok.Core.Models;
using Pustok.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity,new()
    {
        private readonly AppDbContext context;

        public GenericRepository(AppDbContext context)
        {
            this.context = context;
        }

        public DbSet<TEntity> entities => context.Set<TEntity>();

        public async Task<int> CommitAsync()
        {
           return await context.SaveChangesAsync();
        }

        public async Task Create(TEntity entity)
        {
           await entities.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
             entities.Remove(entity);
        }

        public async Task<TEntity> Get(Expression<Func<TEntity,bool>> expression, params string[] includes)
        {
            var query = entities.AsQueryable();
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return await query.Where(expression).FirstOrDefaultAsync();
        }

        public async Task<IQueryable<TEntity>> GetAll(Expression<Func<TEntity,bool>>? expression, params string[]? includes)
        {
            IQueryable<TEntity> query = entities.AsQueryable();
            if(includes is not null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
           
            if(expression is not null)
            {
                query = query.Where(expression);

            }
            
            return  query;
           
        }

        public async Task<TEntity> GetById(int id, params string[] includes)
        {
            IQueryable<TEntity> query = entities.AsQueryable();
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            TEntity? entity = await query.FirstOrDefaultAsync(x => x.Id == id);

            return entity != null ? entity : null;

        }
    }
}
