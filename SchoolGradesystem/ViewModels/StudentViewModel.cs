namespace SchoolGradesystem.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int? GradeId { get; set; }

        public int? GradeNumber { get; set; }

    }
}
