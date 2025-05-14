
using Microsoft.AspNetCore.Mvc;


namespace GreenAgriHub.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }

        public string Role { get; set; }
    }
}