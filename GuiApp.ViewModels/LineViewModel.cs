using Models;

namespace GuiApp.ViewModels;

public class LineViewModel : BaseViewModel
{
    public LineViewModel(Line line) {
        Line = line;
        Number = line.Number;
        Name = line.Name;
    }

    public Line Line { get; }

    private int number;
    public int Number {
        get => number;
        set => SetProperty(ref number, value);
    }

    private string name;
    public string Name {
        get => name;
        set => SetProperty(ref name, value);
    }
}
