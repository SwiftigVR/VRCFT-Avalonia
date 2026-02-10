using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VRCFT.App.Utility;

public class RelayCommand : ICommand
{
    #region ICommand Members

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => CanExecuteMethode == null || CanExecuteMethode(parameter);
    public void Execute(object? parameter) => ExecuteMethode.Invoke(parameter);

    public void RaiseCanExecuteChanged()
    {
        // Copy to local for thread-safety
        Volatile.Read(ref CanExecuteChanged)?.Invoke(this, EventArgs.Empty);
    }

    #endregion

    #region Properties / Constructor

    private Predicate<object?> CanExecuteMethode { get; set; }
    private Action<object?> ExecuteMethode { get; set; }

    public RelayCommand(Action executeMethode) : this(o => executeMethode(), canExecuteMethode: null!) { }
    public RelayCommand(Action<object?> executeMethode) : this(executeMethode, canExecuteMethode: null!) { }
    public RelayCommand(Action<object?> executeMethode, Predicate<object?> canExecuteMethode)
    {
        this.ExecuteMethode = executeMethode;
        this.CanExecuteMethode = canExecuteMethode;
    }

    #endregion
}

public class RelayCommand<T> : ICommand
{
    #region ICommand Members

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => CanExecuteMethode == null || CanExecuteMethode((T?)parameter);
    public void Execute(object? parameter) => ExecuteMethode.Invoke((T?)parameter);

    public void RaiseCanExecuteChanged()
    {
        Volatile.Read(ref CanExecuteChanged)?.Invoke(this, EventArgs.Empty);
    }

    #endregion ICommand Members

    #region Properties / Constructor

    private Predicate<T?> CanExecuteMethode { get; set; }
    private Action<T?> ExecuteMethode { get; set; }

    public RelayCommand(Action<T?> executeMethode) : this(executeMethode, canExecuteMethode: null!) { }
    public RelayCommand(Action<T?> executeMethode, Predicate<T?> canExecuteMethode)
    {
        this.ExecuteMethode = executeMethode;
        this.CanExecuteMethode = canExecuteMethode;
    }

    #endregion Properties / Constructor
}

public class AsyncRelayCommand : ICommand
{
    #region ICommand Members

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => CanExecuteMethode == null || CanExecuteMethode(parameter);
    public async void Execute(object? parameter) => await ExecuteMethode(parameter);

    public void RaiseCanExecuteChanged()
    {
        Volatile.Read(ref CanExecuteChanged)?.Invoke(this, EventArgs.Empty);
    }

    #endregion

    #region Properties / Constructor

    private Predicate<object?> CanExecuteMethode { get; set; }
    private Func<object?, Task> ExecuteMethode { get; set; }

    public AsyncRelayCommand(Func<object?, Task> executeMethode) : this(executeMethode, canExecuteMethode: null!) { }
    public AsyncRelayCommand(Func<object?, Task> executeMethode, Predicate<object?> canExecuteMethode)
    {
        this.ExecuteMethode = executeMethode;
        this.CanExecuteMethode = canExecuteMethode;
    }

    #endregion
}