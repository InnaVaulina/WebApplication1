﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AddressBook_0.Models;

namespace AddressBook_0.Data
{

    public interface INotesCollection 
    {
        List<Note> Notes { get; set; }

        void AddNote(Note note);

        void ChangeNote(Note note);

        void DeleteNote(int id);

        Note SearchNote(int id);

    }

    public class NotesList: INotesCollection
    {
        public List<Note> Notes { get; set; }

        private static int key = 0;


        public void AddNote(Note note) 
        {
            note.Id = key++;
            Notes.Add(note);
        }

        public void ChangeNote(Note note) 
        {
            for(int i=0; i<Notes.Count; i++) 
            {
                if (Notes[i].Id == note.Id)
                {
                    Notes[i] = note;
                    break;
                }
            }
        }

        public void DeleteNote(int id) 
        {
            for (int i = 0; i < Notes.Count; i++)
            {
                if (Notes[i].Id == id)
                {
                    Notes.Remove(Notes[i]);
                    break;
                }
            }
        }

        public Note SearchNote(int id) 
        {
            foreach (Note item in Notes)
                if (item.Id == id) return item;
            return null;
        }

        public NotesList() 
        {
            Notes = new List<Note>();

            Notes.Add(new Note()
            {
                Id = key++,
                FamilyName = "Журавкин",
                Name = "Евгений",
                Tel = "+79787775551",
                Description = "долг 500р."
            });

            Notes.Add(new Note()
            {
                Id = key++,
                FamilyName = "Кара-Гуяр",
                Name = "Людмила",
                PatronymicName = "Марковна",
                Tel = "+79787775552",
                Description = "долг 100р."
            });

            Notes.Add(new Note()
            {
                Id = key++,
                FamilyName = "Санаев",
                Name = "Сергей",
                Tel = "+79787775553",
                Address = "г. Севастополь, ул. Адмирала Юмашева дом 24, кв. 75",
                Description = "долг 20р."
            });

            Notes.Add(new Note()
            {
                Id = key++,
                FamilyName = "Шестакова",
                Name = "Людмила",
                Tel = "+79787775554",
                Description = "долг 20р."
            });

        }
    }
}
