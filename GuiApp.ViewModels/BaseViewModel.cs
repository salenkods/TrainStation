using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GuiApp.ViewModels;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    protected void SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null) {
        if (Equals(storage, value)) {
            return;
        }

        storage = value;
        OnPropertyChanged(propertyName);
    }
}
