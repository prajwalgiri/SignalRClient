using Microsoft.AspNetCore.Mvc;
using SignalRServerApi.Helpers;
using SignalRServerApi.NotificationService;
using System.Net;
using System.Xml.Linq;

namespace SignalRServerApi.Controllers
{
    public class NotificationController : Controller
    {
        private readonly IJwtUtils _jwtUtils;
        public NotificationController(IJwtUtils jwtUtils) { _jwtUtils = jwtUtils; }
        public async Task Index(HttpContext ctx, INotificationService service, CancellationToken token)
        {
            var authuser = _jwtUtils.ValidateJwtToken(ctx.Request.Headers.Authorization);
            if (authuser == null) {
                ctx.Response.StatusCode= (int)HttpStatusCode.Forbidden;
                await ctx.Response.WriteAsync("Unauthorized");
                return;
            }
            await service.ConnectAsync(token,authuser);

        }
    }
}
