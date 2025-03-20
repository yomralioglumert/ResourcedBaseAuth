using System.Security.Claims;
using Authentication.Database;
using Authentication.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Extensions;

public class PermissionMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
{
    // Public endpoint'ler - herkesin erişebileceği endpoint'ler
    private static readonly HashSet<(string Endpoint, string Method)> PublicEndpoints = new()
    {
        ("/login", "POST"),
        ("/register", "POST"),
        ("/refresh", "POST")
    };

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.Request.Path.Value;
        var method = context.Request.Method;

        if (string.IsNullOrEmpty(endpoint))
        {
            await next(context);
            return;
        }

        // Public endpoint kontrolü
        if (PublicEndpoints.Contains((endpoint, method)))
        {
            await next(context);
            return;
        }

        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            context.Response.StatusCode = 401;
            return;
        }

        using (var scope = serviceScopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            
            var hasPermission = await dbContext.UserPermissions
                .Include(up => up.Permission)
                .AnyAsync(up => up.UserId.ToString() == userId &&
                              up.Permission.Endpoint == endpoint &&
                              up.Permission.Method == method);

            if (!hasPermission)
            {
                context.Response.StatusCode = 403;
                return;
            }
        }

        await next(context);
    }
} 