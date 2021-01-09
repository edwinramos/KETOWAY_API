using System;

namespace KetoWay.DataAccess.DataEntities
{
    public class DeEatingGuideDetail
    {
        public int HeadId { get; set; }
        public string FoodCode { get; set; }
        public string FoodDescription { get; set; }
        public string LangCode { get; set; }
        public int SectionID { get; set; }
        public int FoodGroupID { get; set; }
        public double Quantity { get; set; }
        public string Quantity_MeasurementUnitCode { get; set; }
        public double Calories { get; set; }
        public double Carbs { get; set; }
        public double Fat { get; set; }
        public double Protein { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
