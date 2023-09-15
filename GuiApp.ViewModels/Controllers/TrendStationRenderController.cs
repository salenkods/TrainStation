using Models;

namespace GuiApp.ViewModels.Controllers;

public class TrendStationRenderController : ITrendStationRenderController
{
    public event EventHandler<TrainStation>? DrawStationRequested;

    public event EventHandler<Park>? HighlightParkRequested;

    public event EventHandler<Line>? HighlightStartLineRequested;

    public event EventHandler<Line>? HighlightEndLineRequested;

    public event EventHandler<List<Line>>? DrawMinPathRequested;

    public void DrawStation(TrainStation trainStation) {
        DrawStationRequested?.Invoke(null, trainStation);
    }

    public void HighlightPark(Park park) {
        HighlightParkRequested?.Invoke(null, park);
    }

    public void HighlightStartLine(Line line) {
        HighlightStartLineRequested?.Invoke(null, line);
    }

    public void HighlightEndLine(Line line) {
        HighlightEndLineRequested?.Invoke(null, line);
    }

    public void DrawMinPath(List<Line> lines) {
        DrawMinPathRequested?.Invoke(null, lines);
    }
}
