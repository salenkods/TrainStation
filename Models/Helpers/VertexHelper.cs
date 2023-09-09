using Framework.Extensions;

namespace Models.Helpers;

public static class VertexHelper
{
    public static List<Point> FindVertices(List<Point> points) {
        var vertices = new List<Point>();

        // Find points with minX, maxY,maxX and minY 
        var minX = points.Min(p => p.X);
        var maxY = points.Max(p => p.Y);
        var maxX = points.Max(p => p.X);
        var minY = points.Min(p => p.Y);

        var pointsMinX = GetExtremePointsByY(points.Where(p => p.X == minX));
        var pointsMaxY = GetExtremePointsByX(points.Where(p => p.Y == maxY));
        var pointsMaxX = GetExtremePointsByY(points.Where(p => p.X == maxX));
        var pointsMinY = GetExtremePointsByX(points.Where(p => p.Y == minY));

        // Consider edges in every of four areas
        // Add vertices in a clockwise order

        // Top left area
        vertices.AddIfNotContain(pointsMinX.MaxY);
        foreach (var p in GetExtremePointsBeyondLine(pointsMinX.MaxY, pointsMaxY.MinX, points, (yLine, yOutOfLine) => yOutOfLine > yLine).OrderBy(p => p.X)) {
            vertices.AddIfNotContain(p);
        }
        vertices.AddIfNotContain(pointsMaxY.MinX);

        // Top right area
        vertices.AddIfNotContain(pointsMaxY.MaxX);
        foreach (var p in GetExtremePointsBeyondLine(pointsMaxY.MaxX, pointsMaxX.MaxY, points, (yLine, yOutOfLine) => yOutOfLine > yLine).OrderBy(p => p.X)) {
            vertices.AddIfNotContain(p);
        }
        vertices.AddIfNotContain(pointsMaxX.MaxY);

        // Bottom right are
        vertices.AddIfNotContain(pointsMaxX.MinY);
        foreach (var p in GetExtremePointsBeyondLine(pointsMinY.MaxX, pointsMaxX.MinY, points, (yLine, yOutOfLine) => yOutOfLine < yLine).OrderByDescending(p => p.X)) {
            vertices.AddIfNotContain(p);
        }
        vertices.AddIfNotContain(pointsMinY.MaxX);

        // Bottom left are
        vertices.AddIfNotContain(pointsMinY.MinX);
        foreach (var p in GetExtremePointsBeyondLine(pointsMinX.MinY, pointsMinY.MinX, points, (yLine, yOutOfLine) => yOutOfLine < yLine).OrderByDescending(p => p.X)) {
            vertices.AddIfNotContain(p);
        }
        vertices.AddIfNotContain(pointsMinX.MinY);

        return vertices;
    }

    public static List<Point> GetExtremePointsBeyondLine(Point pointA, Point pointB, List<Point> points, Func<double, double, bool> isBeyond) {
        if (pointA == pointB) {
            return new List<Point>();
        }

        var (kLine, bLine) = GetLineEquationCoefficients(pointA, pointB);

        // Points bounded by perpendiculars from points A and B
        var boundedPoints = points
            .Where(p => IsWithin(p.X, pointA.X, pointB.X) && IsWithin(p.Y, pointA.Y, pointB.Y))
            .ToList();

        for (int i = 0; i < boundedPoints.Count; i++) {
            var point = boundedPoints[i];
            if (isBeyond(kLine * point.X + bLine, point.Y)) {
                // This point is beyond the line and might be a vertex
                var (k1, b1) = GetLineEquationCoefficients(point, pointA);
                var (k2, b2) = GetLineEquationCoefficients(point, pointB);

                // Find and remove points which are covered by current one
                for (int j = 0; j < boundedPoints.Count; j++) {
                    var boundedPoint = boundedPoints[j];

                    if (point == boundedPoint) {
                        continue;
                    }

                    if ((IsWithin(boundedPoint.X, point.X, pointA.X) && !isBeyond(k1 * boundedPoint.X + b1, boundedPoint.Y)) ||
                        (IsWithin(boundedPoint.X, point.X, pointB.X) && !isBeyond(k2 * boundedPoint.X + b2, boundedPoint.Y))) {

                        // BoundedPoint is covered by point and hence can not be a vertex

                        if (j < i) {
                            i--;
                        }

                        boundedPoints.RemoveAt(j);
                        j--;
                    }
                }
            }
            else {
                boundedPoints.RemoveAt(i);
                i--;
            }
        }

        return boundedPoints;
    }

    private static (Point MinY, Point MaxY) GetExtremePointsByY(IEnumerable<Point> points) {
        return (points.MinBy(p => p.Y)!, points.MaxBy(p => p.Y)!);
    }

    private static (Point MinX, Point MaxX) GetExtremePointsByX(IEnumerable<Point> points) {
        return (points.MinBy(p => p.X)!, points.MaxBy(p => p.X)!);
    }

    private static (double k, double b) GetLineEquationCoefficients(Point pointA, Point pointB) {
        var k = (pointB.Y - pointA.Y) / (pointB.X - pointA.X);
        var b = pointA.Y - k * pointA.X;
        return (k, b);
    }

    private static bool IsWithin(double value, double firstBound, double secondBound) {
        var min = Math.Min(firstBound, secondBound);
        var max = Math.Max(firstBound, secondBound);
        return value > min && value < max;
    }
}
