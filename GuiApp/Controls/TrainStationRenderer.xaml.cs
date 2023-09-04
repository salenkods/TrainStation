using GuiApp.ViewModels.Controllers;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GuiApp.Controls;
/// <summary>
/// Interaction logic for TrainStationRenderer.xaml
/// </summary>
public partial class TrainStationRenderer : UserControl
{
    private readonly ITrendStationRenderController? trendStationRenderController;

    public TrainStationRenderer() {
        trendStationRenderController = ServiceLocator.GetInstance<ITrendStationRenderController>();
        trendStationRenderController.DrawRequested += OnDrawRequested;

        InitializeComponent();
    }

    private void OnDrawRequested(object? sender, Models.TrainStation e) {
        var trainStation = e;

        var (scaleFactor, offsetX, offsetY) = CalculateDrawScaleFactorAndOffsets(trainStation);

        canvas.Children.Clear();

        foreach (var trainLine in trainStation.Lines) {
            var line = new Line() {
                X1 = (trainLine.PointA.X - offsetX) * scaleFactor,
                Y1 = (trainLine.PointA.Y - offsetY) * scaleFactor,
                X2 = (trainLine.PointB.X - offsetX) * scaleFactor,
                Y2 = (trainLine.PointB.Y - offsetY) * scaleFactor,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            canvas.Children.Add(line);
        }
    }

    private (double scaleFactor, int offsetX, int offsetY) CalculateDrawScaleFactorAndOffsets(Models.TrainStation trainStation) {
        var (minX, minY, maxX, maxY) = trainStation.GetBoundary();
        var scaleX = canvas.ActualWidth / (maxX - minX);
        var scaleY = canvas.ActualHeight / (maxY - minY);
        return (Math.Min(scaleX, scaleY), minX, minY);
    }
}
