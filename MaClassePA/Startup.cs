namespace MaClassePA
{
    using Data;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

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
            //MVC SERVICES
            services.AddControllersWithViews();

            //CLASSE DBCONTEXT
            services.AddDbContext<ClassesDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetSection("ConnectionStrings")["MainConnection"]));

            //...injection
            services.AddScoped<IClassesContext, ClassesDbContext>();

            //USER DBCONTEXT
            services.AddDbContext<UserDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MainConnection")));

            //IDENTITY SERVICES
            services.AddIdentity<IdentityUser, IdentityRole>(
                options => { 
                    options.SignIn.RequireConfirmedAccount = false; 
                    options.Password.RequireDigit = false; 
                    options.Password.RequireLowercase = false; 
                    options.Password.RequiredLength = 4; 
                    options.Password.RequireUppercase = false; 
                    options.Password.RequireNonAlphanumeric = false; 
                }).AddEntityFrameworkStores<UserDbContext>();

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

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Superadmin", policy=> policy.RequireRole("Superadmin"));
                options.AddPolicy("Admin", policy => policy.RequireRole("Superadmin", "Admin"));
                options.AddPolicy("Redacteur", policy => policy.RequireRole("Superadmin", "Admin", "Redacteur"));
                options.AddPolicy("Connecte", policy => policy.RequireAuthenticatedUser());
            });

            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStatusCodePages();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
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
            });

        }
    }
}
