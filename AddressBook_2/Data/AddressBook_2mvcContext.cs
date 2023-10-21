using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AddressBook_2mvc.Models;

namespace AddressBook_2mvc.Data
{

    public class AddressBook_2mvcContext : DbContext
    {
        public AddressBook_2mvcContext (DbContextOptions<AddressBook_2mvcContext> options)
            : base(options)
        {
        }

        public DbSet<Note> Note { get; set; } = default!;


    }
}
