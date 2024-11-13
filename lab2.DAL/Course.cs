using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.DAL;

public class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public int Credits { get; set; }
    public ICollection<StudentCourse> StudentCourses { get; set; }
    public int? TeacherId { get; set; }
    public Teacher Teacher { get; set; }
}
