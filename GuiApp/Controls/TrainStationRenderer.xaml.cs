using GuiApp.ViewModels.Controllers;
using Models;
using Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using Line = System.Windows.Shapes.Line;

namespace GuiApp.Controls;
/// <summary>
/// Interaction logic for TrainStationRenderer.xaml
/// </summary>
public partial class TrainStationRenderer : UserControl
{
    private readonly ITrendStationRenderController? trendStationRenderController;

    private TrainStation? trainStation;
    private double scaleFactor;
    private int offsetX;
    private int offsetY;

    public TrainStationRenderer() {
        trendStationRenderController = ServiceLocator.GetInstance<ITrendStationRenderController>();
        trendStationRenderController.DrawStationRequested += OnDrawStationRequested;
        trendStationRenderController.HighlightParkRequested += OnHighlightParkRequested;

        InitializeComponent();
    }

    private void OnDrawStationRequested(object? sender, TrainStation e) {
        trainStation = e;
        canvasPark.Children.Clear();
        CalculateDrawScaleFactorAndOffsets(trainStation.Lines);
        DrawLines(trainStation.Lines, canvasStation, Brushes.Black);
    }

    private void OnHighlightParkRequested(object? sender, Park e) {
        if (trainStation is null ||
            !trainStation.Parks.Contains(e)) {
            return;
        }

        DrawLines(e.Paths.SelectMany(x => x.Lines).ToList(), canvasPark, Brushes.Blue, 2);
    }

    private void DrawLines(List<Models.Line> lines, Canvas canvas, Brush color, double strokeThickness = 1) {
        canvas.Children.Clear();

        foreach (var trainLine in lines) {
            var line = new Line() {
                X1 = (trainLine.PointA.X - offsetX) * scaleFactor,
                Y1 = (trainLine.PointA.Y - offsetY) * scaleFactor,
                X2 = (trainLine.PointB.X - offsetX) * scaleFactor,
                Y2 = (trainLine.PointB.Y - offsetY) * scaleFactor,
                Stroke = color,
                StrokeThickness = strokeThickness
            };

            canvas.Children.Add(line);
        }
    }

    private void CalculateDrawScaleFactorAndOffsets(IList<Models.Line> lines) {
        var (minX, minY, maxX, maxY) = BoundaryHelper.GetBoundary(lines);
        var scaleX = rootGrid.ActualWidth / (maxX - minX);
        var scaleY = rootGrid.ActualHeight / (maxY - minY);

        scaleFactor = Math.Min(scaleX, scaleY);
        offsetX = minX;
        offsetY = minY;
    }
}
