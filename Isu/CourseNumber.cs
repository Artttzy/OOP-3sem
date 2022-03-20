using System.Collections.Generic;

namespace Isu
{
    public class CourseNumber
    {
        private readonly List<Group> _groups;
        private int _courseNumber;

        public CourseNumber(int courseNumber)
        {
            _courseNumber = courseNumber;
            _groups = new List<Group>();
        }

        public int Number => _courseNumber;
        public IReadOnlyList<Group> Groups => _groups;
        public void AddGroup(Group group)
        {
            _groups.Add(group);
        }

        public List<Group> GetGroups()
        {
            return _groups;
        }
    }
}