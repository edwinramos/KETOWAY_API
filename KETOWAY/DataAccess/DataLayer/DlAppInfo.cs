using KetoWay.DataAccess;
using KetoWay.DataAccess.DataEntities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.DataLayer
{
    public class DlAppInfo : KetoWayContext
    {
        public DlAppInfo()
        {

        }
        public List<DeAppInfo> GetAll()
        {
            var result = new List<DeAppInfo>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from app_info", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new DeAppInfo
                        {
                            InfoCode = reader["InfoCode"].ToString(),
                            InfoContent = reader["InfoContent"].ToString(),
                            LangCode = reader["LangCode"].ToString(),
                            UpdateDateTime = Convert.ToDateTime(reader["UpdateDateTime"].ToString()),
                        });
                    }
                }
            }
            return result;
        }

        public List<DeAppInfo> GetByCode(string code)
        {
            var result = new List<DeAppInfo>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from app_info where InfoCode = '{code}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new DeAppInfo
                        {
                            InfoCode = reader["InfoCode"].ToString(),
                            InfoContent = reader["InfoContent"].ToString(),
                            LangCode = reader["LangCode"].ToString(),
                            UpdateDateTime = Convert.ToDateTime(reader["UpdateDateTime"].ToString()),
                        });
                    }
                }
            }
            return result;
        }
        public DeAppInfo Save(DeAppInfo obj)
        {
            if (GetByCode(obj.InfoCode)?.FirstOrDefault(x => x.LangCode == obj.LangCode) == null)
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"INSERT INTO `app_info` (`InfoCode`, `InfoContent`, `LangCode`, `UpdateDateTime`) VALUES ('{obj.InfoCode}', '{obj.InfoContent}', '{obj.LangCode}', '{obj.UpdateDateTime.ToString("yyyy-MM-dd hh:mm:ss")}');";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"UPDATE `app_info` SET `InfoContent` = '{obj.InfoContent}', `UpdateDateTime` = '{obj.UpdateDateTime.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE `InfoCode` = '{obj.InfoCode}' AND `LangCode` = '{obj.LangCode}';";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            return obj;
        }
    }
}
