    public static class UsetMiddleWareMethod 
    {
        public static void UseFirstMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<FirstMiddleWare>();
        }
        public static void UseSecondMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<SecondMiddleWare>();
        }
    }
