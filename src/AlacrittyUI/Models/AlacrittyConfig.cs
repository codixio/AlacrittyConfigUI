using Tomlyn.Model;

namespace AlacrittyUI.Models;

public class AlacrittyConfig
{
    public ColorPalette Colors { get; set; } = new();

    /// <summary>
    /// Raw TOML document preserved for pass-through of unknown sections.
    /// </summary>
    public TomlTable? RawDocument { get; set; }
}
