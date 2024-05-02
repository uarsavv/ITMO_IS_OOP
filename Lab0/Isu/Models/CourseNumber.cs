using System;
using Isu.Exceptions;

namespace Isu.Models
{
    public class CourseNumber
    {
        private int _courseNumb;

        public CourseNumber(int courseNumb)
        {
            _courseNumb = courseNumb;
        }

        public void CheckCourseNumber(int courseNumb)
        {
            if (courseNumb > 4 || courseNumb < 0)
                throw new StudentValidationException("course value does not match");
        }

        public int GetCourseNumber() // тк поле курс нам прайвет, мы не можем напрямую к нему обращаться, но, чтобы иметь способ его достать мы делаем паблик функцию
        {
            return this._courseNumb;
        }
    }
}