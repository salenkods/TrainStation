using DataProviders;
using GuiApp.ViewModels;
using GuiApp.ViewModels.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GuiApp;

public class ServiceLocator
{
    private static IServiceProvider? serviceProvider;

    public static void InitializeServices() {
        var services = new ServiceCollection();

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<ITrainStationDataProvider, TrainStationDataProvider>();
        services.AddSingleton<ITrendStationRenderController, TrendStationRenderController>();

        serviceProvider = services.BuildServiceProvider();
    }

    public static T GetInstance<T>() where T : class {
        if (serviceProvider is null) {
            throw new InvalidOperationException("Service provider is not initialized.");
        }

        return serviceProvider.GetService<T>()
            ?? throw new InvalidOperationException($"Required instance {typeof(T).Name} not found.");
    }
}
