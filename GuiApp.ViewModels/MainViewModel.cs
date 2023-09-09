using DataProviders;
using GuiApp.ViewModels.Controllers;
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

    public ObservableCollection<ParkViewModel>? ParkItems { get; private set; }

    private ParkViewModel? selectedPark;
    public ParkViewModel? SelectedPark {
        get => selectedPark;
        set {
            SetProperty(ref selectedPark, value);
            trendStationRenderController.HighlightPark(value.Park);
        }
    }

    public void Activate() {
        var trainStation = trainStationDataProvider.GetTrainStation();
        trendStationRenderController.DrawStation(trainStation);
        ParkItems = new ObservableCollection<ParkViewModel>(trainStation.Parks.Select(x => new ParkViewModel(x)));
    }
}
