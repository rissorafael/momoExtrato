using DinkToPdf.Contracts;
using DinkToPdf;
using DomoExtrato.Infra;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Host.UseSerilog();
builder.Services.AddSingleton<IConverter, SynchronizedConverter>();
builder.Services.AddSingleton<ITools, PdfTools>(); // Registrar a implementação de ITools (PdfTools)
builder.Services.AddSingleton<IConverter, SynchronizedConverter>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
