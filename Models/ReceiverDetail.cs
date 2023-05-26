using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBMSAPI.Models
{
    public class ReceiverDetail
    {
        public int ReceiverId { get; set; }
        public string Name { get; set; }
        public Nullable<int> Age { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string BloodGroup { get; set; }
        public int Units { get; set; }
        public string HospitalName { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}