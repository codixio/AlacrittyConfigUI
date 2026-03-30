using System.Text;
using AlacrittyUI.Models;
using Serilog;
using Tomlyn;
using Tomlyn.Model;

namespace AlacrittyUI.Services;

public class ConfigWriterService
{
    private static readonly ILogger Logger = Log.ForContext<ConfigWriterService>();

    public void WriteConfig(string path, AlacrittyConfig config)
    {
        Logger.Information("Writing config to {Path}", path);

        CreateBackup(path);

        var doc = config.RawDocument ?? new TomlTable();

        if (!doc.ContainsKey("colors"))
            doc["colors"] = new TomlTable();

        var colors = (TomlTable)doc["colors"];
        WriteColors(colors, config.Colors);

        var toml = TomlSerializer.Serialize(doc);
        File.WriteAllText(path, toml, Encoding.UTF8);

        Logger.Information("Config saved to {Path}", path);
    }

    private static void WriteColors(TomlTable colors, ColorPalette palette)
    {
        var primary = GetOrCreateTable(colors, "primary");
        primary["foreground"] = palette.Foreground;
        primary["background"] = palette.Background;
        if (palette.DimForeground != null)
            primary["dim_foreground"] = palette.DimForeground;
        if (palette.BrightForeground != null)
            primary["bright_foreground"] = palette.BrightForeground;

        var normal = GetOrCreateTable(colors, "normal");
        normal["black"] = palette.NormalBlack;
        normal["red"] = palette.NormalRed;
        normal["green"] = palette.NormalGreen;
        normal["yellow"] = palette.NormalYellow;
        normal["blue"] = palette.NormalBlue;
        normal["magenta"] = palette.NormalMagenta;
        normal["cyan"] = palette.NormalCyan;
        normal["white"] = palette.NormalWhite;

        var bright = GetOrCreateTable(colors, "bright");
        bright["black"] = palette.BrightBlack;
        bright["red"] = palette.BrightRed;
        bright["green"] = palette.BrightGreen;
        bright["yellow"] = palette.BrightYellow;
        bright["blue"] = palette.BrightBlue;
        bright["magenta"] = palette.BrightMagenta;
        bright["cyan"] = palette.BrightCyan;
        bright["white"] = palette.BrightWhite;

        if (palette.CursorText != null || palette.CursorColor != null)
        {
            var cursor = GetOrCreateTable(colors, "cursor");
            if (palette.CursorText != null) cursor["text"] = palette.CursorText;
            if (palette.CursorColor != null) cursor["cursor"] = palette.CursorColor;
        }

        if (palette.SelectionText != null || palette.SelectionBackground != null)
        {
            var selection = GetOrCreateTable(colors, "selection");
            if (palette.SelectionText != null) selection["text"] = palette.SelectionText;
            if (palette.SelectionBackground != null) selection["background"] = palette.SelectionBackground;
        }

        if (palette.SearchMatchForeground != null || palette.SearchMatchBackground != null ||
            palette.SearchFocusedForeground != null || palette.SearchFocusedBackground != null)
        {
            var search = GetOrCreateTable(colors, "search");

            if (palette.SearchMatchForeground != null || palette.SearchMatchBackground != null)
            {
                var matches = GetOrCreateTable(search, "matches");
                if (palette.SearchMatchForeground != null) matches["foreground"] = palette.SearchMatchForeground;
                if (palette.SearchMatchBackground != null) matches["background"] = palette.SearchMatchBackground;
            }

            if (palette.SearchFocusedForeground != null || palette.SearchFocusedBackground != null)
            {
                var focused = GetOrCreateTable(search, "focused_match");
                if (palette.SearchFocusedForeground != null) focused["foreground"] = palette.SearchFocusedForeground;
                if (palette.SearchFocusedBackground != null) focused["background"] = palette.SearchFocusedBackground;
            }
        }

        if (palette.FooterBarForeground != null || palette.FooterBarBackground != null)
        {
            var footerBar = GetOrCreateTable(colors, "footer_bar");
            if (palette.FooterBarForeground != null) footerBar["foreground"] = palette.FooterBarForeground;
            if (palette.FooterBarBackground != null) footerBar["background"] = palette.FooterBarBackground;
        }

        colors["draw_bold_text_with_bright_colors"] = palette.DrawBoldTextWithBrightColors;
        colors["transparent_background_colors"] = palette.TransparentBackgroundColors;
    }

    private static TomlTable GetOrCreateTable(TomlTable parent, string key)
    {
        if (!parent.ContainsKey(key) || parent[key] is not TomlTable)
            parent[key] = new TomlTable();
        return (TomlTable)parent[key];
    }

    private static void CreateBackup(string path)
    {
        if (!File.Exists(path)) return;

        var backupPath = path + ".bak";
        try
        {
            File.Copy(path, backupPath, overwrite: true);
            Logger.Information("Backup created at {BackupPath}", backupPath);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to create backup at {BackupPath}", backupPath);
            throw;
        }
    }
}
