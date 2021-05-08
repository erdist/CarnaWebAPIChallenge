using System;
using System.Text.Json.Serialization;

namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public Guid AdminId { get; set; }
        
        [JsonIgnore]
        public Admin Admin { get; set; }

    }
}