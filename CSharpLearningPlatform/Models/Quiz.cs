using System.Collections.Generic;

namespace CSharpLearningPlatform.Models
{
    /// <summary>
    /// Represents a quiz question with multiple choice answers
    /// </summary>
    public class QuizQuestion
    {
        public string Question { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new List<string>();
        public int CorrectAnswerIndex { get; set; }
        public string Explanation { get; set; } = string.Empty;
    }

    /// <summary>
    /// Represents a quiz for a lesson or module
    /// </summary>
    public class Quiz
    {
        public string QuizId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();
        public int PassingScore { get; set; } = 70; // Percentage

        /// <summary>
        /// Calculate score based on correct answers
        /// </summary>
        public int CalculateScore(List<int> userAnswers)
        {
            if (Questions.Count == 0) return 0;

            int correct = 0;
            for (int i = 0; i < Questions.Count && i < userAnswers.Count; i++)
            {
                if (Questions[i].CorrectAnswerIndex == userAnswers[i])
                {
                    correct++;
                }
            }

            return (int)((double)correct / Questions.Count * 100);
        }
    }

    /// <summary>
    /// Result of a quiz attempt
    /// </summary>
    public class QuizResult
    {
        public string QuizId { get; set; } = string.Empty;
        public int Score { get; set; }
        public bool Passed { get; set; }
        public List<int> UserAnswers { get; set; } = new List<int>();
        public List<bool> QuestionResults { get; set; } = new List<bool>();
    }
}
