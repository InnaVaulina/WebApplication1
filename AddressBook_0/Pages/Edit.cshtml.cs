using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using AddressBook_0.Data;
using AddressBook_0.Models;

namespace AddressBook_0.Pages
{
    public class EditModel : PageModel
    {
        private readonly AddressBook_0.Data.INotesCollection _context;

        public EditModel(AddressBook_0.Data.INotesCollection context)
        {
            _context = context;
        }

        [BindProperty]
        public Note Note { get; set; } = default!;


        public IActionResult OnGet(int? id, int? tab)
        {
            if (id == null || _context.Notes == null)
            {
                return NotFound();
            }

            var note = _context.SearchNote((int)id);
            if (note == null) 
            { 
                return NotFound(); 
            }

            Note = note;

            return Page();
        }

        public IActionResult OnPostChangeNote()
        {
            if (!ModelState.IsValid || _context.Notes == null || Note == null)
            {
                return Page();
            }

            _context.ChangeNote(Note);


            return RedirectToPage(new { tab = 1, id = Note.Id }); 
        }

        public IActionResult OnPostDeleteNote()
        {
            if (!ModelState.IsValid || _context.Notes == null || Note == null)
            {
                return Page();
            }

            _context.DeleteNote(Note.Id);


            return RedirectToPage("./Index");
        }
    }
}
