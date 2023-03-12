using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddPresentation(this ServiceCollection services)
        {
            return services;
        }
    }
}
