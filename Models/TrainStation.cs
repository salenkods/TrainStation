using Models.Helpers;

namespace Models;

public class TrainStation
{
    public TrainStation(List<Line> lines, List<Park> parks) {
        Lines = lines;
        Parks = parks;
    }

    public List<Line> Lines { get; }

    public List<Park> Parks { get; }

    public (double minX, double minY, double maxX, double maxY) GetBoundary()  => BoundaryHelper.GetBoundary(Lines);
}
