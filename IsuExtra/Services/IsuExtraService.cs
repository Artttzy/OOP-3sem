using System;
using System.Collections.Generic;
using System.Linq;
using Isu;
using IsuExtra.Objects;
using IsuExtra.Tools;
using Group = IsuExtra.Objects.Group;
using Student = IsuExtra.Objects.Student;

namespace IsuExtra.Services
{
    public class IsuExtraService : IsuService
    {
        private List<Group> groups = new List<Group>();
        private List<Student> students = new List<Student>();
        private List<CourseNumber> courses = new List<CourseNumber>();
        private List<CourseOGNP> coursesOgnp = new List<CourseOGNP>();
        private int _lastId = 0;
        public new Group AddGroup(string name)
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

        public new Student FindStudent(string name)
        {
            return students.Find(s => s.Name == name) == null ? null : students.Find(s => s.Name == name);
        }

        public new Group FindGroup(string groupName)
        {
            Group group = groups.Find(g => g.GroupName == groupName);
            return group ?? throw new GroupNotFoundIsuException("Group is not found!");
        }

        public CourseOGNP AddOGNP(char faculty)
        {
            var courseOGNP = new CourseOGNP(faculty);
            coursesOgnp.Add(courseOGNP);
            return courseOGNP;
        }

        public void RegistrateStudent(Student student, CourseOGNP courseOGNP, int streamNum)
        {
            if (courseOGNP.Facultaty != student.Group.GroupName[0])
            {
                if (student.Courses.Count < 2)
                {
                    if (PairsCheck(student.Group, courseOGNP.Streams.Find(s => s.Number == streamNum)) == true)
                    {
                        courseOGNP.Streams.Find(g => g.Number == streamNum).AddStudent(student);
                        student.Courses.Add(courseOGNP);
                    }
                    else
                    {
                        throw new RegisterIsFailedIsuException("Group pairs conflict with course pairs!");
                    }
                }
                else
                {
                    throw new RegisterIsFailedIsuException("Student is already registered for 2 courses!");
                }
            }
            else
            {
                throw new RegisterIsFailedIsuException("Student faculty is tha same as OGNP faculty!");
            }
        }

        public void AnnulRegistration(Student student, CourseOGNP courseOGNP, int streamNum)
        {
            var group = courseOGNP.Streams.Find(g => g.Number == streamNum);
            group.Students.Remove(student);
            student.Courses.Remove(courseOGNP);
        }

        public List<StreamOGNP> GetStreamsOGNP(CourseOGNP course)
        {
            return course.Streams;
        }

        public List<Student> GetStudentsOGNP(StreamOGNP stream)
        {
            return stream.Students;
        }

        public List<Student> GetNotRegisteredStudents(Group group)
        {
            var list = new List<Student>();
            foreach (Student student in group.Students)
            {
                if (student.Courses.Count == 0)
                {
                    list.Add(student);
                }
            }

            return list;
        }

        public bool PairsCheck(Group group, StreamOGNP stream)
        {
            return group.Pairs.SelectMany(pgroup => stream.Pairs.Select(pstream => (pgroup, pstream))).All(pairs => (pairs.pgroup.DateTime - pairs.pstream.DateTime).Duration() >= TimeSpan.FromMinutes(90));
        }

        public CourseOGNP FindCourseOgnp(char facultaty)
        {
            return coursesOgnp.Find(c => c.Facultaty == facultaty);
        }
    }
}