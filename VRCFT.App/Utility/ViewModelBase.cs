using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;

namespace VRCFT.App.Utility
{
    public abstract class ViewModelBase : ObservableObject
    {
        public void SetMainWindow(Window view)
        {
            var applicationLifetime = Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;

            if (applicationLifetime != null)
                applicationLifetime.MainWindow = view;
        }

        public Window? GetMainWindow()
        {
            var applicationLifetime = Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;

            if (applicationLifetime != null && applicationLifetime.MainWindow != null)
                return applicationLifetime.MainWindow;
            else
                return null;
        }
    }
}
