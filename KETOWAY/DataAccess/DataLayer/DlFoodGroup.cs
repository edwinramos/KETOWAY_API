using KetoWay.DataAccess;
using KetoWay.DataAccess.DataEntities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.DataLayer
{
    public class DlFoodGroup : KetoWayContext
    {
        public DlFoodGroup()
        {

        }

        public List<DeFoodGroup> GetAll()
        {
            var result = new List<DeFoodGroup>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from food_group", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new DeFoodGroup
                        {
                            ID = Convert.ToInt32(reader["ID"].ToString()),
                            FoodGroupDescription = reader["FoodGroupDescription"].ToString(),
                            LangCode = reader["LangCode"].ToString()
                        });
                    }
                }
            }
            return result;
        }

        public DeFoodGroup GetByID(int id) 
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from food_group where ID = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new DeFoodGroup
                        {
                            ID = Convert.ToInt32(reader["ID"].ToString()),
                            FoodGroupDescription = reader["FoodGroupDescription"].ToString(),
                            LangCode = reader["LangCode"].ToString()
                        };
                    }
                }
            }
            return null;
        }
        public DeFoodGroup Save(DeFoodGroup obj) 
        {
            if (GetByID(obj.ID) == null)
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"INSERT INTO `food_group` (`ID`, `FoodGroupDescription`, `LangCode`) VALUES ({obj.ID}, '{obj.FoodGroupDescription}', '{obj.LangCode}');";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"UPDATE `food_group` SET `FoodGroupDescription` = '{obj.FoodGroupDescription}' WHERE `ID` = {obj.ID};";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            return obj;
        }
    }
}
