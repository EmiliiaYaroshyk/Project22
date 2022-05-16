using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project22.Service;
using Project22.Domain.Repositories.Abstract;
using Project22.Domain.Repositories.EntityFramework;
using Project22.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Project22
{
    public class Startup
    {
        public IConfiguration Confiquration { get; }
        public Startup(IConfiguration configuration) => Confiquration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            //підключаємо конфіг з appsetting.json 
            Confiquration.Bind("Project", new Config());
            //підключаємо потріний функціонал додатка в якості сервісів
         
            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<DataManager>();

            //підключаємо контекст БД
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString));

            //налаштування identity системи
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //налаштування authentication cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "myCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });

            //налаштування політики авторизації для Admin area
            services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
            });

            //додаємо сервіси для контролера (MVC) 
            services.AddControllersWithViews(x =>
            {
                x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
            })
                //виставляємо сумісність із asp.net core 3.0
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //у процесі розробки нам важливо бачити які саме помилки
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            //підключаємо підтримку статичних файлів у додатку(css, js и т.д.)
            app.UseStaticFiles();

            //підключаємо систему маршрутизації
            app.UseRouting();

            //підключаємо аутентифікацію та авторизацію
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            //реєструємо потрібні маршрути
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
