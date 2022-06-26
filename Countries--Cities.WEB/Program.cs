using Autofac;
using Autofac.Extensions.DependencyInjection;
using Countries__Cities.Repository.Concrete;
using Countries__Cities.Service.Mapping;
using Countries__Cities.WEB.Filters;
using Countries__Cities.WEB.Mapping;
using Countries__Cities.WEB.Modules;
using Countries__Cities.WEB.Services;
using Countries__Cities.WEB.Validations;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(MVCMapProfile));

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

builder.Services.AddHttpClient<APICountryService>(opt =>
{

    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);

});

builder.Services.AddHttpClient<APICityService>(opt =>
{

    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);

});


builder.Services.AddScoped(typeof(NotFoundFilter<>));


builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));


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
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
