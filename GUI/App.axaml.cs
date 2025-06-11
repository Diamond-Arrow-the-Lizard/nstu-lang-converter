using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using GUI.ViewModels;
using GUI.Views;
using Microsoft.Extensions.DependencyInjection;
using Core.Parser.Interfaces.AST;
using Core.Parser.CodeGenerator;
using Core.Parser.Interfaces.Models;
using Core.Parser.Models;
using Core.Parser.AST.ASTParser;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Repositories;
using Core.Parser.Interfaces.Services;
using Core.Parser.Services;
using System.Collections.Generic;
using Core.Parser.Interfaces.Handlers;
using Core.Parser.Handlers.TextToTokenHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.VariableTypeTextToTokenHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.VariableTypeNameTextToTokenHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.OperationTextToTokenHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.ControlFlowHandlers.IfElseHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.ControlFlowHandlers.LoopHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.StatementHandlers.ReadWriteHandlers;
using GUI.Models;

namespace GUI;

/// <summary>
/// Represents the main application class, handling application startup and service registration.
/// </summary>
public partial class App : Application
{
    /// <inheritdoc/>
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <inheritdoc/>
    public override void OnFrameworkInitializationCompleted()
    {
        var collection = new ServiceCollection();
        collection = ProvideServices();
        var services = collection.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = services.GetRequiredService<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }



    /// <summary>
    /// Provides the application's services for Dependency Injection.
    /// </summary>
    /// <returns>A ServiceCollection containing the configured services.</returns>
    private static ServiceCollection ProvideServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IEnumerable<ITextToTokenHandler>>(sp =>
        [
            new ProgramBeginTextToTokenHandler(),
            new ProgramEndTextToTokenHandler(),

            new IntegerTextToTokenHandler(),
            new StringTextToTokenHandler(),
            new DoubleTextToTokenHandler(),

            new DoubleTypeKeywordTextToTokenHandler(),
            new StringTypeKeywordTextToTokenHandler(),
            new IntegerTypeKeywordTextToTokenHandler(),

            new AddOperationTextToTokenHandler(),
            new AssignOperationTextToTokenHandler(),
            new DecrementOperationTextToTokenHandler(),
            new DivideOperationTextToTokenHandler(),
            new EqualsOperationTextToTokenHandler(),
            new ReturnKeywordTextToTokenHandler(),
            new MultiplyOperationTextToTokenHandler(),
            new MoreEqualsOperationTextToTokenHandler(),
            new LessEqualsOperationTextToTokenHandler(),
            new MoreOperationTextToTokenHandler(),
            new LessOperationTextToTokenHandler(),

            new ControlBeginKeywordTextToTokenHandler(),
            new ControlEndKeywordTextToTokenHandler(),
            new WhileTextToTokenHandler(),
            new IfKeywordTextToTokenHandler(),
            new ElseKeywordTextToTokenHandler(),

            new LoopBeginKeywordTextToTokenHandler(),
            new LoopTimesKeywordTextToTokenHandler(),
            new LoopEndKeywordTextToTokenHandler(),

            new ReadKeywordTextToTokenHandler(),
            new WriteKeywordTextToTokenHandler(),
            new SemicolonTextToTokenHandler(),
        ]);

        services.AddSingleton<ITokenRepository, TokenRepository>();
        services.AddSingleton<ITokenService, TokenService>();
        services.AddSingleton<IStringParser, StringParser>();
        services.AddSingleton<IParser, Parser>();

        services.AddSingleton<IAstVisitor, CSharpCodeGeneratorVisitor>();

        services.AddSingleton<PseudocodeEditorView>();
        services.AddSingleton<PseudocodeEditorViewModel>();
        services.AddSingleton<CSharpCodeOutputView>();
        services.AddSingleton<CSharpCodeOutputViewModel>();
        services.AddSingleton<SaveCodeView>();
        services.AddSingleton<SaveCodeViewModel>();
        services.AddSingleton<DocumentationView>();
        services.AddSingleton<DocumentationViewModel>();
        services.AddSingleton<AboutProgramView>();
        services.AddSingleton<AboutProgramViewModel>();

        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();
        return services;
    }
}