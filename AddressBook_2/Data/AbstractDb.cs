using Microsoft.EntityFrameworkCore;
using AddressBook_2mvc.Models;

namespace AddressBook_2mvc.Data
{

    public interface IAddressBookDBContex
    {
        DbSet<Note> Note { get; set; }

        Task<int> SaveChangesAsync();

        int SaveChanges();
    }

    public class AbstractDb: IAddressBookDBContex
    {
        private readonly AddressBook_2mvcContext _context;

        public AbstractDb(AddressBook_2mvcContext context)
        {
            _context = context;
        }

        public DbSet<Note> Note 
        { 
            get { return _context.Note; } 
            set { _context.Note = value; }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int SaveChanges() 
        {
            return _context.SaveChanges();
        }
    }
}
