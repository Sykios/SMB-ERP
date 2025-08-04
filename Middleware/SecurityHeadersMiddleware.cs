using SMBErp.Configuration;

namespace SMBErp.Middleware;

/// <summary>
/// Middleware zur Implementierung von Security Headers
/// </summary>
public class SecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;
    private readonly SecuritySettings _securitySettings;

    public SecurityHeadersMiddleware(RequestDelegate next, SecuritySettings securitySettings)
    {
        _next = next;
        _securitySettings = securitySettings;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (_securitySettings.EnableSecurityHeaders)
        {
            // X-Content-Type-Options: nosniff
            if (_securitySettings.EnableXContentTypeOptions)
                context.Response.Headers.Append("X-Content-Type-Options", "nosniff");

            // X-Frame-Options: DENY
            if (_securitySettings.EnableXFrameOptions)
                context.Response.Headers.Append("X-Frame-Options", "DENY");

            // Content-Security-Policy
            if (_securitySettings.EnableCsp)
            {
                context.Response.Headers.Append("Content-Security-Policy",
                    "default-src 'self'; " +
                    "script-src 'self' 'unsafe-inline'; " +
                    "style-src 'self' 'unsafe-inline'; " +
                    "img-src 'self' data:; " +
                    "font-src 'self'; " +
                    "connect-src 'self'; " +
                    "frame-src 'self'; " +
                    "object-src 'none'; " +
                    "base-uri 'self';");
            }

            // Strict-Transport-Security (HSTS)
            if (_securitySettings.RequireHttps && !context.Request.IsHttps)
            {
                string redirectUrl = "https://" + context.Request.Host + context.Request.Path + context.Request.QueryString;
                context.Response.Redirect(redirectUrl, true);
                return;
            }
            else
            {
                context.Response.Headers.Append("Strict-Transport-Security", $"max-age={_securitySettings.HstsMaxAge}; includeSubDomains");
            }

            // Referrer-Policy
            context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");

            // Permissions-Policy (vormals Feature-Policy)
            context.Response.Headers.Append("Permissions-Policy", 
                "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()");

            // Cross-Origin-Embedder-Policy
            context.Response.Headers.Append("Cross-Origin-Embedder-Policy", "require-corp");

            // Cross-Origin-Opener-Policy
            context.Response.Headers.Append("Cross-Origin-Opener-Policy", "same-origin");

            // Cross-Origin-Resource-Policy
            context.Response.Headers.Append("Cross-Origin-Resource-Policy", "same-origin");
        }

        await _next(context);
    }
}
