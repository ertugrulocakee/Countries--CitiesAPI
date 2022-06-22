using Autofac;
using Autofac.Extensions.DependencyInjection;
using Countries__Cities.Core.Repository;
using Countries__Cities.Core.Service;
using Countries__Cities.Core.UnitOfWorks;
using Countries__Cities.Repository.Concrete;
using Countries__Cities.Repository.Repository;
using Countries__Cities.Repository.UnitOfWorks;
using Countries__Cities.Service.Mapping;
using Countries__Cities.Service.Services;
using Countries__Cities.Service.Validations;
using Countries__CitiesAPI.Filters;
using Countries__CitiesAPI.Middlewares;
using Countries__CitiesAPI.Modules;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers(options => { options.Filters.Add(new ValidateFilterAttribute()); }).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CountryDTOValidation>()); 

builder.Services.Configure<ApiBehaviorOptions>(options =>

 options.SuppressModelStateInvalidFilter = true   // apinin kendi modelini inaktif ettik! 

);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddScoped(typeof(NotFoundFilter<>));


builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();
