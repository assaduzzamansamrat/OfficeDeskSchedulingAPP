using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
        { 
        
        }

       
        public DbSet<User> Users { get; set; }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Desk> Desks { get; set; }

        
    }
}
