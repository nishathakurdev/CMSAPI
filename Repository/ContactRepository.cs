using ContactManagementSystemAPI.Data;
using ContactManagementSystemAPI.IRepository;
using ContactManagementSystemAPI.Models;

namespace ContactManagementSystemAPI.Repository
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(ContactDbContext _context) : base(_context)
        {

        }

        public List<Contact> Search(string text)
        {
            var query = from p in this.context.Contact
                        where (p.FirstName.ToLower().Contains(text.ToLower())
                              ||  p.LastName.ToLower().Contains(text.ToLower())
                              || p.Email.ToLower().Contains(text.ToLower()))
                        select p;
            return query.ToList();
        }
    }
}
