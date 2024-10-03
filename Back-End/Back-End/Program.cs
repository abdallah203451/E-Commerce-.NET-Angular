using Back_End.Application.Payments;
using Back_End.Application.Repositories;
using Back_End.Application.Services;
using Back_End.Application.Users;
using Back_End.Domain.Entities;
using Back_End.Infrastructure.Context;
using Back_End.Infrastructure.Repositories;
using Back_End.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(option =>
{
	option.AddPolicy("MyPolicy", builder =>
	{
		builder.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader();
	});
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr"), x => x.MigrationsAssembly("Back_End.Infrastructure")));

builder.Services.AddIdentityCore<User>()
	.AddRoles<IdentityRole>()
	.AddTokenProvider<DataProtectorTokenProvider<User>>("Back-End")
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration.GetSection("SendGrid"));
builder.Services.AddTransient<IEmailSender, EmailSender>();


builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IPaymentService, PayPalPaymentService>();
builder.Services.AddScoped<ITrendingProductsCacheService, TrendingProductsCacheService>();


builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ClockSkew = TimeSpan.Zero,
		ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
		ValidAudience = builder.Configuration["JwtSettings:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
	};
});

builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddMemoryCache();

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//	options.UseSqlServer(
//		builder.Configuration["ConnectionStrings:ConStr"],
//		b => b.MigrationsAssembly("YourAssemblyName"))
//	//.EnableSensitiveDataLogging() // Include sensitive data in the logs for debugging purposes
//	.LogTo(Console.WriteLine));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyPolicy");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
