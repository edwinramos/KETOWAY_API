using KetoWay.DataAccess;
using KetoWay.DataAccess.DataEntities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.DataLayer
{
    public class DlEatingGuide : KetoWayContext
    {
        public DlEatingGuide()
        {

        }
        public List<DeEatingGuide> GetAll()
        {
            var result = new List<DeEatingGuide>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from eating_guide", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new DeEatingGuide
                        {
                            ID = Convert.ToInt32(reader["ID"].ToString()),
                            EatingGuideContent = reader["EatingGuideContent"].ToString(),
                            EatingGuideTitle = reader["EatingGuideTitle"].ToString(),
                            LangCode = reader["LangCode"].ToString(),
                            UpdateDateTime = Convert.ToDateTime(reader["UpdateDateTime"].ToString())
                        });
                    }
                }
            }
            return result;
        }

        public DeEatingGuide GetByID(int id, string langCode)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from eating_guide where ID = {id} AND LangCode = '{langCode}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new DeEatingGuide
                        {
                            ID = Convert.ToInt32(reader["ID"].ToString()),
                            EatingGuideContent = reader["EatingGuideContent"].ToString(),
                            EatingGuideTitle = reader["EatingGuideTitle"].ToString(),
                            LangCode = reader["LangCode"].ToString(),
                            UpdateDateTime = Convert.ToDateTime(reader["UpdateDateTime"].ToString())
                        };
                    }
                }
            }
            return null;
        }
        public DeEatingGuide Save(DeEatingGuide obj)
        {
            if (GetByID(obj.ID, obj.LangCode) == null)
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"INSERT INTO `eating_guide` (`ID`, `EatingGuideContent`, `EatingGuideTitle`, `LangCode`, `UpdateDateTime`) VALUES ({obj.ID}, '{obj.EatingGuideContent}', '{obj.EatingGuideTitle}', '{obj.LangCode}', '{obj.UpdateDateTime.ToString("yyyy-MM-dd hh:mm:ss")}');";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"UPDATE `eating_guide` SET `EatingGuideTitle` = '{obj.EatingGuideTitle}', `EatingGuideContent` = '{obj.EatingGuideContent}', `UpdateDateTime` = '{obj.UpdateDateTime.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE `ID` = '{obj.ID}' AND `LangCode` = '{obj.LangCode}';";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            return obj;
        }
        public void Delete(int id)
        {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"DELETE FROM `eating_guide_detail` WHERE `ID` = {id}; DELETE FROM `eating_guide` WHERE `HeadId` = {id};";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
           
        }
    }
}
