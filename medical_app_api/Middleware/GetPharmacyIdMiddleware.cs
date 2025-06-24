using Microsoft.AspNetCore.Http;
using System.Net;

namespace medical_app_api.Middleware
{
    public class GetPharmacyIdMiddleware
    {
        private readonly RequestDelegate _next;
        public GetPharmacyIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api/secure"))
            {
                var pharmacyIdClaim = context.User.FindFirst("Pharmacy")?.Value;
                
                if (!Guid.TryParse(pharmacyIdClaim, out var pharmacyId))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "Bad Request"
                    }
                    );
                    return;
                }
                context.Items["PharmacyId"] = pharmacyId;
            }

            await _next(context);
        }
    }
}
