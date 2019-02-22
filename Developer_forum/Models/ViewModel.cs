using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Developer_forum.Models
{
    //ViewModel for questions table to access all user along with questions
    public class UserQuestion
    {
        public int quesId { get; set; }
        public string question { get; set; }
        public DateTime activityDate { get; set; }
        public string userName { get; set; }
        public string userId { get; set; }

    }

    //ViewModel for answers table to access all users and questions along with answers
    public class allAnswers
    {
        public int ansId { get; set; }
        public string answer { get; set; }
        public int votes { get; set; }
        public string userName { get; set; }

    }
}