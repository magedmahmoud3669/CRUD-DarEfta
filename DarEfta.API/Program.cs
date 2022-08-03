using DarEfta.BL.Interface;
using DarEfta.BL.Mapper;
using DarEfta.BL.Repository;
using DarEfta.DAL.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Enhancement ConnectionString
var connectionString = builder.Configuration.GetConnectionString("DarEftaConnection");

builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseSqlServer(connectionString));


#region Auto Mapper Configuration
builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));
#endregion


// Add Dependency Injection Service 
builder.Services.AddScoped<IEmployeeRep, EmployeeRep>();



// CORS Configuration
builder.Services.AddCors();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(options => options
//.WithOrigins("https://localhost:7255", "https://www.ahmed.com")
.AllowAnyOrigin()
//.WithMethods("GET" , "PUT")
.AllowAnyMethod()
//.WithHeaders("JSON")
.AllowAnyHeader());


app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
