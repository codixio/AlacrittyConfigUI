using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AlacrittyUI.ViewModels;

public partial class InfoViewModel : ObservableObject
{
    public string AppName => "AlacrittyUI";

    public string Version
    {
        get
        {
            var asm = Assembly.GetExecutingAssembly();
            var ver = asm.GetName().Version;
            return ver != null ? $"{ver.Major}.{ver.Minor}.{ver.Build}" : "0.1.0";
        }
    }

    public string Developer => "Codixio";
    public string Website => "https://codixio.com";
    public string RepoUrl => "https://github.com/codixio/AlacrittyUI";
    public string AlacrittyRepoUrl => "https://github.com/alacritty/alacritty";
    public string AlacrittyVersion => "0.15+";
    public string Year => "2026";
}
