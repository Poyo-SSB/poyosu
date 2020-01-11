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
        public override string Folder => "hitcircle";
        public override string Name => "hit circles";

        public override async Task Generate(string path, Parameters parameters)
        {
            if (!parameters.SliderEndEnabled)
            {
                Assets.ImageBlank.SaveToFile(System.IO.Path.Combine(path, "sliderendcircle.png"));
            }

            using var hitCircle = new Image<Rgba32>(234, 234);

            hitCircle.Mutate(c => c
                .Fill(Rgba32.FromHex("000000A0"), new EllipsePolygon(new PointF(117, 117), new SizeF(206, 206)))
                .Draw(Rgba32.FromHex("FFFFFF"), 15, new EllipsePolygon(new PointF(117, 117), new SizeF(234 - 15, 234 - 15))));

            hitCircle.SaveToFile(System.IO.Path.Combine(path, "hitcircle@2x.png"));
            Assets.ImageBlank.SaveToFile(System.IO.Path.Combine(path, "hitcircleoverlay.png"));

            await Task.CompletedTask;
        }
    }
}
