namespace dotnet6_webapi_startup;

public interface IStartup
{
    public IConfiguration Configuration { get; }
    void ConfigurationServices(IServiceCollection services);
    void Configure(WebApplication app, IWebHostEnvironment environment);
}

public class Startup : IStartup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigurationServices(IServiceCollection services)
    {
        // Add services to the container.

        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(WebApplication app, IWebHostEnvironment environment)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.MapGet("/", async context => { await context.Response.WriteAsync("Olá eu sou uma API, para mais detalhes tente /swagger"); });
    }
}

public static class StartupExtensions
{
    public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder builder)
        where TStartup : IStartup
    {
        // sim usando reflection porém somente no run, estão o impacto é insignificante
        var startup = Activator.CreateInstance(typeof(TStartup), builder.Configuration) as IStartup;
        if (startup == null)
            throw new ArgumentException("Invalid Startup class!");

        startup.ConfigurationServices(builder.Services);

        var app = builder.Build();
        startup.Configure(app, app.Environment);

        app.Run();

        return builder;
    }
}