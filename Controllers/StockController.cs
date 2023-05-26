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
    public class StockController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @"select StockId, BloodGroup, Units from StockDetails";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
       
            public string Post(StockDetail pat)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"insert into StockDetails(BloodGroup, Units) values('" + pat.BloodGroup + @"','" + pat.Units + @"' )";
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Added Successfully";
            }
            catch (Exception)
            {
                return "Failed to Add";
            }
        }
        public string Put(StockDetail pat)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"update StockDetails set BloodGroup = '" + pat.BloodGroup + @"', Units= '" + pat.Units + @"' where StockId =" + pat.StockId + @"";
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Updated Successfully";
            }
            catch (Exception)
            {
                return "Failed to Update";
            }



        }
        public string Delete(int id)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @" delete from StockDetails where StockId =" + id;
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
