using AlacrittyUI.Models;
using AlacrittyUI.Resources;
using AlacrittyUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Serilog;

namespace AlacrittyUI.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private static readonly ILogger Logger = Log.ForContext<MainWindowViewModel>();

    private readonly ConfigDiscoveryService _discovery;
    private readonly ConfigReaderService _reader;
    private readonly ConfigWriterService _writer;

    [ObservableProperty]
    private string _configPath = string.Empty;

    [ObservableProperty]
    private string _statusText = string.Empty;

    [ObservableProperty]
    private int _selectedTabIndex;

    [ObservableProperty]
    private bool _hasConfig;

    public ColorEditorViewModel ColorEditor { get; }
    public ThemeManagerViewModel ThemeManager { get; }
    public InfoViewModel Info { get; }

    private AlacrittyConfig? _config;

    public MainWindowViewModel(
        ConfigDiscoveryService discovery,
        ConfigReaderService reader,
        ConfigWriterService writer,
        ThemeService themeService)
    {
        _discovery = discovery;
        _reader = reader;
        _writer = writer;

        ColorEditor = new ColorEditorViewModel();
        ThemeManager = new ThemeManagerViewModel(themeService, this);
        Info = new InfoViewModel();

        InitializeConfig();
    }

    private void InitializeConfig()
    {
        var path = _discovery.FindConfigPath();
        if (path != null)
        {
            LoadConfigFromPath(path);
        }
        else
        {
            StatusText = Strings.StatusNoConfig;
            HasConfig = false;
            // load defaults so the editor isn't empty
            ColorEditor.LoadFromPalette(new ColorPalette());
        }
    }

    public void LoadConfigFromPath(string path)
    {
        try
        {
            ConfigPath = path;
            _config = _reader.ReadFromFile(path);
            ColorEditor.LoadFromPalette(_config.Colors);
            HasConfig = true;
            StatusText = Strings.StatusLoaded;
            Logger.Information("Config loaded from {Path}", path);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load config from {Path}", path);
            StatusText = $"Error: {ex.Message}";
            HasConfig = false;
        }
    }

    [RelayCommand]
    private void Save()
    {
        if (_config == null || string.IsNullOrEmpty(ConfigPath)) return;

        try
        {
            ColorEditor.ApplyToPalette(_config.Colors);
            _writer.WriteConfig(ConfigPath, _config);
            StatusText = Strings.StatusSaved;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to save config to {Path}", ConfigPath);
            StatusText = $"Error: {ex.Message}";
        }
    }

    public void ApplyPalette(ColorPalette palette)
    {
        _config ??= new AlacrittyConfig();
        _config.Colors = palette;
        ColorEditor.LoadFromPalette(palette);
    }

    public ColorPalette? GetCurrentPalette()
    {
        if (_config == null) return null;
        ColorEditor.ApplyToPalette(_config.Colors);
        return _config.Colors;
    }
}
