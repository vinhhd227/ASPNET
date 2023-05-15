Console.WriteLine("Start app");
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5000");
// Xem xét đặt Startup.Startup  ở đây
// đọc config
// var testoptions = builder.Configuration.GetSection ("TestOptions");

// Thêm vào dòng lấy IServiceCollection
var services = builder.Services;
/* ============================================================
     Copy code cũ trong Startup.ConfigureServices vào đây, ví dụ
   =========================================================== */
services.AddSingleton<SecondMiddleWare>();
// services.AddControllersWithViews();
// services.AddDistributedMemoryCache();
// services.AddSession(cfg => {
//     cfg.Cookie.Name = "xuanthulab";
//     cfg.IdleTimeout = new TimeSpan(0,30, 0);
// });

var app = builder.Build();

/* ============================================================
    Code viết trong Configure cũ đặt tại đay, ví dụ:
   =========================================================== */

// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     app.UseHsts();
// }
app.UseRouting();
app.UseStaticFiles();

app.UseFirstMiddleWare();
app.UseSecondMiddleWare();
app.UseEndpoints((endpoints) =>
{
    endpoints.MapGet("/product", async (c) => await c.Response.WriteAsync("Trang san pham"));
    endpoints.MapGet("/home", async (c) => await c.Response.WriteAsync("Trang chu"));
    endpoints.MapGet("/about", async (c) => await c.Response.WriteAsync("Trang san pham"));
});

// app.UseStatusCodePages();
app.Map("/admin", (appAdmin) =>
{
    appAdmin.UseRouting();
    appAdmin.UseEndpoints((endpoints) =>
    {
        endpoints.MapGet("/product", async (c) => await c.Response.WriteAsync("Trang quan ly san pham"));
        endpoints.MapGet("/home", async (c) => await c.Response.WriteAsync("Trang chu admin"));
        endpoints.MapGet("/user", async (c) => await c.Response.WriteAsync("Trang san quan ly user"));
    });
    appAdmin.Run(async (context) => await context.Response.WriteAsync("Terminal MiddleWare Admin"));
});
app.Run(async (context) => await context.Response.WriteAsync("Terminal MiddleWare"));
// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
