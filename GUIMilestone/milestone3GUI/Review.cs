using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milestone3GUI
{
    class Review : postgreSQLConnString
    {
        public String review_Id { get; set; }
        public String user_id { get; set; }
        public String business_id { get; set; }
        public DateTime date { get; set; }
        public String text { get; set; }
        public int stars { get; set; }
        public int useful_vote { get; set; }
        public int funny_vote { get; set; }
        public int cool_vote { get; set; }
        List<Review> reviewList;
        User tempUser;

        public Review() { }

        

        public void RateBusiness() { }

        public void UpdateReview() { }

        public void WriteReview() { }

        /**
         * Description: Gets the reviews of a business and stores them in a list of reviews. 
         * Notes: I set the the user_id to the name of the user to show it to the review window, probably should create a reviewer_name
         *        variable to avoid user_id problems. 
         */ 
        public List<Review> GetReviews(String currentBusiness)
        {
            reviewList = new List<Review>();
            using (var conn = new NpgsqlConnection(getConnString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT date,stars,text,useful_vote,funny_vote,cool_vote,user_id FROM review WHERE business_id=" + "'" + currentBusiness + "';";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Sets the properties of the review object
                            tempUser = new User();
                            Review newReview = new Review();
                            newReview.date = reader.GetDateTime(0);
                            newReview.stars = reader.GetInt32(1);
                            newReview.text = reader.GetString(2);
                            newReview.useful_vote = reader.GetInt32(3);
                            newReview.funny_vote = reader.GetInt32(4);
                            newReview.cool_vote = reader.GetInt32(5);
                            //User values are set here. Might need to create a review property username.
                            tempUser.user_id = reader.GetString(6);
                            tempUser.GetUserInformation("username");
                            newReview.user_id = tempUser.name;
                            reviewList.Add(newReview);
                        }
                    }
                }
                conn.Close();
            }
            return reviewList;
        }

        public void InsertReview(Review newReview)
        {
            using (var conn = new NpgsqlConnection(getConnString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "insert into review(review_id,business_id,user_id,stars,date,text,useful_vote,funny_vote,cool_vote) values ('" + 
                        newReview.review_Id  + "', '"+ newReview.business_id + "', '" + newReview.user_id + "', " + newReview.stars + ", '" + newReview.date + "', '" + newReview.text + "', " + newReview.useful_vote +
                         ", " + newReview.funny_vote + ", " + newReview.cool_vote + ");";
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
