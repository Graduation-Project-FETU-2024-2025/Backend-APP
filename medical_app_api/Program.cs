using medical_app_db.Core.Models;
using medical_app_api.Extentions;
using medical_app_db.Core.Interfaces;
using medical_app_db.Services;
using medical_app_api.Middleware;
using medical_app_db.EF.Data;
using Microsoft.EntityFrameworkCore;
using medical_app_db.EF.Services;
using medical_app_db.Core.Helpers;
using medical_app_db.EF.Services;
using medical_app_db.EF.Factory;
using medical_app_db.Core.Services.Interfaces;
using medical_app_db.EF.Migrations;
using medical_app_db.Core.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.InjectDbContext(builder.Configuration, builder.Environment);
builder.Services.AddDbContext<MedicalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.InjectIdentity<ApplicationUser>()
    .AddJWTAuth(builder.Configuration)
    .AddJWTConfiguration(builder.Configuration)
    .AddAuthService()
    .AddHttpContextAccessor()
    .AddEmailService()
    .AddEmailConfiguration(builder.Configuration, builder.Environment);

builder.Services.AddSingleton<IUserFactory, UserFactory>();

builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IProductService, ProductsServices>();

builder.Services.AddScoped<IClinicStatisticsService, ClinicStatisticsService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IClinicService, ClinicService>();

builder.Services.AddMemoryCache();

builder.Services.AddScoped(typeof(IAppointmentService),typeof(AppointmentService));
builder.Services.AddScoped(typeof(IImageService),typeof(CloudinaryService));
builder.Services.Configure<CloudinatuSettings>(builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddAutoMapper(options => options.AddProfile(new MappingProfile()));
builder.Services.AddMemoryCache(); // to add cach

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.SeedAsync(app.Services);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GetPharmacyIdMiddleware>();
app.MapControllers();

app.Run();

