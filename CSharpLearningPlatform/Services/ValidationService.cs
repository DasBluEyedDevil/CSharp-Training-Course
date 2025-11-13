using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CSharpLearningPlatform.Models;

namespace CSharpLearningPlatform.Services
{
    /// <summary>
    /// Service for validating coding challenge solutions
    /// </summary>
    public class ValidationService
    {
        /// <summary>
        /// Validate challenge solution against expected criteria
        /// </summary>
        public ChallengeResult ValidateChallenge(CodeChallenge challenge, ExecutionResult executionResult)
        {
            var result = new ChallengeResult
            {
                CompilationSucceeded = executionResult.Success,
                Output = executionResult.Output,
                ErrorMessage = executionResult.ErrorMessage,
                ValidationMessages = new List<string>()
            };

            // If code didn't compile or threw error, validation fails
            if (!executionResult.Success)
            {
                result.Success = false;
                result.ValidationMessages.Add("❌ Code must compile and run without errors.");
                return result;
            }

            // Check expected output patterns
            if (challenge.ExpectedOutputPatterns != null && challenge.ExpectedOutputPatterns.Any())
            {
                result.OutputMatches = ValidateOutputPatterns(
                    executionResult.Output,
                    challenge.ExpectedOutputPatterns,
                    result.ValidationMessages
                );
            }
            else
            {
                // No specific pattern required, just needs to run successfully
                result.OutputMatches = true;
            }

            // Overall success
            result.Success = result.CompilationSucceeded && result.OutputMatches;

            if (result.Success)
            {
                result.ValidationMessages.Add("✅ Challenge completed successfully!");
            }

            return result;
        }

        /// <summary>
        /// Validate output against expected patterns
        /// </summary>
        private bool ValidateOutputPatterns(string output, List<string> patterns, List<string> messages)
        {
            bool allMatch = true;

            foreach (var pattern in patterns)
            {
                // Try both exact match and regex match
                bool matches = false;

                // Exact substring match
                if (output.Contains(pattern, StringComparison.OrdinalIgnoreCase))
                {
                    matches = true;
                    messages.Add($"✅ Output contains expected text: '{pattern}'");
                }
                else
                {
                    // Try regex match
                    try
                    {
                        if (Regex.IsMatch(output, pattern, RegexOptions.IgnoreCase))
                        {
                            matches = true;
                            messages.Add($"✅ Output matches pattern: '{pattern}'");
                        }
                    }
                    catch
                    {
                        // If regex is invalid, treat as exact match only
                    }
                }

                if (!matches)
                {
                    messages.Add($"❌ Output should contain: '{pattern}'");
                    allMatch = false;
                }
            }

            return allMatch;
        }

        /// <summary>
        /// Check if user's code contains specific constructs (for learning validation)
        /// </summary>
        public bool CodeContains(string userCode, string construct)
        {
            return userCode.Contains(construct, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Provide hints based on common errors
        /// </summary>
        public string GetHintForError(string errorMessage)
        {
            if (errorMessage.Contains("';' expected", StringComparison.OrdinalIgnoreCase))
            {
                return "💡 Hint: Did you forget a semicolon (;) at the end of your statement?";
            }

            if (errorMessage.Contains("does not exist", StringComparison.OrdinalIgnoreCase))
            {
                return "💡 Hint: Check your spelling and make sure you're using the correct class or method name.";
            }

            if (errorMessage.Contains("Cannot implicitly convert", StringComparison.OrdinalIgnoreCase))
            {
                return "💡 Hint: You're trying to put the wrong type of data into a variable. Check your data types!";
            }

            if (errorMessage.Contains("'(' expected", StringComparison.OrdinalIgnoreCase))
            {
                return "💡 Hint: Methods need parentheses () after their name, even if they don't take parameters.";
            }

            return "💡 Hint: Read the error message carefully - it often tells you exactly what's wrong!";
        }
    }
}
