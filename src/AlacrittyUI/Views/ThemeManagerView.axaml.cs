using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using AlacrittyUI.ViewModels;

namespace AlacrittyUI.Views;

public partial class ThemeManagerView : UserControl
{
    public ThemeManagerView()
    {
        InitializeComponent();
    }

    private async void OnImportClick(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null) return;

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Import Theme",
            AllowMultiple = false,
            FileTypeFilter =
            [
                new FilePickerFileType("TOML") { Patterns = ["*.toml"] }
            ]
        });

        if (files.Count > 0 && DataContext is ThemeManagerViewModel vm)
        {
            var path = files[0].TryGetLocalPath();
            if (path != null)
                vm.ImportFromPath(path);
        }
    }

    private async void OnExportClick(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null) return;

        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Export Theme",
            DefaultExtension = "toml",
            FileTypeChoices =
            [
                new FilePickerFileType("TOML") { Patterns = ["*.toml"] }
            ]
        });

        if (file != null && DataContext is ThemeManagerViewModel vm)
        {
            var path = file.TryGetLocalPath();
            if (path != null)
                vm.ExportToPath(path);
        }
    }
}
