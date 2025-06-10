namespace GUI.ViewModels;

public partial class MainWindowViewModel(PseudocodeEditorViewModel pseudocodeEditorViewModel) : ViewModelBase
{
    public PseudocodeEditorViewModel PseudocodeEditorViewModel { get; } = pseudocodeEditorViewModel;
}
