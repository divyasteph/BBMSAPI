using BBMSAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BBMSAPI.Controllers
{
    public class PersonController : ApiController
    {
        SqlConnection db = new SqlConnection();
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @"select * from PersonDetails";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [HttpPost]
        [Route("api/PersonDetail")]
        public string Post(PersonDetail r)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    var query = @"INSERT INTO PersonDetails (Name, Age, Gender, PhoneNo, BloodGroup, Address, Email, Type) 
                                  VALUES (@Name, @Age, @Gender, @PhoneNo, @BloodGroup, @Address, @Email,@Type)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", r.Name);
                        command.Parameters.AddWithValue("@Age", r.Age);
                        command.Parameters.AddWithValue("@Gender", r.Gender);
                        command.Parameters.AddWithValue("@PhoneNo", r.PhoneNo);
                        command.Parameters.AddWithValue("@BloodGroup", r.BloodGroup);
                        command.Parameters.AddWithValue("@Address", r.Address);
                        command.Parameters.AddWithValue("@Email", r.Email);
                        command.Parameters.AddWithValue("@Type", r.Type);

                        connection.Open();
                        command.ExecuteNonQuery();

                        return "Person details added successfully";
                    }
                }
            }
            catch (Exception)
            {
                return "Failed to add Person details";
            }
        }

        public string Put(PersonDetail donor)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    var query = @"UPDATE PersonDetails SET 
                            Name = @Name,
                            Age = @Age,
                            Gender = @Gender,
                            PhoneNo = @PhoneNo,
                            BloodGroup = @BloodGroup,  
                            Address = @Address,
                            Email=@Email,
                            Type=@Type
                          WHERE PersonId = @PersonId";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", donor.Name);
                        command.Parameters.AddWithValue("@Age", donor.Age);
                        command.Parameters.AddWithValue("@Gender", donor.Gender);
                        command.Parameters.AddWithValue("@PhoneNo", donor.PhoneNo);
                        command.Parameters.AddWithValue("@BloodGroup", donor.BloodGroup);
                        command.Parameters.AddWithValue("@Address", donor.Address);
                        command.Parameters.AddWithValue("@Email", donor.Email);
                        command.Parameters.AddWithValue("@Type", donor.Type);
                        command.Parameters.AddWithValue("@PersonId", donor.PersonId);

                        connection.Open();
                        command.ExecuteNonQuery();

                        

                        return "Updated Successfully";
                    }
                }
            }
            catch (Exception)
            {
                return "Failed to Update";
            }
        }

        [HttpGet]
        [Route("api/people/{id}")]
        public IHttpActionResult GetPersonById(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM PersonDetails WHERE PersonId = @PersonId", connection))
                {
                    command.Parameters.AddWithValue("@PersonId", id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        return Ok(dataTable);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }

            public string Delete(int id)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @" delete from PersonDetails where PersonId =" + id;
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Deleted Successfully";
            }
            catch (Exception)
            {
                return "Failed to Delete";
            }

        }
    }
}
