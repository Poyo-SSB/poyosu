using poyosu.Configuration;
using poyosu.Geometry;
using poyosu.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using SixLabors.Shapes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace poyosu.Builders
{
    public class ModBuilder : Builder
    {
        private struct ColorSet
        {
            public Rgba32 Dark;
            public Rgba32 Mid;
            public Rgba32 Light;

            public ColorSet(string dark, string mid, string light)
            {
                this.Dark = Rgba32.FromHex(dark);
                this.Mid = Rgba32.FromHex(mid);
                this.Light = Rgba32.FromHex(light);
            }
        }

        private static readonly ColorSet default_autoplay_color = new ColorSet("001d30", "52AFEA", "C0E5FE");
        private static readonly ColorSet default_cinema_color = new ColorSet("121212", "787878", "D3D3D3");
        private static readonly ColorSet default_doubletime_color = new ColorSet("28002e", "D14DE6", "F5BEFD");
        private static readonly ColorSet default_easy_color = new ColorSet("082c01", "68D44F", "C6FEB7");
        private static readonly ColorSet default_flashlight_color = new ColorSet("080808", "363636", "F9F9F9");
        private static readonly ColorSet default_halftime_color = new ColorSet("0d0b13", "64586E", "E7E1EF");
        private static readonly ColorSet default_hardrock_color = new ColorSet("300007", "E4596F", "FFC1CB");
        private static readonly ColorSet default_hidden_color = new ColorSet("2f1e01", "F6B443", "FEE7C0");
        private static readonly ColorSet default_keymod_color = new ColorSet("0c0f0f", "596264", "C7CDCD");
        private static readonly ColorSet default_nightcore_color = new ColorSet("12012e", "874DE5", "D6BDFD");
        private static readonly ColorSet default_nofail_color = new ColorSet("01092d", "566ED8", "BCC8FE");
        private static readonly ColorSet default_suddendeath_color = new ColorSet("2f1700", "F29B4B", "FEDEC1");
        private static readonly ColorSet default_random_color = new ColorSet("002910", "37D574", "B2FECE");
        private static readonly ColorSet default_relax_color = new ColorSet("002229", "3ABED5", "B2F2FE");
        private static readonly ColorSet default_relax2_color = new ColorSet("001029", "3B7ED4", "B3D3FE");
        private static readonly ColorSet default_scorev2_color = new ColorSet("0c0c0c", "565656", "EDEDED");
        private static readonly ColorSet default_spunout_color = new ColorSet("1e001b", "A22A96", "F4A6EC");
        private static readonly ColorSet default_target_color = new ColorSet("2b012b", "D847D6", "FFB7FD");
        private static readonly ColorSet default_daycore_color = new ColorSet("082727", "AB509C", "4FF2F3");
        private static readonly ColorSet default_nightmare_color = new ColorSet("260900", "FF3D06", "FFC0AE");

        private const int base_image_size = 256;

        private const int base_token_radius = 80;
        private const int base_icon_radius = 50;

        private const float base_token_glow_blur = 12f;
        private const float base_inner_spread_glow_blur = 18f;
        private const float base_outer_spread_glow_blur = 24f;
        private const float base_inner_glow_blur = 3f;
        private const float base_outer_glow_blur = 5f;

        private const float base_border_radius = 8;

        public override string Folder => "mods";
        public override string Name => "mod icons";

        public override async Task Generate(string path, Parameters parameters)
        {
            await Task.WhenAll(new List<Task>
            {
                this.CreateMod("autoplay", path, Assets.ImageIconCogs, 0.9f, default_autoplay_color, parameters.HD, -1, 0),
                this.CreateMod("cinema", path, Assets.ImageIconCinema, 0.85f, default_cinema_color, parameters.HD),
                this.CreateMod("doubletime", path, Assets.ImageIconDoubleTime, 0.9f, default_doubletime_color, parameters.HD),
                this.CreateMod("easy", path, Assets.ImageIconEasy, 0.9f, default_easy_color, parameters.HD),
                this.CreateMod("fadein", path, Assets.ImageIconFadeIn, 1.2f, default_hidden_color, parameters.HD),
                this.CreateMod("fadeout", path, Assets.ImageIconFadeOut, 1.2f, default_hidden_color, parameters.HD),
                this.CreateMod("flashlight", path, Assets.ImageIconFlashlight, 1, default_flashlight_color, parameters.HD),
                this.CreateMod("halftime", path, Assets.ImageIconHalfTime, 0.875f, default_halftime_color, parameters.HD),
                this.CreateMod("hardrock", path, Assets.ImageIconHardRock, 0.9f, default_hardrock_color, parameters.HD, -4, 1),
                this.CreateMod("hidden", path, Assets.ImageIconHidden, 0.9f, default_hidden_color, parameters.HD),
                this.CreateMod("key1", path, Assets.ImageIcon1K, 1.2f, default_keymod_color, parameters.HD),
                this.CreateMod("key2", path, Assets.ImageIcon2K, 1.2f, default_keymod_color, parameters.HD),
                this.CreateMod("key3", path, Assets.ImageIcon3K, 1.2f, default_keymod_color, parameters.HD),
                this.CreateMod("key4", path, Assets.ImageIcon4K, 1.2f, default_keymod_color, parameters.HD),
                this.CreateMod("key5", path, Assets.ImageIcon5K, 1.2f, default_keymod_color, parameters.HD),
                this.CreateMod("key6", path, Assets.ImageIcon6K, 1.2f, default_keymod_color, parameters.HD),
                this.CreateMod("key7", path, Assets.ImageIcon7K, 1.2f, default_keymod_color, parameters.HD),
                this.CreateMod("key8", path, Assets.ImageIcon8K, 1.2f, default_keymod_color, parameters.HD),
                this.CreateMod("key9", path, Assets.ImageIcon9K, 1.2f, default_keymod_color, parameters.HD),
                this.CreateMod("keycoop", path, Assets.ImageIconCoop, 1.2f, default_keymod_color, parameters.HD),
                this.CreateMod("nightcore", path, Assets.ImageIconNightcore, 0.8f, default_nightcore_color, parameters.HD),
                this.CreateMod("nofail", path, Assets.ImageIconNoFail, 0.9f, default_nofail_color, parameters.HD),
                this.CreateMod("perfect", path, Assets.ImageIconPerfect, 0.78f, default_suddendeath_color, parameters.HD),
                this.CreateMod("random", path, Assets.ImageIconRandom, 0.9f, default_random_color, parameters.HD),
                this.CreateMod("relax", path, Assets.ImageIconRelax, 0.9f, default_relax_color, parameters.HD, 3, 2),
                this.CreateMod("relax2", path, Assets.ImageIconAutopilot, 0.8f, default_relax2_color, parameters.HD, -2, 2),
                this.CreateMod("scorev2", path, Assets.ImageIconTachometer, 0.85f, default_scorev2_color, parameters.HD, 0, -1),
                this.CreateMod("spunout", path, Assets.ImageIconSpunOut, 0.9f, default_spunout_color, parameters.HD),
                this.CreateMod("suddendeath", path, Assets.ImageIconSuddenDeath, 0.96f, default_suddendeath_color, parameters.HD),
                this.CreateMod("target", path, Assets.ImageIconTargetPractice, 1, default_target_color, parameters.HD),

                this.CreateMod("touchdevice", path, Assets.ImageIconTouchDevice, 0.94f, default_scorev2_color, parameters.HD, 0, 6),

                // McOsu mods.
                this.CreateMod("vaporwave", path, Assets.ImageIconVaporwave, 0.875f, default_daycore_color, parameters.HD),
                this.CreateMod("nightmare", path, Assets.ImageIconLemon, 0.72f, default_nightmare_color, parameters.HD)
            });
        }

        private async Task CreateMod(string filename, string path, Image<Rgba32> icon, float sizeMultiplier, ColorSet colors, bool hd, int xOffset = 0, int yOffset = 0)
        {
            int imageSize = base_image_size;

            int tokenRadius = base_token_radius;
            int iconRadius = base_icon_radius;

            float tokenGlowBlur = base_token_glow_blur;
            float innerSpreadGlowBlur = base_inner_spread_glow_blur;
            float outerSpreadGlowBlur = base_outer_spread_glow_blur;
            float innerGlowBlur = base_inner_glow_blur;
            float outerGlowBlur = base_outer_glow_blur;

            float borderRadius = base_border_radius;

            if (!hd)
            {
                imageSize /= 2;

                tokenRadius /= 2;
                iconRadius /= 2;

                tokenGlowBlur /= 2;
                innerSpreadGlowBlur /= 2;
                outerSpreadGlowBlur /= 2;
                innerGlowBlur /= 2;
                outerGlowBlur /= 2;

                borderRadius /= 2;

                xOffset /= 2;
                yOffset /= 2;
            }

            using (var mod = new Image<Rgba32>(imageSize, imageSize))
            {
                var center = new PointF(imageSize / 2, imageSize / 2);
                var hexagon = RegularPolygonGenerator.Generate(6, tokenRadius, 0, center);
                IPath roundedHexagon = PolygonRounder.Round(hexagon, borderRadius, 32);

                icon.Mutate(ctx => ctx
                    .Resize((int)(2 * iconRadius * sizeMultiplier), (int)(2 * iconRadius * sizeMultiplier))
                    .Pad(imageSize, imageSize));

                using (var tokenGlow = new Image<Rgba32>(imageSize, imageSize))
                {
                    tokenGlow.Mutate(ctx => ctx
                        .Fill(colors.Mid, roundedHexagon)
                        .GaussianBlur(tokenGlowBlur));

                    mod.Mutate(ctx => ctx.DrawImage(tokenGlow));
                }

                using (var token = new Image<Rgba32>(imageSize, imageSize))
                {
                    token.Mutate(ctx => ctx
                        .Fill(colors.Dark, roundedHexagon));

                    mod.Mutate(ctx => ctx.DrawImage(token));
                }

                using (var tokenContents = new Image<Rgba32>(imageSize, imageSize))
                {
                    using (var iconOuterSpread = new Image<Rgba32>(imageSize, imageSize))
                    {
                        iconOuterSpread.Mutate(ctx => ctx
                            .DrawImage(icon, new Point(xOffset, yOffset))
                            .SetColor(colors.Mid)
                            .GaussianBlur(outerSpreadGlowBlur));

                        tokenContents.Mutate(ctx => ctx.DrawImage(iconOuterSpread));
                    }
                    using (var iconInnerSpread = new Image<Rgba32>(imageSize, imageSize))
                    {
                        iconInnerSpread.Mutate(ctx => ctx
                            .DrawImage(icon, new Point(xOffset, yOffset))
                            .SetColor(colors.Mid)
                            .GaussianBlur(innerSpreadGlowBlur));

                        tokenContents.Mutate(ctx => ctx.DrawImage(iconInnerSpread));
                    }
                    using (var iconOuterGlow = new Image<Rgba32>(imageSize, imageSize))
                    {
                        iconOuterGlow.Mutate(ctx => ctx
                            .DrawImage(icon, new Point(xOffset, yOffset))
                            .SetColor(colors.Mid)
                            .GaussianBlur(outerGlowBlur));

                        tokenContents.Mutate(ctx => ctx.DrawImage(iconOuterGlow));
                    }
                    using (var iconInnerGlow = new Image<Rgba32>(imageSize, imageSize))
                    {
                        iconInnerGlow.Mutate(ctx => ctx
                            .DrawImage(icon, new Point(xOffset, yOffset))
                            .SetColor(colors.Light)
                            .GaussianBlur(innerGlowBlur));

                        tokenContents.Mutate(ctx => ctx.DrawImage(iconInnerGlow));
                    }

                    tokenContents.Mutate(ctx => ctx.DrawImage(icon, new Point(xOffset, yOffset)));

                    var graphicOptions = new GraphicsOptions(true)
                    {
                        AlphaCompositionMode = PixelAlphaCompositionMode.DestOut
                    };

                    tokenContents.Mutate(x => x.Fill(graphicOptions, Rgba32.LimeGreen, new RectangularPolygon(0, 0, imageSize, imageSize).Clip(roundedHexagon)));

                    mod.Mutate(ctx => ctx.DrawImage(tokenContents));
                }

                if (hd)
                {
                    mod.SaveToFileAsPng(System.IO.Path.Combine(path, $"selection-mod-{filename}@2x.png"));
                }
                else
                {
                    mod.SaveToFileAsPng(System.IO.Path.Combine(path, $"selection-mod-{filename}.png"));
                }

                await Task.CompletedTask;
            }
        }
    }
}
