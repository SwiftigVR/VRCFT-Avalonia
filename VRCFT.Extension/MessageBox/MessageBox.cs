using Avalonia.Controls;
using Avalonia.Media.Imaging;

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
            Icon = GetIcon(icon)
        };

        dialog.View = new MessageBoxView()
        {
            DataContext = dialog,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };

        return await dialog.View.ShowDialog<MessageBoxResult>(owner);
    }

    private static Bitmap? GetIcon(MessageBoxIcon icon)
    {
        string? fileName = icon switch
        {
            MessageBoxIcon.Check => "Check.png",
            MessageBoxIcon.Question => "Question.png",
            MessageBoxIcon.Warning => "Warning.png",
            MessageBoxIcon.Error => "Error.png",
            _ => null
        };

        return ImageHelper.Load($"avares://VRCFT.Extension/MessageBox/Assets/{fileName}");
    }
}