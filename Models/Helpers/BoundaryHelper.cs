namespace Models.Helpers;

public static class BoundaryHelper
{
    public static (int minX, int minY, int maxX, int maxY) GetBoundary(IList<Line> lines) {

        int minX = int.MaxValue;
        int minY = int.MaxValue;
        int maxX = 0;
        int maxY = 0;

        foreach (var line in lines) {
            minX = Math.Min(minX, Math.Min(line.PointA.X, line.PointB.X));
            minY = Math.Min(minY, Math.Min(line.PointA.Y, line.PointB.Y));
            maxX = Math.Max(maxX, Math.Max(line.PointA.X, line.PointB.X));
            maxY = Math.Max(maxY, Math.Max(line.PointA.Y, line.PointB.Y));
        }

        return (minX, minY, maxX, maxY);
    }
}
