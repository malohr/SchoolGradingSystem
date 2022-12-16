namespace SchoolGradesystem.DataTransferObjects
{
    public class TeacherDTO
    {
        public TeacherDTO()
        {
            Subjects = new List<int>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int AvailableHours { get; set; }
        public bool IsFullTime { get; set; }

        public List<int> Subjects { get; set; }
    }
}
