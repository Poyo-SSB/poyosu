using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace poyosu.Configuration
{
    [JsonObject(ItemRequired = Required.AllowNull)]
    public struct Config
    {
        [JsonProperty("folder")]
        public string Folder;

        [JsonProperty("hd")]
        public bool HD;
        [JsonProperty("animation_framerate")]
        public int AnimationFramerate;

        [JsonProperty("scorebar_name")]
        public string ScorebarName;
        [JsonProperty("scorebar_flag")]
        public string ScorebarFlag;

        [JsonProperty("followpoint_width")]
        public float FollowpointWidth;

        [JsonProperty("parameters")]
        public JObject Parameters;
    }
}