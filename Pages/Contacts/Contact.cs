namespace ContactManagerWithUsers.Pages.Contacts
{
    public class Contact
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Contact(int id, string username , string name, string surname, string email, string phone)
        {
            Id = id;
            Username = username;
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
        }

        public Contact() { }
    }
}
