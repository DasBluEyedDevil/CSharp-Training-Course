using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CSharpLearningPlatform.Models;
using CSharpLearningPlatform.Services;

namespace CSharpLearningPlatform
{
    public partial class MainWindow : Window
    {
        private readonly LessonLoaderService _lessonLoader;
        private readonly CodeExecutionService _codeExecutor;
        private readonly ProgressService _progressService;
        private readonly ValidationService _validationService;

        private List<Module> _modules;
        private UserProgress _userProgress;
        private Lesson? _currentLesson;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize services
            _lessonLoader = new LessonLoaderService();
            _codeExecutor = new CodeExecutionService();
            _progressService = new ProgressService();
            _validationService = new ValidationService();

            // Load data
            LoadContent();
        }

        /// <summary>
        /// Load modules and user progress
        /// </summary>
        private void LoadContent()
        {
            try
            {
                // Load modules from JSON files
                _modules = _lessonLoader.LoadAllModules();

                // Load user progress
                _userProgress = _progressService.LoadProgress();

                // Display modules in sidebar
                icModules.ItemsSource = _modules;

                // Update progress display
                UpdateProgressDisplay();

                // If user has current lesson, load it
                if (!string.IsNullOrEmpty(_userProgress.CurrentLessonId))
                {
                    var lesson = FindLessonById(_userProgress.CurrentLessonId);
                    if (lesson != null)
                    {
                        LoadLesson(lesson);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading content: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Update progress display in sidebar
        /// </summary>
        private void UpdateProgressDisplay()
        {
            int totalLessons = _modules.Sum(m => m.Lessons.Count);
            double overallProgress = _userProgress.GetOverallProgress(totalLessons);

            txtOverallProgress.Text = $"Progress: {overallProgress:F0}% ({_userProgress.CompletedLessonIds.Count}/{totalLessons} lessons)";
            pbOverallProgress.Value = overallProgress;
        }

        /// <summary>
        /// Find a lesson by its ID across all modules
        /// </summary>
        private Lesson? FindLessonById(string lessonId)
        {
            foreach (var module in _modules)
            {
                var lesson = module.Lessons.FirstOrDefault(l => l.LessonId == lessonId);
                if (lesson != null)
                    return lesson;
            }
            return null;
        }

        /// <summary>
        /// Handle lesson button click
        /// </summary>
        private void BtnLesson_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Lesson lesson)
            {
                LoadLesson(lesson);
            }
        }

        /// <summary>
        /// Load and display a lesson
        /// </summary>
        private void LoadLesson(Lesson lesson)
        {
            _currentLesson = lesson;
            _userProgress.CurrentLessonId = lesson.LessonId;
            _progressService.SaveProgress(_userProgress);

            // Hide welcome, show lesson
            spWelcome.Visibility = Visibility.Collapsed;
            spLesson.Visibility = Visibility.Visible;

            // Display lesson content
            txtLessonTitle.Text = lesson.FullTitle;
            txtSimplifierConcept.Text = lesson.SimplifierConcept;
            txtCoderExample.Text = lesson.CoderExample;

            // Display syntax breakdown
            icSyntaxBreakdown.ItemsSource = lesson.SyntaxBreakdown;

            // Display challenge if available
            if (lesson.Challenge != null)
            {
                spChallenge.Visibility = Visibility.Visible;
                txtChallengeInstructions.Text = lesson.Challenge.Instructions;
                txtCodeEditor.Text = lesson.Challenge.StarterCode;
                borderOutput.Visibility = Visibility.Collapsed;
                spStickingPoints.Visibility = Visibility.Collapsed;
                icValidationMessages.ItemsSource = null;
            }
            else
            {
                spChallenge.Visibility = Visibility.Collapsed;
            }

            // Show next lesson button if not completed
            btnNextLesson.Visibility = _userProgress.CompletedLessonIds.Contains(lesson.LessonId)
                ? Visibility.Visible
                : Visibility.Collapsed;

            // Scroll to top
            ((ScrollViewer)spLessonContent.Parent).ScrollToTop();
        }

        /// <summary>
        /// Run user's code
        /// </summary>
        private async void BtnRunCode_Click(object sender, RoutedEventArgs e)
        {
            if (_currentLesson?.Challenge == null)
                return;

            try
            {
                // Show loading state
                btnRunCode.IsEnabled = false;
                btnRunCode.Content = "⏳ Running...";

                // Get user's code
                string userCode = txtCodeEditor.Text;

                // Execute code
                var executionResult = await _codeExecutor.ExecuteCodeAsync(userCode);

                // Validate against challenge criteria
                var validationResult = _validationService.ValidateChallenge(_currentLesson.Challenge, executionResult);

                // Display output
                borderOutput.Visibility = Visibility.Visible;
                txtOutput.Text = string.IsNullOrEmpty(validationResult.Output)
                    ? "(No output)"
                    : validationResult.Output;

                // Display validation messages
                icValidationMessages.ItemsSource = validationResult.ValidationMessages;

                // If there's an error, show hint
                if (!validationResult.Success && !string.IsNullOrEmpty(validationResult.ErrorMessage))
                {
                    string hint = _validationService.GetHintForError(validationResult.ErrorMessage);
                    validationResult.ValidationMessages.Add(hint);
                    icValidationMessages.ItemsSource = validationResult.ValidationMessages;
                }

                // If successful, mark lesson as completed
                if (validationResult.Success)
                {
                    _userProgress.CompleteLesson(_currentLesson.LessonId);
                    _progressService.SaveProgress(_userProgress);
                    UpdateProgressDisplay();
                    btnNextLesson.Visibility = Visibility.Visible;

                    MessageBox.Show(
                        "🎉 Congratulations! You've completed this challenge!\n\nClick 'Next Lesson' to continue.",
                        "Challenge Completed!",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                }

                // Record attempt
                _userProgress.RecordAttempt(_currentLesson.LessonId);
                _progressService.SaveProgress(_userProgress);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing code: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Restore button state
                btnRunCode.IsEnabled = true;
                btnRunCode.Content = "▶ Run Code";
            }
        }

        /// <summary>
        /// Show hint for the challenge
        /// </summary>
        private void BtnShowHint_Click(object sender, RoutedEventArgs e)
        {
            if (_currentLesson?.Challenge == null)
                return;

            string hint = !string.IsNullOrEmpty(_currentLesson.Challenge.Hint)
                ? _currentLesson.Challenge.Hint
                : "💡 Try reading the instructions carefully and reviewing the code example above!";

            MessageBox.Show(hint, "Hint", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Show solution for the challenge
        /// </summary>
        private void BtnShowSolution_Click(object sender, RoutedEventArgs e)
        {
            if (_currentLesson?.Challenge == null)
                return;

            var result = MessageBox.Show(
                "Are you sure you want to see the solution?\n\nTry your best first - learning happens through struggle!",
                "Show Solution?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                txtCodeEditor.Text = _currentLesson.Challenge.SolutionCode;

                // Show common sticking points
                if (_currentLesson.Challenge.CommonStickingPoints.Any())
                {
                    spStickingPoints.Visibility = Visibility.Visible;
                    icStickingPoints.ItemsSource = _currentLesson.Challenge.CommonStickingPoints;
                }
            }
        }

        /// <summary>
        /// Go to next lesson
        /// </summary>
        private void BtnNextLesson_Click(object sender, RoutedEventArgs e)
        {
            if (_currentLesson == null)
                return;

            // Find current module
            var currentModule = _modules.FirstOrDefault(m => m.ModuleId == _currentLesson.ModuleId);
            if (currentModule == null)
                return;

            // Find current lesson index
            int currentIndex = currentModule.Lessons.FindIndex(l => l.LessonId == _currentLesson.LessonId);

            // Try next lesson in same module
            if (currentIndex < currentModule.Lessons.Count - 1)
            {
                LoadLesson(currentModule.Lessons[currentIndex + 1]);
                return;
            }

            // Try first lesson of next module
            var nextModule = _modules.FirstOrDefault(m => m.ModuleId == currentModule.ModuleId + 1);
            if (nextModule != null && nextModule.Lessons.Any())
            {
                LoadLesson(nextModule.Lessons[0]);
                return;
            }

            // No more lessons
            MessageBox.Show(
                "🎓 Congratulations! You've reached the end of the available lessons!",
                "Course Complete",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }

        /// <summary>
        /// Reset user progress
        /// </summary>
        private void BtnResetProgress_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to reset all progress?\n\nThis cannot be undone.",
                "Reset Progress",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (result == MessageBoxResult.Yes)
            {
                _progressService.ResetProgress();
                _userProgress = new UserProgress();
                UpdateProgressDisplay();

                MessageBox.Show(
                    "Progress has been reset!",
                    "Reset Complete",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }
        }
    }
}
