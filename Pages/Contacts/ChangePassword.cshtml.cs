using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ContactManagerWithUsers.Pages.Contacts
{
    public class ChangePasswordModel : PageModel
    {
        public string successMessage = "";
        public string errorMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            string username = HttpContext.Session.GetString("Username");
            string password = Request.Form["password"];
            string newPassword = Request.Form["newPassword"];
            bool passwordCorrect = false;
            try
            {
                if(newPassword != Request.Form["confirmPassword"])
                {
                    throw new Exception("New passwords are different");
                }
                if (newPassword.Length < 8)
                {
                    throw new Exception("New password should be at least 8 characters");
                }
                if(newPassword == password)
                {
                    throw new Exception("New password should not be the same as your old password");
                }
                string connectionName = "Data Source=WIN-6TSL2R0LRG9\\SQLEXPRESS;Initial Catalog=ContactsManager;Integrated Security=True";
                using SqlConnection connection = new(connectionName);
                {
                    if(username == "" || username == null)
                    {
                        Response.Redirect("/Index");
                    }
                    connection.Open();
                    string sql = "SELECT * FROM users WHERE username=@username";
                    using SqlCommand sqlCommand = new SqlCommand(sql, connection);
                    {
                        sqlCommand.Parameters.AddWithValue("@username", username);
                        using SqlDataReader reader = sqlCommand.ExecuteReader();
                        {
                            while (reader.Read())
                            {
                                if (reader.GetString(2) == HashUtility.HashString(password))
                                {
                                    passwordCorrect = true;
                                }
                            }
                        }
                        reader.Close();
                        if(passwordCorrect)
                        {
                            string sqlUpd = "UPDATE users SET password=@password WHERE username=@username";
                            using SqlCommand sqlUpdCom = new SqlCommand(sqlUpd, connection);
                            {
                                sqlUpdCom.Parameters.AddWithValue("@password", HashUtility.HashString(newPassword));
                                sqlUpdCom.Parameters.AddWithValue("@username", username);
                                sqlUpdCom.ExecuteNonQuery();
                                Response.Redirect("/Contacts/Main");
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
            errorMessage = "Wrong password";
        }
    }
}
