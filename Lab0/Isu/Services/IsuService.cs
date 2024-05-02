using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private List<Student> _allStudents = new List<Student>();
        private List<Group> _allGroups = new List<Group>();
        private List<Group> _allCourseGroups = new List<Group>();

        public IsuService()
        {
            _allCourseGroups = new List<Group>();
            _allStudents = new List<Student>();
            _allGroups = new List<Group>();
        }

        public IReadOnlyList<Student> AllStudents
        {
            get => _allStudents;
        }

        public IReadOnlyList<Group> AllGroups
        {
            get => _allGroups;
        }

        public IReadOnlyList<Group> AllCourseGroups
        {
            get => _allCourseGroups;
        }

        public Group AddGroup(GroupName name)
        {
            if (_allGroups.SingleOrDefault(item => item.Name == name) != null)
            {
                throw CustomException.GroupPresenceCheck(name);
            }
            else
            {
                var group = new Group(name);
                _allGroups.Add(group);
                return group;
            }
        }

        public Student AddStudent(Group group, string name)
        {
            var student = new Student(name, group); // создание объекта класса
            group.AddStudentGroup(student); // обращаюсь к списку студентов в классе

            _allStudents.Add(student);
            return student;
        }

        public Student GetStudent(Guid id)
        {
            if (_allStudents.FirstOrDefault(item => item.Id == id) is null)
            {
                throw new StudentValidationException("no such student");
            }

            return _allStudents.FirstOrDefault(item => item.Id == id);
        }

        public Student FindStudent(Guid id)
        {
            return _allStudents
                .FirstOrDefault(item => item.Id == id);
        }

        public List<Student> FindStudents(GroupName groupName)
        {
            return _allStudents
                .Where(item => item.Group.Name == groupName)
                .Select(item => item).ToList();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return _allStudents
                .Where(item => item.Group.Name.CourseNumb == courseNumber)
                .Select(item => item).ToList();
        }

        public Group FindGroup(GroupName groupName)
        {
            return _allGroups.
                FirstOrDefault(item => item.Name == groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return (List<Group>)_allCourseGroups
                .Where(item => item.Name.CourseNumb == courseNumber)
                .Select(item => item);
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            Student newStudent = _allStudents.FirstOrDefault(item => item.Id == student.Id);
            if (newStudent is null)
            {
                throw new StudentValidationException("no such student");
            }

            Group newGroup1 = _allGroups.FirstOrDefault(item => item.Name == newGroup.Name);
            if (newGroup1 is null)
            {
                throw CustomException.GroupValidationException();
            }

            newGroup1.AddStudentGroup(student);
            newStudent.Group.DeleteStudent(student);
            newStudent.ChangeGroup(newGroup1);
        }
    }
}