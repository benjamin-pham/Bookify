using Bookify.Application.Abstractions.Behaviors;
using Bookify.Domain.Bookings;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bookify.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            
            //configuration.AddBehavior(typeof(LoggingBehavior<,>));

            //configuration.AddBehavior(typeof(ValidationBehavior<,>));
        }).AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(QueryCachingBehavior<,>));

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddTransient<PricingService>();

        return services;
    }
}
