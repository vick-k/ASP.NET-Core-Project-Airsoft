using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAirsoft.Data.Configurations;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Infrastructure.Extensions;
using ProjectAirsoft.Web.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
	options.SignIn.RequireConfirmedAccount = false;
	options.User.RequireUniqueEmail = true;
	options.Password.RequireNonAlphanumeric = false;
})
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddRoles<IdentityRole<Guid>>()
	.AddSignInManager<SignInManager<ApplicationUser>>()
	.AddUserManager<UserManager<ApplicationUser>>()
	.AddDefaultUI();

builder.Services.AddCustomServices();

builder.Services.AddControllersWithViews(configuration =>
{
	configuration.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});
builder.Services.AddRazorPages();

WebApplication app = builder.Build();

// Call the database seeding method
using (IServiceScope scope = app.Services.CreateScope())
{
	IServiceProvider services = scope.ServiceProvider;

	DatabaseSeeder.SeedRoles(services);
	DatabaseSeeder.AssignAdminRole(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
	app.UseDeveloperExceptionPage();
}
else
{
	app.UseExceptionHandler("/Home/Error/500");

	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
	name: "errors",
	pattern: "{controller=Home}/{action=Index}/{statusCode?}");
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
