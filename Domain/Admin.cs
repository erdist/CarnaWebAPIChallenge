using System;
using System.Collections.Generic;

namespace Domain
{
    public class Admin
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public List<User> Users { get; set; }
        public List<Content> Contents { get; set; }
    }
}