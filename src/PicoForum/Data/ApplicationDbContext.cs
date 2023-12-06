using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PicoForum.Models;

namespace PicoForum.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Models.PFSection> Sections { get; set; }
        public DbSet<Models.PFPost> Posts { get; set; }
        public DbSet<PFReply> Replies { get; set; }
    }
}