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
    public class ReceiverController : ApiController
    {
        SqlConnection db = new SqlConnection();
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @"select * from ReceiverDetails";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [HttpGet]
        [Route("api/ReceiverDetail/{id}")]
        public IHttpActionResult GetReceiverById(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM ReceiverDetails WHERE ReceiverId = @ReceiverId", connection))
                {
                    command.Parameters.AddWithValue("@ReceiverId", id);
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
            [HttpPost]
        [Route("api/ReceiverDetail")]
        public string Post(ReceiverDetail r)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    var query = @"INSERT INTO ReceiverDetails (Name, Age, Gender, Mobile, BloodGroup, Units, HospitalName, Address,Status) 
                                  VALUES (@Name, @Age, @Gender, @Mobile, @BloodGroup, @Units, @HospitalName, @Address,@Status)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", r.Name);
                        command.Parameters.AddWithValue("@Age", r.Age);
                        command.Parameters.AddWithValue("@Gender", r.Gender);
                        command.Parameters.AddWithValue("@Mobile", r.Mobile);
                        command.Parameters.AddWithValue("@BloodGroup", r.BloodGroup);
                        command.Parameters.AddWithValue("@Units", r.Units);
                        command.Parameters.AddWithValue("@HospitalName", r.HospitalName);
                        command.Parameters.AddWithValue("@Address", r.Address);
                        command.Parameters.AddWithValue("@Status", r.Status);

                        connection.Open();
                        command.ExecuteNonQuery();

                        return "Requested successfully";
                    }
                }
            }
            catch (Exception)
            {
                return "Failed to request blood";
            }
        }

        public string Put(ReceiverDetail donor)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    var query = @"UPDATE ReceiverDetails SET 
                    Name = @Name,
                    Age = @Age,
                    Gender = @Gender,
                    Mobile = @Mobile,
                    BloodGroup = @BloodGroup,
                    Units = @Units,
                    HospitalName = @HospitalName,
                    Address = @Address,
                    Status = @Status
                  WHERE ReceiverId = @ReceiverId";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", donor.Name);
                        command.Parameters.AddWithValue("@Age", donor.Age);
                        command.Parameters.AddWithValue("@Gender", donor.Gender);
                        command.Parameters.AddWithValue("@Mobile", donor.Mobile);
                        command.Parameters.AddWithValue("@BloodGroup", donor.BloodGroup);
                        command.Parameters.AddWithValue("@Units", donor.Units);
                        command.Parameters.AddWithValue("@HospitalName", donor.HospitalName);
                        command.Parameters.AddWithValue("@Address", donor.Address);
                        command.Parameters.AddWithValue("@Status", donor.Status);
                        command.Parameters.AddWithValue("@ReceiverId", donor.ReceiverId);

                        connection.Open();
                        command.ExecuteNonQuery();

                        UpdateStockDetails(connection, donor.Units, donor.BloodGroup);

                        return "Updated Successfully";
                    }
                }
            }
            catch (Exception)
            {
                return "Failed to Update";
            }
        }

        public void UpdateStockDetails(SqlConnection connection, int donorUnits, string bloodGroup)
        {
            try
            {
                string query = @"
            UPDATE StockDetails
            SET Units = Units - @DonorUnits
            WHERE BloodGroup = @BloodGroup";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@DonorUnits", donorUnits); // Use the donorUnits parameter
                    cmd.Parameters.AddWithValue("@BloodGroup", bloodGroup); // Use the bloodGroup parameter

                    cmd.ExecuteNonQuery();
                }

                Console.WriteLine("SUCCESS");
            }
            catch (Exception)
            {
                Console.WriteLine("FAILED");
            }
        }

        public string Delete(int id)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @" delete from ReceiverDetails where ReceiverId =" + id;
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
