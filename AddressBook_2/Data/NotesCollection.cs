using AddressBook_2mvc.Models;
using AddressBook_2mvc.AddressBookException;
using Microsoft.EntityFrameworkCore;
using AddressBook_2mvc.ViewData;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AddressBook_2mvc.Data
{
    public interface INotesCollection
    {
        Task AddNote(IAddressBookDBContex _context, Note note);
        Task ChangeNote(IAddressBookDBContex _context, Note note);
        Task DeleteNote(IAddressBookDBContex _context, Note note);
        Note SearchNote(IAddressBookDBContex _context, int id);
        Task<List<Note>> SelectList(IAddressBookDBContex _context, IIndexViewData page);
        Task<List<Note>> SelectList(IAddressBookDBContex _context, string clue);

    }


    public class NotesCollection: INotesCollection
    {
        public NotesCollection()
        {}

        public async Task AddNote(IAddressBookDBContex context, Note note)
        {
            try
            {
                context.Note.Add(note);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new CustomException("не удалось добавить строку");
            }
             
        }

    

        public async Task ChangeNote(IAddressBookDBContex context, Note note)
        {
            var desired = context.Note.
                Where(n => n.Id == note.Id).
                FirstOrDefault();

            if (desired != null)
            {
                try
                {
                    desired.FamilyName = note.FamilyName;
                    desired.Name = note.Name;
                    desired.PatronymicName = note.PatronymicName;
                    desired.Tel = note.Tel;
                    desired.Address = note.Address;
                    desired.Description = note.Description;
                    context.Note.Update(desired);
                    await context.SaveChangesAsync();
                }
                catch
                {
                    throw new CustomException("не удалось изменить данные");
                }
            }
            else throw new CustomException("требуемая для изменения строка не найдена");
        }

    


        public async Task DeleteNote(IAddressBookDBContex context, Note note)
        {
            var desired = context.Note.
                 Where(n => (n.Id == note.Id)).
                 FirstOrDefault();
            if (desired != null)
            {
                try
                {
                    context.Note.Remove(desired);
                    await context.SaveChangesAsync();
                }
                catch
                {
                    throw new CustomException("не удалось удалить данные");
                }
            }
            else throw new CustomException("требуемая для удаления строка не найдена");
        }



        public  Note SearchNote(IAddressBookDBContex context, int id)
        {
            var desired = context.Note.
            Where(n => n.Id == id).
            FirstOrDefault();

            if ( desired != null)
            {
                return desired;
            }
            else throw new CustomException("требуемая строка не найдена в бд");
        }


        public async Task<List<Note>> SelectList(IAddressBookDBContex context, IIndexViewData page)
        {
            if(string.Compare(page.Letter, "all") == 0)
                return await context.Note.Skip(page.Page*8).Take(8).ToListAsync();

            return await context.Note.
                Where(n => EF.Functions.Like(n.FamilyName!, page.Letter + "%")).
                Skip(page.Page * 8).Take(8).ToListAsync();
        }


        public async Task<List<Note>> SelectList(IAddressBookDBContex context, string clue)
        {
            return await context.Note.
                Where(n => EF.Functions.Like(n.FamilyName, "%" + clue + "%")|| 
                           EF.Functions.Like(n.Name, "%" + clue + "%") ||
                           EF.Functions.Like(n.PatronymicName!, "%" + clue + "%") ||
                           EF.Functions.Like(n.Tel, "%" + clue + "%") ||
                           EF.Functions.Like(n.Address!, "%" + clue + "%") ||
                           EF.Functions.Like(n.Description!, "%" + clue + "%")).
                ToListAsync();
        }

    }

}
