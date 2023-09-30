namespace AddressBook.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string FamilyName { get; set; }
        public string Name { get; set; }
        public string? PatronymicName { get; set; }

        public string Tel {  get; set; }

        public string? Address {  get; set; }
        public string? Description {  get; set; }

    }
}
