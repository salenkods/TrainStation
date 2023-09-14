namespace Models;

public class Point
{
    public Point(double x, double y) {
        X = x;
        Y = y;
    }

    public double X { get; }

    public double Y { get; }

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

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public override string ToString() => $"X: {X}, Y: {Y}";
}
