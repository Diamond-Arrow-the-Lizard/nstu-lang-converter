using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Parser.Interfaces.Services;
using Core.Parser.Interfaces.AST;
using Core.Parser.AST.ASTParser;
using GUI.Views;
using Core.Parser.Interfaces.Models;
using System;
using Core.Parser.Interfaces.Repositories;
using System.Linq;
using Core.Parser.Repositories;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using Avalonia.Threading;

namespace GUI.ViewModels;

/// <summary>
/// ViewModel for the main application window, orchestrating interactions
/// between the pseudocode editor and the C# code output.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
/// </remarks>
/// <param name="pseudocodeEditorViewModel">The ViewModel for the pseudocode editor.</param>
/// <param name="cSharpCodeOutputViewModel">The ViewModel for the C# code output.</param>
/// <param name="stringParser">The service for parsing input strings into tokens.</param>
/// <param name="parser">The service for parsing tokens into an Abstract Syntax Tree (AST).</param>
/// <param name="cSharpCodeGenerator">The AST visitor for generating C# code.</param>
public partial class MainWindowViewModel(
    PseudocodeEditorViewModel pseudocodeEditorViewModel,
    CSharpCodeOutputViewModel cSharpCodeOutputViewModel,
    SaveCodeViewModel saveCodeViewModel,
    DocumentationViewModel documentationViewModel,
    AboutProgramViewModel aboutProgramViewModel,
    ExampleCodeViewModel exampleCodeViewModel,
    IStringParser stringParser,
    IParser parser,
    IAstVisitor cSharpCodeGenerator,
    ITokenService tokenService) : ViewModelBase
{
    private readonly IStringParser _stringParser = stringParser;
    private readonly IParser _parser = parser;
    private readonly IAstVisitor _cSharpCodeGenerator = cSharpCodeGenerator;
    private ITokenRepository _tokenRepository = new TokenRepository(tokenService);

    public PseudocodeEditorViewModel PseudocodeEditorViewModel { get; } = pseudocodeEditorViewModel;
    public CSharpCodeOutputViewModel CSharpCodeOutputViewModel { get; } = cSharpCodeOutputViewModel;
    public SaveCodeViewModel SaveCodeViewModel { get; } = saveCodeViewModel;
    public DocumentationViewModel DocumentationViewModel { get; } = documentationViewModel;
    public AboutProgramViewModel AboutProgramViewModel { get; } = aboutProgramViewModel;
    public ExampleCodeViewModel ExampleCodeViewModel { get; } = exampleCodeViewModel;


    [RelayCommand]
    private void ShowDocumentation()
    {
        var documentationWindow = new Window
        {
            Title = "Документация по псевдокоду",
            Content = new DocumentationView { DataContext = DocumentationViewModel },
            Width = 800,
            Height = 900,
            CanResize = true,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };

        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && desktop.MainWindow != null)
        {
            documentationWindow.ShowDialog(desktop.MainWindow);
        }
        else
        {
            documentationWindow.Show();
        }

    }

    [RelayCommand]
    private void ShowDocumentationAsWindow()
    {
        var documentationWindow = new Window
        {
            Title = "Документация по псевдокоду",
            Content = new DocumentationView { DataContext = DocumentationViewModel },
            Width = 800,
            Height = 900,
            CanResize = true,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };

        documentationWindow.Show();
    }

    [RelayCommand]
    private void ShowAboutProgram()
    {
        var aboutWindow = new Window
        {
            Title = "О программе",
            Content = new AboutProgramView { DataContext = AboutProgramViewModel },
            Width = 650,
            Height = 300,
            CanResize = false,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };

        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && desktop.MainWindow != null)
        {
            aboutWindow.ShowDialog(desktop.MainWindow);
        }
        else
        {
            aboutWindow.Show();
        }
    }

    [RelayCommand]
    private void PutExampleCode()
    {
        PseudocodeEditorViewModel.PseudocodeText = ExampleCodeViewModel.ExampleCode;
    }


    /// <summary>
    /// Command to convert the pseudocode text into C# code.
    /// </summary>
    [RelayCommand]
    private void ConvertPseudocodeToCSharp()
    {
        CSharpCodeOutputViewModel.GeneratedCSharpCode = string.Empty;
        CSharpCodeOutputViewModel.ErrorMessage = string.Empty;

        _stringParser.ClearRepository();
        _parser.ClearParser();
        _tokenRepository.ClearRepository();
        _cSharpCodeGenerator.ClearGeneratedString();

        if (string.IsNullOrWhiteSpace(PseudocodeEditorViewModel.PseudocodeText))
        {
            CSharpCodeOutputViewModel.ErrorMessage = "Pseudocode field cannot be empty.";
            return;
        }

        try
        {
            // 1. Set the text for the string parser
            _stringParser.SetText(PseudocodeEditorViewModel.PseudocodeText);

            // 2. Tokenization
            _stringParser.MakeTokenizedExpression();
            _tokenRepository = _stringParser.GetTokenRepository();
            _parser.SetTokensToParser(_tokenRepository);

            // 3. Parse AST
            var ast = _parser.Parse();

            // 4. Generate C# code
            ast.Accept(_cSharpCodeGenerator);
            CSharpCodeOutputViewModel.GeneratedCSharpCode = _cSharpCodeGenerator.GetGeneratedCode();
        }
        catch (SyntaxException ex)
        {
            CSharpCodeOutputViewModel.ErrorMessage = $"Syntax Error: {ex.Message}";
            CSharpCodeOutputViewModel.GeneratedCSharpCode = string.Empty;
        }
        catch (Exception ex)
        {
            CSharpCodeOutputViewModel.ErrorMessage = $"An unexpected error occurred: {ex.Message}";
            CSharpCodeOutputViewModel.GeneratedCSharpCode = string.Empty;
        }

    }
}
