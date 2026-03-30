using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using AlacrittyUI.ViewModels;

namespace AlacrittyUI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void OnBrowseClick(object? sender, RoutedEventArgs e)
    {
        var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Alacritty Config",
            AllowMultiple = false,
            FileTypeFilter =
            [
                new FilePickerFileType("TOML") { Patterns = ["*.toml"] },
                new FilePickerFileType("All Files") { Patterns = ["*"] }
            ]
        });

        if (files.Count > 0 && DataContext is MainWindowViewModel vm)
        {
            var path = files[0].TryGetLocalPath();
            if (path != null)
                vm.LoadConfigFromPath(path);
        }
    }
}
