using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace ProfitWell.Helpers
{
    public class PriceConverter : JsonConverter
    {
        //public override object ReadJson(JsonReader reader, Type objectType, JsonSerializer serializer)
        //{
        //    if (reader.TokenType != JsonToken.Integer)
        //        throw new Exception("Wrong Token Type");

        //    long ticks = (long)reader.Value;
        //    return ticks.FromUnixTime();
        //}

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param><param name="value">The value.</param><param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            int val;
            if (value is decimal)
            {
                val = (int)((decimal)value*100);
            }
            else
            {
                throw new Exception("Expected object is decimal.");
            }
            writer.WriteValue(val);
        }

        /// <summary>
        ///   Reads the JSON representation of the object.
        /// </summary>
        /// <param name = "reader">The <see cref = "JsonReader" /> to read from.</param>
        /// <param name = "objectType">Type of the object.</param>
        /// <param name = "existingValue">The existing value of object being read.</param>
        /// <param name = "serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer)
                throw new Exception("Wrong Token Type");

            return Convert.ToDecimal(reader.Value)/100;

        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(decimal)){
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
