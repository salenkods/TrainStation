using Models;
using Models.Helpers;

namespace UnitTests;

public class VertexHelperTests
{
    [Fact]
    public void PositiveEdgeIncline_FindPointsAboveTheLine_ExtremePointsFindCorrectly() {
        // Arrange
        var points = new List<Point> {
            new (40, 40),
            new (50, 45),
        };

        // Act
        var vertices = VertexHelper.GetExtremePointsBeyondLine(
            new Point(30, 20),
            new Point(80, 50),
            points,
            (yLine, yOutOfLine) => yOutOfLine > yLine);

        // Assert
        Assert.Equal(2, vertices.Count);
        Assert.Contains(new Point(40, 40), vertices);
        Assert.Contains(new Point(50, 45), vertices);
    }

    [Fact]
    public void PositiveEdgeIncline_FindPointsUnderTheLine_ExtremePointsFindCorrectly() {
        // Arrange
        var points = GeneratePointsForPositiveEdgeIncline();

        // Act
        var vertices = VertexHelper.GetExtremePointsBeyondLine(
            new Point(30, 20),
            new Point(80, 50),
            points,
            (yLine, yOutOfLine) => yOutOfLine < yLine);

        // Assert
        Assert.Equal(2, vertices.Count);
        Assert.Contains(new Point(50, 22), vertices);
        Assert.Contains(new Point(70, 30), vertices);
    }

    [Fact]
    public void NegativeEdgeIncline_FindPointsAboveTheLine_ExtremePointsFindCorrectly() {
        // Arrange
        var points = GeneratePointsForNegativeEdgeIncline();

        // Act
        var vertices = VertexHelper.GetExtremePointsBeyondLine(
            new Point(50, 70),
            new Point(100, 30),
            points,
            (yLine, yOutOfLine) => yOutOfLine > yLine);

        // Assert
        Assert.Equal(2, vertices.Count);
        Assert.Contains(new Point(99, 50), vertices);
        Assert.Contains(new Point(90, 65), vertices);
    }

    [Fact]
    public void NegativeEdgeIncline_FindPointsUnderTheLine_ExtremePointsFindCorrectly() {
        // Arrange
        var points = GeneratePointsForNegativeEdgeIncline();

        // Act
        var vertices = VertexHelper.GetExtremePointsBeyondLine(
            new Point(50, 70),
            new Point(100, 30),
            points,
            (yLine, yOutOfLine) => yOutOfLine < yLine);

        // Assert
        Assert.Equal(2, vertices.Count);
        Assert.Contains(new Point(55, 50), vertices);
        Assert.Contains(new Point(70, 40), vertices);
    }

    [Fact]
    public void LineGiven_FindVertices_VerticesFoundCorrectly() {
        // Arrange
        var points = new List<Point> {
            new (10, 10),
            new (20, 10),
        };

        // Act
        var vertices = VertexHelper.FindVertices(points);

        // Assert
        Assert.Equal(2, vertices.Count);
        Assert.Equal(vertices[0], new Point(10, 10));
        Assert.Equal(vertices[1], new Point(20, 10));
    }

    [Fact]
    public void TriangleGiven_FindVertices_VerticesFoundCorrectly() {
        // Arrange
        var points = new List<Point> {
            new (10, 10),
            new (20, 10),
            new (20, 20),
        };

        // Act
        var vertices = VertexHelper.FindVertices(points);

        // Assert
        Assert.Equal(3, vertices.Count);
        Assert.Equal(vertices[0], new Point(10, 10));
        Assert.Equal(vertices[1], new Point(20, 20));
        Assert.Equal(vertices[2], new Point(20, 10));
    }

    [Fact]
    public void SquareGiven_FindVertices_VerticesFoundCorrectly() {
        // Arrange
        var points = new List<Point> {
            new (10, 10),
            new (20, 10),
            new (20, 20),
            new (10, 20),
        };

        // Act
        var vertices = VertexHelper.FindVertices(points);

        // Assert
        Assert.Equal(4, vertices.Count);
        Assert.Equal(vertices[0], new Point(10, 20));
        Assert.Equal(vertices[1], new Point(20, 20));
        Assert.Equal(vertices[2], new Point(20, 10));
        Assert.Equal(vertices[3], new Point(10, 10));
    }

    [Fact]
    public void HorizontalLinesWithEqualBoundaryGiven_FindVertices_VerticesFoundCorrectly() {
        // Arrange
        var points = new List<Point> {
            new (10, 10), new (20, 10), new (30, 10),
            new (10, 20), new (20, 20), new (30, 20),
            new (10, 30), new (20, 30), new (30, 30),
        };

        // Act
        var vertices = VertexHelper.FindVertices(points);

        // Assert
        Assert.Equal(4, vertices.Count);
        Assert.Equal(vertices[0], new Point(10, 30));
        Assert.Equal(vertices[1], new Point(30, 30));
        Assert.Equal(vertices[2], new Point(30, 10));
        Assert.Equal(vertices[3], new Point(10, 10));
    }

