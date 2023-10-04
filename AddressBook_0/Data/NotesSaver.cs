using System.Text.Json;
using AddressBook_0.Models;

namespace AddressBook_0.Data
{
    public class NotesSaver
    {
        private string fileName;

        private int maxkey;
        public int MaxKey { get { return maxkey; } }


        public NotesSaver(string filename)
        {
            this.fileName = filename;
            maxkey = 0;
        }


        public void SaveNotes(List<Note> notes)
        {

            string notesJson = JsonSerializer.Serialize(notes, typeof(List<Note>));
            StreamWriter file = File.CreateText(fileName);
            file.WriteLine(notesJson);
            file.Close();

        }

        public List<Note> DovnloadNotes()
        {
            List<Note> notes = new List<Note>();
            maxkey = 0;
            if (File.Exists(fileName))
            {
                string data = File.ReadAllText(fileName);
                notes = JsonSerializer.Deserialize<List<Note>>(data);
            }
            foreach (var item in notes)
            {
                if (maxkey < item.Id)
                    maxkey = item.Id;
            }
            return notes;
        }

    }
}
