using Backend.Database;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var serverVersion = new MySqlServerVersion(new Version(8,0,31));
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ForumDBContext>(options => 
    options.UseMySql(connectionString ,serverVersion));
builder.Services.AddIdentity<ForumUser, IdentityRole>(_config =>
{
    _config.User.RequireUniqueEmail = true;
    _config.Password.RequiredLength = 8;
    _config.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<ForumDBContext>();
builder.Services.AddAuthentication(_auth =>
{
    _auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    _auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddCors(_options =>
{
    _options.AddDefaultPolicy(builder => { builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); }) ;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}"
);

app.UseAuthentication();

app.UseCors();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
