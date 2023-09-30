using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AddressBook.Data;
using AddressBook.Models;

namespace AddressBook.Pages.Notes
{
    public class CreateModel : PageModel
    {
        private readonly AddressBook.Data.AddressBookContext _context;

        public CreateModel(AddressBook.Data.AddressBookContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Note Note { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Note == null || Note == null)
            {
                return Page();
            }

            _context.Note.Add(Note);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
