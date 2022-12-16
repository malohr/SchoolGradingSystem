namespace SchoolGradesystem.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public List<Student> Students { get; set; }
    }
}
