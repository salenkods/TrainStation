using DataProviders;
using GuiApp.ViewModels.Controllers;
using Models;
using System.Collections.ObjectModel;

namespace GuiApp.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly ITrainStationDataProvider trainStationDataProvider;
    private readonly ITrendStationRenderController trendStationRenderController;

    public MainViewModel(
        ITrendStationRenderController trendStationRenderController,
        ITrainStationDataProvider trainStationDataProvider) {
        this.trendStationRenderController = trendStationRenderController;
        this.trainStationDataProvider = trainStationDataProvider;
    }

    public ObservableCollection<Park>? ParkItems { get; private set; }

    private Park? selectedPark;
    public Park? SelectedPark {
        get => selectedPark;
        set {
            SetProperty(ref selectedPark, value);
            trendStationRenderController.HighlightPark(value);
        }
    }

    public void Activate() {
        var trainStation = trainStationDataProvider.GetTrainStation();
        trendStationRenderController.DrawStation(trainStation);
        ParkItems = new ObservableCollection<Park>(trainStation.Parks);
    }
}
