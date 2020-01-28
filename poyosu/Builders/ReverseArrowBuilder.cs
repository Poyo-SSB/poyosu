using System.IO;
using System.Threading.Tasks;
using poyosu.Configuration;
using poyosu.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace poyosu.Builders
{
    public class StarsBuilder : Builder
    {
        private const int image_size = 100;

        public override string Folder => "stars";
        public override string Name => "stars";

        public override async Task Generate(string path, Parameters parameters)
        {
            Assets.ImageBlank.SaveToFileWithHD(Path.Combine(path, $"star2"), parameters.HD);

            using var star = Assets.ImageIconStar.Clone();

            star.Mutate(ctx => ctx.Resize(image_size, image_size));

            star.SaveToFileWithHD(Path.Combine(path, $"star"), parameters.HD);

            await Task.CompletedTask;
        }
    }
}
