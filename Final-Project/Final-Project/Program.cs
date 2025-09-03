var builder = WebApplication.CreateBuilder(args);

// إضافة خدمة HttpClient
builder.Services.AddHttpClient();

// إضافة الخدمات الخاصة بـ MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// ضبط الـ Routing الأساسي
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employees}/{action=Index}/{id?}");

app.Run();