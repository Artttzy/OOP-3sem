using System;
using System.Collections.Generic;
using Isu.Tools;

namespace Isu
{
    public class Group
    {
        private const int Limit = 25;
        private readonly List<Student> _students = new List<Student>();
        private string _groupName;
        private int _count = 0;
        private int _courseNumber;

        public Group(string groupName)
        {
            if (groupName.Length == 5)
            {
                if (groupName[0] != 'M' || groupName[1] != '3' || groupName[2] < '1' || groupName[2] > '4' ||
                    groupName[3] < '0' || groupName[3] > '9' || groupName[4] < '0' || groupName[4] > '9')
                {
                    throw new InvalidGroupNameIsuException("Invalid group name!");
                }
            }
            else
            {
                throw new InvalidGroupNameIsuException("Invalid group name!");
            }

            this._groupName = groupName;
            _courseNumber = Convert.ToInt32(groupName[2]);
        }

        public List<Student> Students => _students;
        public int CourseNumber => _courseNumber;
        public string GroupName
        {
            get => _groupName;
            set => _groupName = value;
        }

        public void AddStudent(Student student)
        {
            _count++;
            if (_count > Limit)
            {
                throw new GroupLimitIsuException("Group limit has reached!");
            }

            _students.Add(student);
        }

        public bool CheckStudent(string name)
        {
            foreach (Student student in Students)
            {
                if (student.Name == name) return true;
            }

            throw new NoStudentInGroupIsuException("This student is not in this group!");
        }
    }
}