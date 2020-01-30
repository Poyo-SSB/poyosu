using System.IO;
using System.Threading.Tasks;
using poyosu.Configuration;
using poyosu.Utilities;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace poyosu.Builders
{
    public class WelcomeTextBuilder : Builder
    {
        private const int image_width = 800;
        private const int image_height = 800;
        private const float font_size = 192;
        private const float glow_tiny_blur = 28;
        private const float glow_small_blur = 10;
        private const float glow_big_blur = 18;

        private static readonly Rgba32 color = Rgba32.FromHex("3AA7FF");

        public override string Folder => "welcometext";
        public override string Name => "welcome text";

        public override async Task Generate(string path, Parameters parameters)
        {
            using var image = new Image<Rgba32>(image_width, image_height);

            var center = new PointF(image_width / 2f, image_height / 2f);

            using (var text = new Image<Rgba32>(image_width, image_height))
            {
                text.Mutate(ctx => ctx
                    .DrawText(new TextGraphicsOptions
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    }, "welcome", new Font(Assets.UniSansBook, font_size), Rgba32.White, center));

                using (var bigGlow = text.Clone())
                {
                    bigGlow.Mutate(ctx => ctx
                        .SetColor(color)
                        .GaussianBlur(glow_big_blur));
                    image.Mutate(ctx => ctx.DrawImage(bigGlow));
                }

                using (var smallGlow = text.Clone())
                {
                    smallGlow.Mutate(ctx => ctx
                        .SetColor(color)
                        .GaussianBlur(glow_small_blur));
                    image.Mutate(ctx => ctx.DrawImage(smallGlow));
                }

                using (var tinyGlow = text.Clone())
                {
                    tinyGlow.Mutate(ctx => ctx
                        .SetColor(color)
                        .GaussianBlur(glow_tiny_blur));
                    image.Mutate(ctx => ctx.DrawImage(tinyGlow));
                }

                image.Mutate(ctx => ctx.DrawImage(text));
            }

            image.SaveToFileWithHD(System.IO.Path.Combine(path, $"welcome_text"), parameters.HD);

            await Task.CompletedTask;
        }
    }
}
