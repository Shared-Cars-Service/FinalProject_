using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class PriceDetermination
    {
        public static double Avg_price_of_taxi_fare_for_km()
        {
            //תעריף א
            if ((DateTime.Now.Day > 0 && DateTime.Now.Day < 6 && DateTime.Now.Hour > 5 && DateTime.Now.Hour < 22) ||
                (DateTime.Now.Day == 6 && DateTime.Now.Hour > 5 && DateTime.Now.Hour < 17))
            {
                return 1.86;
            }
            //תעריף ב
            else if (((DateTime.Now.Day > 0 && DateTime.Now.Day < 5) && (DateTime.Now.Hour > 21 || DateTime.Now.Hour < 6)) ||
                (DateTime.Now.Day == 5 && DateTime.Now.Hour > 9 && DateTime.Now.Hour < 23) ||
                (DateTime.Now.Day == 6 && DateTime.Now.Hour > 15 && DateTime.Now.Hour < 19))
            {
                return 2.22;
            }
            //תעריף ג
            return 2.26;
        }
        public static double Initial_state_of_counter()
        {
            return 11.85;
        }
        public static double Price_per_day()
        {
            if (DateTime.Now.Month == 7 || DateTime.Now.Month == 8)
                return 150.0;
            else
                return 105.0;
        }
        public static double Normal_wolking_distance_in_km()
        {
            return 0.9;
        }
        public static double Discount()
        {
            return 0.2;
        }
    }
}
