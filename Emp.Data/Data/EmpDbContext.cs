using Emp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emp.Data.Data
{
    public class EmpDbContext  : DbContext
    {
        public EmpDbContext(DbContextOptions<EmpDbContext> options)
           : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
