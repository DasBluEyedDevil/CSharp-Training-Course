# 🎓 C# Learning Platform - From Zero to Full-Stack

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![WPF](https://img.shields.io/badge/WPF-Desktop-0078D4?logo=windows)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

> **An interactive, self-contained desktop learning platform that takes you from absolute beginner to job-ready full-stack .NET developer.**

---

## 📖 What is This?

This is **not just a course** — it's a complete **interactive desktop application** built with WPF that teaches C# programming through:

✅ **Concept-First Learning** - Every technical term introduced with real-world analogies
✅ **Live Coding Challenges** - Write and execute C# code directly in the app
✅ **Instant Feedback** - Roslyn-powered code execution with validation
✅ **Progress Tracking** - Your journey is saved automatically
✅ **26 Complete Lessons** - Modules 1-5 fully implemented + Module 6 started
✅ **Quiz Support** - Multiple-choice quiz system with explanations
✅ **Detailed Outlines** - Complete content plans for all remaining 47 lessons

### Philosophy: "Concept First, Jargon Last"

Every lesson follows this proven structure:

1. **The Simplifier (Concept)** - Plain-English analogy before any code
2. **The Coder (Example)** - Commented code demonstrating the concept
3. **Syntax Breakdown** - Line-by-line explanation
4. **Interactive Challenge** - Hands-on coding exercise
5. **Solution & Common Sticking Points** - Answer with beginner pitfalls explained

---

## 🚀 Quick Start

### Prerequisites

- **Windows 10/11** (WPF application)
- **Visual Studio 2022** (Community Edition is free)
- **.NET 8.0 SDK** or later

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/YOUR_USERNAME/CSharp-Training-Course.git
   cd CSharp-Training-Course
   ```

2. **Open the solution:**
   ```bash
   # Open in Visual Studio
   start CSharpLearningPlatform.sln
   ```

   Or open `CSharpLearningPlatform.sln` directly in Visual Studio.

3. **Restore NuGet packages:**
   Visual Studio will automatically restore packages. If not:
   ```bash
   dotnet restore CSharpLearningPlatform/CSharpLearningPlatform.csproj
   ```

4. **Build and Run:**
   - Press `F5` in Visual Studio, or
   ```bash
   cd CSharpLearningPlatform
   dotnet run
   ```

---

## 📚 Course Curriculum

### **Module 1: The Absolute Basics** ✅ *Implemented*
1. ✅ What is Programming?
2. ✅ What is .NET and the CLR?
3. ✅ Displaying Multiple Lines
4. ✅ Comments: Notes for Humans
5. ✅ Combining Text (String Concatenation)

### **Module 2: Storing & Using Information** ✅ *Implemented*
1. ✅ What is a Variable? (The Labeled Box)
2. ✅ Number Variables (int and double)
3. ✅ Boolean Variables (true or false)
4. ✅ Basic Math Operations
5. ✅ Compound Assignment (Shortcuts!)

### **Module 3: Making Decisions** ✅ *Implemented*
1. ✅ The if Statement
2. ✅ else and else if
3. ✅ Comparison & Logical Operators
4. ✅ The switch Statement
5. ✅ The Ternary Operator

### **Module 4: Repeating Actions** ✅ *Implemented*
1. ✅ The for Loop
2. ✅ The while Loop
3. ✅ The do-while Loop
4. ✅ Loop Control (break and continue)
5. ✅ Nested Loops

### **Module 5: Grouping Information** ✅ *Implemented*
1. ✅ Arrays (The Fixed-Size Shelf)
2. ✅ List<T> (The Flexible Container)
3. ✅ Dictionary<TKey, TValue> (The Lookup Table)
4. ✅ foreach Loop & Collection Methods

### **Module 6: OOP Part 1** 🚧 *In Progress (2/7)*
1. ✅ Why Object-Oriented Programming?
2. ✅ Constructors (The Setup Crew)
3. 📋 Properties (Controlled Access)
4. 📋 Methods
5. 📋 The 'this' Keyword
6. 📋 Access Modifiers
7. 📋 Static vs Instance

### **Modules 7-14** 📋 *Detailed Outlines Ready*
See [REMAINING_LESSONS_OUTLINE.md](REMAINING_LESSONS_OUTLINE.md) for complete content plans:
- **Module 7**: OOP Part 2 (Inheritance, Polymorphism, Interfaces)
- **Module 8**: Error Handling & Code Organization
- **Module 9**: LINQ (Querying Data)
- **Module 10**: Async/Await
- **Module 11**: ASP.NET Core 8 Minimal APIs
- **Module 12**: Entity Framework Core 8
- **Module 13**: Blazor .NET 8
- **Module 14**: Full-Stack Integration & Deployment

---

## 🎨 Features

### Interactive Code Editor
- **Syntax Highlighting** powered by AvalonEdit
- **Live Code Execution** using Roslyn compiler
- **Instant Validation** with helpful error messages
- **Hints & Solutions** when you get stuck

### Progress Tracking
- Automatically saves completed lessons
- Visual progress indicators
- Tracks challenge attempts
- Resume from where you left off

### Modern UI
- Clean, professional WPF interface
- ModernWpfUI styling
- Responsive layout
- Dark code editor theme

---

## 🏗️ Architecture

```
CSharpLearningPlatform/
├── Models/                  # Data models (Lesson, Module, UserProgress)
├── Services/                # Business logic
│   ├── LessonLoaderService.cs       # Load lessons from JSON
│   ├── CodeExecutionService.cs      # Roslyn-based code execution
│   ├── ProgressService.cs           # Save/load user progress
│   └── ValidationService.cs         # Challenge validation
├── Views/                   # WPF XAML views
│   └── MainWindow.xaml              # Main application window
├── Content/Lessons/         # JSON lesson files
│   ├── Module01/
│   └── Module02/
└── App.xaml                 # Application entry point
```

### Technologies Used

- **WPF** - Windows Presentation Foundation for desktop UI
- **Roslyn** - Microsoft.CodeAnalysis for C# code compilation and execution
- **AvalonEdit** - Syntax-highlighted code editor
- **ModernWpfUI** - Modern, flat UI styling
- **Newtonsoft.Json** - JSON lesson file parsing

---

## 📝 Adding New Lessons

Lessons are stored as JSON files in `Content/Lessons/ModuleXX/`. Here's the structure:

```json
{
  "moduleId": 1,
  "lessonNumber": 1,
  "lessonId": "M01L01",
  "title": "Lesson Title",
  "simplifierConcept": "Plain-English explanation with analogy...",
  "coderExample": "// Code example here\nConsole.WriteLine(\"Hello\");",
  "syntaxBreakdown": [
    {
      "codeSnippet": "Console.WriteLine",
      "explanation": "What this code does..."
    }
  ],
  "challenge": {
    "instructions": "Task description...",
    "starterCode": "// Starting code",
    "solutionCode": "// Solution",
    "hint": "Helpful hint...",
    "expectedOutputPatterns": ["Expected output"],
    "commonStickingPoints": [
      "Common mistake 1",
      "Common mistake 2"
    ]
  }
}
```

Simply add a new JSON file following this structure, and the app will automatically load it!

---

## 🎯 Learning Path

This platform is designed to take you through a complete journey:

1. **Foundation** (Modules 1-2) → Console applications, variables, basic syntax
2. **Control Flow** (Modules 3-4) → Decision making, loops
3. **Data Structures** (Module 5) → Collections and grouping
4. **OOP Fundamentals** (Modules 6-7) → Classes, inheritance, interfaces
5. **Professional Practices** (Module 8) → Error handling, organization
6. **Advanced C#** (Modules 9-10) → LINQ, async/await
7. **Backend Development** (Modules 11-12) → ASP.NET Core, Entity Framework
8. **Frontend Development** (Module 13) → Blazor components
9. **Full-Stack Integration** (Module 14) → Complete applications, deployment

---

## 🤝 Contributing

Contributions are welcome! Here's how you can help:

- **Add New Lessons** - Create JSON files for Modules 3-14
- **Improve Explanations** - Enhance existing lesson content
- **Report Bugs** - Open issues for any problems
- **Suggest Features** - Ideas for improving the learning experience

### Development Setup

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/new-lesson`
3. Make your changes
4. Test thoroughly
5. Commit: `git commit -m "Add lesson X.Y on topic Z"`
6. Push: `git push origin feature/new-lesson`
7. Open a Pull Request

---

## 📊 Project Status

**Overall Progress: 26/73 lessons (36% complete)**

- ✅ **Core Platform**: Complete with Quiz support
- ✅ **Modules 1-2**: 10/10 lessons (Foundation)
- ✅ **Modules 3-4**: 10/10 lessons (Control Flow)
- ✅ **Module 5**: 4/4 lessons (Collections)
- 🚧 **Module 6**: 2/7 lessons (OOP Part 1 - In Progress)
- 📋 **Modules 7-14**: 47 lessons remaining (Detailed outlines provided)

### 📚 Documentation
- ✅ **CURRICULUM.md** - Complete 73-lesson roadmap
- ✅ **PROGRESS_TRACKER.md** - Detailed progress tracking
- ✅ **LESSON_TEMPLATE.json** - Copy-paste template for new lessons
- ✅ **REMAINING_LESSONS_OUTLINE.md** - Complete content outlines for all 47 remaining lessons

### 🎯 Next Steps
1. Complete Module 6 OOP Part 1 (5 lessons remaining)
2. Create Module 7 OOP Part 2 (5 lessons)
3. Follow the priority order in PROGRESS_TRACKER.md
4. Use LESSON_TEMPLATE.json for consistent lesson creation

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 🙏 Acknowledgments

- **Microsoft** - For the .NET platform and Roslyn compiler
- **AvalonEdit** - For the excellent code editor component
- **ModernWpfUI** - For beautiful, modern WPF styling
- **All learners** - Your journey to becoming a developer starts here!

---

## 📧 Contact & Support

- **Issues**: [GitHub Issues](https://github.com/YOUR_USERNAME/CSharp-Training-Course/issues)
- **Discussions**: [GitHub Discussions](https://github.com/YOUR_USERNAME/CSharp-Training-Course/discussions)

---

## 🌟 Star This Project

If you find this learning platform helpful, please give it a ⭐ on GitHub!

**Happy Learning! 🎓**

---

*Built with ❤️ to make C# programming accessible to everyone.*
