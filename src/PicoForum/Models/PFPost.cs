using PicoForum.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PicoForum.Models
{
    public class PFPost
    {
        public int PFPostId { get; set; }

        public string? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }

        public int? SectionId { get; set; }

        [ForeignKey(nameof(SectionId))]
        public virtual PFSection Section { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:O}", ApplyFormatInEditMode = true)]
        [Display(Name = "Publish Time")]
        public DateTime TimePublish { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:O}", ApplyFormatInEditMode = true)]
        [Display(Name = "Modify Time")]
        public DateTime? TimeModified { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:O}", ApplyFormatInEditMode = true)]
        [Display(Name = "Update Time")]
        public DateTime? TimeUpdate { get; set; }

        public int TotalAccess { get; set; }

        public ICollection<PFReply> Replies { get; set; }

        public PFPost(PFSection section, ApplicationUser user, string title, string content)
        {
            User = user;
            Section = section;
            TimePublish = DateTime.Now;
            TimeUpdate = DateTime.Now;
            Title = title;
            Content = content;
            TotalAccess = 0;
        }

        public PFPost()
        {
        }
    }
}