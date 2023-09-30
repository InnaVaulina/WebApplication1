using AddressBook_2mvc.Models;
using AddressBook_2mvc.Data;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook_2mvc.Controllers
{
    public class HomeController : Controller
    {

        private readonly INotesCollection _context;


        public HomeController(INotesCollection context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            return View(_context.Notes);
        }

        [HttpPost]
        public IActionResult AddNote(Note note)
        {
            _context.AddNote(note);

            return RedirectToAction("Index");
        }



        [HttpGet("{tab}/{id}")]
        public IActionResult Single(string tab, int id)
        {

            var note = _context.SearchNote(id);

            if (note == null)
            {
                return NotFound();
            }

            ViewData["Tab"] = tab;

            return View(note);
        }


        [HttpPost]
        public IActionResult ChangeNote(Note note)
        {
            _context.ChangeNote(note);
            return RedirectToAction("Single",new { tab = "Details", id = note.Id });
        }

        [HttpPost]
        public IActionResult DeleteNote(Note note)
        {
            _context.DeleteNote(note.Id);
            return RedirectToAction("Index");
        }
    }
}
