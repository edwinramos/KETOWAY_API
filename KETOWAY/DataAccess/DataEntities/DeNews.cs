using System;
using System.Collections.Generic;
using System.Text;

namespace KetoWay.DataAccess.DataEntities
{
    public class DeNews
    {
        public string NewsCode { get; set; }
        public string NewsTitle { get; set; }
        public string NewsContent { get; set; }
        public string LangCode { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
