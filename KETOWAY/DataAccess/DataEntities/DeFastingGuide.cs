using System;
using System.Collections.Generic;
using System.Text;

namespace KetoWay.DataAccess.DataEntities
{
    public class DeFastingGuide
    {
        public string FastingCode { get; set; }
        public string FastingTitle { get; set; }
        public string FastingContent { get; set; }
        public string LangCode { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
