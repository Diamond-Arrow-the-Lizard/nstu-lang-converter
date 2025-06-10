using System.Reflection;

namespace Core.CodeSaver;

/// <summary>
/// Provides functionality for saving generated code to files.
/// </summary>
public static class CodeSaver
{
    /// <summary>
    /// The name of the directory where the generated code files will be stored.
    /// This directory will be created relative to the application's base directory.
    /// </summary>
    private const string OutputDirectory = "Code";

    /// <summary>
    /// Saves the provided C# code string to a file within the designated "Code" directory.
    /// The directory will be created if it does not already exist.
    /// </summary>
    /// <param name="code">The C# code content to be saved.</param>
    /// <param name="fileName">The name of the file to save the code into (e.g., "Program.cs").</param>
    /// <returns>
    /// Returns <c>true</c> if the code was successfully saved; otherwise, <c>false</c>.
    /// Error messages are printed to the console in case of failure.
    /// </returns>
    public static bool SaveGeneratedCode(string code, string fileName)
    {
        try
        {
            // Get the directory where the currently executing assembly (e.g., CLI.dll) is located.
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string currentAssemblyDirectory = Path.GetDirectoryName(assemblyLocation) ?? "";

            // Navigate up two levels to reach the solution root directory.
            // currentAssemblyDirectory (bin/Debug/net9.0)
            // Parent (bin/Debug)
            // Grandparent (bin)
            // Great-grandparent (nstu-rgz-converter - solution root)
            string? solutionRoot = Directory.GetParent(currentAssemblyDirectory)?.Parent?.Parent?.Parent?.FullName;

            if (string.IsNullOrEmpty(solutionRoot))
            {
                Console.WriteLine("Could not determine the solution root directory.");
                return false;
            }

            // Combine the solution root with the desired output directory name ("Code").
            string outputPath = Path.Combine(solutionRoot, OutputDirectory);

            // Create the directory if it does not exist.
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
                Console.WriteLine($"Created directory: {outputPath}");
            }

            // Create the full path to the file.
            string filePath = Path.Combine(outputPath, fileName);

            // Write all the provided code text to the specified file.
            File.WriteAllText(filePath, code);
            Console.WriteLine($"C# code successfully saved to: {filePath}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
            return false;
        }
    }
}