    [Fact]
    public void HorizontalLinesGiven_FindVertices_VerticesFoundCorrectly() {
        // Arrange
        var points = new List<Point> {
            new (10, 10), new (20, 10), new (30, 10),
            new (10, 20), new (20, 20), new (45, 20),
            new (10, 30), new (20, 30), new (30, 30),
        };

        // Act
        var vertices = VertexHelper.FindVertices(points);

        // Assert
        Assert.Equal(5, vertices.Count);
        Assert.Equal(vertices[0], new Point(10, 30));
        Assert.Equal(vertices[1], new Point(30, 30));
        Assert.Equal(vertices[2], new Point(45, 20));
        Assert.Equal(vertices[3], new Point(30, 10));
        Assert.Equal(vertices[4], new Point(10, 10));
    }

    [Fact]
    public void VerticalLinesWithEqualBoundaryGiven_FindVertices_VerticesFoundCorrectly() {
        // Arrange
        var points = new List<Point> {
            new (10, 10), new (10, 20), new (10, 30),
            new (20, 10), new (20, 20), new (20, 30),
            new (30, 10), new (20, 20), new (30, 30),
        };

        // Act
        var vertices = VertexHelper.FindVertices(points);

        // Assert
        Assert.Equal(4, vertices.Count);
        Assert.Equal(vertices[0], new Point(10, 30));
        Assert.Equal(vertices[1], new Point(30, 30));
        Assert.Equal(vertices[2], new Point(30, 10));
        Assert.Equal(vertices[3], new Point(10, 10));
    }

    [Fact]
    public void VerticalLinesGiven_FindVertices_VerticesFoundCorrectly() {
        // Arrange
        var points = new List<Point> {
            new (10, 10), new (10, 20), new (10, 30),
            new (20, 10), new (20, 20), new (20, 45),
            new (30, 10), new (20, 20), new (30, 30),
        };

        // Act
        var vertices = VertexHelper.FindVertices(points);

        // Assert
        Assert.Equal(5, vertices.Count);
        Assert.Equal(vertices[0], new Point(10, 30));
        Assert.Equal(vertices[1], new Point(20, 45));
        Assert.Equal(vertices[2], new Point(30, 30));
        Assert.Equal(vertices[3], new Point(30, 10));
        Assert.Equal(vertices[4], new Point(10, 10));
    }

    [Fact]
    public void LinesGiven_FindVertices_VerticesFoundCorrectly() {
        // Arrange
        var points = new List<Point> {
            new (15, 10), new (20, 10), new (30, 10),
            new (5, 20), new (20, 20), new (40, 15),
            new (10, 30), new (20, 30), new (25, 35),
        };

        // Act
        var vertices = VertexHelper.FindVertices(points);

        // Assert
        Assert.Equal(6, vertices.Count);
        Assert.Equal(vertices[0], new Point(5, 20));
        Assert.Equal(vertices[1], new Point(10, 30));
        Assert.Equal(vertices[2], new Point(25, 35));
        Assert.Equal(vertices[3], new Point(40, 15));
        Assert.Equal(vertices[4], new Point(30, 10));
        Assert.Equal(vertices[5], new Point(15, 10));
    }

    [Fact]
    public void LinesWithDuplicatesGiven_FindVertices_VerticesFoundCorrectly() {
        // Arrange
        var points = new List<Point> {
            new (10, 10),
            new (10, 10),
            new (10, 10),
            new (20, 10),
            new (20, 20),
            new (20, 20),
            new (20, 20),
            new (10, 20),
        };

        // Act
        var vertices = VertexHelper.FindVertices(points);

        // Assert
        Assert.Equal(4, vertices.Count);
        Assert.Equal(vertices[0], new Point(10, 20));
        Assert.Equal(vertices[1], new Point(20, 20));
        Assert.Equal(vertices[2], new Point(20, 10));
        Assert.Equal(vertices[3], new Point(10, 10));
    }

    private List<Point> GeneratePointsForPositiveEdgeIncline() {
        return new List<Point> {
            new (60, 31),
            new (10, 10),
            new (40, 30),
            new (200, 200),
            new (50, 40),
            new (40, 45),
            new (50, 45),
            new Point(30, 20),
            new Point(80, 50),
            new (60, 40),
            new (0, 0),
            new (20, 40),
            new (70, 30),
            new (50, 22),
        };
    }

    private List<Point> GeneratePointsForNegativeEdgeIncline() {
        return new List<Point> {
            new (20, 60),
            new (55, 50),
            new (50, 30),
            new (50, 70),
            new (70, 40),
            new (70, 65),
            new (80, 30),
            new (90, 50),
            new (100, 30),
            new (90, 50),
            new (99, 50),
            new (90, 65),
            new (100, 70),
            new (200, 200),
        };
    }
}
