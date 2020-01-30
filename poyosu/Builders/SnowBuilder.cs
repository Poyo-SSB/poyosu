using System.Threading.Tasks;
using poyosu.Configuration;
using poyosu.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace poyosu.Builders
{
    public class SnowBuilder : Builder
    {
        private const int image_size = 64;

        public override string Folder => "menusnow";
        public override string Name => "menu snow";

        public override async Task Generate(string path, Parameters parameters)
        {
            var center = new PointF(image_size / 2f, image_size / 2f);

            using var snow = new Image<Rgba32>(image_size, image_size);

            snow.Mutate(ctx => ctx
                .Fill(Rgba32.White, new EllipsePolygon(center, new SizeF(image_size / 2f, image_size / 2f))));

            snow.SaveToFileWithHD(System.IO.Path.Combine(path, $"menu-snow"), parameters.HD);

            await Task.CompletedTask;
        }
    }
}
