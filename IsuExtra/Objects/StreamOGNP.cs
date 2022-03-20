using System;
using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class StreamOGNP
    {
        private int _limit;
        private int _number;
        private int _count;
        private List<Pair> _pairs = new List<Pair>();
        private List<Student> _students = new List<Student>();

        public StreamOGNP(int number, int limit)
        {
            _number = number;
            _limit = limit;
        }

        public int Number => _number;
        public List<Student> Students => _students;
        public List<Pair> Pairs => _pairs;

        public void AddStudent(Student student)
        {
            _count++;
            if (_count > _limit)
            {
                throw new GroupLimitIsuException("Group limit has reached!");
            }

            _students.Add(student);
        }

        public void AddPair(DateTime dateTime, string teacher, int classNum)
        {
            var pair = new Pair(dateTime, teacher, classNum);
            _pairs.Add(pair);
        }
    }
}