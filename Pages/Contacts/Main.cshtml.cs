using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace ContactManagerWithUsers.Pages.Contacts
{
    public class MainModel : PageModel
    {
        public User user { get; set; } = new User();
        public string username;

        public void OnGet()
        {
            try
            {
                string connectionName = "Data Source=WIN-6TSL2R0LRG9\\SQLEXPRESS;Initial Catalog=ContactsManager;Integrated Security=True";
                username = HttpContext.Session.GetString("Username");
                if(username == null || username == "")
                {
                    Response.Redirect("/Index");
                }
                user.Username = username;
                using SqlConnection connection = new(connectionName);
                {
                    connection.Open();
                    string sql = "SELECT * FROM contacts WHERE username=@username";
                    using SqlCommand sqlCommand = new SqlCommand(sql, connection);
                    {
                        sqlCommand.Parameters.AddWithValue("@username", user.Username);
                        using SqlDataReader reader = sqlCommand.ExecuteReader();
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["id"]);
                                string username = reader.GetString(1);
                                string name = reader.GetString(2);
                                string surname = reader.GetString(3);
                                string email = reader.GetString(4);
                                string phone = reader.GetString(5);

                                Contact contact = new Contact(id, username, name, surname, email, phone);
                                user.AddContact(contact);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
