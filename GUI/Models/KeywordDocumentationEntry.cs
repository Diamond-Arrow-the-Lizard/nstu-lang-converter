using CommunityToolkit.Mvvm.ComponentModel;

namespace GUI.Models
{
    /// <summary>
    /// Represents a single entry for pseudocode keyword documentation.
    /// </summary>
    public partial class KeywordDocumentationEntry : ObservableObject
    {
        /// <summary>
        /// Gets or sets the keyword itself (e.g., "начало", "если").
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// Gets or sets the syntax example for the keyword (e.g., "начало ... конец").
        /// </summary>
        public string Syntax { get; set; }

        /// <summary>
        /// Gets or sets a brief explanation of the keyword's function.
        /// </summary>
        public string Explanation { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeywordDocumentationEntry"/> class.
        /// </summary>
        /// <param name="keyword">The pseudocode keyword.</param>
        /// <param name="syntax">The syntax example for the keyword.</param>
        /// <param name="explanation">A brief explanation of the keyword's function.</param>
        public KeywordDocumentationEntry(string keyword, string syntax, string explanation)
        {
            Keyword = keyword;
            Syntax = syntax;
            Explanation = explanation;
        }
    }
}