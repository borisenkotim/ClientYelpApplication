using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milestone3GUI
{
    abstract class Distance
    {
        private double miles;
        private double kilometers;
        private double RADIUS = 3958.8;

        /**
         * formula from: https://andrew.hedges.name/experiments/haversine/
         */

        public double GetDistance(double alat, double along, double blat, double blong)
        {
            double dlon = blong - along;
            double dlat = blat - alat;
            double a = Math.Pow(Math.Sin(dlat / 2), 2) + Math.Cos(alat) * Math.Cos(blat) * Math.Pow(Math.Sin(dlon / 2), 2);
            //note sure about this 
            double c = 2 * a * Math.Tan(Math.Sqrt(a)) * Math.Tan(Math.Sqrt(1-a)); 
            double distance = 0.0;
            distance = RADIUS * c;
            return distance; 
        }
    }
}
