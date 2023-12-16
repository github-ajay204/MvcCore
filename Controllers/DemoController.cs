using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Configuration;
using MvcTutorial.Models;

namespace MvcTutorial.Controllers
{
    public class DemoController : Controller
    {
        private readonly IConfiguration _configuration;
        public DemoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            List<DemoClass> dataList = new List<DemoClass>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("PRC_GET_PRODUCT", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            DemoClass model = new DemoClass();
                            model.Id = Convert.ToInt32(dataReader["Id"]);
                            model.Name = dataReader["Name"].ToString();
                            dataList.Add(model);
                        }
                    }
                }
            }

            return View(dataList);

        }
    }
}




