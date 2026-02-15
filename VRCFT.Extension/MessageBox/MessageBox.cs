using Avalonia.Controls;
using Avalonia.Media.Imaging;
using VRCFT.Base;

namespace VRCFT.Extension.MessageBox;

public sealed class MessageBox
{
    public static async Task<MessageBoxResult> ShowAsync(Window owner, string title, string text, MessageBoxButtons buttons, MessageBoxIcon icon = MessageBoxIcon.None)
    {
        var dialog = new MessageBoxViewModel()
        {
            Title = title,
            Text = text,
            Buttons = buttons,
            Icon = icon
        };

        dialog.View = new MessageBoxView()
        {
            DataContext = dialog,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
        };

        return await dialog.View.ShowDialog<MessageBoxResult>(owner);
    }
}

internal class MessageBoxViewModel : ViewModelBase
{
    public Window View { get; set; } = null!;

    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public MessageBoxButtons Buttons { get; set; }

    public MessageBoxIcon Icon { get; set; }
    public Bitmap? IconSource => Icon switch
    {
        _ => null
    };

    public MessageBoxResult Result { get; private set; }
    public RelayCommand ResultNo => field ??= new RelayCommand(() => View.Close(MessageBoxResult.No));
    public RelayCommand ResultCancel => field ??= new RelayCommand(() => View.Close(MessageBoxResult.Cancel));
    public RelayCommand ResultYes => field ??= new RelayCommand(() => View.Close(MessageBoxResult.Yes));
    public RelayCommand ResultOk => field ??= new RelayCommand(() => View.Close(MessageBoxResult.Ok));
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