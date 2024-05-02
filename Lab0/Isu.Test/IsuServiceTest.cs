using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test
{
    public class IsuServiceTest
    {
        private readonly IsuService _isuService = new IsuService();

        [Fact]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group newGroup = _isuService.AddGroup(new GroupName("M3211"));
            Student newStudent = _isuService.AddStudent(newGroup, "Anna");
            Assert.Contains(newStudent, newGroup.Students);
        }

        [Fact]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Group newGroup = _isuService.AddGroup(new GroupName("M3210"));
            for (int i = 0; i < newGroup.MaxCountOfStudents; i++)
            {
                _isuService.AddStudent(newGroup, "Marina");
            }

            Assert.Throws<CustomException>(() =>
                {
                    for (int i = 0; i < newGroup.CheckNumbMaxCountOfStudents; i++)
                    {
                        _isuService.AddStudent(newGroup, "Valya");
                    }
                });
        }

        [Fact]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Throws<StudentValidationException>(() =>
            {
                Group newGroup = _isuService.AddGroup(new GroupName("t933485"));
            });
        }

        [Fact]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group group = _isuService.AddGroup(new GroupName("M32111"));
            Group newGroup = _isuService.AddGroup(new GroupName("M32101"));
            Student newStudent = _isuService.AddStudent(group, "Marina");
            for (int i = 0; i <= newGroup.MaxCountOfStudents; i++)
            {
                _isuService.AddStudent(newGroup, "Marina");
            }

            Assert.Throws<CustomException>(() =>
                _isuService.ChangeStudentGroup(newStudent, newGroup));
        }
    }
}