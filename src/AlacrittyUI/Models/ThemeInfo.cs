namespace AlacrittyUI.Models;

public class ThemeInfo
{
    public required string Name { get; init; }
    public required string FilePath { get; init; }
    public bool IsBuiltIn { get; init; }
    public string? PreviewBackground { get; init; }
    public string? PreviewForeground { get; init; }
}
