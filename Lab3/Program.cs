using Lab3;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ��������� ��������� ������������ � �������������
builder.Services.AddControllersWithViews();

// ��������� ������ ����������� � SQLite
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite("Data Source=helloapp.db"));

var app = builder.Build();

// ������������ ��������� ��������
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HTTP Strict Transport Security
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // ����������� ����� (css, js, �������� � �.�.)

app.UseRouting();

app.UseAuthorization();

// ��������� �������������
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
