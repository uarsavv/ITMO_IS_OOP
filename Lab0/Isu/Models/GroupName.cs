using System;
using System.ComponentModel.Design;
using Isu.Exceptions;

namespace Isu.Models
{
    public class GroupName
    {
        private int maxLenghtOfGroupName = 6;
        private int minLengthOfgroupName = 5;
        private int maxNumbOfGroupName = 18;
        private int maxStudyForm = 5;
        private int minStudyForm = 0;
        private string groupName;
        private string facultyValye;
        private int studyForm;
        private int groupNumb;
        private CourseNumber courseNumber;
        public GroupName(string groupName)
        {
            facultyValye = groupName.Substring(0, 1);
            this.groupName = groupName;

            studyForm = int.Parse(groupName.Substring(1, 1));
            courseNumber = new CourseNumber(int.Parse(groupName.Substring(2, 1)));
            groupNumb = int.Parse(groupName.Substring(3, 2));
            CheckGropName(groupName);
        }

        public CourseNumber CourseNumb { get; set; }

        public void CheckGropName(string groupName)
        {
            if (groupName.Length > maxLenghtOfGroupName || groupName.Length < minLengthOfgroupName)
                throw new StudentValidationException("does not fit the format of the group name");
            if (char.Parse(facultyValye) != 'M')
                throw new StudentValidationException("does not match the format of the name of the faculty");
            if (studyForm < minStudyForm || studyForm > maxStudyForm)
                throw new StudentValidationException("does not match the format of the number of study from");
            if (groupNumb > maxNumbOfGroupName)
                throw new StudentValidationException("group number does not match");
        }
    }
}