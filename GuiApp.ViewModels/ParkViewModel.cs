using Models;
using System.Collections.ObjectModel;

namespace GuiApp.ViewModels;

public class ParkViewModel : BaseViewModel
{
    public ParkViewModel(Park park) {
        Park = park;
        Name = park.Name;
        Vertices = new ObservableCollection<string>(park.GetVertices().Select(x => x.ToString()));
    }

    public Park Park { get; }

    private string name;
    public string Name {
        get => name;
        set => SetProperty(ref name, value);
    }

    private ObservableCollection<string> vertices;
    public ObservableCollection<string> Vertices {
        get => vertices;
        set => SetProperty(ref vertices, value);
    }
}
