using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBMSAPI.Models
{
    public class DonorDetail
    {
        public int DonorId { get; set; }
        public string Name { get; set; }
        public Nullable<int> Age { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string BloodGroup { get; set; }
        public int Units { get; set; }
        public Nullable<System.DateTime> DonationDate { get; set; }
        public string Address { get; set; }
    }
}