using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{

    public class Test
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new IsuService();
            _isuService.AddGroup("M3201");
            _isuService.AddGroup("M3204");
            _isuService.AddStudent(_isuService.FindGroup("M3204"), "Artem Vasiliev");

        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            _isuService.AddStudent(_isuService.FindGroup("M3204"), "Mishka Freddi");
            if (_isuService.FindStudent("Mishka Freddi").Group.GroupName != "M3204") Assert.Fail();
            _isuService.FindGroup("M3204").CheckStudent("Mishka Freddi");
        }


        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                for (int i = 0; i < 25; i++)
                {
                    _isuService.AddStudent(_isuService.FindGroup("M3204"), "Artem Vasiliev");
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("N3201");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.ChangeStudentGroup(_isuService.FindStudent("Artem Vasiliev"),
                    _isuService.FindGroup("M3201"));
                _isuService.FindGroup("M3204").CheckStudent("Artem Vasiliev");
            });
        }
    }
}