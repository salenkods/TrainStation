using DataProviders;
using GuiApp.ViewModels.Controllers;
using Models;
using Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GuiApp.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly ITrainStationDataProvider trainStationDataProvider;
    private readonly ITrendStationRenderController trendStationRenderController;

    private TrainStation trainStation;

    public MainViewModel(
        ITrendStationRenderController trendStationRenderController,
        ITrainStationDataProvider trainStationDataProvider) {
        this.trendStationRenderController = trendStationRenderController;
        this.trainStationDataProvider = trainStationDataProvider;

        DrawMinPathCommand = new RelayCommand(OnDrawMinPath);
    }

    public ICommand DrawMinPathCommand { get; set; }

    public ObservableCollection<ParkViewModel>? ParkItems { get; private set; }

    private ParkViewModel? selectedPark;
    public ParkViewModel? SelectedPark {
        get => selectedPark;
        set {
            SetProperty(ref selectedPark, value);
            trendStationRenderController.HighlightPark(value.Park);
        }
    }

    public ObservableCollection<LineViewModel>? LineItems { get; private set; }

    private LineViewModel? selectedStartLine;
    public LineViewModel? SelectedStartLine {
        get => selectedStartLine;
        set {
            SetProperty(ref selectedStartLine, value);
            MinPathLineItems?.Clear();
            trendStationRenderController.HighlightStartLine(value.Line);
        }
    }

    private LineViewModel? selectedEndLine;
    public LineViewModel? SelectedEndLine {
        get => selectedEndLine;
        set {
            SetProperty(ref selectedEndLine, value);
            MinPathLineItems?.Clear();
            trendStationRenderController.HighlightEndLine(value.Line);
        }
    }

    private ObservableCollection<LineViewModel>? minPathLineItems;
    public ObservableCollection<LineViewModel>? MinPathLineItems {
        get => minPathLineItems;
        set {
            SetProperty(ref minPathLineItems, value);
        }
    }

    public void Activate() {
        trainStation = trainStationDataProvider.GetTrainStation();
        trendStationRenderController.DrawStation(trainStation);
        ParkItems = new ObservableCollection<ParkViewModel>(trainStation.Parks.Select(x => new ParkViewModel(x)));
        LineItems = new ObservableCollection<LineViewModel>(trainStation.Lines.Select(x => new LineViewModel(x)));
    }

    private void OnDrawMinPath() {
        if (trainStation is null) {
            return;
        }

        var lines = trainStation.FindMinPath(SelectedStartLine?.Line, SelectedEndLine?.Line);

        if (lines is null) {
            return;
        }

        MinPathLineItems = new ObservableCollection<LineViewModel>(lines.Select(x => new LineViewModel(x)));

        trendStationRenderController.DrawMinPath(lines);
    }
}
