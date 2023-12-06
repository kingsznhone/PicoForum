using Microsoft.AspNetCore.Identity;
using PicoForum.Models;

namespace PicoForum.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Friendlyname { get; set; }
        public bool Mute { get; set; }
        public string AvatarUrl { get; set; }
        public ICollection<PFPost> Posts { get; set; }

        public ICollection<PFReply> Replys { get; set; }

        public void debug()
        {
        }
    }
}