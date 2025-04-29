using Microsoft.AspNetCore.Mvc;
using SignalRServerApi.Helpers;
using SignalRServerApi.NotificationService;
using System.Net;
using System.Xml.Linq;

namespace SignalRServerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : Controller
    {
        private readonly IJwtUtils _jwtUtils;
        private readonly HttpContext ctx;
        private readonly INotificationService service;
        public NotificationController(IJwtUtils jwtUtils,IHttpContextAccessor httpContext,INotificationService notificationService)
        {
            _jwtUtils = jwtUtils;
            ctx = httpContext.HttpContext!;
            service = notificationService;
        
        }
        public async Task Index(CancellationToken token)
        {
            var authuser = _jwtUtils.ValidateJwtTokenString(ctx.Request.Headers.Authorization);
            if (authuser == null) {
                ctx.Response.StatusCode= (int)HttpStatusCode.Forbidden;
                await ctx.Response.WriteAsync("Unauthorized");
                return;
            }
            await service.ConnectAsync(token,authuser);

        }
    }
}
