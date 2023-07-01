using ContactManagerWithUsers.Pages.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace ContactManagerForPUMB.Contacts
{
    public class EditModel : PageModel
    {
        public Contact contact = new Contact();
        public string successMessage = "";
        public string errorMessage = "";
        public void OnGet()
        {
            string id = Request.Query["id"];
            string username = HttpContext.Session.GetString("Username");
            try
            {
                string connectionName = "Data Source=WIN-6TSL2R0LRG9\\SQLEXPRESS;Initial Catalog=ContactsManager;Integrated Security=True";
                using SqlConnection connection = new(connectionName);
                {
                    connection.Open();
                    string sql = "SELECT * FROM contacts WHERE id=@id AND username=@username";
                    using SqlCommand sqlCommand = new SqlCommand(sql, connection);
                    {
                        sqlCommand.Parameters.AddWithValue("@id", id);
                        sqlCommand.Parameters.AddWithValue("@username", username);
                        using SqlDataReader reader = sqlCommand.ExecuteReader();
                        {
                            while (reader.Read())
                            {
                                contact.Id = reader.GetInt32(0);
                                contact.Name = reader.GetString(2);
                                contact.Surname = reader.GetString(3);
                                contact.Email = reader.GetString(4);
                                contact.Phone = reader.GetString(5);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

        }
        public void OnPost()
        {
            string username = HttpContext.Session.GetString("Username");
            contact.Id = Convert.ToInt32(Request.Form["id"]);
            contact.Name = Request.Form["name"];
            contact.Surname = Request.Form["surname"];
            contact.Email = Request.Form["email"];
            contact.Phone = Request.Form["phone"];
            if (contact == null)
            {
                errorMessage = "Something went wrong";
                return;
            }
            try
            {
                string connectionName = "Data Source=WIN-6TSL2R0LRG9\\SQLEXPRESS;Initial Catalog=ContactsManager;Integrated Security=True";
                using SqlConnection connection = new(connectionName);
                {
                    connection.Open();
                    string sql = "UPDATE contacts SET name=@name, surname=@surname, email=@email, phone=@phone WHERE id=@id AND username=@username";
                    using SqlCommand sqlCommand = new SqlCommand(sql, connection);
                    {
                        sqlCommand.Parameters.AddWithValue("@name", contact.Name);
                        sqlCommand.Parameters.AddWithValue("@surname", contact.Surname);
                        sqlCommand.Parameters.AddWithValue("@email", contact.Email);
                        sqlCommand.Parameters.AddWithValue("@phone", contact.Phone);
                        sqlCommand.Parameters.AddWithValue("@id", contact.Id);
                        sqlCommand.Parameters.AddWithValue("@username", username);
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Contacts/Main");
        }
    }
}
