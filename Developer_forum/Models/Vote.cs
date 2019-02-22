using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Developer_forum.Models
{
    public class Vote
    {

        public ApplicationUser applicationUser { get; set; } //foreign key for userid
        [Key, Column(Order = 0), ForeignKey("applicationUser")]
        [MaxLength(128)]
        public string Id { get; set; }
        public Answer answer { get; set; }
        [Key, Column(Order = 1),ForeignKey("answer")]
        public int ansId { get; set; }
        [Range(-1,1)]
        public int votes { get; set; }
    }
}