using Models;

namespace UnitTests;

public class TrainStationTests
{
    [Fact]
    public void TrainStationIsCreated_BoundaryCalculatedCorrectly() {
        // Arrange
        var trainStation = new TrainStation(GenerateLines(), new List<Park>());

        // Act
        var (minX, minY, maxX, maxY) = trainStation.GetBoundary();

        // Assert
        Assert.Equal(2, minX);
        Assert.Equal(1, minY);
        Assert.Equal(15, maxX);
        Assert.Equal(3, maxY);
    }

    public static IEnumerable<object[]> MinPathData() {
        yield return new object[] { 0, 1, new List<int> { 0, 1 } };
        yield return new object[] { 0, 10, new List<int> { 0, 1, 4, 7, 9, 10 } };
        yield return new object[] { 3, 6, new List<int> { 3, 4, 5, 6 } };
        yield return new object[] { 9, 2, new List<int> { 9, 7, 4, 1, 0, 2 } };
    }

    [Theory]
    [MemberData(nameof(MinPathData))]
    public void TrainStationIsCreated_FindMinPath_MinPathFoundCorrectly(int startLineIndex, int endLineIndex, List<int> minPathLineIndexes) {
        // Arrange
        var lines = GenerateLines();
        var trainStation = new TrainStation(lines, new List<Park>());
        var startLine = lines.First(x => x.Number == startLineIndex);
        var endLine = lines.First(x => x.Number == endLineIndex);

        // Act
        var minPathLines = trainStation.FindMinPath(startLine, endLine);

        // Assert
        Assert.Equal(minPathLineIndexes.Count, minPathLines.Count);
        for (int i = 0; i < minPathLineIndexes.Count; i++) {
            Assert.Equal(minPathLineIndexes[i], minPathLines[i].Number);
        }
    }

    public static IEnumerable<object[]> MinPathNullData() {
        yield return new object[] { 0, 999 };
        yield return new object[] { 999, 0 };
    }

    [Theory]
    [MemberData(nameof(MinPathNullData))]
    public void TrainStationIsCreated_FindMinPathWhenOneLineIsNull_MinPathIsNotFound(int startLineIndex, int endLineIndex) {
        // Arrange
        var lines = GenerateLines();
        var trainStation = new TrainStation(lines, new List<Park>());
        var startLine = lines.FirstOrDefault(x => x.Number == startLineIndex);
        var endLine = lines.FirstOrDefault(x => x.Number == endLineIndex);

        // Act
        var minPathLines = trainStation.FindMinPath(startLine, endLine);

        // Assert
        Assert.Null(minPathLines);
    }

    // Check different assignments for points A and B
    public static IEnumerable<object[]> CommonPointData() {
        yield return new object[] {
            new Line(1, "1", new Point(1, 1), new Point(2, 2)),
            new Line(2, "2", new Point(2, 2), new Point(3, 3))
        };
        yield return new object[] {
            new Line(1, "1", new Point(1, 1), new Point(2, 2)),
            new Line(2, "2", new Point(3, 3), new Point(2, 2))
        };
        yield return new object[] {
            new Line(1, "1", new Point(2, 2), new Point(1, 1)),
            new Line(2, "2", new Point(2, 2), new Point(3, 3))
        };
        yield return new object[] {
            new Line(1, "1", new Point(2, 2), new Point(1, 1)),
            new Line(2, "2", new Point(3, 3), new Point(2, 2))
        };
    }

    [Theory]
    [MemberData(nameof(CommonPointData))]
    public void TrainStationIsCreated_LinesHaveCommonPoint_MinPathFoundCorrectly(Line startLine, Line endLine) {
        // Arrange
        var trainStation = new TrainStation(new List<Line> { startLine, endLine }, new List<Park>());

        // Act
        var minPathLines = trainStation.FindMinPath(startLine, endLine);

        // Assert
        Assert.Equal(2, minPathLines.Count);
        Assert.Equal(1, minPathLines[0].Number);
        Assert.Equal(2, minPathLines[1].Number);
    }

    [Fact]
    public void TrainStationIsCreated_StartAndEndLineAreTheSame_MinPathFoundCorrectly() {
        // Arrange
        var lines = GenerateLines();
        var trainStation = new TrainStation(lines, new List<Park>());

        // Act
        var minPathLines = trainStation.FindMinPath(lines[5], lines[5]);

        // Assert
        Assert.Equal(2, minPathLines.Count);
        Assert.Equal(5, minPathLines[0].Number);
        Assert.Equal(5, minPathLines[1].Number);
    }

    [Fact]
    public void TrainStationIsCreated_NoWayBetweenStartAndEndLines_MinPathNotFound() {
        // Arrange
        var lines = GenerateLines();
        var lineOut = new Line(99, "Line_99", new(200, 200), new(300, 300));
        lines.Add(lineOut);
        var trainStation = new TrainStation(lines, new List<Park>());

        // Act
        var minPathLines = trainStation.FindMinPath(lines[0], lineOut);

        // Assert
        Assert.Null(minPathLines);
    }

    private List<Line> GenerateLines() {
        var lines = new List<Line> {
            new (0, "0", new (8, 1), new (12, 1)),
            new (1, "1", new (7, 2), new (8, 1)),
            new (2, "2", new (12, 1), new (13, 2)),
            new (3, "3", new (4, 2), new (6, 2)),
            new (4, "4", new (6, 2), new (7, 2)),
            new (5, "5", new (7, 2), new (13, 2)),
            new (6, "6", new (13, 2), new (15, 2)),
            new (7, "7", new (5, 3), new (6, 2)),
            new (8, "8", new (2, 3), new (5, 3)),
            new (9, "9", new (5, 3), new (9, 3)),
            new (10, "10", new (9, 3), new (14, 3))
        };

        return lines;
    }
}
