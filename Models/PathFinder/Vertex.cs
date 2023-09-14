namespace Models.PathFinder;

public class Vertex
{
    public int Id { get; set; }

    public Dictionary<Vertex, double> Neighbours { get; set; } = new Dictionary<Vertex, double>();

    /// <summary>
    /// Whether point had already been used for path length calculation for other vertices
    /// </summary>
    public bool IsVisited { get; set; }

    /// <summary>
    /// Lenght of the path from the start point to the current one
    /// </summary>
    public double PathLength { get; set; }

    public override string ToString() => $"Id: {Id} / IsVisited: {IsVisited} / PathLength: {PathLength}";
}
