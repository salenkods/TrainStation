namespace Models;

public class TrainStation
{
    public TrainStation(List<Line> lines) {
        Lines = lines;
    }

    public List<Line> Lines { get; }
}
