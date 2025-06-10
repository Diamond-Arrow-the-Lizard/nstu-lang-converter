using CommunityToolkit.Mvvm.Input;
using Core.CodeSaver;

namespace GUI.ViewModels;

public partial class SaveCodeViewModel(CSharpCodeOutputViewModel csCode) : ViewModelBase
{
    private readonly CSharpCodeOutputViewModel _csCode = csCode;

    [RelayCommand]
    public void SaveCode()
    {
        string codeToSave = _csCode.GeneratedCSharpCode;
        CodeSaver.SaveGeneratedCode(codeToSave, "Program.cs");
    }
}