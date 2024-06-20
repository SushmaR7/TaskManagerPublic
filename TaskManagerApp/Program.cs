using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerApp.Data;
using TaskManagerApp.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TaskManagerAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagerAppContext") ?? throw new InvalidOperationException("Connection string 'TaskManagerAppContext' not found.")));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//*****Adding identity service *****
builder.Services.AddIdentity<UserDetail, IdentityRole>(options =>
{  
    options.Password.RequiredLength = 5;
    options.User.RequireUniqueEmail = true;
    
}).AddEntityFrameworkStores<TaskManagerAppContext>().AddDefaultTokenProviders();
builder.Services.AddTransient<PasswordHasher<UserDetail>>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=LoginView}");

app.Run();
