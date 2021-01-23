using KetoWay.DataAccess;
using KetoWay.DataAccess.DataEntities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.DataLayer
{
    public class DlRecipe : KetoWayContext
    {
        public DlRecipe()
        {

        }
        public List<DeRecipe> GetAll()
        {
            var result = new List<DeRecipe>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from recipe", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new DeRecipe
                        {
                            RecipeCode = reader["RecipeCode"].ToString(),
                            RecipeContent = reader["RecipeContent"].ToString(),
                            RecipeTitle = reader["RecipeTitle"].ToString(),
                            LangCode = reader["LangCode"].ToString(),
                            UpdateDateTime = Convert.ToDateTime(reader["UpdateDateTime"].ToString())
                        });
                    }
                }
            }
            return result;
        }

        public DeRecipe GetByCode(string code, string langCode)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from recipe where RecipeCode = '{code}' AND LangCode = '{langCode}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new DeRecipe
                        {
                            RecipeCode = reader["RecipeCode"].ToString(),
                            RecipeContent = reader["RecipeContent"].ToString(),
                            RecipeTitle = reader["RecipeTitle"].ToString(),
                            LangCode = reader["LangCode"].ToString(),
                            UpdateDateTime = Convert.ToDateTime(reader["UpdateDateTime"].ToString())
                        };
                    }
                }
            }
            return null;
        }
        public DeRecipe Save(DeRecipe obj)
        {
            if (GetByCode(obj.RecipeCode, obj.LangCode) == null)
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"INSERT INTO `recipe` (`RecipeCode`, `RecipeContent`, `RecipeTitle`, `LangCode`, `UpdateDateTime`) VALUES ('{obj.RecipeCode}', '{obj.RecipeContent}', '{obj.RecipeTitle}', '{obj.LangCode}', '{obj.UpdateDateTime.ToString("yyyy-MM-dd hh:mm:ss")}');";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"UPDATE `recipe` SET `RecipeTitle` = '{obj.RecipeTitle}', `RecipeContent` = '{obj.RecipeContent}', `UpdateDateTime` = '{obj.UpdateDateTime.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE `RecipeCode` = '{obj.RecipeCode}' AND `LangCode` = '{obj.LangCode}';";
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
                    var script = $"DELETE FROM `recipe` WHERE `RecipeCode` = '{code}';";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
           
        }
    }
}
