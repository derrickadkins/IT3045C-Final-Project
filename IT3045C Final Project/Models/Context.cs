using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT3045C_Final_Project.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Member> Members { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Singer> Singers { get; set; }
        public object Singer { get; internal set; }
    }
}