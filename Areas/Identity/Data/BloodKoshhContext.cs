using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodKoshh.Areas.Identity.Data;
using BloodKoshh.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BloodKoshh.Data
{
    public class BloodKoshhContext : IdentityDbContext<BloodKoshhUser>
    {
        public BloodKoshhContext(DbContextOptions<BloodKoshhContext> options)
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
        //public virtual DbSet<Address> Addresses { get; set; }
        //public virtual DbSet<District> Districts { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Seeker> Seekers { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<BloodBank> BloodBanks { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Districts> Districts { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationUser> UserNotifications { get; set; }

    }
}
