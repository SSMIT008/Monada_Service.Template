namespace MonadaService.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "MonadaService API",
                Version = "v1",
                Description = "Clean Architecture API service generated from Monada.Service.Template."
            });
        });
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MonadaService API v1");
                options.DocumentTitle = "MonadaService API";
            });
        }
        app.MapGet("/", () => "Monada service is running.");
        return app;
    }
}