using MaquininhaTheos.Data.Context;
using MaquininhaTheos.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaquininhaTheos.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly MaquininhaTheosContext _context;

        public Repository(MaquininhaTheosContext context)
        {
            _dbSet = context.Set<TEntity>();
            _context = context;
        }

        public async Task AdicionarAsync(TEntity obj)
        {
            await _dbSet.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(TEntity obj)
        {
            _dbSet.Update(obj);
        }

        public async Task RemoverAsync(TEntity obj)
        {
            _dbSet.Remove(obj);
        }
       
        public async Task<IEnumerable<TEntity>> ListarAsync() => await _dbSet.ToListAsync();
   
    }
}
