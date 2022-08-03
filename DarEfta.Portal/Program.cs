using DarEfta.BL.Interface;
using DarEfta.BL.Mapper;
using DarEfta.BL.Repository;
using DarEfta.DAL.Database;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Newtonsoft Configurarion
builder.Services.AddControllersWithViews().AddNewtonsoftJson(opt => {
    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
});


// Enhancement ConnectionString
var connectionString = builder.Configuration.GetConnectionString("DarEftaConnection");

builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseSqlServer(connectionString));
    


#region Auto Mapper Configuration
builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));
#endregion

// Add Dependency Injection Service 

// Transient (Take Instance With Every Operation)
//builder.Services.AddTransient<IDepartment, DepartmentRep>();

// Scoped (Take One Instance With All Operation)
builder.Services.AddScoped<IDepartmentRep, DepartmentRep>();
builder.Services.AddScoped<IEmployeeRep, EmployeeRep>();
builder.Services.AddScoped<ICountryRep, CountryRep>();
builder.Services.AddScoped<ICityRep, CityRep>();
builder.Services.AddScoped<IDistrictRep, DistrictRep>();

// SingleTone (Take One Instance Shared Between All Users)
//builder.Services.AddSingleton<IDepartment, DepartmentRep>();




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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
