using lab7.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceReference1;
using ServiceReference2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
    //options.UseSqlite("Filename=MyDatabase.db");
});
builder.Services.AddScoped<ICurrencyConversionService, ServiceReference2.CurrencyConversionServiceClient>();
builder.Services.AddScoped<IConversionService, ServiceReference1.ConversionServiceClient>();

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

app.UseAuthorization();

app.MapRazorPages();



using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;


    var db = serviceProvider.GetRequiredService<DBContext>();
    //await db.Database.EnsureDeletedAsync();
    await db.Database.EnsureCreatedAsync();


}

app.Run();
