using System;
using System.Collections.Generic;
using System.IO;
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
    public class SongButtonBuilder : Builder
    {
        private const int song_button_image_width = 1398;
        private const int song_button_image_height = 206;
        private const int song_button_top_margin = 18;
        private const int song_button_border = 17;
        private const int song_button_width = 1378;
        private const int song_button_height = 172;

        public override string Folder => "menubutton";
        public override string Name => "song button";

        public override async Task Generate(string path, Parameters parameters)
        {
            using var menuButton = new Image<Rgba32>(song_button_image_width, song_button_image_height);

            menuButton.Mutate(ctx => ctx.Fill(Rgba32.Black, new RectangleF(0, song_button_top_margin, song_button_width, song_button_height)));
            menuButton.Mutate(ctx => ctx.Fill(Rgba32.White, new RectangleF(0, song_button_top_margin, song_button_border, song_button_height)));

            menuButton.SaveToFileWithHD(Path.Combine(path, $"menu-button-background"), parameters.HD);

            await Task.CompletedTask;
        }
    }
}
