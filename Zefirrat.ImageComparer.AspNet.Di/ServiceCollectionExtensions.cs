using Microsoft.Extensions.DependencyInjection;
using Zefirrat.ImageComparer.Abstrtactions;

namespace Zefirrat.ImageComparer.AspNet.Di
{

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddImageComparer(this IServiceCollection serviceCollection)
        {
            return AddImageComparer(serviceCollection, new ImageComparerOptions());
        }

        public static IServiceCollection AddImageComparer(
            this IServiceCollection serviceCollection,
            ImageComparerOptions options)
        {
            serviceCollection.AddSingleton<IImageComparer>(new ImageComparer(options));
            return serviceCollection;
        }
    }
}