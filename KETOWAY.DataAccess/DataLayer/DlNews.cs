using KetoWay.DataAccess;
using KetoWay.DataAccess.DataEntities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.DataLayer
{
    public class DlNews : KetoWayContext
    {
        public DlNews()
        {

        }
        public List<DeNews> GetAll()
        {
            var result = new List<DeNews>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from news", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new DeNews
                        {
                            NewsCode = reader["NewsCode"].ToString(),
                            NewsContent = reader["NewsContent"].ToString(),
                            NewsTitle = reader["NewsTitle"].ToString(),
                            LangCode = reader["LangCode"].ToString(),
                            UpdateDateTime = Convert.ToDateTime(reader["UpdateDateTime"].ToString())
                        });
                    }
                }
            }
            return result;
        }

        public DeNews GetByCode(string code, string langCode)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from news where NewsCode = '{code}' AND LangCode = '{langCode}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new DeNews
                        {
                            NewsCode = reader["NewsCode"].ToString(),
                            NewsContent = reader["NewsContent"].ToString(),
                            NewsTitle = reader["NewsTitle"].ToString(),
                            LangCode = reader["LangCode"].ToString(),
                            UpdateDateTime = Convert.ToDateTime(reader["UpdateDateTime"].ToString())
                        };
                    }
                }
            }
            return null;
        }
        public DeNews Save(DeNews obj)
        {
            if (GetByCode(obj.NewsCode, obj.LangCode) == null)
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"INSERT INTO `news` (`NewsCode`, `NewsContent`, `NewsTitle`, `LangCode`, `UpdateDateTime`) VALUES ('{obj.NewsCode}', '{obj.NewsContent}', '{obj.NewsTitle}', '{obj.LangCode}', '{obj.UpdateDateTime.ToString("yyyy-MM-dd hh:mm:ss")}');";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"UPDATE `news` SET `NewsTitle` = '{obj.NewsTitle}', `NewsContent` = '{obj.NewsContent}', `UpdateDateTime` = '{obj.UpdateDateTime.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE `NewsCode` = '{obj.NewsCode}' AND `LangCode` = '{obj.LangCode}';";
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
                    var script = $"DELETE FROM `news` WHERE `NewsCode` = '{code}';";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
           
        }
    }
}
