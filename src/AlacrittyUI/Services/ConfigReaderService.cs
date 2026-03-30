using System.Text;
using AlacrittyUI.Models;
using Serilog;
using Tomlyn;
using Tomlyn.Model;

namespace AlacrittyUI.Services;

public class ConfigReaderService
{
    private static readonly ILogger Logger = Log.ForContext<ConfigReaderService>();

    public AlacrittyConfig ReadFromFile(string path)
    {
        Logger.Information("Reading config from {Path}", path);
        var tomlContent = File.ReadAllText(path, Encoding.UTF8);
        return ReadFromString(tomlContent);
    }

    public AlacrittyConfig ReadFromString(string tomlContent)
    {
        var doc = TomlSerializer.Deserialize<TomlTable>(tomlContent)
                  ?? new TomlTable();
        var config = new AlacrittyConfig { RawDocument = doc };

        if (doc.TryGetValue("colors", out var colorsObj) && colorsObj is TomlTable colors)
            ReadColors(colors, config.Colors);

        return config;
    }

    private static void ReadColors(TomlTable colors, ColorPalette palette)
    {
        if (colors.TryGetValue("primary", out var primaryObj) && primaryObj is TomlTable primary)
        {
            palette.Foreground = GetString(primary, "foreground", palette.Foreground);
            palette.Background = GetString(primary, "background", palette.Background);
            palette.DimForeground = GetStringOrNull(primary, "dim_foreground") ?? palette.DimForeground;
            palette.BrightForeground = GetStringOrNull(primary, "bright_foreground");
        }

        if (colors.TryGetValue("normal", out var normalObj) && normalObj is TomlTable normal)
        {
            palette.NormalBlack = GetString(normal, "black", palette.NormalBlack);
            palette.NormalRed = GetString(normal, "red", palette.NormalRed);
            palette.NormalGreen = GetString(normal, "green", palette.NormalGreen);
            palette.NormalYellow = GetString(normal, "yellow", palette.NormalYellow);
            palette.NormalBlue = GetString(normal, "blue", palette.NormalBlue);
            palette.NormalMagenta = GetString(normal, "magenta", palette.NormalMagenta);
            palette.NormalCyan = GetString(normal, "cyan", palette.NormalCyan);
            palette.NormalWhite = GetString(normal, "white", palette.NormalWhite);
        }

        if (colors.TryGetValue("bright", out var brightObj) && brightObj is TomlTable bright)
        {
            palette.BrightBlack = GetString(bright, "black", palette.BrightBlack);
            palette.BrightRed = GetString(bright, "red", palette.BrightRed);
            palette.BrightGreen = GetString(bright, "green", palette.BrightGreen);
            palette.BrightYellow = GetString(bright, "yellow", palette.BrightYellow);
            palette.BrightBlue = GetString(bright, "blue", palette.BrightBlue);
            palette.BrightMagenta = GetString(bright, "magenta", palette.BrightMagenta);
            palette.BrightCyan = GetString(bright, "cyan", palette.BrightCyan);
            palette.BrightWhite = GetString(bright, "white", palette.BrightWhite);
        }

        if (colors.TryGetValue("cursor", out var cursorObj) && cursorObj is TomlTable cursor)
        {
            palette.CursorText = GetStringOrNull(cursor, "text");
            palette.CursorColor = GetStringOrNull(cursor, "cursor");
        }

        if (colors.TryGetValue("selection", out var selObj) && selObj is TomlTable selection)
        {
            palette.SelectionText = GetStringOrNull(selection, "text");
            palette.SelectionBackground = GetStringOrNull(selection, "background");
        }

        if (colors.TryGetValue("search", out var searchObj) && searchObj is TomlTable search)
        {
            if (search.TryGetValue("matches", out var matchesObj) && matchesObj is TomlTable matches)
            {
                palette.SearchMatchForeground = GetStringOrNull(matches, "foreground");
                palette.SearchMatchBackground = GetStringOrNull(matches, "background");
            }

            if (search.TryGetValue("focused_match", out var focusedObj) && focusedObj is TomlTable focused)
            {
                palette.SearchFocusedForeground = GetStringOrNull(focused, "foreground");
                palette.SearchFocusedBackground = GetStringOrNull(focused, "background");
            }
        }

        if (colors.TryGetValue("footer_bar", out var footerObj) && footerObj is TomlTable footer)
        {
            palette.FooterBarForeground = GetStringOrNull(footer, "foreground");
            palette.FooterBarBackground = GetStringOrNull(footer, "background");
        }

        if (colors.TryGetValue("draw_bold_text_with_bright_colors", out var boldBright))
            palette.DrawBoldTextWithBrightColors = boldBright is true;

        if (colors.TryGetValue("transparent_background_colors", out var transBg))
            palette.TransparentBackgroundColors = transBg is true;
    }

    private static string GetString(TomlTable table, string key, string defaultValue)
        => table.TryGetValue(key, out var val) && val is string s ? s : defaultValue;

    private static string? GetStringOrNull(TomlTable table, string key)
        => table.TryGetValue(key, out var val) && val is string s ? s : null;
}
