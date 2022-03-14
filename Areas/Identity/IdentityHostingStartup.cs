using System;
using BloodKoshh.Areas.Identity.Data;
using BloodKoshh.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BloodKoshh.Areas.Identity.IdentityHostingStartup))]
namespace BloodKoshh.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BloodKoshhContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BloodKoshhContextConnection")));

                services.AddDefaultIdentity<BloodKoshhUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<BloodKoshhContext>();
            });
        }
    }
}