using AddressBook_2mvc.AddressBookException;
using AddressBook_2mvc.Data;
using AddressBook_2mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.CreateData
{
    internal class SurrogateController
    {
        private readonly INotesCollection _collection;
        private readonly IAddressBookDBContex _context;

        public SurrogateController(INotesCollection collection, IAddressBookDBContex context)
        {
            _collection = collection;
            _context = context;
        }


        public async Task<string> Index()
        {
            List<Note> notelist;
            try
            {
                notelist = await _collection.SelectList(_context);
            }
            catch (CustomException ex)
            {
                return $"{Task.CurrentId}: {ex.Message}";
            }

            return $"{Task.CurrentId}: успех";
        }

       
        public async Task<string> AddNote(Note note)
        {
            try
            {
                await _collection.AddNote(_context,note);
            }
            catch (CustomException ex)
            {
                return $"{Task.CurrentId}: {ex.Message}";
            }

            return $"{Task.CurrentId}: успех";
        }



       
        public async Task<string> Single(int id)
        {
            Note note;

            try
            {
                var x = Task<Note>.Run(()=> _collection.SearchNote(_context,id));
                await x;
                note = x.Result;
            }
            catch (CustomException ex)
            {
                return $"{Task.CurrentId}: {ex.Message}";
            }

            return $"{Task.CurrentId}: успех";
  
        }


      
        public async Task<string> ChangeNote(Note note)
        {
            try
            {
                await _collection.ChangeNote(_context, note);
            }
            catch (CustomException ex)
            {
                return $"{Task.CurrentId}: {ex.Message}";
            }

            return $"{Task.CurrentId}: успех";
        }

      
        public async Task<string> DeleteNote(Note note)
        {
            try
            {
                await _collection.DeleteNote(_context,note);
            }
            catch (CustomException ex)
            {
                return $"{Task.CurrentId}: {ex.Message}";
            }

            return $"{Task.CurrentId}: успех";
        }
    }
}
