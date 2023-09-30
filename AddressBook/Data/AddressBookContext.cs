using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AddressBook.Models;

namespace AddressBook.Data
{
    public class AddressBookContext : DbContext
    {
        public AddressBookContext (DbContextOptions<AddressBookContext> options)
            : base(options)
        {
        }

        public DbSet<AddressBook.Models.Note> Note { get; set; } = default!;
    }
}
