using KetoWay.DataAccess;
using KetoWay.DataAccess.DataEntities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.DataLayer
{
    public class DlAppLanguage : KetoWayContext
    {
        public DlAppLanguage()
        {

        }

        public List<DeAppLanguage> GetAll()
        {
            var result = new List<DeAppLanguage>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from app_languages", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new DeAppLanguage
                        {
                            LangCode = reader["LangCode"].ToString(),
                            LangDescription = reader["LangDescription"].ToString()
                        });
                    }
                }
            }
            return result;
        }

        public DeAppLanguage GetByCode(string code) 
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from app_languages where LangCode = '{code}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new DeAppLanguage
                        {
                            LangCode = reader["LangCode"].ToString(),
                            LangDescription = reader["LangDescription"].ToString()
                        };
                    }
                }
            }
            return null;
        }
        public DeAppLanguage Save(DeAppLanguage obj) 
        {
            if (GetByCode(obj.LangCode) == null)
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"INSERT INTO `app_languages` (`LangCode`, `LangDescription`) VALUES ('{obj.LangCode}', '{obj.LangDescription}');";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"UPDATE `app_languages` SET `LangDescription` = '{obj.LangDescription}' WHERE `LangCode` = '{obj.LangCode}';";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            return obj;
        }
    }
}
