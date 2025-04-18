using Microsoft.EntityFrameworkCore;
using TapDoc_Mobile_App_Backend.Models;
using TapDoc_Mobile_App_Backend;
using TapDoc_Mobile_App_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ CORS Setup (Adjust origin for production if needed)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin() // 👈 Expo might use random IPs during development
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// ✅ Connection String from Environment Variable
var connectionString = builder.Configuration.GetConnectionString("MyDBContext");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// ✅ Controller + Swagger Setup
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Scoped Services Registration
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<EmergencyContactsService>();
builder.Services.AddScoped<EmergencyServicesService>();
builder.Services.AddScoped<RecordsService>();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<PrescriptionRecordService>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<PrescriptionRecordService>();


var app = builder.Build();

app.UseMiddleware<SwaggerAuthMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.html", "TapDoc API v1");
    c.RoutePrefix = "swagger"; // So URL will be /swagger
});


app.UseHttpsRedirection();
app.UseCors(); // Important: Must come BEFORE Authentication

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
