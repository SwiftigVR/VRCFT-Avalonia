using Avalonia.Controls;
using Avalonia.Media.Imaging;
using VRCFT.Base;

namespace VRCFT.Extension.MessageBox;

internal class MessageBoxViewModel : ViewModelBase
{
    public Window View { get; set; } = null!;

    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public MessageBoxButtons Buttons { get; set; }
    public Bitmap? Icon { get; set; }

    public bool ShowNoButton => Buttons == MessageBoxButtons.YesNo;
    public bool ShowCancelButton => Buttons == MessageBoxButtons.OkCancel;
    public bool ShowYesButton => Buttons == MessageBoxButtons.YesNo;
    public bool ShowOkButton => Buttons == MessageBoxButtons.OK || Buttons == MessageBoxButtons.OkCancel;

    public RelayCommand ResultNo => field ??= new RelayCommand(() => View.Close(MessageBoxResult.No));
    public RelayCommand ResultCancel => field ??= new RelayCommand(() => View.Close(MessageBoxResult.Cancel));
    public RelayCommand ResultYes => field ??= new RelayCommand(() => View.Close(MessageBoxResult.Yes));
    public RelayCommand ResultOk => field ??= new RelayCommand(() => View.Close(MessageBoxResult.Ok));
}