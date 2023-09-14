using Models.PathFinder;

namespace UnitTests;

public class PathFinderTests
{
    public static IEnumerable<object[]> Data() {
        yield return new object[] { 1, 14, new List<int> { 1, 2, 3, 4, 15, 11, 12, 13, 14 } };
        yield return new object[] { 1, 15, new List<int> { 1, 2, 3, 4, 15 } };
        yield return new object[] { 9, 4, new List<int> { 9, 8, 7, 6, 4 } };
        yield return new object[] { 14, 4, new List<int> { 14, 13, 12, 11, 15, 4 } };
        yield return new object[] { 5, 10, new List<int> { 5, 4, 6, 7, 8, 10 } };
        yield return new object[] { 7, 7, new List<int> { 7 } };
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void MinPathCanBeFound_MinPathFoundCorrectly(int startIndex, int endIndex, List<int> minPathIndexes) {
        // Arrange
        var vertices = GenerateVertices();
        var startVertex = vertices.First(x => x.Id == startIndex);
        var endVertex = vertices.First(x => x.Id == endIndex);

        // Act
        var minPathVertices = PathFinder.FindMinPath(startVertex, endVertex, vertices);

        // Assert
        Assert.Equal(minPathIndexes.Count, minPathVertices.Count);
        for (int i = 0; i < minPathIndexes.Count; i++) {
            Assert.Equal(minPathIndexes[i], minPathVertices[i].Id);
        }
    }

    [Theory]
    [InlineData(1, 50)]
    public void MinPathCanNotBeFound_MinPathNotFound(int startIndex, int endIndex) {
        // Arrange
        var vertices = GenerateVertices();
        var startVertex = vertices.First(x => x.Id == startIndex);
        var endVertex = vertices.First(x => x.Id == endIndex);

        // Act
        var minPathVertices = PathFinder.FindMinPath(startVertex, endVertex, vertices);

        // Assert
        Assert.Null(minPathVertices);
    }

    private List<Vertex> GenerateVertices() {
        var vertex1 = new Vertex() { Id = 1 };
        var vertex2 = new Vertex() { Id = 2 };
        var vertex3 = new Vertex() { Id = 3 };
        var vertex4 = new Vertex() { Id = 4 };
        var vertex5 = new Vertex() { Id = 5 };
        var vertex6 = new Vertex() { Id = 6 };
        var vertex7 = new Vertex() { Id = 7 };
        var vertex8 = new Vertex() { Id = 8 };
        var vertex9 = new Vertex() { Id = 9 };
        var vertex10 = new Vertex() { Id = 10 };
        var vertex11 = new Vertex() { Id = 11 };
        var vertex12 = new Vertex() { Id = 12 };
        var vertex13 = new Vertex() { Id = 13 };
        var vertex14 = new Vertex() { Id = 14 };
        var vertex15 = new Vertex() { Id = 15 };

        var vertex50 = new Vertex() { Id = 50 };
        var vertex51 = new Vertex() { Id = 51 };
        var vertex52 = new Vertex() { Id = 52 };

        // Set neighbours and distances
        vertex1.Neighbours.Add(vertex2, 20);

        vertex2.Neighbours.Add(vertex1, 20);
        vertex2.Neighbours.Add(vertex3, 36);

        vertex3.Neighbours.Add(vertex2, 36);
        vertex3.Neighbours.Add(vertex4, 20);

        vertex4.Neighbours.Add(vertex3, 20);
        vertex4.Neighbours.Add(vertex5, 28);
        vertex4.Neighbours.Add(vertex6, 40);
        vertex4.Neighbours.Add(vertex15, 80);

        vertex5.Neighbours.Add(vertex4, 28);

        vertex6.Neighbours.Add(vertex4, 40);
        vertex6.Neighbours.Add(vertex7, 42);

        vertex7.Neighbours.Add(vertex6, 42);
        vertex7.Neighbours.Add(vertex8, 20);

        vertex8.Neighbours.Add(vertex7, 20);
        vertex8.Neighbours.Add(vertex9, 40);
        vertex8.Neighbours.Add(vertex10, 30);

        vertex9.Neighbours.Add(vertex8, 40);

        vertex10.Neighbours.Add(vertex8, 30);
        vertex10.Neighbours.Add(vertex11, 36);

        vertex11.Neighbours.Add(vertex12, 20);
        vertex11.Neighbours.Add(vertex15, 20);

        vertex12.Neighbours.Add(vertex11, 20);
        vertex12.Neighbours.Add(vertex13, 40);

        vertex13.Neighbours.Add(vertex12, 40);
        vertex13.Neighbours.Add(vertex14, 28);

        vertex14.Neighbours.Add(vertex13, 28);

        vertex15.Neighbours.Add(vertex4, 80);
        vertex15.Neighbours.Add(vertex11, 20);

        vertex50.Neighbours.Add(vertex51, 30);

        vertex51.Neighbours.Add(vertex50, 30);
        vertex51.Neighbours.Add(vertex52, 40);

        vertex52.Neighbours.Add(vertex51, 40);

        return new List<Vertex> {
            vertex1,
            vertex2,
            vertex3,
            vertex4,
            vertex5,
            vertex6,
            vertex7,
            vertex8,
            vertex9,
            vertex10,
            vertex11,
            vertex12,
            vertex13,
            vertex14,
            vertex15,
            vertex50,
            vertex51,
            vertex52,
        };
    }
}
