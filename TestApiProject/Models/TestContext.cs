using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestApiProject.Models
{
    public class TestContext:DbContext
    {
        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
        }

       // public DbSet<TestItem> TestItems { get; set; }
        public DbSet<ApiTestItem> TestItems { get; set; }
    }
}
