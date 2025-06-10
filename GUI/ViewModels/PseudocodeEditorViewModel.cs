using CommunityToolkit.Mvvm.ComponentModel;

namespace GUI.ViewModels
{
    public partial class PseudocodeEditorViewModel : ViewModelBase 
    {
        [ObservableProperty] private string _pseudocodeText = string.Empty;

        public PseudocodeEditorViewModel()
        {
        }
    }
}