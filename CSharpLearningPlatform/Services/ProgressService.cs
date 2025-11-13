using System;
using System.IO;
using CSharpLearningPlatform.Models;
using Newtonsoft.Json;

namespace CSharpLearningPlatform.Services
{
    /// <summary>
    /// Service for persisting and loading user progress
    /// </summary>
    public class ProgressService
    {
        private readonly string _progressFilePath;

        public ProgressService()
        {
            // Store progress file in user's AppData folder
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolder = Path.Combine(appDataPath, "CSharpLearningPlatform");

            // Create directory if it doesn't exist
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            _progressFilePath = Path.Combine(appFolder, "progress.json");
        }

        /// <summary>
        /// Load user progress from file, or create new if doesn't exist
        /// </summary>
        public UserProgress LoadProgress()
        {
            try
            {
                if (File.Exists(_progressFilePath))
                {
                    string json = File.ReadAllText(_progressFilePath);
                    var progress = JsonConvert.DeserializeObject<UserProgress>(json);
                    return progress ?? new UserProgress();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading progress: {ex.Message}");
            }

            return new UserProgress();
        }

        /// <summary>
        /// Save user progress to file
        /// </summary>
        public void SaveProgress(UserProgress progress)
        {
            try
            {
                string json = JsonConvert.SerializeObject(progress, Formatting.Indented);
                File.WriteAllText(_progressFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving progress: {ex.Message}");
            }
        }

        /// <summary>
        /// Reset all progress (useful for testing or starting over)
        /// </summary>
        public void ResetProgress()
        {
            try
            {
                if (File.Exists(_progressFilePath))
                {
                    File.Delete(_progressFilePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error resetting progress: {ex.Message}");
            }
        }

        /// <summary>
        /// Get the path where progress is stored (for user reference)
        /// </summary>
        public string GetProgressFilePath()
        {
            return _progressFilePath;
        }
    }
}
