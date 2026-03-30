using System.Globalization;
using System.Resources;

namespace AlacrittyUI.Resources;

public static class Strings
{
    private static readonly ResourceManager Rm =
        new("AlacrittyUI.Resources.Strings", typeof(Strings).Assembly);

    private static string Get(string key) =>
        Rm.GetString(key, CultureInfo.CurrentUICulture) ?? key;

    // Tabs
    public static string TabColors => Get(nameof(TabColors));
    public static string TabThemes => Get(nameof(TabThemes));
    public static string TabInfo => Get(nameof(TabInfo));
    public static string SubTabPalette => Get(nameof(SubTabPalette));
    public static string SubTabSpecial => Get(nameof(SubTabSpecial));

    // Section headers
    public static string SectionPrimary => Get(nameof(SectionPrimary));
    public static string SectionNormal => Get(nameof(SectionNormal));
    public static string SectionBright => Get(nameof(SectionBright));
    public static string SectionCursor => Get(nameof(SectionCursor));
    public static string SectionSelection => Get(nameof(SectionSelection));
    public static string SectionSearch => Get(nameof(SectionSearch));
    public static string SectionFooter => Get(nameof(SectionFooter));
    public static string SectionFlags => Get(nameof(SectionFlags));

    // Buttons
    public static string ButtonSave => Get(nameof(ButtonSave));
    public static string ButtonOpen => Get(nameof(ButtonOpen));
    public static string ButtonApply => Get(nameof(ButtonApply));
    public static string ButtonImport => Get(nameof(ButtonImport));
    public static string ButtonExport => Get(nameof(ButtonExport));
    public static string ButtonDelete => Get(nameof(ButtonDelete));
    public static string ButtonSaveAsTheme => Get(nameof(ButtonSaveAsTheme));

    // Status
    public static string StatusReady => Get(nameof(StatusReady));
    public static string StatusSaved => Get(nameof(StatusSaved));
    public static string StatusLoaded => Get(nameof(StatusLoaded));
    public static string StatusNoConfig => Get(nameof(StatusNoConfig));
    public static string StatusThemeApplied => Get(nameof(StatusThemeApplied));

    // Toggles
    public static string ToggleBoldBright => Get(nameof(ToggleBoldBright));
    public static string ToggleTransparentBg => Get(nameof(ToggleTransparentBg));

    // Info
    public static string InfoDescription => Get(nameof(InfoDescription));
    public static string InfoDeveloper => Get(nameof(InfoDeveloper));
    public static string InfoAlacrittyVersion => Get(nameof(InfoAlacrittyVersion));

    // Theme manager
    public static string ThemeBuiltIn => Get(nameof(ThemeBuiltIn));
    public static string ThemeUser => Get(nameof(ThemeUser));
    public static string ThemeNamePrompt => Get(nameof(ThemeNamePrompt));
}
