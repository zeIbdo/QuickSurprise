using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuickSurprise.Context.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public required string Firstname { get; set; }

        public required string Lastname { get; set; }

        public int Age { get; set; }
    }
}
