using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Developer_forum.Models
{
    public class UserQuestion
    {
        public int quesId { get; set; }
        public string question { get; set; }
        public DateTime activityDate { get; set; }
        public string userName { get; set; }
        public string userId { get; set; }

    }
}