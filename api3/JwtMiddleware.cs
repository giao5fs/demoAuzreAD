using System.Security.Claims;
using Microsoft.Extensions.Primitives;

namespace api3;
public class JwtMiddleware
{
    private readonly ILogger<JwtMiddleware> _logger;
    private readonly RequestDelegate _next;

    public JwtMiddleware(ILogger<JwtMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation(GetClaimValue(context.User, "aud"));
        _logger.LogInformation(GetBearerToken(context.Request.Headers));

        await _next(context);
    }

    protected string GetBearerToken(IHeaderDictionary headers)
    {
        string token = "";
        if (headers.TryGetValue("Authorization", out StringValues authHeaderVal))
        {
            var headerValues = authHeaderVal.ToString().Split(" ");
            if (headerValues.Length > 1)
            {
                token = headerValues[1];
            }
        }
        return token;
    }

    protected string GetClaimValue(ClaimsPrincipal user, string claimIdentifier)
    {
        string claimValue = string.Empty;
        Claim claim = ((ClaimsIdentity)user.Identity).FindFirst(claimIdentifier);
        if (claim != null)
        {
            claimValue = claim.Value;
        }

        return claimValue;
    }
}
