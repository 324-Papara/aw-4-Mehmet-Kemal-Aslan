using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Para.Data.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task Save();
        Task<TEntity?> GetById(int Id);
        //Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task Delete(int Id);
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> expression, params string[] includes);
        Task<List<TEntity>> GetAll(params string[] includes);
        Task<TEntity?> GetById(long Id, params string[] includes);
        Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> expression, params string[] includes);
        Task<TEntity> Insert(TEntity entity);
    }
}
