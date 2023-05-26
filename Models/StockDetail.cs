using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBMSAPI.Models
{
    public class StockDetail
    {
        public int StockId { get; set; }
        public string BloodGroup { get; set; }
        public int Units { get; set; }
    }
}