using Models;

namespace GuiApp.ViewModels.Controllers;

public interface ITrendStationRenderController
{
    void Draw(TrainStation trainStation);

    event EventHandler<TrainStation> DrawRequested;
}
