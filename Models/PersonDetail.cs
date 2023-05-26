using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBMSAPI.Models
{
    public class PersonDetail
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
       
        public Nullable<int> Age { get; set; }
        public string Gender { get; set; }
        
        public string PhoneNo { get; set; }
        public string BloodGroup { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
    }
}