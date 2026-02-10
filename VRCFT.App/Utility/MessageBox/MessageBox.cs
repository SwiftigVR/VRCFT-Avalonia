using Avalonia.Controls;

namespace VRCFT.App.Utility.MessageBox;

public static class MessageBox
{
    public static MessageBoxResult Show(string title, string text, MessageBoxIcon icon = MessageBoxIcon.None)
    {
        var messageBoxViewModel = new MessageBoxViewModel()
        {
            Title = title,
            Text = text,
            Icon = icon
        };
        messageBoxViewModel.Initialize();

        return messageBoxViewModel.Result;
    }

}

public class MessageBoxViewModel : ViewModelBase
{
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public MessageBoxIcon Icon { get; set; }

    #region Window Initialize

    private Window View { get; set; } = null!;

    public void Initialize()
    {
        View = new MessageBoxView();
        View.DataContext = this;

        var mainWindow = GetMainWindow();
        View.ShowDialog(mainWindow!);
    }

    #endregion

    public MessageBoxResult Result { get; private set; }

    public RelayCommand ResultYes => field ??= new RelayCommand(() =>
    {
        Result = MessageBoxResult.Yes;
        View.Close();
    });

    public RelayCommand ResultCancel => field ??= new RelayCommand(() =>
    {
        Result = MessageBoxResult.Cancel;
        View.Close();
    });

    public RelayCommand ResultNo => field ??= new RelayCommand(() =>
    {
        Result = MessageBoxResult.No;
        View.Close();
    });
}

public enum MessageBoxButtons
{
    OK,
    YesNo,
    YesCancel,
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
    No,
    Cancel,
    Yes,
}