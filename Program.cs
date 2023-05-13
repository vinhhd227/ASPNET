Console.WriteLine("Start app");
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5000");
// Xem xét đặt Startup.Startup  ở đây
// đọc config
// var testoptions = builder.Configuration.GetSection ("TestOptions");

// Thêm vào dòng lấy IServiceCollection
// var services = builder.Services;
/* ============================================================
     Copy code cũ trong Startup.ConfigureServices vào đây, ví dụ
   =========================================================== */

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

app.UseStatusCodePages();
app.UseStaticFiles();
app.UseRouting();
// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapGet("/home", async (context) =>
    {
        string html = @"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""UTF-8"">
                    <title>Trang web đầu tiên</title>
                    <link rel=""stylesheet"" href=""/css/bootstrap.min.css"" />
                    <script src=""/js/jquery.min.js""></script>
                    <script src=""/js/popper.min.js""></script>
                    <script src=""/js/bootstrap.min.js""></script>


                </head>
                <body>
                    <nav class=""navbar navbar-expand-lg navbar-dark bg-danger"">
                            <a class=""navbar-brand"" href=""#"">Brand-Logo</a>
                            <button class=""navbar-toggler"" type=""button"" data-toggle=""collapse"" data-target=""#my-nav-bar"" aria-controls=""my-nav-bar"" aria-expanded=""false"" aria-label=""Toggle navigation"">
                                    <span class=""navbar-toggler-icon""></span>
                            </button>
                            <div class=""collapse navbar-collapse"" id=""my-nav-bar"">
                            <ul class=""navbar-nav"">
                                <li class=""nav-item active"">
                                    <a class=""nav-link"" href=""#"">Trang chủ</a>
                                </li>
                            
                                <li class=""nav-item"">
                                    <a class=""nav-link"" href=""#"">Học HTML</a>
                                </li>
                            
                                <li class=""nav-item"">
                                    <a class=""nav-link disabled"" href=""#"">Gửi bài</a>
                                </li>
                        </ul>
                        </div>
                    </nav> 
                    <p class=""display-4 text-danger"">Đây là trang đã có Bootstrap</p>
                </body>
                </html>
    ";
        await context.Response.WriteAsync(html);
    });
app.MapGet("/about.html", () => "Trang thong tin");
app.MapGet("/contact", () => "Trang lien he");

app.Run();
