using Models;

namespace GuiApp.ViewModels.Controllers;

public interface ITrendStationRenderController
{
    void DrawStation(TrainStation trainStation);

    void HighlightPark(Park park);

    event EventHandler<TrainStation> DrawStationRequested;

    event EventHandler<Park> HighlightParkRequested;
}
