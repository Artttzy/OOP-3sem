using Isu.Tools;

namespace Isu
{
    public class Student
    {
        private string _name;
        private int _id;
        private Group _group;
        public Student(string name, int id, Group @group)
        {
            _name = name;
            _id = id;
            _group = @group;
        }

        public int Id => _id;
        public string Name => _name;
        public Group Group
        {
            get => _group;
            set => _group = value;
        }
    }
}