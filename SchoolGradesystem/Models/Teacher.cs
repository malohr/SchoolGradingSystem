namespace SchoolGradesystem.Models
{
    public class Teacher : Person
    {
        public Teacher()
        {
            Subjects = new List<Subject>();
        }
     
        // The available hours a teacher can work
        public int AvailableHours { get; set; }
        // This is used to detect if a teacher works full time
        public bool IsFullTime { get; set; }

        public List<Mark> Marks { get; set; }

        //many relationship (a teacher can have a list of subject)
        public List<Subject> Subjects { get; set; }
    }
}
