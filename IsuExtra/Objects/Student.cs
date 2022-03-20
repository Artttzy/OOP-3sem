using System.Collections.Generic;
using Isu;

namespace IsuExtra.Objects
{
    public class Student : Isu.Student
    {
        private List<CourseOGNP> _courses = new List<CourseOGNP>();
        private Group _group;

        public Student(string name, int id, IsuExtra.Objects.Group @group)
            : base(name, id, @group)
        {
            _group = group;
        }

        public new Group Group => _group;
        public List<CourseOGNP> Courses => _courses;
    }
}