using KetoWay.DataAccess;
using KetoWay.DataAccess.DataEntities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.DataLayer
{
    public class DlFastingGuide : KetoWayContext
    {
        public DlFastingGuide()
        {

        }
        public List<DeFastingGuide> GetAll()
        {
            var result = new List<DeFastingGuide>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from fasting_guide", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new DeFastingGuide
                        {
                            FastingCode = reader["FastingCode"].ToString(),
                            FastingContent = reader["FastingContent"].ToString(),
                            FastingTitle = reader["FastingTitle"].ToString(),
                            LangCode = reader["LangCode"].ToString(),
                            UpdateDateTime = Convert.ToDateTime(reader["UpdateDateTime"].ToString())
                        });
                    }
                }
            }
            return result;
        }

        public DeFastingGuide GetByCode(string code, string langCode)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from fasting_guide where FastingCode = '{code}' AND LangCode = '{langCode}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new DeFastingGuide
                        {
                            FastingCode = reader["FastingCode"].ToString(),
                            FastingContent = reader["FastingContent"].ToString(),
                            FastingTitle = reader["FastingTitle"].ToString(),
                            LangCode = reader["LangCode"].ToString(),
                            UpdateDateTime = Convert.ToDateTime(reader["UpdateDateTime"].ToString())
                        };
                    }
                }
            }
            return null;
        }
        public DeFastingGuide Save(DeFastingGuide obj)
        {
            if (GetByCode(obj.FastingCode, obj.LangCode) == null)
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"INSERT INTO `fasting_guide` (`FastingCode`, `FastingContent`, `FastingTitle`, `LangCode`, `UpdateDateTime`) VALUES ('{obj.FastingCode}', '{obj.FastingContent}', '{obj.FastingTitle}', '{obj.LangCode}', '{obj.UpdateDateTime.ToString("yyyy-MM-dd hh:mm:ss")}');";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"UPDATE `fasting_guide` SET `FastingTitle` = '{obj.FastingTitle}', `FastingContent` = '{obj.FastingContent}', `UpdateDateTime` = '{obj.UpdateDateTime.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE `FastingCode` = '{obj.FastingCode}' AND `LangCode` = '{obj.LangCode}';";
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
                    var script = $"DELETE FROM `fasting_guide` WHERE `FastingCode` = '{code}';";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
           
        }
    }
}
