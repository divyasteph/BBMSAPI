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
    public class DonorController : ApiController
    {
        SqlConnection db = new SqlConnection();
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @"select * from DonorDetails";
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
        [Route("api/DonorDetail/{id}")]
        public IHttpActionResult GetDonorById(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM DonorDetails WHERE DonorId = @DonorId", connection))
                {
                    command.Parameters.AddWithValue("@DonorId", id);
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
        [Route("api/DonorDetail")]
        public string Post(DonorDetail donor)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    var query = @"INSERT INTO DonorDetails (Name, Age, Gender, Mobile, BloodGroup, Units, DonationDate, Address) 
                                  VALUES (@Name, @Age, @Gender, @Mobile, @BloodGroup, @Units, @DonationDate, @Address)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", donor.Name);
                        command.Parameters.AddWithValue("@Age", donor.Age);
                        command.Parameters.AddWithValue("@Gender", donor.Gender);
                        command.Parameters.AddWithValue("@Mobile", donor.Mobile);
                        command.Parameters.AddWithValue("@BloodGroup", donor.BloodGroup);
                        command.Parameters.AddWithValue("@Units", donor.Units);
                        command.Parameters.AddWithValue("@DonationDate", donor.DonationDate);
                        command.Parameters.AddWithValue("@Address", donor.Address);

                        connection.Open();
                        command.ExecuteNonQuery();
                        UpdateStockDetails(connection, donor.Units, donor.BloodGroup);
                        return "Donated successfully";
                    }
                }
            }
            catch (Exception)
            {
                return "Failed to donate";
            }
        }

        public string Put(DonorDetail donor)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    var query = @"UPDATE DonorDetails SET 
                    Name = @Name,
                    Age = @Age,
                    Gender = @Gender,
                    Mobile = @Mobile,
                    BloodGroup = @BloodGroup,
                    Units = @Units,
                    DonationDate = @DonationDate,
                    Address = @Address
                  WHERE DonorId = @DonorId";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", donor.Name);
                        command.Parameters.AddWithValue("@Age", donor.Age);
                        command.Parameters.AddWithValue("@Gender", donor.Gender);
                        command.Parameters.AddWithValue("@Mobile", donor.Mobile);
                        command.Parameters.AddWithValue("@BloodGroup", donor.BloodGroup);
                        command.Parameters.AddWithValue("@Units", donor.Units);
                        command.Parameters.AddWithValue("@DonationDate", donor.DonationDate);
                        command.Parameters.AddWithValue("@Address", donor.Address);
                        command.Parameters.AddWithValue("@DonorId", donor.DonorId);

                        connection.Open();
                        command.ExecuteNonQuery();

                        // Pass donor.Units and donor.BloodGroup as parameters
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
            SET Units = Units + @DonorUnits
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
                string query = @" delete from DonorDetails where DonorId =" + id;
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
