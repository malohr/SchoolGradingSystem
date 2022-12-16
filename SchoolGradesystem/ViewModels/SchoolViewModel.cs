namespace SchoolGradesystem.ViewModels
{
    public class SchoolViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
        public AddressViewModel Address { get; set; }
    }
}
