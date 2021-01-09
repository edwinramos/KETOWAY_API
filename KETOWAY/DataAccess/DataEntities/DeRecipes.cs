using System;
using System.Collections.Generic;
using System.Text;

namespace KetoWay.DataAccess.DataEntities
{
    public class DeRecipe
    {
        public string RecipeCode { get; set; }
        public string RecipeTitle { get; set; }
        public string RecipeContent { get; set; }
        public string LangCode { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
