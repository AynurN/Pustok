using Microsoft.EntityFrameworkCore;
using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Core.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {

        public DbSet<TEntity> entities { get;}
        public Task Create(TEntity entity);
        public void Delete(TEntity entity);
        public Task<int> CommitAsync();
        public Task<TEntity> GetById(int id, params string[] includes);
        public Task<TEntity> Get(Expression<Func<TEntity,bool>> expression, params string[] includes);   
        public Task<IQueryable<TEntity>> GetAll(Expression<Func<TEntity,bool>> expression, params string[] includes);   

    }
}
