using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Infrastructure;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connection));

builder.Services.AddScoped<RepositoryFactory>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
builder.Services.AddScoped<IDiseaseService, DiseaseService>();
builder.Services.AddScoped<ICheckupService, CheckupService>();
builder.Services.AddScoped<ICheckupImageService, CheckupImageService>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IDoctorPatientService, DoctorPatientService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IStorageService, MinioStorageService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();