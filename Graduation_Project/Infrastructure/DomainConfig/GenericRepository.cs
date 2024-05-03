using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Infrastructure.DomainConfig
{
    public class GenericRepository<T, Tkey> : IGenericRepository<T, Tkey>
        where T : Entity<Tkey>
        where Tkey : ValueObjectId
    {

        public readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Add(T entity)
        {
            var res = await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public virtual async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<T> GetById(Tkey id)
        {

            //return await _context.Set<T>().FirstOrDefaultAsync(e => EF.Property<TKey>(e, nameof(AggregateRoot<TKey>.Id.value)).Equals(id.value));
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<Tkey>(e, nameof(Entity<Tkey>.Id))!.Equals(id)) ?? null;
            //return await _context.Set<T>().FindAsync(x =>x.id);
        }



        public virtual async Task<T> Update(T entity)
        {
            var entry = _context.Set<T>().Update(entity);
            return entry.Entity;
        }
    }
}
