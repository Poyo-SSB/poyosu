using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using poyosu.Configuration;
using poyosu.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using SixLabors.Shapes;

namespace poyosu.Builders
{
    public class HitCircleBuilder : Builder
    {
        private const int image_size = 234;
        private const int hit_circle_border = 15;

        public override string Folder => "hitcircle";
        public override string Name => "hit circles";

        public override async Task Generate(string path, Parameters parameters)
        {
            if (!parameters.SliderEndEnabled)
            {
                Assets.ImageBlank.SaveToFileAsPng(System.IO.Path.Combine(path, "sliderendcircle.png"));
            }

            using var hitCircle = new Image<Rgba32>(image_size, image_size);

            var center = new PointF(image_size / 2f, image_size / 2f);

            hitCircle.Mutate(c => c
                .Fill(Rgba32.FromHex("000000A0"), new EllipsePolygon(center, new SizeF(image_size - (hit_circle_border / 2), image_size - (hit_circle_border / 2))))
                .Draw(Rgba32.FromHex("FFFFFF"), hit_circle_border, new EllipsePolygon(center, new SizeF(image_size - hit_circle_border, image_size - hit_circle_border))));

            hitCircle.SaveToFileWithHD(System.IO.Path.Combine(path, $"hitcircle"), parameters.HD);
            Assets.ImageBlank.SaveToFileWithHD(System.IO.Path.Combine(path, $"hitcircleoverlay"), parameters.HD);

            await Task.CompletedTask;
        }
    }
}
