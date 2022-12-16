namespace SchoolGradesystem.DataTransferObjects
{
    public class UpdateTeacherDTO
    {
        public UpdateTeacherDTO()
        {
            Subjects = new List<int>();
        }

        public string LastName { get; set; }
        public int AvailableHours { get; set; }
        public bool IsFullTime { get; set; }

        public List<int> Subjects { get; set; }
    }
}
