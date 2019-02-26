using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Developer_forum.Models
{
    public class SuccessStatus
    {

        public string status { get; set; }
        public Object data { get; set; }
        public records records { get; set; }
        public filters filters { get; set; }
        public search search { get; set; }
        public sort sort { get; set; }
    }

    public class records
    {
        public int total { get; set; }
        public int pageNo { get; set; }
        public int returned { get; set; }
    }

    public class filters
    {
        public string field { get; set; }
        public string value { get; set; }
        public string operators { get; set; }
    }
    public class search
    {
        public string question { get; set; }
    }
    public class sort
    {
        public string field { get; set; }
        public string order { get; set; }
    }

}