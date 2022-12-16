namespace SchoolGradesystem.Models
{
    public class School
    {
        public School(string name, bool isPrivate)
        {
            Name = name;
            IsPrivate = isPrivate;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        // is this a private school?
        public bool IsPrivate { get; set; }

        // building our relations now

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public void AddAddress(Address addressToAdd)
        {
            Address = addressToAdd;
            AddressId = addressToAdd.Id;
        }
    }
}
