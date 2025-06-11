using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using GUI.Models;
using System.Runtime.InteropServices.JavaScript;
using System.Text;

namespace GUI.ViewModels
{
    /// <summary>
    /// ViewModel for the documentation popup, providing information about pseudocode syntax.
    /// </summary>
    public partial class DocumentationViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets a collection of documentation entries for pseudocode keywords.
        /// </summary>
        public ObservableCollection<KeywordDocumentationEntry> DocumentationEntries { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentationViewModel"/> class.
        /// </summary>
        public DocumentationViewModel()
        {
            DocumentationEntries = new ObservableCollection<KeywordDocumentationEntry>();
            LoadDocumentationEntries();
        }

        /// <summary>
        /// Loads predefined documentation entries into the collection.
        /// </summary>
        public void LoadDocumentationEntries()
        {
            // Program Structure
            DocumentationEntries.Add(new KeywordDocumentationEntry(
                "начало ... конец",
                "начало\n    <операторы>\nконец",
                "Обозначает начало и конец основной программы или блока кода."
            ));

            // Variable Declarations
            DocumentationEntries.Add(new KeywordDocumentationEntry(
                "цел <имя_переменной>",
                "цел x;",
                "Объявляет целочисленную переменную."
            ));
            DocumentationEntries.Add(new KeywordDocumentationEntry(
                "плав <имя_переменной>",
                "плав pi;",
                "Объявляет переменную с плавающей точкой."
            ));
            DocumentationEntries.Add(new KeywordDocumentationEntry(
                "строка <имя_переменной>",
                "строка имя;",
                "Объявляет строковую переменную."
            ));

            // Assignment
            DocumentationEntries.Add(new KeywordDocumentationEntry(
                "Присваивание",
                "x = 10;",
                "Присваивает значение переменной."
            ));

            // Arithmetic Operations
            DocumentationEntries.Add(new KeywordDocumentationEntry(
                "Сложение (+)",
                "x = a + b;",
                "Выполняет операцию сложения."
            ));
            DocumentationEntries.Add(new KeywordDocumentationEntry(
                "Вычитание (-)",
                "x = a - b;",
                "Выполняет операцию вычитания."
            ));
            DocumentationEntries.Add(new KeywordDocumentationEntry(
                "Умножение (*)",
                "x = a * b;",
                "Выполняет операцию умножения."
            ));
            DocumentationEntries.Add(new KeywordDocumentationEntry(
                "Деление (/)",
                "x = a / b;",
                "Выполняет операцию деления."
            ));

            // Comparison Operations
            DocumentationEntries.Add(new KeywordDocumentationEntry(
                "Равенство (==)",
                "если x == 10 то ... кесли",
                "Проверяет равенство двух значений."
            ));

            // Conditional Statements (If-Else)
            DocumentationEntries.Add(new KeywordDocumentationEntry(
                "если ... то ... кесли",
                "если <условие> то\n    <операторы>\nкесли",
                "Выполняет блок кода, если условие истинно."
            ));
            DocumentationEntries.Add(new KeywordDocumentationEntry(
                "если ... то ... иначе ... кесли",
                "если <условие> то\n    <операторы1>\nиначе\n    <операторы2>\nкесли",
                "Выполняет один блок кода, если условие истинно, и другой, если ложно."
            ));

            // Loops
            DocumentationEntries.Add(new KeywordDocumentationEntry(
                "нц ... раз ... кц",
                "нц <число> раз\n    <операторы>\nкц",
                "Выполняет цикл указанное количество раз."
            ));

            DocumentationEntries.Add(new KeywordDocumentationEntry(
                "пока ... нц ... кц",
                "пока <условие> нц\n    <операторы>\nкц",
                "Выполняет цикл, пока условие не будет ложным (предусловие)."
            ));

            DocumentationEntries.Add(new KeywordDocumentationEntry(
                "нц ... пока кц",
                "нц\n <операторы>\nпока <условие> кц",
                "Выполняет цикл, пока условие не будет ложным (постусловие)."
            ));

            // Return Statement
            DocumentationEntries.Add(new KeywordDocumentationEntry(
                "вернуть",
                "вернуть 0;",
                "Возвращает значение из функции."
            ));

            // Input/Output
            DocumentationEntries.Add(new KeywordDocumentationEntry(
                "написать",
                "написать\"Привет, мир!\";\nнаписать x;",
                "Выводит текст или значение переменной на экран."
            ));
            DocumentationEntries.Add(new KeywordDocumentationEntry(
                "прочитать",
                "прочитать x;",
                "Считывает ввод пользователя в переменную."
            ));

            // Semicolon
            DocumentationEntries.Add(new KeywordDocumentationEntry(
                "Точка с запятой (;)",
                "цел x = 5;",
                "Разделяет операторы. Каждый оператор должен заканчиваться точкой с запятой."
            ));
        }
    }
}