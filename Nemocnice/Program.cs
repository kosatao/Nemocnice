using Microsoft.AspNetCore.Identity;
using Nemocnice.Models;

using Microsoft.EntityFrameworkCore;
using Nemocnice.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DbContext>(options => {
   //options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb"));
	options.UseSqlServer(builder.Configuration.GetConnectionString("MonsterDb"));
	//options.UseSqlServer(builder.Configuration.GetConnectionString("MonsterForLocalDb"));
	
});
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<DbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<HospitalizationService>();
builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
}
);

builder.Services.ConfigureApplicationCookie(options => {
    options.Cookie.Name = "AspNetCore.Identity.Application";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    options.SlidingExpiration = true;
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
	app.UseDeveloperExceptionPage();
}
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
