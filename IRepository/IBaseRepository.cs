using System.Linq;

namespace ContactManagementSystemAPI.IRepository
{
    public interface IBaseRepository <T> where T : class
    {
        public Task<T> GetById (int Id);
        public Task<IEnumerable<T>> GetAll();
        public void Add(T entity);
        public void AddRange(List<T> entity);
        public void Update(T entity);
        public void Delete(int Id);
        public Task<int> Commit();
    }
}
