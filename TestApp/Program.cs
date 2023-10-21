// See https://aka.ms/new-console-template for more information
using AddressBook_2mvc.Data;
using Microsoft.EntityFrameworkCore;
using System;
using TestApp;
using TestApp.CreateData;

Console.WriteLine("Hello, World!");



TestDataFactory factory = new TestDataFactory(new NotesCollection());
factory.TestRun();

Console.ReadLine();



