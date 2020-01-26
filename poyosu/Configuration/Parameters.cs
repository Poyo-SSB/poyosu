using Newtonsoft.Json;
using SixLabors.ImageSharp.PixelFormats;
using System;

namespace poyosu.Configuration
{
    public class Parameters
    {
        [JsonProperty("hd")]
        public bool HD { get; private set; } = true;

        [JsonProperty("animation_enabled")]
        public bool AnimationEnabled { get; private set; } = true;
        [JsonProperty("animation_framerate")]
        public int AnimationFramerate { get; private set; } = 60;

        [JsonProperty("combo_colors")]
        public Rgba32[] ComboColors { get; private set; } = {
            Rgba32.FromHex("FF969F"),
            Rgba32.FromHex("FFD387"),
            Rgba32.FromHex("FFFE9A"),
            Rgba32.FromHex("AAFF92"),
            Rgba32.FromHex("86FFD3"),
            Rgba32.FromHex("87E0FF"),
            Rgba32.FromHex("92ADFF"),
            Rgba32.FromHex("EF98F5"),
        };

        [JsonProperty("cursor_color")]
        public Rgba32 CursorColor { get; private set; } = Rgba32.FromHex("FF7FB3");
        [JsonProperty("cursor_radius")]
        public float CursorRadius { get; private set; } = 26;

        [JsonProperty("cursor_trail_enabled")]
        public bool CursorTrailEnabled { get; private set; } = true;
        [JsonProperty("cursor_trail_color")]
        public Rgba32 CursorTrailColor { get; private set; } = Rgba32.FromHex("FF7FB388");
        [JsonProperty("cursor_trail_radius")]
        public float CursorTrailRadius { get; private set; } = 13;
        [JsonProperty("cursor_trail_smooth")]
        public bool CursorTrailSmooth { get; private set; } = true;

        [JsonProperty("scorebar_label_enabled")]
        public bool ScorebarLabelEnabled { get; private set; } = true;
        [JsonProperty("scorebar_label_name")]
        public string ScorebarLabelName { get; private set; } = "Poyo";
        [JsonProperty("scorebar_label_flag_enabled")]
        public bool ScorebarLabelFlagEnabled { get; private set; } = true;
        [JsonProperty("scorebar_label_flag")]
        public string ScorebarLabelFlag { get; private set; } = "US";

        [JsonProperty("followpoint_enabled")]
        public bool FollowpointEnabled { get; private set; } = true;
        [JsonProperty("followpoint_width")]
        public float FollowpointWidth { get; private set; } = 9;

        [JsonProperty("slider_end_enabled")]
        public bool SliderEndEnabled { get; internal set; } = false;

        [JsonProperty("judgement_length")]
        public float JudgementLength { get; internal set; } = 1;

        public void Populate(Config config)
        {
            // This is run before values are populated from JSON.

            this.HD = config.HD;

            this.AnimationFramerate = config.AnimationFramerate;
            this.AnimationEnabled = this.AnimationFramerate > 0;

            this.ScorebarLabelName = config.ScorebarName;
            this.ScorebarLabelFlag = config.ScorebarFlag;

            if (String.IsNullOrWhiteSpace(this.ScorebarLabelName))
            {
                this.ScorebarLabelEnabled = false;
            }
            if (String.IsNullOrWhiteSpace(this.ScorebarLabelFlag))
            {
                this.ScorebarLabelFlagEnabled = false;
            }

            this.FollowpointWidth = config.FollowpointWidth;
            this.FollowpointEnabled = this.FollowpointWidth > 0;

            this.SliderEndEnabled = config.SliderEndEnabled;

            this.JudgementLength = config.JudgementLength;
        }
    }
}