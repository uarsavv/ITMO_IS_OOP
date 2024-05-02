using System.Collections.Generic;
using System.Linq;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities
{
    public class Group
    {
        private GroupName _name;
        private List<Student> _students;
        private int maxCountOfPeople = 24;
        private int checkNumbMaxCountOfStudents = 2;
        public Group(GroupName name)
        {
            if (name is null)
            {
                throw CustomException.GroupValidationException();
            }

            _name = name;
            _students = new List<Student>();
        }

        public GroupName Name { get => _name; }

        public IReadOnlyList<Student> Students
        {
            get => _students;
        }

        public int MaxCountOfStudents
        {
            get => maxCountOfPeople;
        }

        public int CheckNumbMaxCountOfStudents
        {
            get => checkNumbMaxCountOfStudents;
        }

        public void AddStudentGroup(Student student)
        {
            if (Students.Count > maxCountOfPeople)
            {
                throw CustomException.MaxCountException(student);
            }

            _students.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            _students.Remove(student);
        }
    }
}
