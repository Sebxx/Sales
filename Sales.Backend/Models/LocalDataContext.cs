﻿namespace Sales.Backend.Models
{
    using Domain.Models;

    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Sales.Domain.Models.Product> Products { get; set; }
    }
}