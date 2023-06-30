namespace ContactManagerWithUsers.Pages.Contacts
{
    public class User
    {
        public string Username { get; set; }
        public List<Contact> contacts = new List<Contact>();

        public User(int id, string username)
        {
            Username = username;
        }
        public User()
        { }
        public Contact GetContact(int id)
        {
            for(int i = 0; i < contacts.Count; i++)
            {
                if (contacts[i].Id == id)
                {
                    return contacts[i];
                }
            }
            return null;
        }

        public void AddContact(Contact contact)
        {
            contacts.Add(contact);
        }

        public void RemoveContact(int id)
        {
            for (int i = 0; i < contacts.Count; i++)
            {
                if (contacts[i].Id == id)
                {
                    contacts.Remove(contacts[i]);
                }
            }
        }

        public void EditContact(int contactId, Contact editedContact)
        {
            this.RemoveContact(contactId);
            contacts.Add(editedContact);
        }
    }
}
