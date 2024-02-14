using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Todolib.JsonToRawObject
{
    public static class JsonElementExtensions
    {
        public static object ToObject(this JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Undefined: return null;
                case JsonValueKind.Null: return null;
                case JsonValueKind.True: return true;
                case JsonValueKind.False: return false;
                case JsonValueKind.String: return element.GetString();
                case JsonValueKind.Number: return element.GetDouble(); // https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Number
                case JsonValueKind.Object: return ReadObject(element);
                case JsonValueKind.Array: return ReadArray(element);

                default: throw new ArgumentException($"Unsupported {nameof(JsonElement.ValueKind)} = \"{element.ValueKind}\"");
            }
        }

        private static Dictionary<string, object> ReadObject(JsonElement element)
        {
            var result = new Dictionary<string, object>(); // TODO - does JsonElement have props count to allocate the dictionary properly?

            foreach (var property in element.EnumerateObject())
                result[property.Name] = property.Value.ToObject();

            return result;
        }

        private static List<object> ReadArray(JsonElement element)
        {
            var result = new List<object>(element.GetArrayLength());

            foreach (var item in element.EnumerateArray())
                result.Add(item.ToObject());

            return result;
        }
    }
}
