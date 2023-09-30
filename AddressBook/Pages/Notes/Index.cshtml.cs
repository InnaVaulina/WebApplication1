using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AddressBook.Data;
using AddressBook.Models;

namespace AddressBook.Pages.Notes
{
    public class IndexModel : PageModel
    {
        private readonly AddressBook.Data.AddressBookContext _context;

        public IndexModel(AddressBook.Data.AddressBookContext context)
        {
            _context = context;
        }

        public IList<Note> Note { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Note != null)
            {
                Note = await _context.Note.ToListAsync();
            }
        }
    }
}
