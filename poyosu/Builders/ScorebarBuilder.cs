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
        private const int base_bar_width = 1024;
        private const int base_bar_height = 32;
        private const int base_bar_border = 4;
        private const int base_bar_padding = 20;
        private const int base_text_padding = 24;
        private const int base_flag_padding = 52;
        private const int base_flag_width = 44;
        private const float base_font_size = 24f;

        private const int color_offset_x = 9;
        private const int color_offset_y = 8;

        public override string Folder => "scorebar";
        public override string Name => "scorebar";

        public override async Task Generate(string path, Parameters parameters)
        {
            int barWidth = base_bar_width;
            int barHeight = base_bar_height;
            int barBorder = base_bar_border;
            int barPadding = base_bar_padding;
            int textPadding = base_text_padding;
            int flagPadding = base_flag_padding;
            int flagWidth = base_flag_width;
            float fontSize = base_font_size;
            int colorOffsetX = color_offset_x;
            int colorOffsetY = color_offset_y;

            if (!parameters.HD)
            {
                barWidth /= 2;
                barHeight /= 2;
                barBorder /= 2;
                barPadding /= 2;
                textPadding /= 2;
                flagPadding /= 2;
                flagWidth /= 2;
                fontSize /= 2;
                colorOffsetX /= 2;
                colorOffsetY /= 2;
            }

            int bgWidth = barPadding + barWidth;
            int bgHeight = barPadding + barHeight + textPadding;

            var canvas = new RectangularPolygon(0, 0, bgWidth, bgHeight);

            var rectangle = new RectangularPolygon(
                barPadding + (barHeight / 2),
                barPadding + textPadding,
                barWidth - barHeight,
                barHeight);
            var leftEllipse = new EllipsePolygon(
                barPadding + (barHeight / 2),
                barPadding + (barHeight / 2) + textPadding,
                barHeight / 2);
            var rightEllipse = new EllipsePolygon(
                barPadding + barWidth - (barHeight / 2),
                barPadding + (barHeight / 2) + textPadding,
                barHeight / 2);

            IPath outerShape = canvas.Clip(canvas.Clip(rectangle).Clip(leftEllipse).Clip(rightEllipse));

            rectangle = new RectangularPolygon(
                barPadding + (barHeight / 2),
                barPadding + barBorder + textPadding,
                barWidth - barHeight,
                barHeight - (2 * barBorder));
            leftEllipse = new EllipsePolygon(
                barPadding + (barHeight / 2),
                barPadding + (barHeight / 2) + textPadding,
                (barHeight / 2) - barBorder);
            rightEllipse = new EllipsePolygon(
                barPadding + barWidth - (barHeight / 2),
                barPadding + (barHeight / 2) + textPadding,
                (barHeight / 2) - barBorder);

            IPath innerShape = outerShape.Clip(rectangle).Clip(leftEllipse).Clip(rightEllipse);

            using (var bg = new Image<Rgba32>(bgWidth, bgHeight))
            {
                bg.Mutate(ctx => ctx.Fill(Rgba32.White, innerShape));

                if (parameters.ScorebarLabelEnabled)
                {
                    Image<Rgba32> flag = null;
                    if (parameters.ScorebarLabelFlagEnabled)
                    {
                        using (var client = new WebClient())
                        {
                            try
                            {
                                flag = Image.Load<Rgba32>(await client.OpenReadTaskAsync($"https://osu.ppy.sh/images/flags/{parameters.ScorebarLabelFlag.ToUpperInvariant()}.png"));

                                flag.Mutate(ctx => ctx.Resize(flagWidth, flagWidth * flag.Height / flag.Width));
                            }
                            catch (WebException)
                            {
                                Logger.Log($"Failed to get the {parameters.ScorebarLabelFlag.ToUpperInvariant()} flag from the osu! website. Omitting flag from scorebar.", Logger.MessageType.Error);
                            }
                        }
                    }

                    var textPoint = new PointF(2 * barPadding, ((barPadding + textPadding) / 2) - (textPadding / 8));

                    if (flag != null)
                    {
                        textPoint += new PointF(flagPadding, 0);

                        bg.Mutate(ctx => ctx.DrawImage(flag, new Point(2 * barPadding, (barPadding + textPadding - flag.Height) / 2)));
                    }

                    bg.Mutate(ctx => ctx
                        .DrawText(new TextGraphicsOptions(true)
                        {
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Center
                        }, parameters.ScorebarLabelName, new Font(Assets.ExoSemiBold, fontSize), Rgba32.White, textPoint));
                }

                if (parameters.HD)
                {
                    bg.SaveToFileAsPng(System.IO.Path.Combine(path, "scorebar-bg@2x.png"));
                }
                else
                {
                    bg.SaveToFileAsPng(System.IO.Path.Combine(path, "scorebar-bg.png"));
                };
            }

            using (var color = new Image<Rgba32>(bgWidth, bgHeight))
            {
                rectangle = new RectangularPolygon(
                    barPadding + (barHeight / 2),
                    barPadding,
                    barWidth - barHeight,
                    barHeight);
                leftEllipse = new EllipsePolygon(
                    barPadding + (barHeight / 2),
                    barPadding + (barHeight / 2),
                    barHeight / 2);
                rightEllipse = new EllipsePolygon(
                    barPadding + barWidth - (barHeight / 2),
                    barPadding + (barHeight / 2),
                    barHeight / 2);

                IPath fill = canvas.Clip(canvas.Clip(rectangle).Clip(leftEllipse).Clip(rightEllipse));
                
                color.Mutate(ctx => ctx.Fill(Rgba32.White, fill));

                color.Mutate(ctx => ctx.Crop(new Rectangle(colorOffsetX, colorOffsetY, bgWidth - colorOffsetX, bgHeight - colorOffsetY)));

                if (parameters.HD)
                {
                    color.SaveToFileAsPng(System.IO.Path.Combine(path, "scorebar-colour@2x.png"));
                }
                else
                {
                    color.SaveToFileAsPng(System.IO.Path.Combine(path, "scorebar-colour.png"));
                }
            }
        }
    }
}
