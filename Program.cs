using Microsoft.EntityFrameworkCore;
using NewsPortal.Data;
using NewsPortal.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrar DbContext SQLite
builder.Services.AddDbContext<FeedbackContext>(options =>
    options.UseSqlite("Data Source=feedback.db"));

// MVC y controladores
builder.Services.AddControllersWithViews();

// Registrar servicio HTTP para API externa JSONPlaceholder
builder.Services.AddHttpClient<IExternalApiService, ExternalApiService>();

// Swagger para documentar API (opcional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// No es necesario configurar CORS porque API y MVC están en el mismo proyecto

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=News}/{action=Index}/{id?}");

app.MapControllers(); // Mapear controladores API como feedback

app.Run();
