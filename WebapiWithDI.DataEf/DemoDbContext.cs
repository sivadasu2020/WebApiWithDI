using Microsoft.EntityFrameworkCore;
using System;
using WebapiWithDI.Common;

namespace WebapiWithDI.DataEf
{
    public class DemoDbContext:DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options):base(options)
        {

        }
       public DbSet<Product> Products { get; set; }
    }
}
