using Models;

namespace GuiApp.ViewModels.Controllers;

public class TrendStationRenderController : ITrendStationRenderController
{
    public event EventHandler<TrainStation>? DrawRequested;

    public void Draw(TrainStation trainStation) {
        DrawRequested?.Invoke(null, trainStation);
    }
}
