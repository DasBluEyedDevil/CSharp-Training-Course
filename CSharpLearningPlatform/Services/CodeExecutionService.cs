using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace CSharpLearningPlatform.Services
{
    /// <summary>
    /// Service for executing C# code using Roslyn scripting
    /// </summary>
    public class CodeExecutionService
    {
        private readonly StringBuilder _outputCapture;

        public CodeExecutionService()
        {
            _outputCapture = new StringBuilder();
        }

        /// <summary>
        /// Execute C# code and capture console output
        /// </summary>
        public async Task<ExecutionResult> ExecuteCodeAsync(string code)
        {
            _outputCapture.Clear();

            try
            {
                // Create a custom text writer to capture Console.WriteLine output
                var originalOut = Console.Out;
                using var stringWriter = new StringWriter(_outputCapture);
                Console.SetOut(stringWriter);

                try
                {
                    // Script options with common imports
                    var options = ScriptOptions.Default
                        .AddReferences(typeof(Console).Assembly)
                        .AddImports("System", "System.Collections.Generic", "System.Linq", "System.Text");

                    // Execute the code
                    var result = await CSharpScript.EvaluateAsync(code, options);

                    // Restore original console output
                    Console.SetOut(originalOut);

                    // Get captured output
                    string output = _outputCapture.ToString();

                    // If there's a return value, add it to output
                    if (result != null && !string.IsNullOrEmpty(result.ToString()))
                    {
                        output += result.ToString();
                    }

                    return new ExecutionResult
                    {
                        Success = true,
                        Output = output,
                        ErrorMessage = null
                    };
                }
                finally
                {
                    Console.SetOut(originalOut);
                }
            }
            catch (CompilationErrorException ex)
            {
                // Compilation errors (syntax errors, type errors, etc.)
                return new ExecutionResult
                {
                    Success = false,
                    Output = _outputCapture.ToString(),
                    ErrorMessage = $"Compilation Error:\n{ex.Message}"
                };
            }
            catch (Exception ex)
            {
                // Runtime errors
                return new ExecutionResult
                {
                    Success = false,
                    Output = _outputCapture.ToString(),
                    ErrorMessage = $"Runtime Error:\n{ex.Message}"
                };
            }
        }

        /// <summary>
        /// Validate if code compiles without executing it
        /// </summary>
        public async Task<bool> ValidateCodeAsync(string code)
        {
            try
            {
                var options = ScriptOptions.Default
                    .AddReferences(typeof(Console).Assembly)
                    .AddImports("System", "System.Collections.Generic", "System.Linq");

                var script = CSharpScript.Create(code, options);
                var diagnostics = script.Compile();

                return diagnostics.Length == 0;
            }
            catch
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Result of code execution
    /// </summary>
    public class ExecutionResult
    {
        public bool Success { get; set; }
        public string Output { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
    }
}
