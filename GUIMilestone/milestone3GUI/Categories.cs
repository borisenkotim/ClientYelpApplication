using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milestone3GUI
{
    class Categories : postgreSQLConnString
    {
        public List<String> categoryList;

        public Categories() { }

        /**
         *  Description: Gets all the categories that are linked to a business id. When a business id 
         *               is not specified, it will make a query on all the businesses and return all
         *               the categories that the businesses in the database have. 
         *  Return: String of categories. 
         */
        public List<String> GetCategories(String business_id)
        {
            categoryList = new List<String>();
            using (var conn = new NpgsqlConnection(getConnString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    if (business_id.Length > 0)
                        cmd.CommandText = "SELECT distinct category_name FROM categories WHERE business_Id = '" + business_id +"' order by category_name;";
                    else 
                        cmd.CommandText = "SELECT distinct category_name FROM categories order by category_name;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categoryList.Add(reader.GetString(0));
                        }
                    }
                }
                conn.Close();
            }
            return categoryList;
        }
        
    }
}
