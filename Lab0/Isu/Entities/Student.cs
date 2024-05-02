using System;
using Isu.Exceptions;

namespace Isu.Entities
{
    public class Student
    {
        private Group _group;
        private string studentName;
        private Guid id;
        public Student(string name, Group group)
        {
            if (name is null)
            {
                throw new StudentValidationException("Invalid name");
            }

            studentName = name;
            id = Guid.NewGuid();
            _group = group;
        }

        public Group Group
        {
            get => _group;
            set => _group = value;
        }

        public Guid Id => id;

        public void ChangeGroup(Group newGroup)
        {
            if (newGroup is null)
            {
                throw CustomException.GroupPresenceCheck(newGroup.Name);
            }

            Group = newGroup;
        }
    }
}
