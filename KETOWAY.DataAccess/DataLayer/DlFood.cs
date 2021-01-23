using KetoWay.DataAccess;
using KetoWay.DataAccess.DataEntities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.DataLayer
{
    public class DlFood : KetoWayContext
    {
        public DlFood()
        {

        }
        public List<DeFood> GetAll()
        {
            var result = new List<DeFood>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from food", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new DeFood
                        {
                            FoodCode = reader["FoodCode"].ToString(),
                            FoodContent = reader["FoodContent"].ToString(),
                            FoodTitle = reader["FoodTitle"].ToString(),
                            FoodGroupID = Convert.ToInt32(reader["FoodGroupID"].ToString()),
                            LangCode = reader["LangCode"].ToString(),
                            IsAllowed = Convert.ToBoolean(reader["IsAllowed"]),
                            UpdateDateTime = Convert.ToDateTime(reader["UpdateDateTime"].ToString())
                        });
                    }
                }
            }
            return result;
        }

        public DeFood GetByCode(string code, string langCode)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from food where FoodCode = '{code}' AND LangCode = '{langCode}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new DeFood
                        {
                            FoodCode = reader["FoodCode"].ToString(),
                            FoodContent = reader["FoodContent"].ToString(),
                            FoodTitle = reader["FoodTitle"].ToString(),
                            FoodGroupID = Convert.ToInt32(reader["FoodGroupID"].ToString()),
                            LangCode = reader["LangCode"].ToString(),
                            IsAllowed = Convert.ToBoolean(reader["IsAllowed"]),
                            UpdateDateTime = Convert.ToDateTime(reader["UpdateDateTime"].ToString())
                        };
                    }
                }
            }
            return null;
        }
        public DeFood Save(DeFood obj)
        {
            if (GetByCode(obj.FoodCode, obj.LangCode) == null)
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"INSERT INTO `food` (`FoodCode`, `FoodContent`, `FoodTitle`, `FoodGroupID`, `LangCode`, `IsAllowed`, `UpdateDateTime`) VALUES ('{obj.FoodCode}', '{obj.FoodContent}', '{obj.FoodTitle}', {obj.FoodGroupID}, '{obj.LangCode}', {obj.IsAllowed}, '{obj.UpdateDateTime.ToString("yyyy-MM-dd hh:mm:ss")}');";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"UPDATE `food` SET `FoodTitle` = '{obj.FoodTitle}', `FoodContent` = '{obj.FoodContent}', `FoodGroupID` = {obj.FoodGroupID}, `IsAllowed`= {obj.IsAllowed}, `UpdateDateTime` = '{obj.UpdateDateTime.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE `FoodCode` = '{obj.FoodCode}' AND `LangCode` = '{obj.LangCode}';";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            return obj;
        }
        public void Delete(string code)
        {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"DELETE FROM `food` WHERE `FoodCode` = '{code}';";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
           
        }
    }
}
