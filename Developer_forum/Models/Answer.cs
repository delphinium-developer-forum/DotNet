using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Developer_forum.Models
{
    public class Answer
    {
        public ApplicationUser applicationUser { get; set; } //foreign key for userid
        [Key,Column(Order =0),ForeignKey("applicationUser")]
        [MaxLength(128)]
        public string Id { get; set; }

        public Question question { get; set; }
        [Key,Column(Order =1),ForeignKey("question")]
        public int quesId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ansId { get; set; }
        [Required]
        [MinLength(10)]
        public string answer { get; set; }

    }
}