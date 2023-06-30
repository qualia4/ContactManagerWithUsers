using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace ContactManagerWithUsers.Pages.Contacts
{
    public class SignInPageModel : PageModel
    {
        public User user = new User();
        public string successMessage = "";
        public string errorMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            user.Username = Request.Form["username"];
            string password = Request.Form["password"];
            bool passwordCorrect = false;
            bool usernameExists = false;
            try
            {
                string connectionName = "Data Source=WIN-6TSL2R0LRG9\\SQLEXPRESS;Initial Catalog=ContactsManager;Integrated Security=True";
                using SqlConnection connection = new(connectionName);
                {
                    connection.Open();
                    string sql = "SELECT * FROM users WHERE username=@username";
                    using SqlCommand sqlCommand = new SqlCommand(sql, connection);
                    {
                        sqlCommand.Parameters.AddWithValue("@username", user.Username);
                        using SqlDataReader reader = sqlCommand.ExecuteReader();
                        {
                            while (reader.Read())
                            {
                                user.Username = reader.GetString(1);
                                usernameExists = true;
                                if (reader.GetString(2) == password)
                                {
                                    passwordCorrect = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            if (usernameExists && passwordCorrect)
            {
                HttpContext.Session.SetString("Username", user.Username);
                Response.Redirect("/Contacts/Main");
            }
            else if(!usernameExists)
            {
                errorMessage = "Wrong username";
                return;
            }
            errorMessage = "Wrong password";     
        }
    }
}
