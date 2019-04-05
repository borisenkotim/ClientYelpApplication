using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milestone3GUI
{
    class Hours : postgreSQLConnString
    {
        public DateTime day { get; set; }
        public DateTime closed { get; set; }
        public DateTime open { get; set; } 
        public String business_id { get; set; }

        public Hours() { }

        public Hours GetBusinessHours(String currBusiness, DateTime currDay)
        {
            Hours newHours = new Hours();
            using (var conn = new NpgsqlConnection(getConnString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT open,closed FROM hours WHERE business_id=" + "'" + currBusiness + "'and day = '"+ currDay + "';";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            newHours.day = currDay;
                            newHours.open = reader.GetDateTime(0);
                            newHours.closed = reader.GetDateTime(1);
                            newHours.business_id = currBusiness;
                        }
                    }
                }
            }
            return newHours;
        }
    }
}
