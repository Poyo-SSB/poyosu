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
    public class MenuButtonBuilder : Builder
    {
        private const int menu_button_selection_width = 148;
        private const int menu_button_wide_selection_width = 178;
        private const int menu_button_selection_height = 180;
        private const int menu_button_selection_top = 6;

        private const int menu_glow_blur = 12;
        private const float menu_glow_opacity = 0.5f;

        private static readonly Rgba32 color_menu_mode = Rgba32.FromHex("8B3BEE");
        private static readonly Rgba32 color_menu_mods = Rgba32.FromHex("D747AD");
        private static readonly Rgba32 color_menu_options = Rgba32.FromHex("0096ED");
        private static readonly Rgba32 color_menu_random = Rgba32.FromHex("8ED700");

        public override string Folder => "selectionbutton";
        public override string Name => "menu buttons";

        public override async Task Generate(string path, Parameters parameters)
        {
            // menu button
            await this.GenerateSelectionButton(path, parameters, color_selection_mode, null, menu_button_wide_selection_width, "mode");
            await this.GenerateSelectionButton(path, parameters, color_selection_mods, Assets.ImageIconStar, menu_button_selection_width, "mods");
            await this.GenerateSelectionButton(path, parameters, color_selection_options, Assets.ImageIconCog, menu_button_selection_width, "options");
            await this.GenerateSelectionButton(path, parameters, color_selection_random, Assets.ImageIconDice, menu_button_selection_width, "random");

            await Task.CompletedTask;
        }

        private async Task GenerateSelectionButton(string path, Parameters parameters, Rgba32 color, Image<Rgba32> buttonIcon, int baseWidth, string name)
        {
            int width = baseWidth;
            int height = menu_button_selection_height;
            int topHeight = menu_button_selection_top;

            int blur = menu_glow_blur;

            double size = 0.3;

            var icon = buttonIcon?.Clone();

            using (var button = new Image<Rgba32>(width, height))
            {
                button.Mutate(ctx => ctx.Fill(color));
                button.Mutate(ctx => ctx.Fill(Rgba32.White, new RectangleF(0, 0, width, topHeight)));

                if (icon != null)
                {
                icon.Mutate(ctx => ctx.Resize((int)(buttonIcon.Width * size), (int)(buttonIcon.Height * size)));

                    var baseCenter = new Point((width - icon.Width) / 2, (topHeight - icon.Height + height) / 2);
                    var glowCenter = new Point((width - (3 * icon.Width)) / 2, (topHeight - (3 * icon.Height) + height) / 2);
                    
                    using (var glow = icon.Clone())
                    {
                        glow.Mutate(ctx => ctx
                            .Pad(glow.Width * 3, glow.Height * 3)
                            .GaussianBlur(blur));
                        button.Mutate(ctx => ctx.DrawImage(glow, glowCenter, menu_glow_opacity));
                    }

                    button.Mutate(ctx => ctx.DrawImage(icon, baseCenter));
                }

                button.SaveToFileWithHD(Path.Combine(path, $"selection-{name}"), parameters.HD);
            }

            using (var hovered = new Image<Rgba32>(width, height))
            {
                hovered.Mutate(ctx => ctx.Fill(Rgba32.White));
                hovered.Mutate(ctx => ctx.Fill(color, new RectangleF(0, 0, width, topHeight)));

                if (icon != null)
                {
                    icon.Mutate(ctx => ctx.SetColor(color)); // Icon was already resized.

                    var center = new Point((width - icon.Width) / 2, (topHeight - icon.Height + height) / 2);

                    hovered.Mutate(ctx => ctx.DrawImage(icon, center));
                }

                hovered.SaveToFileWithHD(Path.Combine(path, $"selection-{name}-over"), parameters.HD);
            }

            await Task.CompletedTask;
        }
    }
}
