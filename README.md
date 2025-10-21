Superhero Database System
PRG2782 - Project 2025

Group: PTA_21PM
Members: Paballo Masekela, Lebogang Masia, Aphelele Mdebuka, Jamie Leeuw

================================================================================

About This Project

This is a C# Windows Forms application that manages superhero trainee records for the One Kick Heroes Academy. The system allows users to add, view, update, and delete superhero information while automatically calculating hero ranks (S, A, B, C) based on exam scores (0-100). It stores all data in text files (superheroes.txt, summary.txt) and includes comprehensive error handling for file operations and input validation. The application also generates summary reports showing statistics like total heroes, average scores, and rank distribution. All development changes are tracked using Git version control and pushed to GitHub.

================================================================================

Individual Contributions

Jamie Leeuw
- Part 1: Add New Superhero
- Part 2: View All Superheroes

Lebogang Masia
- Part 3: Update Superhero Information
- Part 5: Generate Summary Report

Aphelele Mdebuka
- Part 4: Delete Superhero

Paballo Masekela
- Part 8: Error Handling and Documentation

================================================================================

How to Set Up the Project

1. Fork the Repository
   - Click the "Fork" button on GitHub to create your own copy

2. Download the Project
   - Click "Code" then "Download ZIP" on your forked repository
   - Alternatively, clone using: git clone [repository-url]

3. Extract the Files
   - Unzip the downloaded folder to your destination of choice
   - Example: C:\Projects\SuperheroDatabase

4. Open in Visual Studio
   - Launch Visual Studio 2019 or 2022
   - Select "Open a project or solution"
   - Navigate to the extracted folder
   - Select SuperheroDatabase.sln
   - The project will load in the IDE

5. Build and Run
   - Press Ctrl+Shift+B to build the solution
   - Press F5 to run the application

================================================================================

Requirements Satisfied

Core Functionality:

- Add New Superhero: Form input with validation, auto-calculates rank and threat level, saves to superheroes.txt

- View All Superheroes: DataGridView displays all heroes with their calculated ranks and threat levels

- Update Superhero: Load hero by clicking row, edit fields, recalculates rank if exam score changes

- Delete Superhero: Select hero from grid, confirm deletion, removes record from file

- Generate Summary Report: Calculates total heroes, average age, average score, heroes per rank, saves to summary.txt

Technical Requirements:

- Input Validation: Checks for empty fields, validates data types, ensures exam scores are 0-100, validates age range

- Error Handling: File I/O error handling, permission checks, corrupted record handling, automatic backup/restore system

- Git Version Control: Repository initialized, meaningful commits after each feature, clear commit history

- GitHub Integration: Project pushed to GitHub with complete commit history

- Code Documentation: Inline comments throughout, XML documentation for methods, clear variable naming

Ranking System Implementation:

- S-Rank (81-100): Finals Week threat level
- A-Rank (61-80): Midterm Madness threat level
- B-Rank (41-60): Group Project Gone Wrong threat level
- C-Rank (0-40): Pop Quiz threat level

================================================================================

Project Files

- Superhero.cs - Data model with rank calculation logic
- MainForm.cs - Application logic and event handlers
- MainForm.Designer.cs - UI design and controls
- Program.cs - Application entry point
- superheroes.txt - Hero data storage (auto-created)
- summary.txt - Generated reports (auto-created)

================================================================================

Version 1.0 | 2025
