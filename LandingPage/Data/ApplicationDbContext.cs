using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using test.Models;
using DBModels;
using LandingPage.Models.NewsletterUsersViewModels;
using LandingPage.Models.ScriptsViewModels;

namespace test.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<DBModels.NewsletterUsers> NewsletterUsers { get; set; }

        public DbSet<LandingPage.Models.NewsletterUsersViewModels.NewsletterUsersViewModels> NewsletterUsersViewModels { get; set; }

        public DbSet<DBModels.Scripts> Scripts { get; set; }

        public DbSet<LandingPage.Models.ScriptsViewModels.ScriptsViewModels> ScriptsViewModels { get; set; }
    }
}
