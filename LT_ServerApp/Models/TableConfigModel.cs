using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LT_ServerApp.Models
{
    public class TableConfigModel
    {
        public string TestBenchID { get; set; }
        public string TestBenchName { get; set; }
        public string TableName { get; set; }
        public string PriorityName { get; set; }
        public string Frequency{ get; set; }
    }
}