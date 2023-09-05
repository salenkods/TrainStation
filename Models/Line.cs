namespace Models;

public class Line
{
    public Line(int number, Point pointA, Point pointB) {
        Number = number;
        PointA = pointA;
        PointB = pointB;
    }

    public int Number { get; }

    public Point PointA { get; }

    public Point PointB { get; }
}
