using Lab3;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Добавляем поддержку контроллеров и представлений
builder.Services.AddControllersWithViews();

// Указываем строку подключения к SQLite
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite("Data Source=helloapp.db"));

var app = builder.Build();

// Конфигурация пайплайна запросов
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HTTP Strict Transport Security
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Статические файлы (css, js, картинки и т.д.)

app.UseRouting();

app.UseAuthorization();

// Настройка маршрутизации
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
