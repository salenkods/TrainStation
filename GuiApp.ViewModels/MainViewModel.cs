using DataProviders;
using GuiApp.ViewModels.Controllers;

namespace GuiApp.ViewModels;

public class MainViewModel
{
    private readonly ITrainStationDataProvider trainStationDataProvider;
    private readonly ITrendStationRenderController trendStationRenderController;

    public MainViewModel(
        ITrendStationRenderController trendStationRenderController,
        ITrainStationDataProvider trainStationDataProvider) {
        this.trendStationRenderController = trendStationRenderController;
        this.trainStationDataProvider = trainStationDataProvider;
    }

    public void Activate() {
        var trainStation = trainStationDataProvider.GetTrainStation();
        trendStationRenderController.Draw(trainStation);
    }
}
