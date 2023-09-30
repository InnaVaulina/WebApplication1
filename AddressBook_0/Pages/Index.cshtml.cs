using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using AddressBook_0.Data;
using AddressBook_0.Models;

namespace AddressBook_0.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AddressBook_0.Data.INotesCollection _context;

        public IndexModel(AddressBook_0.Data.INotesCollection context)
        {
            _context = context;
        }

        public IList<Note> Notes { get; set; } = default!;

        public void OnGet()
        {
            if (_context.Notes != null)
            {
                Notes = _context.Notes;
            }

        }

        [BindProperty]
        public Note Note { get; set; } = default!;


        public  IActionResult OnPost()
        {
            if (!ModelState.IsValid || _context.Notes == null || Note == null)
            {
                return Page();
            }

            _context.AddNote(Note);
            

            return RedirectToPage("./Index");
        }
    }
}
