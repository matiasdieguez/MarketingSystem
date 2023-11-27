namespace MarketingSystem.Backend.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string PhoneLineNumber { get; set; }
        public double Age
        {
            get { return (DateTime.Now - BirthDate).TotalDays / 365; }
        }
        public bool Active { get; set; }
    }
}
