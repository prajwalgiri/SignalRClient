using Microsoft.AspNetCore.Authentication.JwtBearer;
using SignalRServerApi.Controllers;
using SignalRServerApi.Helpers;
using SignalRServerApi.NotificationService;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options=>
{
        options.IdleTimeout = TimeSpan.FromSeconds(10);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
});
//only for jwt validation middleware
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddSingleton<IJwtUtils, JwtUtils>();
builder.Services.AddSingleton<IUserService, UserService>();

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<INotificationManager, NotificationManager>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddAuthentication(options =>
{
    // Identity made Cookie authentication the default.
    // However, we want JWT Bearer Auth to be the default.
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    // Configure the Authority to the expected value for
    // the authentication provider. This ensures the token
    // is appropriately validated.
    options.Authority = "https://localhost:7109"; // TODO: Update URL

    // We have to hook the OnMessageReceived event in order to
    // allow the JWT authentication handler to read the access
    // token from the query string when a WebSocket or 
    // Server-Sent Events request comes in.

    // Sending the access token in the query string is required when using WebSockets or ServerSentEvents
    // due to a limitation in Browser APIs. We restrict it to only calls to the
    // SignalR hub in this code.
    // See https://docs.microsoft.com/aspnet/core/signalr/security#access-token-logging
    // for more information about security considerations when using
    // the query string to transmit the access token.
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Headers["Authorization"].ToString();

            // If the request is for our hub...
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) 
                )
            {
                // Read the token out of the query string
                context.Token = accessToken.Split(' ')[1];
            }

            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.MapHub<MiddlewareHub>("/connectionhub");
app.UseSession();
//app.MapGet("/notifications", async Task (HttpContext ctx, INotificationService service, CancellationToken token) =>
//{
//    var name = ctx.Request.Query["name"];
//    await service.ConnectAsync(token, name);
//});
//app.MapGet("/notifications/mark-as-read", async Task (HttpContext ctx, INotificationService service, CancellationToken token) =>
//{
//    var id = ctx.Request.Query["id"];
//    var name = ctx.Request.Query["user"];
//    await service.MarkAsRead(id, name, token);
//});
app.MapPost("/notifications/add", async Task (HttpContext ctx,
    INotificationService service,
    CancellationToken token

    ) =>
{
    var users = ctx.Request.Form["users"];
    var msg = ctx.Request.Form["msg"];
    var userList = JsonSerializer.Deserialize<List<string>>(users) ?? new List<string>();
    var @notification = new Notification(Guid.NewGuid(), msg);
    await service.AddNotification(@notification, userList, token);
});
app.Run();
