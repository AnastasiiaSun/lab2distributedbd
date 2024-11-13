using lab2.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class DatabaseSeeder : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseSeeder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var _context = scope.ServiceProvider.GetRequiredService<Lab2DbContext>();
        _context.Database.Migrate();

        if (!_context.Teachers.Any())
        {
            var teachers = new[]
            {
                new Teacher { FirstName = "Alice", LastName = "Johnson" },
                new Teacher { FirstName = "Bob", LastName = "Williams" }
            };
            await _context.Teachers.AddRangeAsync(teachers, stoppingToken);
            await _context.SaveChangesAsync(stoppingToken);
        }

        if (!_context.Courses.Any())
        {
            var teachers = _context.Teachers.ToList();
            var courses = new[]
            {
                new Course { Title = "Math", Credits = 3, TeacherId = teachers[0].TeacherId },
                new Course { Title = "Science", Credits = 4, TeacherId = teachers[1].TeacherId },
                new Course { Title = "History", Credits = 3, TeacherId = teachers[0].TeacherId }
            };
            await _context.Courses.AddRangeAsync(courses, stoppingToken);
            await _context.SaveChangesAsync(stoppingToken);
        }

        if (!_context.Students.Any())
        {
            var students = new[]
            {
                new Student { FirstName = "John", LastName = "Doe", GroupNumber = 501 },
                new Student { FirstName = "Jane", LastName = "Smith", GroupNumber = 502 },
                new Student { FirstName = "Bob", LastName = "Brown", GroupNumber = 503 }
            };
            await _context.Students.AddRangeAsync(students, stoppingToken);
            await _context.SaveChangesAsync(stoppingToken);
        }

        if (!_context.StudentCourses.Any())
        {
            var students = _context.Students.ToList();
            var courses = _context.Courses.ToList();

            var studentCourses = new[]
            {
                new StudentCourse { StudentId = students[0].StudentId, CourseId = courses[0].CourseId },
                new StudentCourse { StudentId = students[0].StudentId, CourseId = courses[1].CourseId },
                new StudentCourse { StudentId = students[0].StudentId, CourseId = courses[2].CourseId },
                new StudentCourse { StudentId = students[1].StudentId, CourseId = courses[1].CourseId },
                new StudentCourse { StudentId = students[1].StudentId, CourseId = courses[2].CourseId }
            };
            await _context.StudentCourses.AddRangeAsync(studentCourses, stoppingToken);
            await _context.SaveChangesAsync(stoppingToken);
        }
    }
}
