using Library.Constants;
using Library.Services;
using Library.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICachingService, CachingService>();
builder.Services.AddScoped<IUserInfoService, UserInfoService>();
builder.Services.AddScoped<ILibraryService, LibraryService>();
builder.Services.AddScoped<IXmLDataService, XMLDataService>();
builder.Services.Configure<LibrarySettings>(
    builder.Configuration.GetSection(LibraryConstants.LibraryConfigSectionName));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Library}/{action=Index}/{id?}");

app.Run();
