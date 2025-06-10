using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GUI.ViewModels
{
    public partial class PseudocodeEditorViewModel : ViewModelBase 
    {
        [ObservableProperty] private string _pseudocodeText = string.Empty;

        [RelayCommand]
        public void ClearPseudocode()
        {
            PseudocodeText = string.Empty; 
        }

    }
}