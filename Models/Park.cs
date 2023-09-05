namespace Models;

public class Park
{
    public Park(string name, List<Path> paths) {
        Name = name;
        Paths = paths;
    }

    public string Name { get; }

    public List<Path> Paths { get; }
}
