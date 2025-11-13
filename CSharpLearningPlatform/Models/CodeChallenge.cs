using System.Collections.Generic;

namespace CSharpLearningPlatform.Models
{
    /// <summary>
    /// Represents an interactive coding challenge for a lesson
    /// </summary>
    public class CodeChallenge
    {
        public string Instructions { get; set; } = string.Empty;
        public string StarterCode { get; set; } = string.Empty;
        public string SolutionCode { get; set; } = string.Empty;
        public List<string> ValidationRules { get; set; } = new List<string>();
        public List<string> CommonStickingPoints { get; set; } = new List<string>();
        public string? Hint { get; set; }

        /// <summary>
        /// Expected output patterns for validation (optional)
        /// </summary>
        public List<string> ExpectedOutputPatterns { get; set; } = new List<string>();
    }

    /// <summary>
    /// Result of code execution and validation
    /// </summary>
    public class ChallengeResult
    {
        public bool Success { get; set; }
        public string Output { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
        public List<string> ValidationMessages { get; set; } = new List<string>();
        public bool CompilationSucceeded { get; set; }
        public bool OutputMatches { get; set; }
    }
}
