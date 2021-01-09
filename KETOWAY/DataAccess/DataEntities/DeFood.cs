using System;
using System.Collections.Generic;
using System.Text;

namespace KetoWay.DataAccess.DataEntities
{
    public class DeFood
    {
        public string FoodCode { get; set; }
        public string FoodTitle { get; set; }
        public string FoodContent { get; set; }
        public int FoodGroupID { get; set; }
        public string LangCode { get; set; }
        public bool IsAllowed { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
