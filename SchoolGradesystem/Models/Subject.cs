namespace SchoolGradesystem.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ExamCount { get; set; }
        public int Hours { get; set; }
        public int MinimumMark { get; set; }



        //relationships
 
        public int? GradeId { get; set; }
        public Grade? Grade { get; set; }

        public List<Mark> Marks { get; set; }
        public List<Student> Students { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}
