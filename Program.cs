using RCJY_Project.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Admin",
    pattern: "{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Profil",
    pattern: "{controller=Admin}/{action=Profil}/{id?}");

app.MapControllerRoute(
    name: "DeleteEmployee",
    pattern: "{controller=Admin}/{action=DeleteEmployee}/{id?}");

app.MapControllerRoute( 
    name: "UpdateEmployee",
    pattern: "{controller=Admin}/{action=UpdateEmployee}/{id?}");


app.MapControllerRoute(
    name: "Home",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "Profile",
    pattern: "{controller=Home}/{action=Profile}/{id?}");

app.MapControllerRoute(
    name: "SearchDepartments",
    pattern: "{controller=Home}/{action=SearchDepartments}/{id?}");
app.MapControllerRoute(
    name: "Departments",
    pattern: "{controller=Home}/{action=Departments}/{id?}");
app.MapControllerRoute(
    name: "EmployeeInfCard",
    pattern: "{controller=Home}/{action=EmployeeInfCard}/{id?}");
app.MapControllerRoute(
    name: "Login",
    pattern: "{controller=Login}/{action=Index}/{id?}");


app.Run();
