public class JwtMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<JwtMiddleware> _logger;
    public JwtMiddleware(RequestDelegate next, ILogger<JwtMiddleware> logger)
    {
        this.next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation(Environment.NewLine);
        _logger.LogInformation(DateTime.UtcNow.ToString());
        _logger.LogInformation(context.Request.Headers["Authorization"]);
        _logger.LogInformation(Environment.NewLine);
        await next(context);
    }
}