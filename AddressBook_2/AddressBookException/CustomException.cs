using System;

namespace AddressBook_2mvc.AddressBookException
{
    public class CustomException: Exception
    {
        public CustomException(string message): base(message)
        {    
        }
    }
}
