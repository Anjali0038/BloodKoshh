using BloodKoshh.Models;
using BloodKoshh.Areas.Identity.Data;
using BloodKoshh.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodKosh.Service;
using BloodKoshh.Repository;
using BloodKoshh.Service;

namespace BloodKoshh
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<BloodKoshhUser>>();
            IdentityResult roleResult, roleResult1;
            //Adding Addmin Role  
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                //create the roles and seed them to the database  
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }
            //Assign Admin role to the main User here we have given our newly loregistered login id for Admin management  
            BloodKoshhUser user = await UserManager.FindByEmailAsync("admin@gmail.com");
            var User = new BloodKoshhUser();
            await UserManager.AddToRoleAsync(user, "Admin");
            var roleCheckDonor = await RoleManager.RoleExistsAsync("Donor");
            if (!roleCheckDonor)
            {
                //create the roles and seed them to the database  
                roleResult1 = await RoleManager.CreateAsync(new IdentityRole("Donor"));
            }
            var roleCheckSeeker = await RoleManager.RoleExistsAsync("Seeker");
            if (!roleCheckSeeker)
            {
                //create the roles and seed them to the database  
                roleResult1 = await RoleManager.CreateAsync(new IdentityRole("Seeker"));
            }
            var roleCheckbloodbank = await RoleManager.RoleExistsAsync("BloodBank");
            if (!roleCheckSeeker)
            {
                //create the roles and seed them to the database  
                roleResult1 = await RoleManager.CreateAsync(new IdentityRole("BloodBank"));
            }

        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapProfile));
            services.AddDbContext<BloodKoshhContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddControllersWithViews();
            services.AddScoped<IBloodProvider, BloodProvider>();
            services.AddScoped<IDonorRepository, DonorRepository>();
            services.AddScoped<ISeekerRepository, SeekerRepository>();
            services.AddScoped<IBloodBankRepository, BloodBankRepository>();
            services.AddScoped<IEventRepository, EventRepository>();

            services.AddScoped<IDonorProvider, DonorProvider>();
            services.AddScoped<ISeekerProvider, SeekerProvider>();
            services.AddScoped<IBloodBankProvider, BloodBankProvider>();
            services.AddScoped<IEventProvider, EventProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            CreateUserRoles(services).Wait();

            //insert into AspNetRoles values('439ec27b-acc4-4980-b2b6-913be965090c','Seeker','SEEKER','2422706d-cccb-4a23-bb2c-cc98d12a635d');
            //insert into AspNetRoles values('439ec27b-acc4-4980-b2b6-913be965090d','BloodBank','BLOODBANK','3422706d-cccb-4a23-bb2c-cc98d12a635d');
            //insert into AspNetUserRoles values('ba14cabf-a422-4a94-874b-87e621e1b2b2','439ec27b-acc4-4980-b2b6-913be965090a');(for admin role);
        }
    }
}
