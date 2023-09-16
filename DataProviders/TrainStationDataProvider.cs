using Models;
using Path = Models.Path;

namespace DataProviders;

public class TrainStationDataProvider : ITrainStationDataProvider
{
    public TrainStation GetTrainStation() {
        var points = new List<(Point PointA, Point PointB)>() {
            new (new (8, 9), new (12, 9)),
            new (new (7, 8), new (8, 9)),
            new (new (12, 9), new (13, 8)),
            new (new (4, 8), new (6, 8)),
            new (new (6, 8), new (7, 8)),
            new (new (7, 8), new (13, 8)),
            new (new (13, 8), new (15, 8)),
            new (new (5, 7), new (6, 8)),
            new (new (2, 7), new (5, 7)),
            new (new (5, 7), new (9, 7)),
            new (new (9, 7), new (14, 7)),
            new (new (8, 6), new (9, 7)),
            new (new (6, 6), new (8, 6)),
            new (new (8, 6), new (11, 6)),
            new (new (5, 5), new (6, 6)),
            new (new (11, 6), new (12, 5)),
            new (new (2, 5), new (5, 5)),
            new (new (5, 5), new (12, 5)),
            new (new (12, 5), new (16, 5)),
            new (new (1, 4), new (2, 5)),
            new (new (16, 5), new (17, 4)),
            new (new (0, 4), new (1, 4)),
            new (new (1, 4), new (3, 4)),
            new (new (3, 4), new (6, 4)),
            new (new (16, 4), new (17, 4)),
            new (new (17, 4), new (18, 4)),
            new (new (3, 4), new (4, 3)),
            new (new (15, 3), new (16, 4)),
            new (new (0, 3), new (1, 3)),
            new (new (4, 3), new (7, 3)),
            new (new (7, 3), new (8, 3)),
            new (new (10, 3), new (15, 3)),
            new (new (1, 3), new (2, 2)),
            new (new (6, 2), new (7, 3)),
            new (new (16, 2), new (17, 4)),
            new (new (2, 2), new (6, 2)),
            new (new (12, 2), new (16, 2)),
            new (new (2, 2), new (3, 1)),
            new (new (15, 1), new (16, 2)),
            new (new (3, 1), new (15, 1)),
            new (new (6, 4), new (16, 4))
        };

        var lines = points
            .Select((p, i) => new Line(i, $"Line_{i}", points[i].PointA, points[i].PointB))
            .ToList();

        lines.Add(new Line(99, "Line_Out", new(17, 5), new(19, 7)));

        var path1 = new Path();
        path1.TryAddLine(lines[0]);

        var path2 = new Path();
        path2.TryAddLine(lines[3]);
        path2.TryAddLine(lines[4]);
        path2.TryAddLine(lines[5]);
        path2.TryAddLine(lines[6]);

        var path3 = new Path();
        path3.TryAddLine(lines[16]);
        path3.TryAddLine(lines[14]);
        path3.TryAddLine(lines[12]);
        path3.TryAddLine(lines[13]);

        var path4 = new Path();
        path4.TryAddLine(lines[17]);
        path4.TryAddLine(lines[18]);

        var path5 = new Path();
        path5.TryAddLine(lines[23]);

        var path6 = new Path();
        path6.TryAddLine(lines[31]);
        path6.TryAddLine(lines[27]);

        var path7 = new Path();
        path7.TryAddLine(lines[31]);
        path7.TryAddLine(lines[27]);

        var path8 = new Path();
        path8.TryAddLine(lines[37]);
        path8.TryAddLine(lines[39]);

        var path9 = new Path();
        path9.TryAddLine(lines[40]);

        var parks = new List<Park>();
        parks.Add(new Park("Park 1", new List<Path> { path1, path3, path5}));
        parks.Add(new Park("Park 2", new List<Path> { path2, path3, path8 }));
        parks.Add(new Park("Park 3", new List<Path> { path1, path2, path9, path6, path4 }));
        parks.Add(new Park("Park 4", new List<Path> { path2, path3, path4, path7, path8 }));
        parks.Add(new Park("Park 5", new List<Path> { path3, path5 }));

        return new TrainStation(lines, parks);
    }
}
