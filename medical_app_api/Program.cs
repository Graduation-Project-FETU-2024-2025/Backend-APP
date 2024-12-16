using medical_app_db.EF.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using medical_app_db.Core.Models;
using medical_app_db.Core.Interfaces;
using medical_app_db.EF.Services;
using medical_app_db.Core.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MedicalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// register Identity For Account Model
builder.Services.AddIdentity<Account, IdentityRole<Guid>>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MedicalDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MedicalDbContext>()
    .AddDefaultTokenProviders();

// Bind EmailSetting Class with configuration 
builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailConfiguration"));

// Binf JWT class with configuration in appsettings.json
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
// Adding jwt configurations
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = false;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:AudienceIP"],
        ValidIssuer = builder.Configuration["JWT:IssureIP"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"] ?? "hasjkhdjaskhda")
            )
    };
});

// register services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// register cach
builder.Services.AddMemoryCache();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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


