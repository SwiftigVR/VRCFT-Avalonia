using Avalonia.Controls;
using Avalonia.Media.Imaging;
using VRCFT.Base;

namespace VRCFT.Extension.MessageBox;

public sealed class MessageBox
{
    public static async Task<MessageBoxResult> ShowAsync(Window owner, string title, string text)
        => await ShowAsync(owner, title, text, MessageBoxButtons.OK, MessageBoxIcon.None);

    public static async Task<MessageBoxResult> ShowAsync(Window owner, string title, string text, MessageBoxButtons buttons)
        => await ShowAsync(owner, title, text, buttons, MessageBoxIcon.None);

    public static async Task<MessageBoxResult> ShowAsync(Window owner, string title, string text, MessageBoxButtons buttons, MessageBoxIcon icon)
    {
        var dialog = new MessageBoxViewModel()
        {
            Title = title,
            Text = text,
            Buttons = buttons,
            Icon = icon
        };

        return await dialog.View.ShowDialog<MessageBoxResult>(owner);
    }
}

internal class MessageBoxViewModel : ViewModelBase
{
    public Window View => new MessageBoxView()
    {
        DataContext = this,
        WindowStartupLocation = WindowStartupLocation.CenterOwner
    };

    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public MessageBoxButtons Buttons { get; set; }

    public MessageBoxIcon Icon { get; set; }
    public Bitmap? IconSource => Icon switch
    {
        MessageBoxIcon.Question => GetIcon("Question.png"),
        MessageBoxIcon.Warning => GetIcon("Warning.png"),
        MessageBoxIcon.Error => GetIcon("Error.png"),
        _ => null
    };

    private Bitmap GetIcon(string fileName) => ImageHelper.Load($"avares://VRCFT.Extension/MessageBox/Assets/{fileName}");

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