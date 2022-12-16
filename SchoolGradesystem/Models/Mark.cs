namespace SchoolGradesystem.Models
{
    public class Mark
    {
        public Mark(int value, int? subjectId, int studentId)
        {
          
            Value = value;
            SubjectId = subjectId;
            StudentId = studentId;

        }

        public int Id { get; set; }
        public int Value { get; set; }

        // building relationships
        public int? SubjectId { get; set; }
        public Subject Subject { get; set; }
        //to one relation
        public int StudentId { get; set; }
        public Student Student { get; set; }
        
        //many relation
        public List<Teacher> Teachers { get; set; }
        
    }
}
