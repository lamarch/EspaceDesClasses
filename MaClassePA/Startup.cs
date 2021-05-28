namespace MaClassePA
{
    using Data;

    using MaClassePA.Services;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using System;

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
            //MVC Services
            services.AddControllersWithViews();

            var connectionString = Configuration.GetConnectionString("MainConnection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            //ClassesDbContext
            services.AddDbContext<ClassesDbContext>(options => options.UseLazyLoadingProxies().UseMySql(connectionString, serverVersion));

            //UserDbContext
            services.AddDbContext<UserDbContext>(options => options.UseMySql(connectionString, serverVersion));

            //Identity Services
            services.AddIdentity<IdentityUser, IdentityRole>(
                options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredLength = 4;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                }).AddEntityFrameworkStores<UserDbContext>();

            //Cookies (Identity mainly)
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;

                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = "/compte/in";
                options.LogoutPath = "/compte/out";
                options.AccessDeniedPath = "/compte/interdit";
                options.ReturnUrlParameter = "url_retour";
                options.SlidingExpiration = true;
            });

            //Authorization Services
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Superadmin", policy => policy.RequireRole("Superadmin"));
                options.AddPolicy("Admin", policy => policy.RequireRole("Superadmin", "Admin"));
                options.AddPolicy("Redacteur", policy => policy.RequireRole("Superadmin", "Admin", "Redacteur"));
                options.AddPolicy("Connecte", policy => policy.RequireAuthenticatedUser());
            });

            //Useful ? => we don't use Pages...
            services.AddRazorPages().AddRazorRuntimeCompilation();

            //...services
            services.AddScoped<IClassesContext, ClassesDbContext>(); // Db Context
            services.AddScoped<MarkdownParser>(); // Markdown parser
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {

            app.UseStatusCodePages();

            if (env.IsDevelopment())
            {
                logger.LogInformation("Development environment");
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                logger.LogInformation("Production environment");
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseForwardedHeaders(new()
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedHost
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
