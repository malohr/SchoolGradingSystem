namespace SchoolGradesystem.ViewModels
{
    public class SubjectViewModel
    {
        public SubjectViewModel(int id, string name, int examCount, int hours, int minimumMark)
        {
            Id = id;
            Name = name;
            ExamCount = examCount;
            Hours = hours;
            MinimumMark = minimumMark;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ExamCount { get; set; }
        public int Hours { get; set; }
        public int MinimumMark { get; set; }
    }
}
