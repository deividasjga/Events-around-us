using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenginiaiWebApp.Models
{
    public class AppUser : IdentityUser
    {
        public int? AA { get; set; }
        public int? BB { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}


//namespace RenginiaiWebApp.Models
//{
//    public class AppUser
//    {
//        [Key]
//        public string Id { get; set; }
//        public int? AA { get; set; }
//        public int? BB { get; set; }
//        public Address? Address { get; set; }
//        public ICollection<Club> Clubs { get; set; }
//        public ICollection<Event> Events { get; set; }
//    }
//}