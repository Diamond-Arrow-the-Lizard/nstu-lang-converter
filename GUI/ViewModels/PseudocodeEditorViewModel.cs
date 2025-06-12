using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic; 
using Avalonia.Controls.Documents; 
using Avalonia.Media; 
using System.Text.RegularExpressions; 

namespace GUI.ViewModels
{
    public partial class PseudocodeEditorViewModel : ViewModelBase
    {
        [ObservableProperty] private string _pseudocodeText = string.Empty;
        [ObservableProperty] private IEnumerable<Inline> _highlightedPseudocodeInlines = []; 

        private readonly List<string> _keywords = new List<string>
        {
            "плав", "строка", "цел", "нц", "кц", "если", "иначе", "кесли", "раз", "пока",
            "начало", "конец", "написать", "прочитать"
        };

        public PseudocodeEditorViewModel()
        {
            UpdateHighlightedPseudocode();
        }

        partial void OnPseudocodeTextChanged(string value)
        {
            UpdateHighlightedPseudocode();
        }

        [RelayCommand]
        public void ClearPseudocode()
        {
            PseudocodeText = string.Empty;
        }

        private void UpdateHighlightedPseudocode()
        {
            var inlines = new List<Inline>();
            if (string.IsNullOrEmpty(PseudocodeText))
            {
                HighlightedPseudocodeInlines = inlines;
                return;
            }

            // Используем регулярное выражение для поиска слов и пробелов
            // \b - граница слова
            // \w+ - одно или более буквенно-цифровых символов (для слов)
            // \s+ - одно или более пробельных символов
            var regex = new Regex(@"(\b\w+\b|\s+|\S)"); 

            foreach (Match match in regex.Matches(PseudocodeText))
            {
                var text = match.Value;
                if (_keywords.Contains(text))
                {
                    inlines.Add(new Run(text) { Foreground = Brushes.Blue }); // Синий цвет для ключевых слов
                }
                else
                {
                    inlines.Add(new Run(text) { Foreground = Brushes.Black }); // Черный цвет для остального текста
                }
            }
            HighlightedPseudocodeInlines = inlines;
        }
    }
}