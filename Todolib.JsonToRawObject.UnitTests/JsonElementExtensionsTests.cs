using System.Text.Json;

namespace Todolib.JsonToRawObject.UnitTests
{
    public class JsonElementExtensionsTests
    {
        [Theory]
        [InlineData("null")]
        [InlineData("123.456")]
        [InlineData("\"hi\"")]

        [InlineData("[]")]
        [InlineData("[123,456]")]
        [InlineData("{}")]
        [InlineData("{\"property1\":123,\"property2\":\"hi\"}")]
        [InlineData("{\"property1\":123,\"property2\":[123,true,false]}")]
        [InlineData("{\"property1\":123,\"property2\":[123,true,{\"property1\":123,\"property2\":[123,true,false]}]}")]
        public void Test(string json)
        {
            var parsed = JsonSerializer.Deserialize<JsonElement>(json);
            var rawObject = parsed.ToObject();

            var backToJson = JsonSerializer.Serialize(rawObject);

            Assert.Equal(json, backToJson);
        }
    }
}