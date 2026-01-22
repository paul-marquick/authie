var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/p1-token", async (RequestData requestData) =>
{
    if (requestData.client_id == "p1-client" && requestData.client_secret == "super")
    {
        return Results.Ok("tesla");
    }
    else
    {
        return Results.BadRequest("{ error: invalid_grant }");
    }
});

app.MapGet("/p1", () => {
    return "Best platform in the world.";
}).AddEndpointFilter(async (context, next) => {
    if (context.HttpContext.Request.Headers.TryGetValue("authorization", out var auth))
    {
        return auth == "tesla" ? await next(context) : Results.Unauthorized();
    }
    return Results.Unauthorized();
});


app.MapPost("/seccl-token", async (HttpContext httpContext, RequestData requestData) =>
{
    if (requestData.client_id == "seccl-client" && requestData.client_secret == "nerds")
    {
        // Extra weirdness, need a request header.
        if (httpContext.Request.Headers.TryGetValue("seccl-id", out var key))
        {
            if (key == "999")
            {
                return Results.Ok("convoluted");
            }
            return Results.BadRequest("{ error: invalid_grant }");
        }
        return Results.BadRequest("{ error: invalid_grant }");
    }
    return Results.BadRequest("{ error: invalid_grant }");
});

app.MapGet("/seccl", () => {
    return "Seccl is cool.";
}).AddEndpointFilter(async (context, next) => {
    if (context.HttpContext.Request.Headers.TryGetValue("authorization", out var auth))
    {
        if (auth == "convoluted")
        {
            // Extra weirdness in the RESOURCE REQUEST, need a request query param.
            if (context.HttpContext.Request.Query.TryGetValue("x-api-key", out var secclId))
            {
                if (secclId == "seccl-key")
                {
                    await next(context);
                }
                else
                {
                    return Results.Unauthorized();
                }
            }
            return Results.Unauthorized();
        }
        return Results.Unauthorized();
    }
    return Results.Unauthorized();
});

app.MapPost("/intelliflo-token", async (HttpContext httpContext, RequestData requestData) =>
{
    if (requestData.client_id == "intelliflo-client" && requestData.client_secret == "cool")
    {
        // Extra weirdness, need a request query string value.
        if (httpContext.Request.Query.TryGetValue("zzz", out var zzz))
        {
            if (zzz == "nice-key")
            {
                return Results.Ok("webhook");
            }
            return Results.BadRequest("{ error: invalid_grant }");
        }
        return Results.BadRequest("{ error: invalid_grant }");
    }
    return Results.BadRequest("{ error: invalid_grant }");
});

app.MapGet("/intelliflo", () => {
    return "Toilet flush system.";
}).AddEndpointFilter(async (context, next) => {
    if (context.HttpContext.Request.Headers.TryGetValue("authorization", out var auth))
    {
        if (auth == "webhook")
        {
            // Extra weirdness in the RESOURCE REQUEST, need a request header.
            if (context.HttpContext.Request.Headers.TryGetValue("sys", out var key))
            {
                if (key == "ai-toilet")
                {
                    await next(context);
                }
                else
                {
                    return Results.Unauthorized();
                }
            }
            return Results.Unauthorized();
        }
        return Results.Unauthorized();
    }
    return Results.Unauthorized();
});

app.Run();

record RequestData(string client_id, string client_secret);