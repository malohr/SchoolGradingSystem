namespace SchoolGradesystem.Models
{
    public class Address
    {
        public Address(string streetName, int houseNumber, string cityName, int zipCode)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
            CityName = cityName;
            ZipCode = zipCode;
            Schools = new List<School>();
        }

        public int Id { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public string CityName { get; set; }
        public int ZipCode { get; set; }

        //relationships
        public List<School> Schools { get; set; }
    }
}
