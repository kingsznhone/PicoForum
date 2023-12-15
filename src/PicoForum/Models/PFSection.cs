using System.ComponentModel.DataAnnotations;

namespace PicoForum.Models
{
    public class PFSection
    {
        public int PFSectionId { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:O}", ApplyFormatInEditMode = true)]
        [Display(Name = "Update Time")]
        public DateTime? TimeUpdate { get; set; }

    }
}