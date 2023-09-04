namespace Models;

public class TrainStation
{
    public TrainStation(List<Line> lines) {
        Lines = lines;
    }

    public List<Line> Lines { get; }

    public (int minX, int minY, int maxX, int maxY) GetBoundary() {

        int minX = int.MaxValue;
        int minY = int.MaxValue;
        int maxX = 0;
        int maxY = 0;

        foreach (var line in Lines) {
            minX = Math.Min(minX, Math.Min(line.PointA.X, line.PointB.X));
            minY = Math.Min(minY, Math.Min(line.PointA.Y, line.PointB.Y));
            maxX = Math.Max(maxX, Math.Max(line.PointA.X, line.PointB.X));
            maxY = Math.Max(maxY, Math.Max(line.PointA.Y, line.PointB.Y));
        }

        return (minX, minY, maxX, maxY);
    }
}
