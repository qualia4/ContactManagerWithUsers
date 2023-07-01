using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace ContactManagerWithUsers.Pages.Contacts
{
    public class SignUpPageModel : PageModel
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
            try
            {
                string connectionName = "Data Source=WIN-6TSL2R0LRG9\\SQLEXPRESS;Initial Catalog=ContactsManager;Integrated Security=True";
                using SqlConnection connection = new(connectionName);
                {
                    connection.Open();
                    string sql = "INSERT INTO users (username, password) VALUES (@username, @password);";
                    using SqlCommand sqlCommand = new SqlCommand(sql, connection);
                    {
                        sqlCommand.Parameters.AddWithValue("@username", user.Username);
                        sqlCommand.Parameters.AddWithValue("@password", HashUtility.HashString(password));
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            if(user == null)
            {
                errorMessage = "Something went wrong";
                return;
            }
            HttpContext.Session.SetString("Username", user.Username);
            Response.Redirect("/Contacts/Main");
        }
    }
}
