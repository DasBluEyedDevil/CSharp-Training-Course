using System.Collections.Generic;

namespace CSharpLearningPlatform.Models
{
    /// <summary>
    /// Represents a learning module containing multiple lessons
    /// </summary>
    public class Module
    {
        public int ModuleId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();

        /// <summary>
        /// Calculate completion percentage for this module
        /// </summary>
        public double GetCompletionPercentage(HashSet<string> completedLessonIds)
        {
            if (Lessons.Count == 0) return 0;

            int completedCount = 0;
            foreach (var lesson in Lessons)
            {
                if (completedLessonIds.Contains(lesson.LessonId))
                {
                    completedCount++;
                }
            }

            return (double)completedCount / Lessons.Count * 100;
        }
    }
}
