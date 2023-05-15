using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class FirstMiddleWare
{
    private readonly RequestDelegate _next;
    public FirstMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine($"URL:{context.Request.Path}");
        context.Items.Add("DataFirstMiddleWare", $"<p>URL:{context.Request.Path}</p>");
        await _next(context);
    }
}