using KetoWay.DataAccess;
using KetoWay.DataAccess.DataEntities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.DataLayer
{
    public class DlMeasurementUnit : KetoWayContext
    {
        public DlMeasurementUnit()
        {

        }

        public List<DeMeasurementUnit> GetAll()
        {
            var result = new List<DeMeasurementUnit>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from measurement_unit", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new DeMeasurementUnit
                        {
                            Description = reader["Description"].ToString(),
                            Code = reader["Code"].ToString()
                        });
                    }
                }
            }
            return result;
        }

        public DeMeasurementUnit GetByCode(string code) 
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from measurement_unit where Code = {code}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new DeMeasurementUnit
                        {
                            Description = reader["Description"].ToString(),
                            Code = reader["Code"].ToString()
                        };
                    }
                }
            }
            return null;
        }
        public DeMeasurementUnit Save(DeMeasurementUnit obj) 
        {
            if (GetByCode(obj.Code) == null)
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"INSERT INTO `measurement_unit` (`Code`, `Description`) VALUES ('{obj.Code}', '{obj.Description}');";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"UPDATE `measurement_unit` SET `Description` = '{obj.Description}' WHERE `Code` = {obj.Code};";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            return obj;
        }
    }
}
