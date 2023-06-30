namespace ContactManagerWithUsers.Pages.Contacts
{
    public class Contact
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Contact(int id, int userId , string name, string surname, string email, string phone)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
        }

        public Contact() { }
    }
}
