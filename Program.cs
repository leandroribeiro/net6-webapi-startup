using dotnet6_webapi_startup;

WebApplication.CreateBuilder(args)
    .UseStartup<Startup>();