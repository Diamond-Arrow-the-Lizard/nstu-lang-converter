using CommunityToolkit.Mvvm.ComponentModel;

namespace GUI.ViewModels
{
    public partial class AboutProgramViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _title = "Генератор кода (Псевдокод -> C#)";

        [ObservableProperty]
        private string _author1 = "Дементьев Артём Андреевич, АВТ-313"; 

        [ObservableProperty]
        private string _author2 = "Обеленец Павел Викторович, АВТ-313";

    }
}