using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Developer_forum.Models
{
    public class Question
    {
        [Key]
        public int quesId { get; set; }
        [Required]
        [MinLength(10)]
        public string question { get; set; }
        public DateTime activityDate { get; set; }
        public virtual ApplicationUser applicationUser { get; set; }
        [ForeignKey("applicationUser")]
        [MaxLength(128)]
        public string Id { get; set; }


        [NotMapped]
        public string userName { get; set; }
        [NotMapped]
        public string userId { get; set; }
    }
}