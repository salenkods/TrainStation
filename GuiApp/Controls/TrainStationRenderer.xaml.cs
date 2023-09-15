using GuiApp.ViewModels.Controllers;
using Models;
using Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Line = System.Windows.Shapes.Line;

namespace GuiApp.Controls;
/// <summary>
/// Interaction logic for TrainStationRenderer.xaml
/// </summary>
public partial class TrainStationRenderer : UserControl
{
    private readonly ITrendStationRenderController? trendStationRenderController;

    private readonly SolidColorBrush lineColor = Brushes.Black;
    private readonly SolidColorBrush lineHighlightColor = Brushes.Blue;
    private readonly SolidColorBrush parkColor = Brushes.LightGreen;

    private readonly int lineHighlightWidth = 2;

    private TrainStation? trainStation;
    private double scaleFactor;
    private double offsetX;
    private double offsetY;

    public TrainStationRenderer() {
        trendStationRenderController = ServiceLocator.GetInstance<ITrendStationRenderController>();

        trendStationRenderController.DrawStationRequested += OnDrawStationRequested;
        trendStationRenderController.HighlightParkRequested += OnHighlightParkRequested;
        trendStationRenderController.HighlightStartLineRequested += OnHighlightStartLineRequested;
        trendStationRenderController.HighlightEndLineRequested += OnHighlightEndLineRequested;
        trendStationRenderController.DrawMinPathRequested += OnDrawMinPathRequested;

        InitializeComponent();
    }

    private void OnDrawStationRequested(object? sender, TrainStation e) {
        trainStation = e;

        canvasStation.Children.Clear();
        canvasPark.Children.Clear();
        canvasStartLine.Children.Clear();
        canvasEndLine.Children.Clear();
        canvasMinPath.Children.Clear();

        CalculateDrawScaleFactorAndOffsets(trainStation.Lines);
        DrawLines(trainStation.Lines, canvasStation, lineColor);
    }

    private void OnHighlightParkRequested(object? sender, Park e) {
        if (trainStation is null || !trainStation.Parks.Contains(e)) {
            return;
        }

        canvasPark.Children.Clear();
        canvasStartLine.Children.Clear();
        canvasEndLine.Children.Clear();
        canvasMinPath.Children.Clear();

        DrawLines(e.Paths.SelectMany(x => x.Lines).ToList(), canvasPark, lineHighlightColor, lineHighlightWidth);
        HighlightPark(e, canvasPark);
    }

    private void OnHighlightStartLineRequested(object? sender, Models.Line e) {
        if (trainStation is null || !trainStation.Lines.Contains(e)) {
            return;
        }

        canvasPark.Children.Clear();
        canvasStartLine.Children.Clear();
        canvasMinPath.Children.Clear();

        DrawLines(new List<Models.Line> { e }, canvasStartLine, lineHighlightColor, lineHighlightWidth);
    }

    private void OnHighlightEndLineRequested(object? sender, Models.Line e) {
        if (trainStation is null || !trainStation.Lines.Contains(e)) {
            return;
        }

        canvasPark.Children.Clear();
        canvasEndLine.Children.Clear();
        canvasMinPath.Children.Clear();

        DrawLines(new List<Models.Line> { e }, canvasEndLine, lineHighlightColor, lineHighlightWidth);
    }

    private void OnDrawMinPathRequested(object? sender, List<Models.Line> e) {
        if (trainStation is null) {
            return;
        }

        canvasPark.Children.Clear();
        canvasMinPath.Children.Clear();

        DrawLines(e, canvasMinPath, lineHighlightColor, lineHighlightWidth);
    }

    private void DrawLines(List<Models.Line> lines, Canvas canvas, Brush color, double strokeThickness = 1) {
        // Take into account that canvas coordinate system vertical axis is directed from top to bottom

        foreach (var trainLine in lines) {
            var line = new Line() {
                X1 = ToCanvasX(trainLine.PointA.X),
                Y1 = ToCanvasY(trainLine.PointA.Y),
                X2 = ToCanvasX(trainLine.PointB.X),
                Y2 = ToCanvasY(trainLine.PointB.Y),
                Stroke = color,
                StrokeThickness = strokeThickness
            };

            canvas.Children.Add(line);
        }
    }

    private void HighlightPark(Park park, Canvas canvas) {
        var pointCollection = new PointCollection();
        foreach (var vertex in park.GetVertices()) {
            pointCollection.Add(new System.Windows.Point(ToCanvasX(vertex.X), ToCanvasY(vertex.Y)));
        }

        var polygon = new Polygon {
            Points = pointCollection,
            Fill = parkColor,
            Opacity = 0.25
        };

        canvas.Children.Add(polygon);
    }

    private void CalculateDrawScaleFactorAndOffsets(IList<Models.Line> lines) {
        // Use offsets to shift points closer to coordinate system zero
        var (minX, minY, maxX, maxY) = BoundaryHelper.GetBoundary(lines);
        var scaleX = rootGrid.ActualWidth / (maxX - minX);
        var scaleY = rootGrid.ActualHeight / (maxY - minY);

        scaleFactor = Math.Min(scaleX, scaleY);
        offsetX = minX;
        offsetY = maxY;
    }

    private double ToCanvasX(double x) {
        return (x - offsetX) * scaleFactor;
    }

    private double ToCanvasY(double y) {
        return Math.Abs((y - offsetY)) * scaleFactor;
    }
}
