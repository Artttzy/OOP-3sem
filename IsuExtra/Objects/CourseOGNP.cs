using System.Collections.Generic;

namespace IsuExtra.Objects
{
    public class CourseOGNP
    {
        private char _faculty;
        private List<StreamOGNP> _streams = new List<StreamOGNP>();

        public CourseOGNP(char faculty)
        {
            _faculty = faculty;
        }

        public char Facultaty => _faculty;
        public List<StreamOGNP> Streams => _streams;
        public void AddStream(int num, int limit)
        {
            var stream = new StreamOGNP(num, limit);
            _streams.Add(stream);
        }

        public StreamOGNP FindStream(int num)
        {
            return _streams.Find(s => s.Number == num);
        }
    }
}