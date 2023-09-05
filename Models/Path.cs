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
}
