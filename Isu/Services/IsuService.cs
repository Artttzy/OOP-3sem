using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Services;
using Isu.Tools;

namespace Isu
{
    public class IsuService : IIsuService
    {
        private List<Group> groups = new List<Group>();
        private List<Student> students = new List<Student>();
        private List<CourseNumber> courses = new List<CourseNumber>();
        private int _lastId = 0;
        public Group AddGroup(string name)
        {
            var group = new Group(name);
            groups.Add(group);
            int courseNumber = Convert.ToInt32(name[2]);
            CourseNumber course = courses.Find(c => c.Number == courseNumber);
            if (course == null)
            {
                course = new CourseNumber(courseNumber);
                courses.Add(course);
            }

            courses.Find(c => c.Number == courseNumber)?.AddGroup(group);
            return group;
        }

        public void AddStudent(Group group, string name)
        {
            var student = new Student(name, _lastId++, group);
            groups.Find(g => g.GroupName == group.GroupName)?.AddStudent(student);
            students.Add(student);
        }

        public Student GetStudent(int id)
        {
            if (students.Find(s => s.Id == id) == null) throw new StudentNotFoundIsuException("Student was not found!");
            return students.Find(s => s.Id == id);
        }

        public Student FindStudent(string name)
        {
            return students.Find(s => s.Name == name) == null ? null : students.Find(s => s.Name == name);
        }

        public List<Student> FindStudents(string groupName)
        {
            return groups.Find(g => g.GroupName == groupName)?.Students;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var list = new List<Student>();
            foreach (Group group in groups.Where(g => g.CourseNumber == courseNumber.Number))
            {
                list.AddRange(group.Students);
            }

            return list;
        }

        public Group FindGroup(string groupName)
        {
            Group group = groups.Find(g => g.GroupName == groupName);
            return group ?? throw new GroupNotFoundIsuException("Group is not found!");
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return courses.Find(c => c.Number == courseNumber.Number)?.GetGroups();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            Group oldGroup = groups.Find(g => g.Students.Any(s => s.Id == student.Id));
            oldGroup?.Students.Remove(oldGroup.Students.Find(s => s.Id == student.Id));
            groups.Find(g => g.GroupName == newGroup.GroupName)?.AddStudent(student);
            student.Group = newGroup;
        }
    }
}