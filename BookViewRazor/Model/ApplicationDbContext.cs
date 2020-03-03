using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookViewRazor.Model
{
    public class ApplicationDbContext : DbContext 
    {
        // this is an empty constructor in which it parameters are needed for DI
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Models that will be added to the DB
        public DbSet<Book> Book { get; set; }
    }
}
