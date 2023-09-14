namespace Models;

public class Line
{
    public Line(int number, string name, Point pointA, Point pointB) {
        Number = number;
        Name = name;
        PointA = pointA;
        PointB = pointB;
    }

    public int Number { get; }

    public string Name {  get; }

    public Point PointA { get; }

    public Point PointB { get; }
}
