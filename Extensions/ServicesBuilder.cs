namespace StoreApi.Extensions;

public static class ServicesBuilder
{
    public static IServiceCollection AddServices(
        this IServiceCollection services
    )
    {
        services.AddSingleton<TodoDbContext>();

        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();

        services.AddLogging();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        //Inject services
        services.AddScoped<ITodoServices, TodoServices>();

        return services;
    }
}