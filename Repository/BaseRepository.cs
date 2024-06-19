using ContactManagementSystemAPI.Data;
using ContactManagementSystemAPI.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementSystemAPI.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public ContactDbContext context { get; set; }
        public BaseRepository(ContactDbContext _context) 
        {
            context = _context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await this.context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int Id)
        {
            return await this.context.Set<T>().FindAsync(Id);
        }

        public void Add(T entity)
        {
           this.context.Set<T>().Add(entity);
        }

        public void AddRange(List<T> entity)
        {
            this.context.Set<T>().AddRange(entity);
        }

        public async void Delete(int Id)
        {
            var entity = await this.context.Set<T>().FindAsync(Id);
            if (entity != null)
                this.context.Set<T>().Remove(entity);
        }        

        public void Update(T entity)
        {
            this.context.Set<T>().Update(entity);
        }

        public async Task<int> Commit()
        {
           return await this.context.SaveChangesAsync();
        }
    }
}
