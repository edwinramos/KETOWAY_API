using KetoWay.DataAccess;
using KetoWay.DataAccess.DataEntities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.DataLayer
{
    public class DlEatingGuideDetail : KetoWayContext
    {
        public DlEatingGuideDetail()
        {

        }
        public List<DeEatingGuideDetail> GetAll()
        {
            var result = new List<DeEatingGuideDetail>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from eating_guide_detail", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new DeEatingGuideDetail
                        {
                            HeadId = Convert.ToInt32(reader["HeadId"].ToString()),
                            FoodCode = reader["FoodCode"].ToString(),
                            FoodDescription = reader["FoodDescription"].ToString(),
                            FoodGroupID = Convert.ToInt32(reader["FoodGroupID"].ToString()),
                            SectionID = Convert.ToInt32(reader["SectionID"].ToString()),
                            LangCode = reader["LangCode"].ToString(),
                            Quantity_MeasurementUnitCode = reader["Quantity_MeasurementUnitCode"].ToString(),
                            Quantity = Convert.ToDouble(reader["Quantity"].ToString()),
                            Calories = Convert.ToDouble(reader["Calories"].ToString()),
                            Carbs = Convert.ToDouble(reader["Carbs"].ToString()),
                            Protein = Convert.ToDouble(reader["Protein"].ToString()),
                            Fat = Convert.ToDouble(reader["Fat"].ToString()),
                            UpdateDateTime = Convert.ToDateTime(reader["UpdateDateTime"].ToString())
                        });
                    }
                }
            }
            return result;
        }

        public DeEatingGuideDetail GetByID(int headId, string foodCode, string langCode)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from eating_guide_detail where HeadId = {headId} AND FoodCode = '{foodCode}' AND LangCode = '{langCode}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new DeEatingGuideDetail
                        {
                            HeadId = Convert.ToInt32(reader["HeadId"].ToString()),
                            FoodCode = reader["FoodCode"].ToString(),
                            FoodDescription = reader["FoodDescription"].ToString(),
                            FoodGroupID = Convert.ToInt32(reader["FoodGroupID"].ToString()),
                            SectionID = Convert.ToInt32(reader["SectionID"].ToString()),
                            LangCode = reader["LangCode"].ToString(),
                            Quantity_MeasurementUnitCode = reader["Quantity_MeasurementUnitCode"].ToString(),
                            Quantity = Convert.ToDouble(reader["Quantity"].ToString()),
                            Calories = Convert.ToDouble(reader["Calories"].ToString()),
                            Carbs = Convert.ToDouble(reader["Carbs"].ToString()),
                            Protein = Convert.ToDouble(reader["Protein"].ToString()),
                            Fat = Convert.ToDouble(reader["Fat"].ToString()),
                            UpdateDateTime = Convert.ToDateTime(reader["UpdateDateTime"].ToString())
                        };
                    }
                }
            }
            return null;
        }
        public DeEatingGuideDetail Save(DeEatingGuideDetail obj)
        {
            if (GetByID(obj.HeadId, obj.FoodCode, obj.LangCode) == null)
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"INSERT INTO `eating_guide_detail` (`HeadId`, `FoodCode`, `FoodDescription`, `FoodGroupID`, `SectionID`, `Quantity`, `Calories`, `Carbs`, `Protein`, `Fat`, `Quantity_MeasurementUnitCode`, `LangCode`, `UpdateDateTime`) VALUES ({obj.HeadId}, '{obj.FoodCode}', '{obj.FoodDescription}', {obj.FoodGroupID}, {obj.SectionID}, {obj.Quantity}, {obj.Calories}, {obj.Carbs}, {obj.Protein}, {obj.Fat}, '{obj.Quantity_MeasurementUnitCode}', '{obj.LangCode}', '{obj.UpdateDateTime.ToString("yyyy-MM-dd hh:mm:ss")}');";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"UPDATE `eating_guide_detail` SET `Fat` = {obj.Fat}, `Protein` = {obj.Protein}, `Carbs` = {obj.Carbs}, `Quantity_MeasurementUnitCode` = '{obj.Quantity_MeasurementUnitCode}', `FoodCode` = '{obj.FoodCode}', `FoodDescription` = '{obj.FoodDescription}', `FoodGroupID` = {obj.FoodGroupID}, SectionID = {obj.SectionID}, `Quantity` = {obj.Quantity}, `Calories` = '{obj.Calories}', `UpdateDateTime` = '{obj.UpdateDateTime.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE `HeadId` = '{obj.HeadId}' AND `LangCode` = '{obj.LangCode}';";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            return obj;
        }
        public void Delete(int headId, string foodCode)
        {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"DELETE FROM `eating_guide_detail` WHERE `HeadId` = {headId} AND `FoodCode` = '{foodCode}';";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
           
        }
    }
}
