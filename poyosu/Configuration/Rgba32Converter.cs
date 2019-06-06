using System;
using Newtonsoft.Json;
using SixLabors.ImageSharp.PixelFormats;

namespace poyosu.Configuration
{
    public class Rgba32Converter : JsonConverter<Rgba32>
    {
        public override Rgba32 ReadJson(JsonReader reader, Type objectType, Rgba32 existingValue, bool hasExistingValue, JsonSerializer serializer)
            => Rgba32.FromHex((string)reader.Value);

        public override void WriteJson(JsonWriter writer, Rgba32 value, JsonSerializer serializer)
            => writer.WriteValue($"#{value.R.ToString("X2")}{value.G.ToString("X2")}{value.B.ToString("X2")}{value.A.ToString("X2")}");
    }
}