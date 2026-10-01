using EcommerceDemo.ApplicationDbContext;
using EcommerceDemo.Areas.User.ApiEndPoints;
using EcommerceDemo.Areas.User.Services.IRepository;
using EcommerceDemo.Areas.User.Services.Repository;
using EcommerceDemo._commanmethods;
using EcommerceDemo.Models.DTO_Models;
using EcommerceDemo.Repository.Implementation;
using EcommerceDemo.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using EcommerceDemo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<DTOEmailSettings>(builder.Configuration.GetSection("EmailSettings"));

//Session Configration
builder.Services.AddSession(op =>
{
	op.IdleTimeout = TimeSpan.FromMinutes(90);
	op.Cookie.HttpOnly = true;
	op.Cookie.IsEssential = true;

}
);
//To access session in view 
builder.Services.AddHttpContextAccessor();


// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("sqlcon")));

// Dependency Injection
builder.Services.AddScoped<IAuthInterface, AuthRepository>();
builder.Services.AddScoped<IUserAuthInterface, UserAuthRepository>();
builder.Services.AddScoped<IUserInterface, UserRepository>();
builder.Services.AddScoped<IManageUserInterface, ManageUserRepository>();
builder.Services.AddTransient<IEmailServiceInterface, EmailServiceRepository>();
builder.Services.AddScoped<IProductInterface, ProductRepository>();
builder.Services.AddSingleton<CommanMethods>();
builder.Services.AddHttpClient<IUserAuthInterface, UserAuthRepository>(client =>
{
	client.BaseAddress = new Uri("http://localhost:5209/api/");
});
builder.Services.AddHttpClient<IManageUserInterface, ManageUserRepository>(client =>
	{
		client.BaseAddress = new Uri("http://localhost:5209/api/");
	});
builder.Services.AddScoped<UserApiEndPoints>();

// Swagger for API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = Microsoft.OpenApi.Models.ParameterLocation.Header
	});

	options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
	{
		{
			new Microsoft.OpenApi.Models.OpenApiSecurityScheme
			{
				Reference = new Microsoft.OpenApi.Models.OpenApiReference
				{
					Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});


builder.Services.AddScoped<AuthService>();

var app = builder.Build();



// Error & Swagger
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}
else
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

//Route for areas 

app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=User}/{action=Index}/{id?}");


app.MapControllerRoute(
	name: "default",
	pattern: "{area=User}/{controller=UserHome}/{action=Home}/{id?}");


//  Enable API endpoints using Attribte routing 
app.MapControllers();

app.Run();
