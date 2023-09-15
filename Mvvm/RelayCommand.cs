using System.Windows.Input;

namespace Mvvm;

public class RelayCommand : ICommand
{
    public Action Action { get; }

    public RelayCommand(Action action) {
        Action = action;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) {
        // Let's always be true
        return true;
    }

    public void Execute(object? parameter) {
        if (CanExecute(parameter)) {
            Action();
        }
    }
}
