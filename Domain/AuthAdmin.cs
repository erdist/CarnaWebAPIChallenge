using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain
{
    public class AuthAdmin
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Admin Admin { get; set; }
        public Guid AdminId { get; set; }
    }
}