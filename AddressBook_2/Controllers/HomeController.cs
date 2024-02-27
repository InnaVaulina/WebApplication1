using AddressBook_2mvc.Models;
using AddressBook_2mvc.Data;
using Microsoft.AspNetCore.Mvc;
using AddressBook_2mvc.AddressBookException;
using Microsoft.EntityFrameworkCore;
using AddressBook_2mvc.ViewData;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace AddressBook_2mvc.Controllers
{
    
    public class HomeController : Controller
    {

        private readonly INotesCollection _collection;
        private readonly IAddressBookDBContex _context;
        private readonly IIndexViewData _viewLetterPage;

        public HomeController(INotesCollection collection, 
                              IAddressBookDBContex context, 
                              IIndexViewData viewLetterPage)
        {
            _collection = collection;
            _context = context;
            _viewLetterPage = viewLetterPage;
        }


        [HttpGet("{letter?}")]
        [AllowAnonymous]
        [Route("Home/Index")]
        public async Task<IActionResult> Index(string letter)
        {
            if (letter != null) 
                _viewLetterPage.ChooseLetter(letter); 
            
            List<Note> notelist;
            try
            {
                notelist = await _collection.SelectList(_context, _viewLetterPage);
                if(notelist.Count == 0 && _viewLetterPage.Page>0) 
                {
                    _viewLetterPage.ChooseLetter("left");
                    notelist = await _collection.SelectList(_context, _viewLetterPage);
                }
            }
            catch (CustomException ex)
            {
                return NotFound(ex.Message);
            }
  
           
            ViewData["Letter"] = _viewLetterPage.Letter;
            

            return View(notelist);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddNote(Note note)
        {
            try
            {
                await _collection.AddNote(_context, note);
            }
            catch (CustomException ex)
            {
                return NotFound(ex.Message);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Retrieval(Clue clue)
        {
            List<Note> notelist;

            if (clue.ClueText != null || clue.ClueText != "")
            {
                try
                {
                    notelist = await _collection.SelectList(_context, clue.ClueText);
                }
                catch (CustomException ex)
                {
                    return NotFound(ex.Message);
                }
            }
            else
                notelist = new List<Note>();

            return View("Retrieval", notelist);
        }

    }


}
