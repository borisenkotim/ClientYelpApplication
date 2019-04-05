using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milestone3GUI
{
    class User : postgreSQLConnString
    {
        public String user_id { get; set; }
        public String name { get; set; }
        public String yelping_since { get; set; }
        public double average_stars { get; set; }
        public double user_latitude { get; set; }
        public double user_longitude { get; set; }
        public int fans { get; set; }
        public int cool { get; set; } 
        public int funny { get; set; }
        public int useful { get; set; }
        public int review_count { get; set; }
        List<String> friends;
        List<String> users;

        public User() { }

        public void PostReviews() { }

        /**
         *  Notes: Probably not needed. Not really using it anywhere I think. 
         */
        public List<String> FriendsList() {
            friends = new List<String>();  
            return friends; 
        }

        /**
         *  Description: Gets all the users that match the name. 
         *  Return: Returns a list of those users that have the matched name. 
         */
        public List<String> GetUsers(String currUser)
        {
            users = new List<String>();
            using (var conn = new NpgsqlConnection(getConnString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT user_id FROM userinfo WHERE username=" + "'" + currUser + "';";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(reader.GetString(0));
                        }
                    }
                }
                conn.Close();
            }
            return users;
        }

        /**
         *  Description: Gets specific info of a user. The function takes in the type of 
         *               information the user wants updated or assigned. 
         */
        public void GetUserInformation(String info)
        {
            using (var conn = new NpgsqlConnection(getConnString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT " + info  + " FROM userinfo WHERE user_id=" + "'" + user_id + "';";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            switch(info)
                            {
                                case "user_id": this.user_id = reader.GetString(0);
                                break;
                                case "username": this.name = reader.GetString(0);
                                break;
                                case "yelping_since": this.yelping_since = reader.GetString(0);
                                break;
                                case "user_latitude": this.user_latitude = reader.GetDouble(0);
                                break;
                                case "user_longitude": this.user_longitude = reader.GetDouble(0);
                                break;
                                case "average_stars": this.average_stars = reader.GetDouble(0);
                                break;
                                case "fans": this.fans = reader.GetInt32(0);
                                break;
                                case "cool": this.cool = reader.GetInt32(0);
                                break;
                                case "funny": this.funny = reader.GetInt32(0);
                                break;
                                case "useful": this.useful = reader.GetInt32(0);
                                break;
                            }
                        }
                    }
                }
                conn.Close();
            }
        }

        /**
         *  Description: Sets the user information. Uses the current user_id that 
         *               was assigned wherever the call was made. This function
         *               populates all the information about the user. 
         *  Return: Returns the current user that is being filled out. 
         */
        public User SetUserInformation()
        {
            this.GetUserInformation("username");
            this.GetUserInformation("average_stars");
            this.GetUserInformation("fans");
            this.GetUserInformation("yelping_since");
            this.GetUserInformation("cool");
            this.GetUserInformation("funny");
            this.GetUserInformation("useful");
            this.GetUserInformation("user_latitude");
            this.GetUserInformation("user_longitude");
            return this;
        }

        /**
         *  Notes: Probably not needed since we still have access to the 
         *         current user regardless of what we do. 
         */
        public User GetThisCurrentUser()
        {
            return this;
        }


        public void UpdateUserLocation(User currUser)
        {
            using (var conn = new NpgsqlConnection(getConnString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "update userinfo set user_latitude = " + currUser.user_latitude
                        + ", user_longitude =" + currUser.user_longitude + "where user_id = '"
                        + currUser.user_id + "';";
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
