using System;
using System.Globalization;

namespace Harvest.Net.Utils
{
    public class TimeOfDay
    {
        private readonly DateTime _time;
        private readonly CultureInfo _usCulture;

        public TimeOfDay(string time) : this()
        {
            var dateTime = DateTime.Parse(time);
            _time = dateTime;
        }

        public TimeOfDay(DateTime time) : this()
        {
            _time = time;
        }

        private TimeOfDay()
        {
            _usCulture = CultureInfo.CreateSpecificCulture("en-US");
        }

        public override string ToString()
        {
            var jsonTimeString = _time.ToString("h:mmtt", _usCulture); // It will give "3:00AM"
            return jsonTimeString;
        }
    }
}