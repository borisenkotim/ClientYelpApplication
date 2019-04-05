using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milestone3GUI
{
    
    class Friends : postgreSQLConnString
    {
        public String user_id { get; set; }
        public String friend_id { get; set; }
        public List<String> friends_ids = new List<String>();

        public Friends() { }
        /**
         *  Description: Gets the list of friends that a user has. 
         *  Return: Returns a list of friend_ids, which are the 
         *          ids of the friends of the current user. 
         */
        public List<String> GetListOfFriends(String currUser)
        {
            Friends newUser = new Friends();
            using (var conn = new NpgsqlConnection(getConnString()))
            {
                conn.Open(); 
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT friend_id FROM friends WHERE user_id=" + "'" + currUser + "';";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            friends_ids.Add(reader.GetString(0));
                        }
                    }
                }
                conn.Close();
            }
            return friends_ids;
        }

        /**
         *  Description: Gets a specific user that matches the user_id. 
         *  Return: Returns a user. 
         */
        public User GetFriend(String user)
        {
            User newFriend = new User();
            using (var conn = new NpgsqlConnection(getConnString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT username,average_stars,yelping_since FROM userinfo WHERE user_id=" + "'" + user + "';";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            newFriend.user_id = user;
                            newFriend.name = reader.GetString(0);
                            newFriend.average_stars = Math.Round(reader.GetDouble(1), 0);
                            newFriend.yelping_since = reader.GetString(2);
                        }
                    }
                }
                conn.Close();
            }
            return newFriend;
        }
    }
}

