namespace SchoolGradesystem.DataTransferObjects
{
    public class SchoolDTO
    {
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
        public AddressDTO Address { get; set; }
    }
}
