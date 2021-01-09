using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace KetoWay.DataAccess
{
    public class KetoWayContext
    {

        public KetoWayContext()
        {
        }

        public IConfiguration Configuration { get; }
        public MySqlConnection GetConnection()
        {
            //var m = Configuration["DefaultConnection"];
            return new MySqlConnection("Server=127.0.0.1;Database=ketowaydb;Uid=root;Pwd=;");
        }
    }
}