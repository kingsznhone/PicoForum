using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PicoForum.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace PicoForum.Models
{
    public class PFReply
    {
        public int PFReplyId { get; set; }

        public int? PostId {  get; set; }
        [ForeignKey(nameof(PostId))]

        public virtual PFPost Post { get; set; }

        public string? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:O}", ApplyFormatInEditMode = true)]
        [Display(Name = "Publish Time")]
        public DateTime TimePublish { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:O}", ApplyFormatInEditMode = true)]
        [Display(Name = "Modify Time")]
        public DateTime TimeModified { get; set; }

        public PFReply(PFPost post, ApplicationUser user,string content )
        {
            Post = post;
            User = user;
            Content = content;
            TimePublish = DateTime.Now;
            post.TimeUpdate = DateTime.Now;
        }
        public PFReply()
        {
            
        }
    }
}
