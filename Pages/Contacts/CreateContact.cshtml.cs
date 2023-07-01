using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ContactManagerWithUsers.Pages.Contacts
{
    public class CreateContactModel : PageModel
    {
        public Contact contact = new Contact();
        public string successMessage = "";
        public string errorMessage = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {
            contact.Name = Request.Form["name"];
            contact.Surname = Request.Form["surname"];
            contact.Email = Request.Form["email"];
            contact.Phone = Request.Form["phone"];
            contact.Username = HttpContext.Session.GetString("Username");
            try
            {
                if (contact.Username == null)
                {
                    Response.Redirect("/Index");
                }
                string connectionName = "Data Source=WIN-6TSL2R0LRG9\\SQLEXPRESS;Initial Catalog=ContactsManager;Integrated Security=True";
                using SqlConnection connection = new(connectionName);
                {
                    connection.Open();
                    string sql = "INSERT INTO contacts (username, name, surname, email, phone) VALUES (@username, @name, @surname, @email, @phone);";
                    using SqlCommand sqlCommand = new SqlCommand(sql, connection);
                    {
                        sqlCommand.Parameters.AddWithValue("@username", contact.Username);
                        sqlCommand.Parameters.AddWithValue("@name", contact.Name);
                        sqlCommand.Parameters.AddWithValue("@surname", contact.Surname);
                        sqlCommand.Parameters.AddWithValue("@email", contact.Email);
                        sqlCommand.Parameters.AddWithValue("@phone", contact.Phone);

                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            if (contact == null)
            {
                errorMessage = "Something went wrong";
                return;
            }
            Response.Redirect("/Contacts/Main");
        }
    }
}
