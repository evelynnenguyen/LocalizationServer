using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LocalizationServer;

namespace LocalizationServer.Data
{
    public class LocalizationServerContext : DbContext
    {
        public LocalizationServerContext (DbContextOptions<LocalizationServerContext> options)
            : base(options)
        {
        }

        public DbSet<LocalizationServer.Student> Student { get; set; }
    }
}
