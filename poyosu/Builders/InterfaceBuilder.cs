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

namespace poyosu.Builders
{
    public class InterfaceBuilder : Builder
    {
        private const int base_ranking_panel_width = 2732;
        private const int base_ranking_panel_height = 1333;

        private static readonly Rgba32 color_background = Rgba32.FromHex("000000D9");
        private static readonly Rgba32 color_lines = Rgba32.FromHex("FFFFFF");

        public override string Folder => "interface";
        public override string Name => "interface";

        public override async Task Generate(string path, Parameters parameters)
        {
            using (var rankingPanel = new Image<Rgba32>(base_ranking_panel_width, base_ranking_panel_height))
            {
                rankingPanel.Mutate(ctx => ctx.Fill(color_background, new RectangleF(0, 8, base_ranking_panel_width, base_ranking_panel_height - 8)));
                rankingPanel.Mutate(ctx => ctx.Fill(color_lines, new RectangleF(0, 0, base_ranking_panel_width, 8)));

                rankingPanel.Mutate(ctx => ctx.Fill(color_lines, new RectangleF(20, 209, 1252, 4)));
                rankingPanel.Mutate(ctx => ctx.Fill(color_lines, new RectangleF(20, 401, 1252, 4)));
                rankingPanel.Mutate(ctx => ctx.Fill(color_lines, new RectangleF(20, 593, 1252, 4)));
                rankingPanel.Mutate(ctx => ctx.Fill(color_lines, new RectangleF(20, 783, 1252, 4)));

                // Assume there's no performance hit from a single image being loaded.
                rankingPanel.SaveToFileAsPng(System.IO.Path.Combine(path, "ranking-panel@2x.png"));
            }

            await Task.CompletedTask;
        }
    }
}
