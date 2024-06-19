using ContactManagementSystemAPI.Models;

namespace ContactManagementSystemAPI.IRepository
{
    public interface IContactRepository : IBaseRepository<Contact>
    {
        public List<Contact> Search(string text);
    }
}
