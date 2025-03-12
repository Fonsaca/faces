using Faces.Api;
using Faces.Application.Factories;
using Faces.Application.Services;
using Faces.Database.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IJobFunctionService, JobFunctionService>();
builder.Services.AddTransient<IJobFunctionRepository, JobFunctionRepository>();
builder.Services.AddTransient<IEmployeeFactory, EmployeeFactory>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IAuthenticatedEmployee, ApiAuthenticatedEmployee>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
