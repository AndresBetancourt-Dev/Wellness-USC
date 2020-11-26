using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wellness_USC.Data;
using Wellness_USC.Models;
using Wellness_USC.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Wellness_USC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDbContext<CursoDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ClaseDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ProfesoresDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHttpContextAccessor();

            /*services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
            });*/
            // services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // app.UseSession();
            //app.UseMvc();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }
            else
            {
                app.UseStatusCodePages();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // 
            //app.UseStatusCodePagesWithReExecute("/Error{404}");
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
        }
    }
}
