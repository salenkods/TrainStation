using Models;

namespace DataProviders;

public class TrainStationDataProvider : ITrainStationDataProvider
{
    public TrainStation GetTrainStation() {
        var lines = new List<Line>() {
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
            new (new (9, 3), new (14, 3)),
            new (new (8, 4), new (9, 3)),
            new (new (6, 4), new (11, 4)),
            new (new (5, 5), new (6, 4)),
            new (new (11, 4), new (12, 5)),
            new (new (2, 5), new (5, 5)),
            new (new (5, 5), new (12, 5)),
            new (new (12, 5), new (16, 5)),
            new (new (1, 6), new (2, 5)),
            new (new (16, 5), new (17, 6)),
            new (new (0, 6), new (1, 6)),
            new (new (1, 6), new (3, 6)),
            new (new (3, 6), new (16, 6)),
            new (new (16, 6), new (17, 6)),
            new (new (17, 6), new (18, 6)),
            new (new (3, 6), new (4, 7)),
            new (new (15, 7), new (16, 6)),
            new (new (0, 7), new (1, 7)),
            new (new (4, 7), new (7, 7)),
            new (new (7, 7), new (8, 7)),
            new (new (10, 7), new (15, 7)),
            new (new (1, 7), new (2, 8)),
            new (new (6, 8), new (7, 7)),
            new (new (16, 8), new (17, 6)),
            new (new (2, 8), new (6, 8)),
            new (new (12, 8), new (16, 8)),
            new (new (2, 8), new (3, 9)),
            new (new (15, 9), new (16, 8)),
            new (new (3, 9), new (15, 9))
        };

        return new TrainStation(lines);
    }
}
