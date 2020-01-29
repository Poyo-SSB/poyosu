using poyosu.Configuration;
using poyosu.Geometry;
using poyosu.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Collections.Generic;
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

        private static readonly ColorSet color_autoplay = new ColorSet("001D30", "52AFEA", "C0E5FE");
        private static readonly ColorSet color_cinema = new ColorSet("121212", "787878", "D3D3D3");
        private static readonly ColorSet color_doubletime = new ColorSet("28002E", "D14DE6", "F5BEFD");
        private static readonly ColorSet color_easy = new ColorSet("082C01", "68D44F", "C6FEB7");
        private static readonly ColorSet color_flashlight = new ColorSet("080808", "363636", "F9F9F9");
        private static readonly ColorSet color_halftime = new ColorSet("0D0B13", "64586E", "E7E1EF");
        private static readonly ColorSet color_hardrock = new ColorSet("300007", "E4596F", "FFC1CB");
        private static readonly ColorSet color_hidden = new ColorSet("2F1E01", "F6B443", "FEE7C0");
        private static readonly ColorSet color_keymod = new ColorSet("0C0F0F", "596264", "C7CDCD");
        private static readonly ColorSet color_nightcore = new ColorSet("12012E", "874DE5", "D6BDFD");
        private static readonly ColorSet color_nofail = new ColorSet("01092D", "566ED8", "BCC8FE");
        private static readonly ColorSet color_suddendeath = new ColorSet("2F1700", "F29B4B", "FEDEC1");
        private static readonly ColorSet color_random = new ColorSet("002910", "37D574", "B2FECE");
        private static readonly ColorSet color_mirror = new ColorSet("002910", "068A19", "95F0A2");
        private static readonly ColorSet color_relax = new ColorSet("002229", "3ABED5", "B2F2FE");
        private static readonly ColorSet color_relax2 = new ColorSet("001029", "3B7ED4", "B3D3FE");
        private static readonly ColorSet color_scorev2 = new ColorSet("0C0C0C", "565656", "EDEDED");
        private static readonly ColorSet color_spunout = new ColorSet("1E001B", "A22A96", "F4A6EC");
        private static readonly ColorSet color_target = new ColorSet("2B012B", "D847D6", "FFB7FD");
        private static readonly ColorSet color_daycore = new ColorSet("082727", "AB509C", "4FF2F3");
        private static readonly ColorSet color_nightmare = new ColorSet("260900", "FF3D06", "FFC0AE");

        private const int image_size = 256;

        private const int token_radius = 80;
        private const int icon_radius = 50;

        private const float token_glow_blur = 12f;
        private const float inner_spread_glow_blur = 18f;
        private const float outer_spread_glow_blur = 24f;
        private const float inner_glow_blur = 3f;
        private const float outer_glow_blur = 5f;

        private const float border_radius = 8;

        public override string Folder => "mods";
        public override string Name => "mod icons";

        public override async Task Generate(string path, Parameters parameters)
        {
            await Task.WhenAll(new List<Task>
            {
                this.CreateMod(path, parameters, "autoplay", Assets.ImageIconCogs, 0.9f, color_autoplay, -1, 0),
                this.CreateMod(path, parameters, "cinema", Assets.ImageIconCinema, 0.85f, color_cinema),
                this.CreateMod(path, parameters, "doubletime", Assets.ImageIconDoubleTime, 0.9f, color_doubletime),
                this.CreateMod(path, parameters, "easy", Assets.ImageIconEasy, 0.9f, color_easy),
                this.CreateMod(path, parameters, "fadein", Assets.ImageIconFadeIn, 1.2f, color_hidden),
                this.CreateMod(path, parameters, "fadeout", Assets.ImageIconFadeOut, 1.2f, color_hidden),
                this.CreateMod(path, parameters, "flashlight", Assets.ImageIconFlashlight, 0.90f, color_flashlight, -5, 0),
                this.CreateMod(path, parameters, "halftime", Assets.ImageIconHalfTime, 0.875f, color_halftime),
                this.CreateMod(path, parameters, "hardrock", Assets.ImageIconHardRock, 0.9f, color_hardrock, -4, 1),
                this.CreateMod(path, parameters, "hidden", Assets.ImageIconHidden, 0.9f, color_hidden),
                this.CreateMod(path, parameters, "key1", Assets.ImageIcon1K, 1.2f, color_keymod),
                this.CreateMod(path, parameters, "key2", Assets.ImageIcon2K, 1.2f, color_keymod),
                this.CreateMod(path, parameters, "key3", Assets.ImageIcon3K, 1.2f, color_keymod),
                this.CreateMod(path, parameters, "key4", Assets.ImageIcon4K, 1.2f, color_keymod),
                this.CreateMod(path, parameters, "key5", Assets.ImageIcon5K, 1.2f, color_keymod),
                this.CreateMod(path, parameters, "key6", Assets.ImageIcon6K, 1.2f, color_keymod),
                this.CreateMod(path, parameters, "key7", Assets.ImageIcon7K, 1.2f, color_keymod),
                this.CreateMod(path, parameters, "key8", Assets.ImageIcon8K, 1.2f, color_keymod),
                this.CreateMod(path, parameters, "key9", Assets.ImageIcon9K, 1.2f, color_keymod),
                this.CreateMod(path, parameters, "keycoop", Assets.ImageIconCoop, 1.2f, color_keymod),
                this.CreateMod(path, parameters, "nightcore", Assets.ImageIconNightcore, 0.8f, color_nightcore),
                this.CreateMod(path, parameters, "nofail", Assets.ImageIconNoFail, 0.9f, color_nofail),
                this.CreateMod(path, parameters, "perfect", Assets.ImageIconPerfect, 0.78f, color_suddendeath),
                this.CreateMod(path, parameters, "mirror", Assets.ImageIconMirror, 1.2f, color_mirror),
                this.CreateMod(path, parameters, "random", Assets.ImageIconRandom, 0.9f, color_random),
                this.CreateMod(path, parameters, "relax", Assets.ImageIconRelax, 0.9f, color_relax, 3, 2),
                this.CreateMod(path, parameters, "relax2", Assets.ImageIconAutopilot, 0.8f, color_relax2, -2, 2),
                this.CreateMod(path, parameters, "scorev2", Assets.ImageIconTachometer, 0.85f, color_scorev2, 0, -1),
                this.CreateMod(path, parameters, "spunout", Assets.ImageIconSpunOut, 0.9f, color_spunout),
                this.CreateMod(path, parameters, "suddendeath", Assets.ImageIconSuddenDeath, 0.96f, color_suddendeath),
                this.CreateMod(path, parameters, "target", Assets.ImageIconTargetPractice, 1, color_target),

                this.CreateMod(path, parameters, "touchdevice", Assets.ImageIconTouchDevice, 0.94f, color_scorev2, 0, 6),

                // McOsu mods

                this.CreateMod(path, parameters, "vaporwave", Assets.ImageIconVaporwave, 0.875f, color_daycore),
                this.CreateMod(path, parameters, "nightmare", Assets.ImageIconLemon, 0.72f, color_nightmare)
            });
        }

        private async Task CreateMod(string path, Parameters parameters, string filename, Image<Rgba32> icon, float sizeMultiplier, ColorSet colors, int xOffset = 0, int yOffset = 0)
        {
            using var mod = new Image<Rgba32>(image_size, image_size);

            var center = new PointF(image_size / 2, image_size / 2);
            var hexagon = RegularPolygonGenerator.Generate(6, token_radius, 0, center);
            IPath roundedHexagon = PolygonRounder.Round(hexagon, border_radius, 32);

            icon.Mutate(ctx => ctx
                .Resize((int)(2 * icon_radius * sizeMultiplier), (int)(2 * icon_radius * sizeMultiplier))
                .Pad(image_size, image_size));

            using (var tokenGlow = new Image<Rgba32>(image_size, image_size))
            {
                tokenGlow.Mutate(ctx => ctx
                    .Fill(colors.Mid, roundedHexagon)
                    .GaussianBlur(token_glow_blur));

                mod.Mutate(ctx => ctx.DrawImage(tokenGlow));
            }

            using (var token = new Image<Rgba32>(image_size, image_size))
            {
                token.Mutate(ctx => ctx
                    .Fill(colors.Dark, roundedHexagon));

                mod.Mutate(ctx => ctx.DrawImage(token));
            }

            using (var tokenContents = new Image<Rgba32>(image_size, image_size))
            {
                using (var iconOuterSpread = new Image<Rgba32>(image_size, image_size))
                {
                    iconOuterSpread.Mutate(ctx => ctx
                        .DrawImage(icon, new Point(xOffset, yOffset))
                        .SetColor(colors.Mid)
                        .GaussianBlur(outer_spread_glow_blur));

                    tokenContents.Mutate(ctx => ctx.DrawImage(iconOuterSpread));
                }
                using (var iconInnerSpread = new Image<Rgba32>(image_size, image_size))
                {
                    iconInnerSpread.Mutate(ctx => ctx
                        .DrawImage(icon, new Point(xOffset, yOffset))
                        .SetColor(colors.Mid)
                        .GaussianBlur(inner_spread_glow_blur));

                    tokenContents.Mutate(ctx => ctx.DrawImage(iconInnerSpread));
                }
                using (var iconOuterGlow = new Image<Rgba32>(image_size, image_size))
                {
                    iconOuterGlow.Mutate(ctx => ctx
                        .DrawImage(icon, new Point(xOffset, yOffset))
                        .SetColor(colors.Mid)
                        .GaussianBlur(outer_glow_blur));

                    tokenContents.Mutate(ctx => ctx.DrawImage(iconOuterGlow));
                }
                using (var iconInnerGlow = new Image<Rgba32>(image_size, image_size))
                {
                    iconInnerGlow.Mutate(ctx => ctx
                        .DrawImage(icon, new Point(xOffset, yOffset))
                        .SetColor(colors.Light)
                        .GaussianBlur(inner_glow_blur));

                    tokenContents.Mutate(ctx => ctx.DrawImage(iconInnerGlow));
                }

                tokenContents.Mutate(ctx => ctx.DrawImage(icon, new Point(xOffset, yOffset)));

                var graphicOptions = new GraphicsOptions
                {
                    AlphaCompositionMode = PixelAlphaCompositionMode.DestOut
                };

                tokenContents.Mutate(x => x.Fill(graphicOptions, Rgba32.White, new RectangularPolygon(0, 0, image_size, image_size).Clip(roundedHexagon)));

                mod.Mutate(ctx => ctx.DrawImage(tokenContents));
            }

            mod.SaveToFileWithHD(System.IO.Path.Combine(path, $"selection-mod-{filename}"), parameters.HD);

            await Task.CompletedTask;
        }
    }
}
