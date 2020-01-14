using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogEngineProject.Models;
using BlogEngineProject.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BlogEngineProject.Repositories
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(
           DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Post> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
