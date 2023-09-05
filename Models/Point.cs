namespace Models;

public class Point
{
    public Point(int x, int y) {
        X = x;
        Y = y;
    }

    public int X { get; }

    public int Y { get; }

    public override bool Equals(object? obj) {
        if ((obj == null) || !GetType().Equals(obj.GetType())) {
            return false;
        }
        else {
            var p = (Point) obj;
            return (X == p.X) && (Y == p.Y);
        }
    }

    public static bool operator ==(Point left, Point right) => Equals(left, right);

    public static bool operator !=(Point left, Point right) => !Equals(left, right);

    public override int GetHashCode() => base.GetHashCode();
}
