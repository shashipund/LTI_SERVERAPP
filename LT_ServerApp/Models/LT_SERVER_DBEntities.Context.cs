﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class LT_SERVER_DBEntities1 : DbContext
    {
        public LT_SERVER_DBEntities1()
            : base("name=LT_SERVER_DBEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<TablePriority> TablePriorities { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<TestBenchDetail> TestBenchDetails { get; set; }
        public virtual DbSet<TablePriorityView> TablePriorityViews { get; set; }
        public virtual DbSet<TableColumnInfo> TableColumnInfoes { get; set; }
        public virtual DbSet<tblWebJobLog> tblWebJobLogs { get; set; }
        public virtual DbSet<tblErrorLog> tblErrorLogs { get; set; }
        public virtual DbSet<BackUpDetail> BackUpDetails { get; set; }
    
        public virtual int CreateTable(string query, string tableName)
        {
            var queryParameter = query != null ?
                new ObjectParameter("query", query) :
                new ObjectParameter("query", typeof(string));
    
            var tableNameParameter = tableName != null ?
                new ObjectParameter("tableName", tableName) :
                new ObjectParameter("tableName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateTable", queryParameter, tableNameParameter);
        }
    
        public virtual int CreateTable1(string query, string tableName)
        {
            var queryParameter = query != null ?
                new ObjectParameter("query", query) :
                new ObjectParameter("query", typeof(string));
    
            var tableNameParameter = tableName != null ?
                new ObjectParameter("tableName", tableName) :
                new ObjectParameter("tableName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateTable1", queryParameter, tableNameParameter);
        }
    
        public virtual int CreateType(string query, string tableName)
        {
            var queryParameter = query != null ?
                new ObjectParameter("query", query) :
                new ObjectParameter("query", typeof(string));
    
            var tableNameParameter = tableName != null ?
                new ObjectParameter("tableName", tableName) :
                new ObjectParameter("tableName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateType", queryParameter, tableNameParameter);
        }
    
        public virtual int CreateType1(string query, string tableName)
        {
            var queryParameter = query != null ?
                new ObjectParameter("query", query) :
                new ObjectParameter("query", typeof(string));
    
            var tableNameParameter = tableName != null ?
                new ObjectParameter("tableName", tableName) :
                new ObjectParameter("tableName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateType1", queryParameter, tableNameParameter);
        }
    
        public virtual int CreateDatabase(string tableName)
        {
            var tableNameParameter = tableName != null ?
                new ObjectParameter("tableName", tableName) :
                new ObjectParameter("tableName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateDatabase", tableNameParameter);
        }
    }
}
