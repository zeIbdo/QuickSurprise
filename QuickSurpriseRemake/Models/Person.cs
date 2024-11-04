namespace QuickSurpriseRemake.Models
{
    public class Person:BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int Age { get; set; }
    }
}
