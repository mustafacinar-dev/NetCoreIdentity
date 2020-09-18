using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreIdentity.Context;
using NetCoreIdentity.CustomValidator;
using System;

namespace NetCoreIdentity
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IdentityContext>();
            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                //opt.Password.RequireDigit = false;
                //opt.Password.RequireLowercase = false;
                //opt.Password.RequiredLength = 1;
                //opt.Password.RequireNonAlphanumeric = false;
                //opt.Password.RequireUppercase = false;
                //opt.SignIn.RequireConfirmedEmail = true;
            }).AddErrorDescriber<CustomIdentityValidator>().AddPasswordValidator<CustomPasswordValidator>().AddEntityFrameworkStores<IdentityContext>();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Home/Index");
                opt.AccessDeniedPath = new PathString("/Home/AccessDenied");
                opt.Cookie.HttpOnly = true;
                opt.Cookie.Name = "NetCoreIndetityCookie";
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                opt.ExpireTimeSpan = TimeSpan.FromDays(21);
            });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
