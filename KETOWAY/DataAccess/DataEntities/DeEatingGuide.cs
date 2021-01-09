using System;
using System.Collections.Generic;
using System.Text;

namespace KetoWay.DataAccess.DataEntities
{
    public class DeEatingGuide
    {
        public int ID { get; set; }
        public string EatingGuideTitle { get; set; }
        public string EatingGuideContent { get; set; }
        public string LangCode { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
