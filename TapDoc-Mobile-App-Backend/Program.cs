using Microsoft.EntityFrameworkCore;
using TapDoc_Mobile_App_Backend.Models;

using Microsoft.AspNetCore.Authentication;
using TapDoc_Mobile_App_Backend;
using TapDoc_Mobile_App_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5173") // Replace with the actual URL of your React app
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); // If your front-end needs to send credentials like cookies or auth headers
    });
});

var connectionString = builder.Configuration.GetConnectionString("MyDBContext");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("ConnectionStrings"));
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<EmergencyContactsService>();
builder.Services.AddScoped<EmergencyServicesService>();
builder.Services.AddScoped<RecordsService>();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<PrescriptionRecordService>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
