# .NET 6 WebApi + Startup Class

:brazil: Você estava com saudades da classe Startup? Então seus problemas estão resolvidos....

:us: Are you missing Startup class? No more....

## How To Run

### Linux

```shell
sh ./run.sh
```

### Windows

```shell
# Powershell Terminal
./run.ps1
```


## Startup.cs

```csharp
public class Startup : IStartup
{
    ...
    public void ConfigurationServices(IServiceCollection services)
    {
        // Add services to the container.

        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
    ...
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
    ...
}
```
## Program.cs

```csharp
WebApplication.CreateBuilder(args)
    .UseStartup<Startup>();
```