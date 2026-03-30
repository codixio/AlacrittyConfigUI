using System.Collections.ObjectModel;
using AlacrittyUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AlacrittyUI.ViewModels;

public partial class ColorEditorViewModel : ObservableObject
{
    public ObservableCollection<ColorEntry> PrimaryColors { get; } = [];
    public ObservableCollection<ColorEntry> NormalColors { get; } = [];
    public ObservableCollection<ColorEntry> BrightColors { get; } = [];
    public ObservableCollection<ColorEntry> CursorColors { get; } = [];
    public ObservableCollection<ColorEntry> SelectionColors { get; } = [];
    public ObservableCollection<ColorEntry> SearchColors { get; } = [];
    public ObservableCollection<ColorEntry> FooterColors { get; } = [];

    [ObservableProperty]
    private bool _drawBoldTextWithBrightColors;

    [ObservableProperty]
    private bool _transparentBackgroundColors;

    public void LoadFromPalette(ColorPalette p)
    {
        PrimaryColors.Clear();
        PrimaryColors.Add(new ColorEntry("Foreground", p.Foreground, "foreground"));
        PrimaryColors.Add(new ColorEntry("Background", p.Background, "background"));
        if (p.DimForeground != null)
            PrimaryColors.Add(new ColorEntry("Dim Foreground", p.DimForeground, "dim_foreground"));
        if (p.BrightForeground != null)
            PrimaryColors.Add(new ColorEntry("Bright Foreground", p.BrightForeground, "bright_foreground"));

        NormalColors.Clear();
        NormalColors.Add(new ColorEntry("Black", p.NormalBlack, "normal.black"));
        NormalColors.Add(new ColorEntry("Red", p.NormalRed, "normal.red"));
        NormalColors.Add(new ColorEntry("Green", p.NormalGreen, "normal.green"));
        NormalColors.Add(new ColorEntry("Yellow", p.NormalYellow, "normal.yellow"));
        NormalColors.Add(new ColorEntry("Blue", p.NormalBlue, "normal.blue"));
        NormalColors.Add(new ColorEntry("Magenta", p.NormalMagenta, "normal.magenta"));
        NormalColors.Add(new ColorEntry("Cyan", p.NormalCyan, "normal.cyan"));
        NormalColors.Add(new ColorEntry("White", p.NormalWhite, "normal.white"));

        BrightColors.Clear();
        BrightColors.Add(new ColorEntry("Black", p.BrightBlack, "bright.black"));
        BrightColors.Add(new ColorEntry("Red", p.BrightRed, "bright.red"));
        BrightColors.Add(new ColorEntry("Green", p.BrightGreen, "bright.green"));
        BrightColors.Add(new ColorEntry("Yellow", p.BrightYellow, "bright.yellow"));
        BrightColors.Add(new ColorEntry("Blue", p.BrightBlue, "bright.blue"));
        BrightColors.Add(new ColorEntry("Magenta", p.BrightMagenta, "bright.magenta"));
        BrightColors.Add(new ColorEntry("Cyan", p.BrightCyan, "bright.cyan"));
        BrightColors.Add(new ColorEntry("White", p.BrightWhite, "bright.white"));

        CursorColors.Clear();
        CursorColors.Add(new ColorEntry("Text", p.CursorText ?? "#181818", "cursor.text"));
        CursorColors.Add(new ColorEntry("Cursor", p.CursorColor ?? "#d8d8d8", "cursor.cursor"));

        SelectionColors.Clear();
        SelectionColors.Add(new ColorEntry("Text", p.SelectionText ?? "#181818", "selection.text"));
        SelectionColors.Add(new ColorEntry("Background", p.SelectionBackground ?? "#d8d8d8", "selection.background"));

        SearchColors.Clear();
        SearchColors.Add(new ColorEntry("Match FG", p.SearchMatchForeground ?? "#181818", "search.matches.fg"));
        SearchColors.Add(new ColorEntry("Match BG", p.SearchMatchBackground ?? "#ac4242", "search.matches.bg"));
        SearchColors.Add(new ColorEntry("Focused FG", p.SearchFocusedForeground ?? "#181818", "search.focused.fg"));
        SearchColors.Add(new ColorEntry("Focused BG", p.SearchFocusedBackground ?? "#f4bf75", "search.focused.bg"));

        FooterColors.Clear();
        FooterColors.Add(new ColorEntry("Foreground", p.FooterBarForeground ?? "#181818", "footer.fg"));
        FooterColors.Add(new ColorEntry("Background", p.FooterBarBackground ?? "#d8d8d8", "footer.bg"));

        DrawBoldTextWithBrightColors = p.DrawBoldTextWithBrightColors;
        TransparentBackgroundColors = p.TransparentBackgroundColors;
    }

    public void ApplyToPalette(ColorPalette p)
    {
        if (PrimaryColors.Count > 0) p.Foreground = PrimaryColors[0].HexValue;
        if (PrimaryColors.Count > 1) p.Background = PrimaryColors[1].HexValue;
        if (PrimaryColors.Count > 2) p.DimForeground = PrimaryColors[2].HexValue;
        if (PrimaryColors.Count > 3) p.BrightForeground = PrimaryColors[3].HexValue;

        if (NormalColors.Count == 8)
        {
            p.NormalBlack = NormalColors[0].HexValue;
            p.NormalRed = NormalColors[1].HexValue;
            p.NormalGreen = NormalColors[2].HexValue;
            p.NormalYellow = NormalColors[3].HexValue;
            p.NormalBlue = NormalColors[4].HexValue;
            p.NormalMagenta = NormalColors[5].HexValue;
            p.NormalCyan = NormalColors[6].HexValue;
            p.NormalWhite = NormalColors[7].HexValue;
        }

        if (BrightColors.Count == 8)
        {
            p.BrightBlack = BrightColors[0].HexValue;
            p.BrightRed = BrightColors[1].HexValue;
            p.BrightGreen = BrightColors[2].HexValue;
            p.BrightYellow = BrightColors[3].HexValue;
            p.BrightBlue = BrightColors[4].HexValue;
            p.BrightMagenta = BrightColors[5].HexValue;
            p.BrightCyan = BrightColors[6].HexValue;
            p.BrightWhite = BrightColors[7].HexValue;
        }

        if (CursorColors.Count == 2)
        {
            p.CursorText = CursorColors[0].HexValue;
            p.CursorColor = CursorColors[1].HexValue;
        }

        if (SelectionColors.Count == 2)
        {
            p.SelectionText = SelectionColors[0].HexValue;
            p.SelectionBackground = SelectionColors[1].HexValue;
        }

        if (SearchColors.Count == 4)
        {
            p.SearchMatchForeground = SearchColors[0].HexValue;
            p.SearchMatchBackground = SearchColors[1].HexValue;
            p.SearchFocusedForeground = SearchColors[2].HexValue;
            p.SearchFocusedBackground = SearchColors[3].HexValue;
        }

        if (FooterColors.Count == 2)
        {
            p.FooterBarForeground = FooterColors[0].HexValue;
            p.FooterBarBackground = FooterColors[1].HexValue;
        }

        p.DrawBoldTextWithBrightColors = DrawBoldTextWithBrightColors;
        p.TransparentBackgroundColors = TransparentBackgroundColors;
    }
}
