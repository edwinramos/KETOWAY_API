using KetoWay.DataAccess;
using KetoWay.DataAccess.DataEntities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.DataLayer
{
    public class DlSection : KetoWayContext
    {
        public DlSection()
        {

        }

        public List<DeSection> GetAll()
        {
            var result = new List<DeSection>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from section", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new DeSection
                        {
                            ID = Convert.ToInt32(reader["ID"].ToString()),
                            SectionDescription = reader["SectionDescription"].ToString(),
                            LangCode = reader["LangCode"].ToString(),
                            OrderNumber = Convert.ToInt32(reader["OrderNumber"].ToString())
                        });
                    }
                }
            }
            return result;
        }

        public DeSection GetByID(int id) 
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from section where ID = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new DeSection
                        {
                            ID = Convert.ToInt32(reader["ID"].ToString()),
                            SectionDescription = reader["SectionDescription"].ToString(),
                            LangCode = reader["LangCode"].ToString(),
                            OrderNumber = Convert.ToInt32(reader["OrderNumber"].ToString())
                        };
                    }
                }
            }
            return null;
        }
        public DeSection Save(DeSection obj) 
        {
            if (GetByID(obj.ID) == null)
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"INSERT INTO `section` (`ID`, `SectionDescription`, `LangCode`, `OrderNumber`) VALUES ({obj.ID}, '{obj.SectionDescription}', '{obj.LangCode}', {obj.OrderNumber});";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"UPDATE `section` SET `SectionDescription` = '{obj.SectionDescription}', `OrderNumber`= {obj.OrderNumber} WHERE `ID` = {obj.ID};";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            return obj;
        }
    }
}
