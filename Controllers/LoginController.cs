using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MvcTutorial.Models;
using System.Data;
using System.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MvcTutorial.Controllers
{
    public class LoginController : Controller
    {
        #region Connection string
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }

        public  IActionResult AuthUser(VMLogin ModelLogin)
        {
            bool Isvalid= false;
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("PRC_USER_IUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Mailid", ModelLogin.MailId);
                    command.Parameters.AddWithValue("@Password", ModelLogin.Password);
                    connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            Isvalid = true;
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }
            if(Isvalid == true)
            {
                return RedirectToAction("Index", "Demo");
            }
            return RedirectToAction("NotFound", "Error");
        }
    }
}
