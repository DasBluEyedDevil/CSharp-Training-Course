using System.Collections.Generic;

namespace CSharpLearningPlatform.Models
{
    /// <summary>
    /// Represents a single lesson with theory content and coding challenge
    /// </summary>
    public class Lesson
    {
        public int ModuleId { get; set; }
        public int LessonNumber { get; set; }
        public string LessonId { get; set; } = string.Empty; // Format: "M01L01"
        public string Title { get; set; } = string.Empty;

        // Theory Content (The Simplifier)
        public string SimplifierConcept { get; set; } = string.Empty;
        public string CoderExample { get; set; } = string.Empty;
        public List<SyntaxBreakdownItem> SyntaxBreakdown { get; set; } = new List<SyntaxBreakdownItem>();

        // Coding Challenge (The Coder)
        public CodeChallenge? Challenge { get; set; }

        /// <summary>
        /// Full display title with module/lesson numbers
        /// </summary>
        public string FullTitle => $"{ModuleId}.{LessonNumber} - {Title}";
    }

    /// <summary>
    /// Represents a line-by-line explanation of code syntax
    /// </summary>
    public class SyntaxBreakdownItem
    {
        public string CodeSnippet { get; set; } = string.Empty;
        public string Explanation { get; set; } = string.Empty;
    }
}
