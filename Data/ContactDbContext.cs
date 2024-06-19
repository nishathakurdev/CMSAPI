using ContactManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementSystemAPI.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options) { }

        public DbSet<Contact> Contact { get; set; }

        internal Task FindAsync(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
