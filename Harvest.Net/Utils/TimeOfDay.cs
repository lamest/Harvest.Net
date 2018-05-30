using System;
using System.Collections.Generic;
using System.Text;

namespace Harvest.Net.Utils
{
    public class TimeOfDay
    {
        private TimeSpan 

        public TimeOfDay(string time)
        {

            
        }

        public override string ToString()
        {
            var jsonTimeString = time.ToString("hh:mmtt", _usCulture); // It will give "03:00AM"
            writer.WriteValue(jsonTimeString);
        }
    }
}
