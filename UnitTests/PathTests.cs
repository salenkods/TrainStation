using Models;
using Path = Models.Path;

namespace UnitTests;

public class PathTests
{
    [Fact]
    public void PathIsEmpty_AddLine_LineIsAdded() {
        // Arrange
        var path = new Path();

        // Act
        var result = path.TryAddLine(new Line(1, new Point(1, 1), new Point(2, 1)));

        // Assert
        Assert.True(result);
        Assert.True(path.Lines.Count == 1);
    }

    [Fact]
    public void PathIsNotEmpty_AddValidLine_LineIsAdded() {
        // Arrange
        var path = new Path();
        path.TryAddLine(new Line(1, new Point(1, 1), new Point(2, 1)));

        // Act
        var result = path.TryAddLine(new Line(2, new Point(2, 1), new Point(3, 1)));

        // Assert
        Assert.True(result);
        Assert.True(path.Lines.Count == 2);
    }

    [Fact]
    public void PathIsNotEmpty_AddInvalidLineAsLast_LineIsNotAdded() {
        // Arrange
        var path = new Path();
        path.TryAddLine(new Line(1, new Point(1, 1), new Point(2, 1)));

        // Act
        var result = path.TryAddLine(new Line(2, new Point(3, 1), new Point(4, 1)));

        // Assert
        Assert.False(result);
        Assert.True(path.Lines.Count == 1);
    }

    [Fact]
    public void PathIsNotEmpty_GetPoints_ResultIsCorrect() {
        // Arrange
        var path = new Path();
        path.TryAddLine(new Line(1, new Point(1, 1), new Point(2, 1)));
        path.TryAddLine(new Line(1, new Point(2, 1), new Point(3, 1)));
        path.TryAddLine(new Line(1, new Point(3, 1), new Point(4, 1)));
        path.TryAddLine(new Line(1, new Point(4, 1), new Point(5, 1)));

        // Act
        var points = path.GetPoints();

        // Assert
        Assert.Equal(5, points.Count);
        Assert.Contains(new Point(1, 1), points);
        Assert.Contains(new Point(2, 1), points);
        Assert.Contains(new Point(3, 1), points);
        Assert.Contains(new Point(4, 1), points);
        Assert.Contains(new Point(5, 1), points);
    }

    [Fact]
    public void PathEmpty_GetPoints_ResultIsEmpty() {
        // Arrange
        var path = new Path();

        // Act
        var points = path.GetPoints();

        // Assert
        Assert.Empty(points);
    }
}
