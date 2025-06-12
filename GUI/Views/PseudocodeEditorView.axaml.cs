using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GUI.ViewModels;
using Avalonia.Controls.Documents;
using Avalonia.Media;
using System.Collections.Generic;
using Avalonia.Interactivity; 
using System; 
using System.ComponentModel; 

namespace GUI.Views
{
    public partial class PseudocodeEditorView : UserControl
    {
        private PseudocodeEditorViewModel? _previousViewModel; 

        public PseudocodeEditorView()
        {
            InitializeComponent();
            this.DataContextChanged += OnDataContextChanged;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnDataContextChanged(object? sender, EventArgs e)
        {
            if (DataContext is PseudocodeEditorViewModel viewModel)
            {
                if (_previousViewModel != null)
                {
                    _previousViewModel.PropertyChanged -= ViewModel_PropertyChanged;
                }

                viewModel.PropertyChanged += ViewModel_PropertyChanged;
                _previousViewModel = viewModel;

                UpdateTextBlockInlines(viewModel.HighlightedPseudocodeInlines);
            }
        }

        private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PseudocodeEditorViewModel.HighlightedPseudocodeInlines))
            {
                if (DataContext is PseudocodeEditorViewModel viewModel)
                {
                    UpdateTextBlockInlines(viewModel.HighlightedPseudocodeInlines);
                }
            }
        }

        private void UpdateTextBlockInlines(IEnumerable<Inline> inlines)
        {
            var textBlock = this.FindControl<TextBlock>("HighlightedTextBlock");
            if (textBlock != null)
            {
                textBlock?.Inlines?.Clear();
                foreach (var inline in inlines)
                {
                    textBlock?.Inlines?.Add(inline);
                }
            }
        }

        private void EditorTextBox_GotFocus(object? sender, Avalonia.Input.GotFocusEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Foreground = Brushes.White;
            }
        }

        private void EditorTextBox_LostFocus(object? sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Foreground = Brushes.Transparent;
            }
        }
    }
}