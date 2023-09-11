using JuniorWeb.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorWeb.DetaAccess
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<JuniorWebDbContext>
    {
            public JuniorWebDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<JuniorWebDbContext>();
                optionsBuilder.UseSqlServer("Data Source=WIN7-PC\\SQLEXPRESS;Initial Catalog=Hospitals;Integrated Security=True;TrustServerCertificate=True");

                return new JuniorWebDbContext(optionsBuilder.Options);
            }
    
    }
}
