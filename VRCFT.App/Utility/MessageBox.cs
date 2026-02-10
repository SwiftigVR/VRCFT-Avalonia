using Avalonia.Controls;

namespace VRCFT.App.Utility;

public class MessageBox
{
    private static Window View = null!;

    public static MessageBoxResult Show(string title, string text, MessageBoxIcon icon = MessageBoxIcon.None)
    {
        View = new Window();

        var messageBoxContext = new MessageBox();
        View.DataContext = messageBoxContext;

        var mainWindow = ViewModelBase.GetMainWindow();
        View.ShowDialog(mainWindow!);

        return messageBoxContext.Result;
    }

    private MessageBoxResult Result { get; set; }

    public RelayCommand ResultYes => field ??= new RelayCommand(() =>
    {

    });

    public RelayCommand ResultCancel => field ??= new RelayCommand(() =>
    {

    });

    public RelayCommand ResultNo => field ??= new RelayCommand(() =>
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