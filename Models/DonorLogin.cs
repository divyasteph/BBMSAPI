using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBMSAPI.Models
{
    public class DonorLogin
    {
        public int DonorId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Nullable<int> Age { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
    }
}