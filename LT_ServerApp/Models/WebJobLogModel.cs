using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LT_ServerApp.Models
{
    public class WebJobLogModel
    {
        public int ID { get; set; }
        public string TestBenchID { get; set; }
        public string TableName { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public string Status { get; set; }
    }
}