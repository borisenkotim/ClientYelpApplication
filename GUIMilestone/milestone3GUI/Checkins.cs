using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milestone3GUI
{
    class Checkins : postgreSQLConnString
    {
        public DateTime day { get; set; }
        public DateTime time { get; set; }
        public String business_id { get; set; }
        public int count { get; set; }
        List<Checkins> checkinList;

        public Checkins() { }

        public void SetTotalCheckins(Business tempBusiness)
        {
            using (var conn = new NpgsqlConnection(getConnString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE  business SET num_checkins =(SELECT SUM(count) AS num_checkins FROM checkins WHERE business_id = '"+ tempBusiness.business_Id + "');";
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public int GetTotalCheckins(String currBusiness)
        {
            int total = 0;
            using (var conn = new NpgsqlConnection(getConnString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Select num_checkins FROM business WHERE business_id = '" + currBusiness + "';";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            total = reader.GetInt32(0);
                        }
                    }
                }
                conn.Close();
            }
            return total;
        }

        public List<Checkins> GetCheckins(String currentBusiness)
        {
            using (var conn = new NpgsqlConnection(getConnString()))
            {
                checkinList = new List<Checkins>();
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT day,time,count FROM checkins WHERE business_id = '"+ currentBusiness+ "';";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Checkins newCheckin = new Checkins();
                            newCheckin.day = reader.GetDateTime(0);
                            newCheckin.time = reader.GetDateTime(1);
                            newCheckin.count = reader.GetInt32(2);
                            newCheckin.business_id = currentBusiness;
                            checkinList.Add(newCheckin);
                        }
                    }
                }
                conn.Close();
            }
            return checkinList;
        }

        public void UpdateCheckins(Checkins newCheckin)
        {
            using (var conn = new NpgsqlConnection(getConnString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Insert into checkins(day,time,count,business_id) values('" + newCheckin.day + "','" + newCheckin.time + "'," + newCheckin.count + ",'" + newCheckin.business_id + "');";
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
