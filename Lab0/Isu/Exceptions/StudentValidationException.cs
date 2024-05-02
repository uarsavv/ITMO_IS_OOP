using System;

namespace Isu.Exceptions
{
    public class StudentValidationException : Exception
    {
        public StudentValidationException()
            : base(message: "Student validation error")
        {
        }

        public StudentValidationException(string message)
            : base(message)
        {
        }
    }
}