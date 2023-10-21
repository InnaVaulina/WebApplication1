using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics.Metrics;

namespace AddressBook_2mvc.ViewData
{
    public interface IIndexViewData 
    {
        string Letter { get; }
        int Page { get; }

        void ChooseLetter(string letter);
    }

    public class IndexViewData: IIndexViewData
    {
        string letter;
        int page;

        public string Letter { get { return letter; } }
        public int Page { get { return page; } }

        private readonly string[] letters = {"А", "Б", "В", "Г", "Д", "Е", "Ж", "З", "И", "К",
                "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Э", "Ю", "Я"};

        public IndexViewData() 
        {
            letter = "all";
            page = 0;
        }

        public void ChooseLetter(string newLetter) 
        {
            switch (newLetter) 
            {
                case "left":
                    if (page > 0) page--;
                    break;
                case "right":
                    page++;
                    break;
                case "all":
                    this.letter = newLetter;
                    page = 0;
                    break;
                default:
                    foreach(string item in letters) 
                    {
                        if (string.Compare(item, newLetter) == 0)
                        {
                            this.letter = newLetter;
                            page = 0;
                        }    
                    }
                    break;
            }
        }


    }
}
