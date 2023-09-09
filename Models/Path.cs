namespace Models;

public class Path
{
    public List<Line> Lines { get; } = new();

    public bool TryAddLine(Line newLine) {
        if (Lines.Any()) {
            var lastLine = Lines.Last();
            if (lastLine.PointB == newLine.PointA) {
                Lines.Add(newLine);
                return true;
            }
            else {
                return false;
            }
        }
        else {
            Lines.Add(newLine);
            return true;
        }
    }

    public List<Point> GetPoints() {
        var points = new List<Point>();

        if (Lines.Any()) {
            points.Add(Lines.First().PointA);
            Lines.ForEach(x => points.Add(x.PointB));
        }

        return points;
    }
}
