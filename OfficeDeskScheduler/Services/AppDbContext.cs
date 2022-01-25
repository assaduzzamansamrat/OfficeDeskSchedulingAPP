using Microsoft.EntityFrameworkCore;
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
      

        public DbSet<User> Users { get; set; }
    }
}
