namespace Models.PathFinder;

public static class PathFinder
{
    public static List<Vertex>? FindMinPath(Vertex startVertex, Vertex endVertex, List<Vertex> vertices) {
        // Vertex / Previous vertex from which we should go to get the smallest path
        var pathMap = new Dictionary<Vertex, Vertex>();

        // Set initial path length for every vertex
        startVertex.IsVisited = true;
        startVertex.PathLength = 0;
        foreach (var vertex in vertices.Where(x => !x.IsVisited)) {
            if (startVertex.Neighbours.TryGetValue(vertex, out var length)) {
                vertex.PathLength = length;
                pathMap[vertex] = startVertex;
            }
            else {
                vertex.PathLength = double.PositiveInfinity;
            }
        }

        // Go through unvisited vertices
        while (vertices.Where(x => !x.IsVisited).Any()) {
            var vertexWithMinPathLength = vertices.Where(x => !x.IsVisited).MinBy(x => x.PathLength);

            foreach (var neighbour in vertexWithMinPathLength.Neighbours.Where(x => !x.Key.IsVisited)) {
                var neighbourVertex = neighbour.Key;
                var neighbourEdgeLength = neighbour.Value;

                var newPathLength = vertexWithMinPathLength.PathLength + neighbourEdgeLength;
                if (newPathLength < neighbourVertex.PathLength) {
                    neighbourVertex.PathLength = newPathLength;
                    pathMap[neighbourVertex] = vertexWithMinPathLength;
                }
            }

            vertexWithMinPathLength.IsVisited = true;
        }

        var resultVertices = new List<Vertex> { endVertex };
        CalculateMinPath(resultVertices, pathMap);

        if (!resultVertices.Contains(startVertex)) {
            return null;
        }

        return resultVertices;
    }

    private static void CalculateMinPath(List<Vertex> vertices, Dictionary<Vertex, Vertex> map) {
        if (map.TryGetValue(vertices.First(), out var prevVertex)) {
            vertices.Insert(0, prevVertex);
            CalculateMinPath(vertices, map);
        }
    }
}
