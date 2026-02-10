using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;

namespace VRCFT.App.Utility
{
    public abstract class ViewModelBase : ObservableObject
    {
        public static void SetMainWindow(Window view)
        {
            var desktop = Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;

            if (desktop != null)
                desktop.MainWindow = view;
        }

        public static Window? GetMainWindow()
        {
            var desktop = Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;

            if (desktop != null && desktop.MainWindow != null)
                return desktop.MainWindow;
            else
                return null;
        }
    }
}
