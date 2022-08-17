using InstagramMVC.Models;
using InstagramMVC.Services;
using InstagramMVC.Services.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace InstagramMVC
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
          
            services.AddDbContext<InstagramContext>(options => options.UseNpgsql(connection));
            services.AddIdentity<User, Role>(
                options =>
                {
                    options.Password.RequiredLength = 5;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                }).AddEntityFrameworkStores<InstagramContext>().AddDefaultTokenProviders();
            services.AddTransient<UploadService>();
            services.AddTransient<IAccountRegisterService, AccountRegisterService>();
            services.AddTransient<IPublicationRegisterService, AccountRegisterService>();
            services.AddTransient<IDefaultUserImageAvatar>(_ =>
                new DefalutUserImageAvatar(Configuration["PathToDefaultAvatar:Path"]));
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<ISubscribeService, SubscriberService>();
            services.AddTransient<ILikeService, LikeService>();
            services.AddTransient<IPublicationService, PublicationService>();
            services.AddMemoryCache();
           
       
            services.AddControllersWithViews();
            services.AddResponseCompression(options => options.EnableForHttps = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseResponseCaching();
            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();
            
            app.UseAuthentication().UseAuthorization();
       
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Register}/{id?}");
            });
        }
    }
}