//using AddressBook_2mvc.Data;
//using AddressBook_2mvc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    //public class MyDBContext: DbContext 
    //{ 
      
    //    public MyDBContext() 
    //    { 
    //    }

    //    public MyDBContext(DbContextOptions<MyDBContext> options)
    //        : base(options)
    //    {
    //    }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        if (!optionsBuilder.IsConfigured)
    //        {
    //           // optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AddressBook_2mvc.Data;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    //        }
    //    }

    //    public DbSet<Note> Note { get; set; } = default!;
    //}


    //public class TestAbstractDb : IAddressBookDBContex
    //{
    //    private readonly MyDBContext _context;

    //    public TestAbstractDb()
    //    {
    //        _context = new MyDBContext();
    //    }

    //    public DbSet<Note> Note
    //    {
    //        get { return _context.Note; }
    //        set { _context.Note = value; }
    //    }

    //    public async Task<int> SaveChangesAsync()
    //    {
    //        return await _context.SaveChangesAsync();
    //    }

    //    public int SaveChanges()
    //    {
    //        return _context.SaveChanges();
    //    }
    //}
}
