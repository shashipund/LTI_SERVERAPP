//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LT_ServerApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TablePriority
    {
        public int ID { get; set; }
        public string TestBenchID { get; set; }
        public string TableName { get; set; }
        public Nullable<int> PriorityID { get; set; }
    
        public virtual TablePriority TablePriority1 { get; set; }
        public virtual TablePriority TablePriority2 { get; set; }
    }
}
