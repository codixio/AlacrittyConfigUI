# AlacrittyUI

A cross-platform settings editor for the [Alacritty](https://alacritty.org/) terminal emulator.

Alacritty is fast, minimal, and configurable — but all configuration happens through a TOML file. AlacrittyUI gives you a visual interface to tweak colors, fonts, keybindings, and every other setting without memorizing config keys. Changes are written directly to your `alacritty.toml` and apply instantly via Alacritty's live reload.

![Screenshot](docs/screenshot.png)

## Features

- **Color scheme editor** — edit all 16 terminal colors, primary colors, cursor, selection, search, and footer bar colors
- **Theme management** — comes with 7 built-in themes (Gruvbox, Dracula, Nord, Solarized, One Dark, Catppuccin Mocha, Tokyo Night). Save your own, import from the [alacritty-themes](https://github.com/alacritty/alacritty-theme) collection, or export to share
- **Font configuration** — font family, size, bold/italic overrides, character and glyph offsets, built-in box drawing toggle
- **Window settings** — dimensions, padding, opacity, blur, decorations, startup mode, position, and more
- **Cursor settings** — shape (block, underline, beam), blinking behavior, thickness, Vi mode cursor style
- **Terminal & shell** — shell program and args, scrolling history, bell configuration, OSC 52 clipboard, working directory, config imports
- **Keybinding editor** — view, add, edit, and remove keybindings with full modifier and action support
- **URL hints** — configure regex-based URL detection rules with custom actions and mouse bindings
- **Auto-detection** — finds your `alacritty.toml` automatically on Windows, macOS, and Linux
- **Non-destructive editing** — preserves comments and config sections it doesn't manage
- **Localized UI** — German and English interface

## Requirements

- .NET 10 runtime or later
- Alacritty (any recent version with TOML config support)

## Installation

Download the latest release for your platform from the [Releases](https://github.com/codixio/AlacrittyUI/releases) page.

Or build from source:

```
git clone https://github.com/codixio/AlacrittyUI.git
cd AlacrittyUI
dotnet build src/AlacrittyUI/AlacrittyUI.csproj -c Release
```

## Usage

Launch AlacrittyUI — it will find your Alacritty config automatically. If you keep your config in a non-standard location, use the file picker to point it to the right path.

Edit colors, fonts, keybindings, or any other setting visually. Hit save, and Alacritty picks up the changes immediately.

## Built with

- [Avalonia UI](https://avaloniaui.net/) — cross-platform .NET UI framework
- [Tomlyn](https://github.com/xoofx/tomlyn) — TOML parser for .NET
- [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet) — MVVM helpers
- [Serilog](https://serilog.net/) — structured logging

## Contributing

Contributions are welcome. Please open an issue first to discuss what you'd like to change.

## License

[MIT](LICENSE)
