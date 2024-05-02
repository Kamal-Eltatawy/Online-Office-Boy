using ApplicationService.AuthFilters;
using ApplicationService.Mapper;
using ApplicationService.Services.CategoryServices;
using ApplicationService.Services.DepartmentServices;
using ApplicationService.Services.ImageServices;
using ApplicationService.Services.OrderProductServices;
using ApplicationService.Services.OrderServices;
using ApplicationService.Services.ProductServices;
using ApplicationService.Services.SiftsServices;
using ApplicationService.Services.UserServices;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Reposatory;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Online_Office_Boy.Middleware;
using Online_Office_Boy.Seeding;
using System;

namespace Online_Office_Boy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<ExceptionMiddleware>();
            builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            builder.Services.AddDbContext<ApplicationContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            }).AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });
            builder.Services.AddAuthentication().AddCookie();

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            builder.Services.AddScoped<IOfficeServices,OfficeServices>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddScoped<IOrderServices, OrderServices>();
            builder.Services.AddScoped<IOrderProductServices, OrderProductServices>();
            builder.Services.AddScoped<IImageServices, ImageServices>();
            builder.Services.AddScoped<ICategoryServices, CategoryServices>();
            builder.Services.AddScoped<IShiftServices, ShiftServices>();







            builder.Services.AddAutoMapper(typeof(MapperProfile));
            builder.Services.AddHostedService<SeedDataHostedService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();


            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.MapControllerRoute(
              name: "areas",
             pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
