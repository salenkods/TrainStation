using Models;

namespace GuiApp.ViewModels.Controllers;

public class TrendStationRenderController : ITrendStationRenderController
{
    public event EventHandler<TrainStation>? DrawStationRequested;

    public event EventHandler<Park>? HighlightParkRequested;

    public void DrawStation(TrainStation trainStation) {
        DrawStationRequested?.Invoke(null, trainStation);
    }

    public void HighlightPark(Park park) {
        HighlightParkRequested?.Invoke(null, park);
    }
}
