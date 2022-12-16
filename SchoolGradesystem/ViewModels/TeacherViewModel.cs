namespace SchoolGradesystem.ViewModels
{
    public class TeacherViewModel
    {

        public TeacherViewModel(int id,string firstName, string lastName, 
            int age, string gender, 
            int availableHours, bool isFullTime)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age; 
            Gender = gender;
            AvailableHours = availableHours;
            IsFullTime = isFullTime;

            Subjects = new List<SubjectViewModel>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int AvailableHours { get; set; }
        public bool IsFullTime { get; set; }

        public List<SubjectViewModel> Subjects { get; set; }
    }
}
