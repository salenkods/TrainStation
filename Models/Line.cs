namespace Models;

public class Line
{
    public Line(Point pointA, Point pointB) {
        PointA = pointA;
        PointB = pointB;
    }

    public Point PointA { get; }

    public Point PointB { get; }
}
