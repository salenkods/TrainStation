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

    public (int minX, int minY, int maxX, int maxY) GetBoundary()  => BoundaryHelper.GetBoundary(Lines);
}
