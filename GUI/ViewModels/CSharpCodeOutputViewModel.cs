using CommunityToolkit.Mvvm.ComponentModel;

namespace GUI.ViewModels
{
    /// <summary>
    /// ViewModel for displaying generated C# code and associated messages.
    /// </summary>
    public partial class CSharpCodeOutputViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the generated C# code.
        /// This property is bound to a read-only text box in the view.
        /// </summary>
        [ObservableProperty]
        private string _generatedCSharpCode = string.Empty;

        /// <summary>
        /// Gets or sets any error message related to the code generation process.
        /// This property is bound to a text block in the view to display errors.
        /// </summary>
        [ObservableProperty]
        private string _errorMessage = string.Empty;

    }
}