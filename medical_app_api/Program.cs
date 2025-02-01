using medical_app_db.Core.Models;
using medical_app_api.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .InjectDbContext(builder.Configuration)
    .InjectIdentityCore<User>()
    .InjectIdentity<Account>()
    .AddJWTAuth(builder.Configuration)
    .AddJWTConfiguration(builder.Configuration)
    .AddAuthService()
    .AddEmailService()
    .AddEmailConfiguration(builder.Configuration);

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

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


