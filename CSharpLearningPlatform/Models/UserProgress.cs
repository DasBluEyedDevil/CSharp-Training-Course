using System;
using System.Collections.Generic;

namespace CSharpLearningPlatform.Models
{
    /// <summary>
    /// Tracks user's progress through the course
    /// </summary>
    public class UserProgress
    {
        public string UserId { get; set; } = "default_user";
        public HashSet<string> CompletedLessonIds { get; set; } = new HashSet<string>();
        public Dictionary<string, int> ChallengeAttempts { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, DateTime> CompletionDates { get; set; } = new Dictionary<string, DateTime>();
        public string? CurrentLessonId { get; set; }

        /// <summary>
        /// Mark a lesson as completed
        /// </summary>
        public void CompleteLesson(string lessonId)
        {
            if (!CompletedLessonIds.Contains(lessonId))
            {
                CompletedLessonIds.Add(lessonId);
                CompletionDates[lessonId] = DateTime.Now;
            }
        }

        /// <summary>
        /// Record a challenge attempt
        /// </summary>
        public void RecordAttempt(string lessonId)
        {
            if (ChallengeAttempts.ContainsKey(lessonId))
            {
                ChallengeAttempts[lessonId]++;
            }
            else
            {
                ChallengeAttempts[lessonId] = 1;
            }
        }

        /// <summary>
        /// Get overall completion percentage
        /// </summary>
        public double GetOverallProgress(int totalLessons)
        {
            if (totalLessons == 0) return 0;
            return (double)CompletedLessonIds.Count / totalLessons * 100;
        }
    }
}
