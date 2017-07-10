namespace OfficeLocationMicroservice.Core.SharedContext.OfficeLocationDatabase
{
    public class OfficeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public string Switchboard { get; set; }

        public string Fax { get; set; }

        public string TimeZone { get; set; }

        public bool Operating { get; set; }
    }
}