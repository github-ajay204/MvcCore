using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Configuration;
using MvcTutorial.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MvcTutorial.Controllers
{
    public class DemoController : Controller
    {
        #region Connection string
        private readonly IConfiguration _configuration;

        public DemoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Get Data from db
        public IActionResult Index()
        {
            List<DemoClass> dataList = new List<DemoClass>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("PRC_GET_PRODUCT", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            DemoClass model = new DemoClass
                            {
                                Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                                Name = dataReader.GetString(dataReader.GetOrdinal("Name"))
                            };
                            dataList.Add(model);
                        }
                    }
                }
            }

            return View(dataList);
        }
       
        #endregion

        #region Add Event code

        #region Add button event
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region Main Add button event
        public IActionResult Add(DemoClass demoClass)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("PRC_INS_PRODUCT", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", demoClass.Id);
                    command.Parameters.AddWithValue("@Name", demoClass.Name);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
        #endregion
        
        #endregion

        #region Update button Code

        #region Click Event on Edit pen button
        public IActionResult GetForEdit(int id)
        {
            DemoClass demoClass;
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("PRC_GETWHERE_PRODUCT", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            demoClass = new DemoClass
                            {
                                Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                                Name = dataReader.GetString(dataReader.GetOrdinal("Name")),
                            };
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }

            return View(demoClass);
        }
        #endregion

        #region Click event on update button
        [HttpPost]
        public IActionResult Edit(DemoClass demoClass)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("PRC_UPD_PRODUCT", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", demoClass.Id);
                    command.Parameters.AddWithValue("@Name", demoClass.Name);
                    connection.Open();
                    command.ExecuteNonQuery();  
                }
            }
            return RedirectToAction("Index");
        }
        #endregion

        #endregion

        #region Delete Event
        public IActionResult Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("PRC_DEL_DEMO", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
