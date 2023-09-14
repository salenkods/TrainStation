using Framework.Extensions;
using Models.Helpers;
using Models.PathFinder;

namespace Models;

public class TrainStation
{
    public TrainStation(List<Line> lines, List<Park> parks) {
        Lines = lines;
        Parks = parks;
    }

    public List<Line> Lines { get; }

    public List<Park> Parks { get; }

    public (double minX, double minY, double maxX, double maxY) GetBoundary() => BoundaryHelper.GetBoundary(Lines);

    public List<Line>? FindMinPath(Line startLine, Line endLine) {
        // Take point A for both lines
        var startPoint = startLine.PointA;
        var endPoint = endLine.PointA;

        if (startPoint == endPoint) {
            return new List<Line> { startLine, endLine };
        }

        // Collect all points, create assotiated vertices
        var points = new List<Point>();
        var vertices = new List<Vertex>();
        var map = new Dictionary<Point, Vertex>();

        foreach (var line in Lines) {
            if (points.AddIfNotContain(line.PointA)) {
                var vertex = new Vertex();
                vertices.Add(vertex);
                map[line.PointA] = vertex;
            }

            if (points.AddIfNotContain(line.PointB)) {
                var vertex = new Vertex();
                vertices.Add(vertex);
                map[line.PointB] = vertex;
            }
        }

        // Find neighbours for every point, calculate distance
        foreach (var point in points) {
            var lines = Lines.Where(x => x.PointA == point || x.PointB == point);
            var neighbourPoints = new List<Point>();
            foreach (var line in lines) {
                if (line.PointA != point) {
                    neighbourPoints.AddIfNotContain(line.PointA);
                }

                if (line.PointB != point) {
                    neighbourPoints.AddIfNotContain(line.PointB);
                }
            }

            foreach (var neighbourPoint in neighbourPoints) {
                var deltaX = Math.Abs(point.X - neighbourPoint.X);
                var deltaY = Math.Abs(point.Y - neighbourPoint.Y);
                var distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                map[point].Neighbours.Add(map[neighbourPoint], distance);
            }
        }

        // Calculate min path
        var minPathVertices = PathFinder.PathFinder.FindMinPath(map[startPoint], map[endPoint], vertices);
        if (minPathVertices is null) {
            return null;
        }

        // Convert vertices back to points
        var minPathPoints = new List<Point>();
        foreach (var vertex in minPathVertices) {
            var point = map.FirstOrDefault(x => x.Value == vertex).Key;
            if (point is not null) {
                minPathPoints.Add(point);
            }
            else {
                // Log warning because every vertex should have associated point
            }
        }

        // First need to understand whether pointB of startLine and endLine are inside result list
        int startIndex = startLine.PointB == minPathPoints[1] ? 1 : 0;
        int endIndex = endLine.PointB == minPathPoints[^2] ? minPathPoints.Count - 2 : minPathPoints.Count - 1;

        // Find all lines which contain min path points
        var resultLines = new List<Line> { startLine };

        for (int i = startIndex; i < endIndex; i++) {
            var p1 = minPathPoints[i];
            var p2 = minPathPoints[i + 1];

            foreach (var line in Lines) {
                if ((line.PointA == p1 && line.PointB == p2) ||
                    (line.PointA == p2 && line.PointB == p1)) {
                    resultLines.Add(line);
                    break;
                }
            }
        }

        resultLines.Add(endLine);

        return resultLines;
    }
}
