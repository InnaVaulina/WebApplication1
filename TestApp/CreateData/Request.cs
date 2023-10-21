using AddressBook_2mvc.Data;
using AddressBook_2mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.CreateData
{

    abstract class Request<T>
        where T : IAddressBookDBContex, new()
    {

        protected readonly INotesCollection _collection;
        protected readonly Note note;

        public Request(INotesCollection collection, Note note)
        {
            _collection = collection;
            this.note = note;
        }

        public abstract Task<string> RequestRun();
    }


    internal class AddRequest<T>: Request<T>
        where T : IAddressBookDBContex, new()
    {
        public AddRequest(INotesCollection _collection, Note note): 
            base(_collection,note) 
        { }


        public override Task<string> RequestRun() 
        {
            var controller = new SurrogateController(_collection, new T());
            return Task<string>.Run(() => controller.AddNote(note));
        }
    }

    internal class ChangeRequest<T> : Request<T>
        where T : IAddressBookDBContex, new()
    {
        public ChangeRequest(INotesCollection _collection, Note note) :
            base(_collection, note)
        { }
       

        public override Task<string> RequestRun()
        {
            var controller = new SurrogateController(_collection, new T());
            return Task<string>.Run(() => controller.ChangeNote(note));
        }
    }

    internal class DeleteRequest<T> : Request<T>
        where T : IAddressBookDBContex, new()
    {
        public DeleteRequest(INotesCollection _collection, Note note) :
            base(_collection, note)
        { }
       
        public override Task<string> RequestRun()
        {
            var controller = new SurrogateController(_collection, new T());
            return Task<string>.Run(() => controller.DeleteNote(note));
        }
    }
}
