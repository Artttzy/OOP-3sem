using System;

namespace IsuExtra.Objects
{
    public class Pair
    {
        private DateTime _dateTime;
        private string _teacher;
        private int _classNum;

        public Pair(DateTime dateTime,  string teacher, int classNum)
        {
            _dateTime = dateTime;
            _classNum = classNum;
            _teacher = teacher;
        }

        public DateTime DateTime => _dateTime;
    }
}