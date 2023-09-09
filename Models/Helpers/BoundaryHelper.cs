namespace Models.Helpers;

public static class BoundaryHelper
{
    public static (double minX, double minY, double maxX, double maxY) GetBoundary(IList<Line> lines) {

        var minX = double.MaxValue;
        var minY = double.MaxValue;
        var maxX = 0d;
        var maxY = 0d;

        foreach (var line in lines) {
            minX = Math.Min(minX, Math.Min(line.PointA.X, line.PointB.X));
            minY = Math.Min(minY, Math.Min(line.PointA.Y, line.PointB.Y));
            maxX = Math.Max(maxX, Math.Max(line.PointA.X, line.PointB.X));
            maxY = Math.Max(maxY, Math.Max(line.PointA.Y, line.PointB.Y));
        }

        return (minX, minY, maxX, maxY);
    }
}
