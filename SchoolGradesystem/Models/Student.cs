namespace SchoolGradesystem.Models
{
    public class Student : Person
    {

        public int? GradeId { get; set; }
        public Grade? Grade { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Mark> Marks { get; set; }
    }
}
