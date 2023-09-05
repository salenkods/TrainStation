using Models;

namespace UnitTests;

public class TrainStationTests
{
    [Fact]
    public void TrainStatinIsCreated_BoundaryCalculatedCorrectly() {
        // Arrange
        var points = new List<(Point PointA, Point PointB)>() {
            new (new (8, 1), new (12, 1)),
            new (new (7, 2), new (8, 1)),
            new (new (12, 1), new (13, 2)),
            new (new (4, 2), new (6, 2)),
            new (new (6, 2), new (7, 2)),
            new (new (7, 2), new (13, 2)),
            new (new (13, 2), new (15, 2)),
            new (new (5, 3), new (6, 2)),
            new (new (2, 3), new (5, 3)),
            new (new (5, 3), new (9, 3)),
            new (new (9, 3), new (14, 3))
        };

        var lines = points
            .Select((p, i) => new Line(i, points[i].PointA, points[i].PointB))
            .ToList();

        var trainStation = new TrainStation(lines, new List<Park>());

        // Act
        var (minX, minY, maxX, maxY) = trainStation.GetBoundary();

        // Assert
        Assert.Equal(2, minX);
        Assert.Equal(1, minY);
        Assert.Equal(15, maxX);
        Assert.Equal(3, maxY);
    }
}
