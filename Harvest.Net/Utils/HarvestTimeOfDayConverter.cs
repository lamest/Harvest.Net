using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Harvest.Net.Utils
{
    public class HarvestTimeOfDayConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is TimeSpan time)
            {
                var dateTime = DateTime.MinValue.Add(time);
                var jsonTimeString = time.ToString("hh:mmtt"); // It will give "03:00 AM"
                writer.WriteValue(jsonTimeString);
            }
            else
            {
                throw new Exception("Unsupported type:" + value?.GetType()?.Name);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            if (reader.TokenType == JsonToken.String)
            {
                var text = reader.Value as string;
                if (string.IsNullOrWhiteSpace(text))
                    return null;

                try
                {
                    var dateTime = DateTime.Parse(text);
                    var span = dateTime.TimeOfDay;
                    return span;
                }
                catch (Exception ex)
                {
                }

                throw new JsonReaderException($"Failed to parse as {typeof(TimeSpan).Name}:{text}");
            }

            throw new JsonReaderException($"Unexcepted token {reader.TokenType}, expected {JsonToken.Null} or {JsonToken.String}");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeSpan?) || objectType == typeof(TimeSpan);
        }
    }
}