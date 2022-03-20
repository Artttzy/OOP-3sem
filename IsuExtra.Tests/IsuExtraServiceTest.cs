using System;
using System.Globalization;
using IsuExtra.Tools;
using IsuExtra.Services;
using NUnit.Framework;


namespace IsuExtra.Tests
{
    [TestFixture]
    public class IsuExtraServiceTest
    {
        private IsuExtraService _isuExtraService;

        [SetUp]
        public void Setup()
        {
            _isuExtraService = new IsuExtraService();
            _isuExtraService.AddGroup("M3204");
            _isuExtraService.AddStudent(_isuExtraService.FindGroup("M3204"), "Artem Vasiliev");
            _isuExtraService.FindGroup("M3204").AddPair(Convert.ToDateTime("21/11/2021 11:00:00", new CultureInfo("ru-Ru")), "Povysh", 302);
            _isuExtraService.FindGroup("M3204").AddPair(Convert.ToDateTime("22/11/2021 13:00:00", new CultureInfo("ru-Ru")), "Vosya", 466);
            _isuExtraService.FindGroup("M3204").AddPair(Convert.ToDateTime("23/11/2021 15:00:00", new CultureInfo("ru-Ru")), "Povysh", 302);
            _isuExtraService.AddOGNP('M');
            _isuExtraService.FindCourseOgnp('M').AddStream(1, 30);
            _isuExtraService.FindCourseOgnp('M').AddStream(2, 20);
            _isuExtraService.AddOGNP('I');
            _isuExtraService.FindCourseOgnp('I').AddStream(1, 20);
        }

        [Test]
        public void RegisterStudentInHisFacultyOgnpCourse_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuExtraService.RegistrateStudent(_isuExtraService.FindStudent("Artem Vasiliev"), _isuExtraService.FindCourseOgnp('M'), 1);
            });
        }

        [Test]
        public void RegisterStudentOnCourseWithPairConflict_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuExtraService.FindCourseOgnp('I').FindStream(1).AddPair(Convert.ToDateTime("22/11/2021 14:00:00", new CultureInfo("ru-Ru")), "Yatoro", 228);
                _isuExtraService.RegistrateStudent(_isuExtraService.FindStudent("Artem Vasiliev"), _isuExtraService.FindCourseOgnp('I'), 1);
            });  
        }

        [Test]
        public void RegisterStudentOnThreeCourses_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuExtraService.AddOGNP('C');
                _isuExtraService.FindCourseOgnp('C').AddStream(1, 30);
                _isuExtraService.AddOGNP('K');
                _isuExtraService.FindCourseOgnp('K').AddStream(1, 30);
                _isuExtraService.RegistrateStudent(_isuExtraService.FindStudent("Artem Vasiliev"), _isuExtraService.FindCourseOgnp('K'), 1);
                _isuExtraService.RegistrateStudent(_isuExtraService.FindStudent("Artem Vasiliev"), _isuExtraService.FindCourseOgnp('I'), 1);
                _isuExtraService.RegistrateStudent(_isuExtraService.FindStudent("Artem Vasiliev"), _isuExtraService.FindCourseOgnp('C'), 1);
            });       
        }

        [Test]
        public void RegisterAndDeleteStudentInStream_StudentInNotRegisteredStudentsList()
        {
            _isuExtraService.RegistrateStudent(_isuExtraService.FindStudent("Artem Vasiliev"), _isuExtraService.FindCourseOgnp('I'), 1);
            _isuExtraService.AnnulRegistration(_isuExtraService.FindStudent("Artem Vasiliev"), _isuExtraService.FindCourseOgnp('I'), 1);
            var list = _isuExtraService.GetNotRegisteredStudents(_isuExtraService.FindGroup("M3204"));
            Assert.Contains(_isuExtraService.FindStudent("Artem Vasiliev"), list);
        }
    }
}