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
    [RoutePrefix("api/Receiver")]
    public class ReceiverLoginController : ApiController
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        [HttpPost]
        [Route("Registration")]
        public string Registration(ReceiverLogin donor)
        {
            string msg = string.Empty;
            try
            {
                cmd = new SqlCommand("Receiver_Registration", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", donor.Name);
                cmd.Parameters.AddWithValue("@Password", donor.Password);
                cmd.Parameters.AddWithValue("@Age", donor.Age);
                cmd.Parameters.AddWithValue("@Gender", donor.Gender);
                cmd.Parameters.AddWithValue("@Email", donor.Email);
                cmd.Parameters.AddWithValue("@Mobile", donor.Mobile);
                cmd.Parameters.AddWithValue("@Address", donor.Address);


                //SqlParameter outParam = new SqlParameter("@IsExist", SqlDbType.Int);
                //outParam.Direction = ParameterDirection.Output;
                //cmd.Parameters.Add(outParam);

                //conn.Open();
                //int isExist = cmd.ExecuteNonQuery();
                //conn.Close();

                //isExist = Convert.ToInt32(cmd.Parameters["@IsExist"].Value);
                //if (isExist == 1)
                //{
                //    msg = "User already exists.";
                //}
                //else
                //{
                //    msg = "Registration successful!";
                //}
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                if (i > 0)
                {
                    msg = "Registered Successfully..!";

                }
                else
                {
                    msg = "Error";
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        [HttpPost]
        [Route("Login")]
        public string Login(ReceiverLogin donor)
        {
            string msg = string.Empty;
            try
            {
                da = new SqlDataAdapter("Receiver_Login", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Name", donor.Name);
                da.SelectCommand.Parameters.AddWithValue("@Password", donor.Password);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    msg = "Login successful!";

                }
                else
                {
                    msg = "Invalid login credentials.";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
    }
}
