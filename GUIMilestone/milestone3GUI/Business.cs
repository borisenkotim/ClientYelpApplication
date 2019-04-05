using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milestone3GUI
{
    class Business : postgreSQLConnString
    {
        public String business_Id { get; set; }
        public String name { get; set; }
        public String city { get; set; }
        public String state { get; set; }
        public String zipcode { get; set; }
        public String address { get; set; }
        public double latitute { get; set; }
        public double longtitude { get; set; }
        public double stars { get; set; }
        public double review_rating { get; set; }
        public int review_count { get; set; }
        public int num_checkins { get; set; }
        public Boolean is_open { get; set; }
        List<String> locationList;
        List<Business> businessList;

        public Business() { }


        public void AccessBusinessReviews() { }

        /**
         * Description: Gets all the distinct states,cities or zipcodes that match the
         *              query. The function also accepts an order by if needed. 
         *  Return: Returns a list of locations. IE list of (state or city or zip)
         */ 
        public List<String> GetLocation(String selectFrom, String where, String orderBy)
        {

            locationList = new List<String>();
            using (var conn = new NpgsqlConnection(getConnString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT distinct " + selectFrom + " FROM business" + where + "ORDER BY " + orderBy + ";";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            locationList.Add(reader.GetString(0));
                        }
                    }
                }
                conn.Close();
            }
            return locationList;
        }

        /**
         * Description: Gets a list of businesses that matches a very specified query that include a location and 
         *              might include certain categories and attributes. 
         * Return: Returns a list of businesses that match all the specified queries. 
         * Notes: List of attributes has not been implemented yet. Might need to add an extra command text for when attributes filled out.
         *        There might be a better or cleaner way to do this. 
         */
        public List<Business> GetBusinesses(String stateItem, String cityItem, String zipcodeItem, String orderBy, List<String> categories, List<String> attributes)
        {
            businessList = new List<Business>();
            using (var conn = new NpgsqlConnection(getConnString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "";
                    //when the user has added categories to refine search 
                    if (categories.Count > 0)
                    {
                        String filter = "";
                        String addCategory = " category_name = '";
                        for(int i = 0; i < categories.Count; i++)
                        {
                            if(i == 0)
                            {
                                filter += " and" + addCategory + categories.ElementAt(i) + "'";
                            }
                            else
                            {
                                filter += " or" + addCategory + categories.ElementAt(i) + "'";
                            }
                        }
                        cmd.CommandText = "select name,address,city,state,stars,review_count,review_rating,num_checkins,B.business_id"
                        + " FROM business as B inner join categories as C"
                        + " on B.business_id = C.business_id"
                        + " where state = '" + stateItem + "' and city = '" + cityItem
                        + "' and zipcode = '" + zipcodeItem + "'" + filter + " order by " + orderBy + ";";
                    }
                    //when no categories are selected 
                    else
                    {
                        cmd.CommandText = "select name,address,city,state,stars,review_count,review_rating,num_checkins,business_id FROM business " +
                        "where state = '" + stateItem + "' and city = '" + cityItem
                        + "' and zipcode = '" + zipcodeItem + "' order by " + orderBy + ";";
                    }
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //create new business, then add to business list.
                            Business newBusiness = new Business();
                            newBusiness.name = reader.GetString(0);
                            newBusiness.address = reader.GetString(1);
                            newBusiness.city = reader.GetString(2);
                            newBusiness.state = reader.GetString(3);
                            newBusiness.stars = reader.GetDouble(4);
                            newBusiness.review_count = reader.GetInt32(5);
                            newBusiness.review_rating = reader.GetDouble(6);
                            newBusiness.num_checkins = reader.GetInt32(7);
                            newBusiness.business_Id = reader.GetString(8);
                            businessList.Add(newBusiness);
                        }
                    }
                }
            }
            return businessList;
        }
    }
}
