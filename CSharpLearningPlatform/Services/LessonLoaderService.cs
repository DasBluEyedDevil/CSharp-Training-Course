using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSharpLearningPlatform.Models;
using Newtonsoft.Json;

namespace CSharpLearningPlatform.Services
{
    /// <summary>
    /// Service responsible for loading lesson content from JSON files
    /// </summary>
    public class LessonLoaderService
    {
        private readonly string _contentBasePath;

        public LessonLoaderService()
        {
            // Get the base directory of the application
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            _contentBasePath = Path.Combine(baseDir, "Content", "Lessons");
        }

        /// <summary>
        /// Load all modules and their lessons from the Content directory
        /// </summary>
        public List<Module> LoadAllModules()
        {
            var modules = new List<Module>();

            try
            {
                // Check if content directory exists
                if (!Directory.Exists(_contentBasePath))
                {
                    Console.WriteLine($"Content directory not found: {_contentBasePath}");
                    return GetFallbackModules();
                }

                // Get all module directories (Module01, Module02, etc.)
                var moduleDirectories = Directory.GetDirectories(_contentBasePath)
                    .OrderBy(d => d)
                    .ToList();

                foreach (var moduleDir in moduleDirectories)
                {
                    var moduleName = Path.GetFileName(moduleDir);

                    // Extract module number from directory name (e.g., "Module01" -> 1)
                    if (int.TryParse(moduleName.Replace("Module", ""), out int moduleId))
                    {
                        var module = LoadModule(moduleDir, moduleId);
                        if (module != null)
                        {
                            modules.Add(module);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading modules: {ex.Message}");
                return GetFallbackModules();
            }

            return modules.Count > 0 ? modules : GetFallbackModules();
        }

        /// <summary>
        /// Load a single module from a directory
        /// </summary>
        private Module? LoadModule(string moduleDirectory, int moduleId)
        {
            try
            {
                var lessons = new List<Lesson>();

                // Get all lesson JSON files
                var lessonFiles = Directory.GetFiles(moduleDirectory, "*.json")
                    .OrderBy(f => f)
                    .ToList();

                foreach (var lessonFile in lessonFiles)
                {
                    var lesson = LoadLesson(lessonFile);
                    if (lesson != null)
                    {
                        lessons.Add(lesson);
                    }
                }

                if (lessons.Count > 0)
                {
                    return new Module
                    {
                        ModuleId = moduleId,
                        Title = GetModuleTitle(moduleId),
                        Description = GetModuleDescription(moduleId),
                        Lessons = lessons
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading module {moduleId}: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Load a single lesson from a JSON file
        /// </summary>
        private Lesson? LoadLesson(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                var lesson = JsonConvert.DeserializeObject<Lesson>(json);
                return lesson;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading lesson from {filePath}: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Get module title based on module ID
        /// </summary>
        private string GetModuleTitle(int moduleId)
        {
            return moduleId switch
            {
                1 => "The Absolute Basics",
                2 => "Storing & Using Information",
                3 => "Making Decisions",
                4 => "Repeating Actions",
                5 => "Grouping Information",
                6 => "OOP Part 1 - Blueprints",
                7 => "OOP Part 2 - Relationships",
                8 => "Handling Mistakes & Organizing Code",
                9 => "Querying Your Data (LINQ)",
                10 => "Asynchronous C#",
                11 => "Building for the Server",
                12 => "Storing Data (EF Core)",
                13 => "Building a Modern Front-End",
                14 => "Full Stack Integration",
                _ => $"Module {moduleId}"
            };
        }

        /// <summary>
        /// Get module description based on module ID
        /// </summary>
        private string GetModuleDescription(int moduleId)
        {
            return moduleId switch
            {
                1 => "Understanding the landscape before writing code",
                2 => "How programs remember and manipulate data",
                _ => $"Learning objectives for Module {moduleId}"
            };
        }

        /// <summary>
        /// Provide fallback modules if content files are missing
        /// </summary>
        private List<Module> GetFallbackModules()
        {
            return new List<Module>
            {
                new Module
                {
                    ModuleId = 1,
                    Title = "The Absolute Basics",
                    Description = "Getting started with C# programming",
                    Lessons = new List<Lesson>
                    {
                        new Lesson
                        {
                            ModuleId = 1,
                            LessonNumber = 1,
                            LessonId = "M01L01",
                            Title = "Welcome to C# Learning Platform",
                            SimplifierConcept = "Welcome! This is a fallback lesson. Please ensure lesson content files are in the Content/Lessons directory.",
                            CoderExample = "Console.WriteLine(\"Hello, World!\");",
                            SyntaxBreakdown = new List<SyntaxBreakdownItem>(),
                            Challenge = null
                        }
                    }
                }
            };
        }
    }
}
