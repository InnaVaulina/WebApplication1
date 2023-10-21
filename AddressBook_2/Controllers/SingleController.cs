using AddressBook_2mvc.AddressBookException;
using AddressBook_2mvc.Data;
using AddressBook_2mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook_2mvc.Controllers
{
    public class SingleController: Controller
    {
        private readonly INotesCollection _collection;
        private readonly IAddressBookDBContex _context;

        public SingleController(INotesCollection collection, IAddressBookDBContex context)
        {
            _collection = collection;
            _context = context;
        }

        [HttpGet("{tab}/{id}")]
        public IActionResult Single(string tab, int id)
        {
            if (tab == null || id < 0)
                return NotFound();

            Note note;

            try
            {
                note = _collection.SearchNote(_context, id);
            }
            catch (CustomException ex)
            {
                return NotFound(ex.Message);
            }

            ViewData["Tab"] = tab;

            return View(note);
        }





        [HttpPost]
        public async Task<IActionResult> ChangeNote(Note note)
        {
            try
            {
                await _collection.ChangeNote(_context, note);
            }
            catch (CustomException ex)
            {
                return NotFound(ex.Message);
            }

            return RedirectToAction("Single", new { tab = "Details", id = note.Id });
        }




        [HttpPost]
        public async Task<IActionResult> DeleteNote(Note note)
        {
            try
            {
                await _collection.DeleteNote(_context, note);
            }
            catch (CustomException ex)
            {
                return NotFound(ex.Message);
            }

            return RedirectToAction("Index","Home");
        }

    }
}
