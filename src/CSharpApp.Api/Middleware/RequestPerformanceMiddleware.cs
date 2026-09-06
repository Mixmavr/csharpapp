using System.Diagnostics;
using CSharpApp.Core.Settings;
using Microsoft.Extensions.Options;

namespace CSharpApp.Api.Middleware;

public sealed class RequestPerformanceMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestPerformanceMiddleware> _logger;
    private readonly PerformanceSettings _performanceSettings;

    public RequestPerformanceMiddleware(
        RequestDelegate next,
        ILogger<RequestPerformanceMiddleware> logger,
        IOptions<PerformanceSettings> performanceSettings)
    {
        _next = next;
        _logger = logger;
        _performanceSettings = performanceSettings.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();

            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            var logLevel = elapsedMilliseconds > _performanceSettings.SlowRequestThresholdMilliseconds
                ? LogLevel.Warning
                : LogLevel.Information;

            _logger.Log(
                logLevel,
                "Request {Method} {Path} completed with status code {StatusCode} in {ElapsedMilliseconds} ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                elapsedMilliseconds);
        }
    }
}