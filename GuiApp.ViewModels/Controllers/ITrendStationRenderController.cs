using Models;

namespace GuiApp.ViewModels.Controllers;

public interface ITrendStationRenderController
{
    event EventHandler<TrainStation> DrawStationRequested;

    event EventHandler<Park> HighlightParkRequested;

    event EventHandler<Line> HighlightStartLineRequested;

    event EventHandler<Line> HighlightEndLineRequested;

    event EventHandler<List<Line>> DrawMinPathRequested;

    void DrawStation(TrainStation trainStation);

    void HighlightPark(Park park);

    void HighlightStartLine(Line line);

    void HighlightEndLine(Line line);

    void DrawMinPath(List<Line> lines);
}
