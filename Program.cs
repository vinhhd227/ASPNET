using Newtonsoft.Json;

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
app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints((endpoints) =>
{
    endpoints.MapGet("/", async (c) =>
    {
        var menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), c.Request);
        var html = HtmlHelper.HtmlDocument("Xin chao ASP", menu + HtmlHelper.HtmlTrangchu());
        await c.Response.WriteAsync(html);
    });
    endpoints.MapGet("/RequestInfo", async (c) =>
    {
        var menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), c.Request);
        string info = RequestProcess.RequestInfo(c.Request).HtmlTag("div", "container");
        var html = HtmlHelper.HtmlDocument("Thong tin Request", menu + info);
        await c.Response.WriteAsync(html);
    });
    endpoints.MapGet("/Encoding", async (c) =>
    {
        var menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), c.Request);
        var html = HtmlHelper.HtmlDocument("Thong tin Request", menu);
        await c.Response.WriteAsync(html);
    });
    endpoints.MapGet("/Cookies/{*action}", async (c) =>
    {
        var menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), c.Request);
        var action = c.GetRouteValue("action") ?? "read";
        string message = "";
        if (action.ToString() == "write")
        {

            var option = new CookieOptions()
            {
                Path = "/",
                Expires = DateTime.Now.AddDays(1)
            };
            c.Response.Cookies.Append("masanpham", "928356392486524985672", option);
            message = "Cookie được ghi";
        }
        else
        {
            // Lấy danh sách các Header và giá trị  của nó, dùng Linq để lấy
            var listcokie = c.Request.Cookies.Select((header) => $"{header.Key}: {header.Value}".HtmlTag("li"));
            message = string.Join("", listcokie).HtmlTag("ul");
        }
        var huongdan = "<a class=\"btn btn-danger me-3\" href=\"/Cookies/read\">Doc cookies</a><a class=\"btn btn-success\" href=\"/Cookies/write\">Ghi cookies</a>";
        huongdan = huongdan.HtmlTag("div", "container mt-4");
        message = message.HtmlTag("div", "alert alert-danger");
        var html = HtmlHelper.HtmlDocument("Thong tin Cookies", menu + huongdan + message);
        await c.Response.WriteAsync(html);
    });
    endpoints.MapGet("/Json", async (c) =>
    {
        var menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), c.Request);
        var p = new
        {
            TenSP = "Dong ho abc",
            Gia = 5000000,
            NgaySX = new DateTime(2000, 12, 31)
        };
        c.Response.ContentType = "application/json";
        var json = JsonConvert.SerializeObject(p);


        // var html = HtmlHelper.HtmlDocument("Thong tin Json", menu);
        await c.Response.WriteAsync(json);
    });
    endpoints.MapMethods("/Form", new string[] { "POST", "GET" }, async (c) =>
    {
        var menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), c.Request);
        string form = RequestProcess.ProcessForm(c.Request).HtmlTag("div", "container");
        var html = HtmlHelper.HtmlDocument("Thong tin Form", menu + form);
        await c.Response.WriteAsync(html);
    });
});

// app.UseStatusCodePages();  
// app.Map("/admin", (appAdmin) =>
// {
//     appAdmin.UseRouting();
//     appAdmin.UseEndpoints((endpoints) =>
//     {
//         endpoints.MapGet("/product", async (c) => await c.Response.WriteAsync("Trang quan ly san pham"));
//         endpoints.MapGet("/home", async (c) => await c.Response.WriteAsync("Trang chu admin"));
//         endpoints.MapGet("/user", async (c) => await c.Response.WriteAsync("Trang san quan ly user"));
//     });
//     appAdmin.Run(async (context) => await context.Response.WriteAsync("Terminal MiddleWare Admin"));
// });
// app.Run(async (context) => await context.Response.WriteAsync("Terminal MiddleWare"));
// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
