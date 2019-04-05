using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milestone3GUI
{
    /**
     * Use this whole class for queries to the database.
     * Do it with strings. Take in the Object of a certain class.
     * Return Object. Have one that returns a list.
     * Take in the projection, selection, and condition, orderby, groupby
     * 
     * 
     */  
    abstract class postgreSQLConnString
    {
        public String getConnString()
        {
            return "Host=localhost; Username=postgres; Password=password; Database=milestone1db; Port = 5433";
        }
    }
}
