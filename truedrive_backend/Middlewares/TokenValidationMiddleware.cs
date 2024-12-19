using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using truedrive_backend.Data;

namespace truedrive_backend.Middlewares
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, TrueDriveContext dbContext)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var tokenEntry = dbContext.Tokens.SingleOrDefault(t => t.JwtToken == token);
                if (tokenEntry == null || !tokenEntry.IsValid)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Invalid token");
                    return;
                }
            }

            await _next(context);
        }
    }
}
