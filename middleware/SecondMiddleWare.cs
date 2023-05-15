public class SecondMiddleWare : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {

        if (context.Request.Path == "/xxx.html")
        {
            context.Response.Headers.Add("SecondMiddleWare", "Ban ko dc truy cap");
            var dataFirstMiddleWare = context.Items["DataFirstMiddleWare"];
            if (dataFirstMiddleWare != null)
            {
                await context.Response.WriteAsJsonAsync((string)dataFirstMiddleWare);
            }
            await context.Response.WriteAsync("Ban khong duoc truy cap");
        }
        else
        {
            context.Response.Headers.Add("SecondMiddleWare", "Ban duoc truy cap");
            var dataFirstMiddleWare = context.Items["DataFirstMiddleWare"];
            if (dataFirstMiddleWare != null)
            {
                await context.Response.WriteAsync((string)dataFirstMiddleWare);
            }
            await next(context);
        }
    }
}
