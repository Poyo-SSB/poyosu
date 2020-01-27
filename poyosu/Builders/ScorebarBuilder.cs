using System.Net;
using System.Threading.Tasks;
using poyosu.Configuration;
using poyosu.Utilities;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using SixLabors.Shapes;

namespace poyosu.Builders
{
    public class ScorebarBuilder : Builder
    {
        private const int bar_width = 1024;
        private const int bar_height = 32;
        private const int bar_border = 4;
        private const int bar_padding = 20;
        private const int text_padding = 24;
        private const int flag_padding = 52;
        private const int flag_width = 44;
        private const float font_size = 28f;

        private const int color_offset_x = 9;
        private const int color_offset_y = 8;

        public override string Folder => "scorebar";
        public override string Name => "scorebar";

        public override async Task Generate(string path, Parameters parameters)
        {
            int bgWidth = bar_padding + bar_width;
            int bgHeight = bar_padding + bar_height + text_padding;

            var canvas = new RectangularPolygon(0, 0, bgWidth, bgHeight);

            var rectangle = new RectangularPolygon(
                bar_padding + (bar_height / 2),
                bar_padding + text_padding,
                bar_width - bar_height,
                bar_height);
            var leftEllipse = new EllipsePolygon(
                bar_padding + (bar_height / 2),
                bar_padding + (bar_height / 2) + text_padding,
                bar_height / 2);
            var rightEllipse = new EllipsePolygon(
                bar_padding + bar_width - (bar_height / 2),
                bar_padding + (bar_height / 2) + text_padding,
                bar_height / 2);

            IPath outerShape = canvas.Clip(canvas.Clip(rectangle).Clip(leftEllipse).Clip(rightEllipse));

            rectangle = new RectangularPolygon(
                bar_padding + (bar_height / 2),
                bar_padding + bar_border + text_padding,
                bar_width - bar_height,
                bar_height - (2 * bar_border));
            leftEllipse = new EllipsePolygon(
                bar_padding + (bar_height / 2),
                bar_padding + (bar_height / 2) + text_padding,
                (bar_height / 2) - bar_border);
            rightEllipse = new EllipsePolygon(
                bar_padding + bar_width - (bar_height / 2),
                bar_padding + (bar_height / 2) + text_padding,
                (bar_height / 2) - bar_border);

            IPath innerShape = outerShape.Clip(rectangle).Clip(leftEllipse).Clip(rightEllipse);

            using (var bg = new Image<Rgba32>(bgWidth, bgHeight))
            {
                bg.Mutate(ctx => ctx.Fill(Rgba32.White, innerShape));

                if (parameters.ScorebarLabelEnabled)
                {
                    Image<Rgba32> flag = null;
                    if (parameters.ScorebarLabelFlagEnabled)
                    {
                        using var client = new WebClient();

                        try
                        {
                            flag = Image.Load<Rgba32>(await client.OpenReadTaskAsync($"https://osu.ppy.sh/images/flags/{parameters.ScorebarLabelFlag.ToUpperInvariant()}.png"));

                            flag.Mutate(ctx => ctx.Resize(flag_width, flag_width * flag.Height / flag.Width));
                        }
                        catch (WebException)
                        {
                            Logger.Log($"Failed to get the {parameters.ScorebarLabelFlag.ToUpperInvariant()} flag from the osu! website. Omitting flag from scorebar.", Logger.MessageType.Error);
                        }
                    }

                    var textPoint = new PointF(2 * bar_padding, ((bar_padding + text_padding) / 2) - (text_padding / 12));

                    if (flag != null)
                    {
                        textPoint += new PointF(flag_padding, 0);

                        bg.Mutate(ctx => ctx.DrawImage(flag, new Point(2 * bar_padding, (bar_padding + text_padding - flag.Height) / 2)));
                    }

                    bg.Mutate(ctx => ctx
                        .DrawText(new TextGraphicsOptions(true)
                        {
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Center
                        }, parameters.ScorebarLabelName, new Font(Assets.UniSansSemiBold, font_size), Rgba32.White, textPoint));
                }

                bg.SaveToFileWithHD(System.IO.Path.Combine(path, $"scorebar-bg"), parameters.HD);
            }

            using var color = new Image<Rgba32>(bgWidth, bgHeight);

            rectangle = new RectangularPolygon(
                bar_padding + (bar_height / 2),
                bar_padding,
                bar_width - bar_height,
                bar_height);
            leftEllipse = new EllipsePolygon(
                bar_padding + (bar_height / 2),
                bar_padding + (bar_height / 2),
                bar_height / 2);
            rightEllipse = new EllipsePolygon(
                bar_padding + bar_width - (bar_height / 2),
                bar_padding + (bar_height / 2),
                bar_height / 2);

            IPath fill = canvas.Clip(canvas.Clip(rectangle).Clip(leftEllipse).Clip(rightEllipse));
                
            color.Mutate(ctx => ctx.Fill(Rgba32.White, fill));

            color.Mutate(ctx => ctx.Crop(new Rectangle(color_offset_x, color_offset_y, bgWidth - color_offset_x, bgHeight - color_offset_y)));

            color.SaveToFileWithHD(System.IO.Path.Combine(path, $"scorebar-colour"), parameters.HD);
        }
    }
}
