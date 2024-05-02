using System;
using Isu.Entities;
using Isu.Models;

namespace Isu.Exceptions
{
    public class CustomException : Exception
    {
        private CustomException(string msg)
            : base(msg)
        {
        }

        public static CustomException MaxCountException(Student student)
        {
            return new CustomException($"Already reached the maximum number of students");
        }

        public static CustomException GroupValidationException()
        {
            return new CustomException($"Invalid no found");
        }

        public static CustomException GroupPresenceCheck(GroupName name)
        {
            return new CustomException($"Invalid {name} already exists");
        }
    }
}