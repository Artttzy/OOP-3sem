using System;
using System.Collections.Generic;
using Isu;
using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class Group : Isu.Group
    {
        private List<Pair> _pairs = new List<Pair>();

        public Group(string groupName)
            : base(groupName)
        {
        }

        public List<Pair> Pairs => _pairs;
        public void AddPair(DateTime dateTime, string teacher, int classNum)
        {
            var pair = new Pair(dateTime, teacher, classNum);
            _pairs.Add(pair);
        }
    }
}