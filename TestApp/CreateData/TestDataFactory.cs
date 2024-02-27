//using AddressBook_2mvc.Controllers;
//using AddressBook_2mvc.Data;
//using AddressBook_2mvc.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestApp.CreateData
{
    internal class TestDataFactory
    {
        private readonly INotesCollection _collection;
        public TestDataFactory(INotesCollection collection) 
        {
            _collection = collection;
        }

        public void TestRun()
        {

            AddRequest<TestAbstractDb> add1 = new AddRequest<TestAbstractDb>(_collection, new Note()
            {
                Id = 0,
                FamilyName = $"M0",
                Name = $"S0",
                Tel = $"5550"
            });
            var x1 = add1.RequestRun();

            ChangeRequest<TestAbstractDb> ch1 = new ChangeRequest<TestAbstractDb>(_collection, new Note()
            {
                Id = 77,
                FamilyName = $"M1",
                Name = $"S1",
                Tel = $"5551"
            });
            var x2 = ch1.RequestRun();

            ChangeRequest<TestAbstractDb> ch2 = new ChangeRequest<TestAbstractDb>(_collection, new Note()
            {
                Id = 75,
                FamilyName = $"M1",
                Name = $"S1",
                Tel = $"5551"
            });
            var x3 = ch2.RequestRun();

            DeleteRequest<TestAbstractDb> del1 = new DeleteRequest<TestAbstractDb>(_collection, new Note()
            {
                Id = 76,
                FamilyName = $"M1",
                Name = $"S1",
                Tel = $"5551"
            });
            var x4 = del1.RequestRun();

            AddRequest<TestAbstractDb> add2 = new AddRequest<TestAbstractDb>(_collection, new Note()
            {
                Id = 0,
                FamilyName = $"M0",
                Name = $"S0",
                Tel = $"5550"
            });
            var x5 = add2.RequestRun();

            ChangeRequest<TestAbstractDb> ch3 = new ChangeRequest<TestAbstractDb>(_collection, new Note()
            {
                Id = 77,
                FamilyName = $"M1",
                Name = $"S1",
                Tel = $"5551"
            });
            var x6 = ch3.RequestRun();

            ChangeRequest<TestAbstractDb> ch4 = new ChangeRequest<TestAbstractDb>(_collection, new Note()
            {
                Id = 75,
                FamilyName = $"M1",
                Name = $"S1",
                Tel = $"5551"
            });
            var x7 = ch4.RequestRun();

            DeleteRequest<TestAbstractDb> del2 = new DeleteRequest<TestAbstractDb>(_collection, new Note()
            {
                Id = 76,
                FamilyName = $"M1",
                Name = $"S1",
                Tel = $"5551"
            });
            var x8 = del2.RequestRun();

            DeleteRequest<TestAbstractDb> del3 = new DeleteRequest<TestAbstractDb>(_collection, new Note()
            {
                Id = 77,
                FamilyName = $"M1",
                Name = $"S1",
                Tel = $"5551"
            });
            var x9 = del3.RequestRun();

            Console.WriteLine($"{x1.Result}");
            Console.WriteLine($"{x2.Result}");
            Console.WriteLine($"{x3.Result}");
            Console.WriteLine($"{x4.Result}");
            Console.WriteLine($"{x5.Result}");
            Console.WriteLine($"{x6.Result}");
            Console.WriteLine($"{x7.Result}");
            Console.WriteLine($"{x8.Result}");
            Console.WriteLine($"{x9.Result}");

        }
    }
}
