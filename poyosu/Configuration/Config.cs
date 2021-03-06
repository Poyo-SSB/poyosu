﻿using Newtonsoft.Json;
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

        [JsonProperty("slider_end_enabled")]
        public bool SliderEndEnabled;

        [JsonProperty("judgement_length")]
        public float JudgementLength;
        [JsonProperty("judgement_300_enabled")]
        public bool Judgement300Enabled;

        [JsonProperty("parameters")]
        public JObject Parameters;
    }
}