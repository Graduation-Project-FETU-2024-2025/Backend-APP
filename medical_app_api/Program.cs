using medical_app_db.Core.Models;
using medical_app_api.Extentions;
using medical_app_db.Core.Interfaces;
using medical_app_db.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .InjectDbContext(builder.Configuration, builder.Environment)
    .InjectIdentity<ApplicationUser>()
    .AddJWTAuth(builder.Configuration)
    .AddJWTConfiguration(builder.Configuration)
    .AddAuthService()
    .AddHttpContextAccessor()
    .AddEmailService()
    .AddEmailConfiguration(builder.Configuration, builder.Environment);

builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IProductService, ProductsServices>();

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

