using System;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Content
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ContentBody { get; set; }
        public Guid AdminId { get; set; }
        
        [JsonIgnore]
        public Admin Admin { get; set; }
        
    }
}