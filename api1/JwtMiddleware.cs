public class JwtMiddleware
{
    private readonly RequestDelegate next;
    public JwtMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public Task InvokeAsync(HttpContext context)
    {
        System.Console.WriteLine(Environment.NewLine);
        System.Console.WriteLine(DateTime.UtcNow.ToString());
        System.Console.WriteLine(context.Request.Headers["Authorization"]);
        System.Console.WriteLine(Environment.NewLine);

        next(context);

        return Task.CompletedTask;
    }
}