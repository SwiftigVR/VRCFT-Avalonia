using Avalonia.Controls;

namespace VRCFT.App.Utility;

public static class MessageBox
{
    private static Window View = null!;

    public static MessageBoxResult Show(string title, string text, MessageBoxIcon icon = MessageBoxIcon.None)
    {
        View = new Window();

        var mainWindow = ViewModelBase.GetMainWindow();
        View.ShowDialog(mainWindow!);

        return Result;
    }

    private static MessageBoxResult Result { get; set; }

    public static RelayCommand ResultYes => field ??= new RelayCommand(() =>
    {

    });

    public static RelayCommand ResultCancel => field ??= new RelayCommand(() =>
    {

    });

    public static RelayCommand ResultNo => field ??= new RelayCommand(() =>
    {

    });
}

public enum MessageBoxIcon
{
    None,
    Question,
    Warning,
    Error,
}

public enum MessageBoxResult
{
    No = 0,
    Cancel = 1,
    Yes = 2,
}