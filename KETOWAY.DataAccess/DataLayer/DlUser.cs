using KetoWay.DataAccess;
using KetoWay.DataAccess.DataEntities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.DataLayer
{
    public class DlUser : KetoWayContext
    {
        public DlUser()
        {

        }

        public List<DeUser> GetAll()
        {
            var result = new List<DeUser>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from User", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new DeUser
                        {
                            UserCode = reader["userCode"].ToString(),
                            Password = reader["Password"].ToString(),
                            Name = reader["Name"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            CountryCode = reader["CountryCode"].ToString(),
                            StateCode = reader["StateCode"].ToString(),
                            ImagePath = reader["ImagePath"].ToString(),
                            Gender = (Gender)Enum.Parse(typeof(Gender), reader["Gender"].ToString(), true),
                            BirthDate = Convert.ToDateTime(reader["BirthDate"].ToString()),
                            IsAdmin = Convert.ToBoolean(reader["IsAdmin"].ToString())
                        });
                    }
                }
            }
            return result;
        }

        public DeUser GetByCode(string id) 
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from User where UserCode = '{id}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new DeUser
                        {
                            UserCode = reader["userCode"].ToString(),
                            Password = reader["Password"].ToString(),
                            Name = reader["Name"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            CountryCode = reader["CountryCode"].ToString(),
                            StateCode = reader["StateCode"].ToString(),
                            ImagePath = reader["ImagePath"].ToString(),
                            Gender = (Gender)Enum.Parse(typeof(Gender), reader["Gender"].ToString(), true),
                            BirthDate = Convert.ToDateTime(reader["BirthDate"].ToString()),
                            IsAdmin = Convert.ToBoolean(reader["IsAdmin"].ToString())
                        };
                    }
                }
            }
            return null;
        }
        public DeUser Save(DeUser obj) 
        {
            if (GetByCode(obj.UserCode) == null)
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var script = $"INSERT INTO `User` (`UserCode`, `Password`, `Name`, `LastName`, `Email`, `CountryCode`, `StateCode`, `Imagepath`, `BirthDate`, `Gender`) VALUES ('{obj.UserCode}', '{obj.Password}', '{obj.Name}', '{obj.LastName}', '{obj.Email}', '{obj.CountryCode}', '{obj.StateCode}', '{obj.ImagePath}', '{obj.BirthDate.ToString("yyyyMMdd")}', '{obj.Gender}');";
                    MySqlCommand cmd = new MySqlCommand(script, conn);

                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    //var script = $"UPDATE `User` SET `Password` = '{obj.Password}', `Name`= '{obj.Name}', `LastName`= '{obj.LastName}', `Email`= '{obj.Email}', `CountryCode`= '{obj.CountryCode}', `StateCode`= '{obj.StateCode}', `ImagePath`= '{obj.ImagePath}', `BirthDate`='{obj.BirthDate.ToString("yyyyMMdd")}', `IsAdmin`='{Convert.ToInt32(obj.IsAdmin)}' WHERE `UserCode` = '{obj.UserCode}';";
                    var script = $"UPDATE `User` SET `Password` = '{obj.Password}', `Name`= '{obj.Name}', `LastName`= '{obj.LastName}', `Email`= '{obj.Email}', `CountryCode`= '{obj.CountryCode}', `StateCode`= '{obj.StateCode}', `ImagePath`= '{obj.ImagePath}', `BirthDate`='{obj.BirthDate.ToString("yyyyMMdd")}' WHERE `UserCode` = '{obj.UserCode}';";
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
                var script = $"DELETE FROM `user` WHERE `UserCode` = '{code}';";
                MySqlCommand cmd = new MySqlCommand(script, conn);

                cmd.ExecuteNonQuery();
            }

        }
    }
}
