using System;

// Step 4: Test the Student record and extensions
Student student1 = new Student("Alice Johnson", 3.8);
Student student2 = new Student("Bob Smith", 2.5);
Student student3 = new Student("Charlie Brown", 3.6);

Console.WriteLine("=== Student Grade Management System ===\n");

Console.WriteLine($"Student: {student1.Name}");
Console.WriteLine($"GPA: {student1.GPA}");
Console.WriteLine($"Letter Grade: {student1.GetLetterGrade()}");
Console.WriteLine($"Honor Roll: {student1.IsHonorRoll()}");
Console.WriteLine();

Console.WriteLine($"Student: {student2.Name}");
Console.WriteLine($"GPA: {student2.GPA}");
Console.WriteLine($"Letter Grade: {student2.GetLetterGrade()}");
Console.WriteLine($"Honor Roll: {student2.IsHonorRoll()}");
Console.WriteLine();

Console.WriteLine($"Minimum Passing GPA: {StudentExtensions.MinimumPassingGpa}");
Console.WriteLine();

// Test validation - this will throw an exception
try
{
    Student invalidStudent = new Student("Invalid Student", 5.0);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Validation Error: {ex.Message}");
}

Console.WriteLine();

// Test ref struct for temporary grade calculations (stack-only)
Console.WriteLine("=== Temporary Grade Calculations ===\n");
GradeCalculator calc = new GradeCalculator(85.5, 92.0, 88.5, 90.0);
Console.WriteLine($"Average Score: {calc.CalculateAverage():F2}");
Console.WriteLine($"GPA Equivalent: {calc.ConvertToGPA():F2}");
Console.WriteLine();

// Test null-coalescing assignment operator (??=)
Console.WriteLine("=== Null-Coalescing Assignment Test ===\n");
Student? nullableStudent = null;
Console.WriteLine($"Student is null: {nullableStudent == null}");

// Use ??= to instantiate only if null
nullableStudent ??= new Student("Default Student", 3.0);
Console.WriteLine($"After ??= assignment - Student: {nullableStudent.Name}, Grade: {nullableStudent.GetLetterGrade()}");

// Try again - this time it won't reassign because nullableStudent is not null
nullableStudent ??= new Student("Another Student", 2.0);
Console.WriteLine($"After second ??= - Student: {nullableStudent.Name}, Grade: {nullableStudent.GetLetterGrade()}");

// Step 1: Define Student record with validation using expression-bodied property
public record Student(string Name, double gpa)
{
    public double GPA { get; init; } = gpa switch
    {
        < 0.0 or > 4.0 => throw new ArgumentException("GPA must be between 0.0 and 4.0"),
        _ => gpa
    };
}

// Step 2 & 3: Extension block with helper methods and static property
public static class StudentExtensions
{
    public static string GetLetterGrade(this Student student)
    {
        return student.GPA switch
        {
            >= 3.7 => "A",
            >= 3.0 => "B",
            >= 2.0 => "C",
            >= 1.0 => "D",
            _ => "F"
        };
    }

    public static bool IsHonorRoll(this Student student)
    {
        return student.GPA >= 3.5;
    }

    public static double MinimumPassingGpa => 2.0;
}

// Interface for grade calculations
public interface IGradeCalculator
{
    double CalculateAverage();
    double ConvertToGPA();
}

// Ref struct implementing interface for temporary grade calculations (stack-only)
public ref struct GradeCalculator : IGradeCalculator
{
    private double _score1;
    private double _score2;
    private double _score3;
    private double _score4;

    public GradeCalculator(double score1, double score2, double score3, double score4)
    {
        _score1 = score1;
        _score2 = score2;
        _score3 = score3;
        _score4 = score4;
    }

    public double CalculateAverage()
    {
        return (_score1 + _score2 + _score3 + _score4) / 4.0;
    }

    public double ConvertToGPA()
    {
        double average = CalculateAverage();
        return average switch
        {
            >= 93.0 => 4.0,
            >= 90.0 => 3.7,
            >= 87.0 => 3.3,
            >= 83.0 => 3.0,
            >= 80.0 => 2.7,
            >= 77.0 => 2.3,
            >= 73.0 => 2.0,
            >= 70.0 => 1.7,
            >= 67.0 => 1.3,
            >= 63.0 => 1.0,
            _ => 0.0
        };
    }
}