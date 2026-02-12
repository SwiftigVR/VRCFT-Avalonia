using Avalonia.Controls;
using Avalonia.Media.Imaging;
using VRCFT.Base;

namespace VRCFT.Extension.MessageBox;

public static class MessageBox
{
    public static MessageBoxResult Show(string title, string text, MessageBoxButtons buttons, MessageBoxIcon icon = MessageBoxIcon.None)
    {
        var result = ShowAsync(title, text, buttons, icon).GetAwaiter().GetResult();
        return result;
    }

    public static async Task<MessageBoxResult> ShowAsync(string title, string text, MessageBoxButtons buttons, MessageBoxIcon icon = MessageBoxIcon.None)
    {
        var messageBoxViewModel = new MessageBoxViewModel()
        {
            Title = title,
            Text = text,
            Buttons = buttons,
            Icon = icon
        };

        messageBoxViewModel.Initialize();

        return messageBoxViewModel.Result;
    }
}

internal class MessageBoxViewModel : ViewModelBase
{
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public MessageBoxButtons Buttons { get; set; }
    public MessageBoxIcon Icon { get; set; }

    #region Window Initialize

    private Window View { get; set; } = null!;

    public async void Initialize()
    {
        View = new MessageBoxView();
        View.DataContext = this;
        View.WindowStartupLocation = WindowStartupLocation.CenterOwner;

        var mainWindow = GetMainWindow();
        await View.ShowDialog(mainWindow!);
    }

    #endregion

    public Bitmap? IconSource => Icon switch
    {
        _ => null
    };

    public MessageBoxResult Result { get; private set; }

    public RelayCommand ResultNo => field ??= new RelayCommand(() =>
    {
        Result = MessageBoxResult.No;
        View.Close();
    });

    public RelayCommand ResultCancel => field ??= new RelayCommand(() =>
    {
        Result = MessageBoxResult.Cancel;
        View.Close();
    });

    public RelayCommand ResultYes => field ??= new RelayCommand(() =>
    {
        Result = MessageBoxResult.Yes;
        View.Close();
    });

    public RelayCommand ResultOk => field ??= new RelayCommand(() =>
    {
        Result = MessageBoxResult.Ok;
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
    Ok,
}