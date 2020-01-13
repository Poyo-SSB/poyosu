using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace poyosu
{
    public static class Assets
    {
        public static readonly Font ExoSemiBold;
        public static readonly Font ExoExtraBold;
        public static readonly Font ExoBlack;

        public static readonly Font UniSansBook;
        public static readonly Font UniSansSemiBold;
        public static readonly Font UniSansBold;
        public static readonly Font UniSansHeavy;

        public static Image<Rgba32> ImageBlank => new Image<Rgba32>(1, 1);

        public static Image<Rgba32> ImageModeOsu => Image.Load<Rgba32>("Resources/osu/mode-osu.png");
        public static Image<Rgba32> ImageModeTaiko => Image.Load<Rgba32>("Resources/osu/mode-taiko.png");
        public static Image<Rgba32> ImageModeCatch => Image.Load<Rgba32>("Resources/osu/mode-catch.png");
        public static Image<Rgba32> ImageModeMania => Image.Load<Rgba32>("Resources/osu/mode-mania.png");

        public static Image<Rgba32> ImageIconAutopilot => Image.Load<Rgba32>("Resources/osu/icon-autopilot.png");
        public static Image<Rgba32> ImageIconCinema => Image.Load<Rgba32>("Resources/osu/icon-cinema.png");
        public static Image<Rgba32> ImageIconDoubleTime => Image.Load<Rgba32>("Resources/osu/icon-doubletime.png");
        public static Image<Rgba32> ImageIconEasy => Image.Load<Rgba32>("Resources/osu/icon-easy.png");
        public static Image<Rgba32> ImageIconFlashlight => Image.Load<Rgba32>("Resources/osu/icon-flashlight.png");
        public static Image<Rgba32> ImageIconHalfTime => Image.Load<Rgba32>("Resources/osu/icon-halftime.png");
        public static Image<Rgba32> ImageIconHardRock => Image.Load<Rgba32>("Resources/osu/icon-hardrock.png");
        public static Image<Rgba32> ImageIconHidden => Image.Load<Rgba32>("Resources/osu/icon-hidden.png");
        public static Image<Rgba32> ImageIconNightcore => Image.Load<Rgba32>("Resources/osu/icon-nightcore.png");
        public static Image<Rgba32> ImageIconNoFail => Image.Load<Rgba32>("Resources/osu/icon-nofail.png");
        public static Image<Rgba32> ImageIconNoMod => Image.Load<Rgba32>("Resources/osu/icon-nomod.png");
        public static Image<Rgba32> ImageIconPerfect => Image.Load<Rgba32>("Resources/osu/icon-perfect.png");
        public static Image<Rgba32> ImageIconRandom => Image.Load<Rgba32>("Resources/osu/icon-random.png");
        public static Image<Rgba32> ImageIconRelax => Image.Load<Rgba32>("Resources/osu/icon-relax.png");
        public static Image<Rgba32> ImageIconSpunOut => Image.Load<Rgba32>("Resources/osu/icon-spunout.png");
        public static Image<Rgba32> ImageIconSuddenDeath => Image.Load<Rgba32>("Resources/osu/icon-suddendeath.png");
        public static Image<Rgba32> ImageIconTargetPractice => Image.Load<Rgba32>("Resources/osu/icon-targetpractice.png");
        public static Image<Rgba32> ImageIconTouchDevice => Image.Load<Rgba32>("Resources/osu/icon-touchdevice.png");
        public static Image<Rgba32> ImageIconVaporwave => Image.Load<Rgba32>("Resources/osu/icon-vaporwave.png");

        public static Image<Rgba32> ImageIcon1K => Image.Load<Rgba32>("Resources/osu/icon-1k.png");
        public static Image<Rgba32> ImageIcon2K => Image.Load<Rgba32>("Resources/osu/icon-2k.png");
        public static Image<Rgba32> ImageIcon3K => Image.Load<Rgba32>("Resources/osu/icon-3k.png");
        public static Image<Rgba32> ImageIcon4K => Image.Load<Rgba32>("Resources/osu/icon-4k.png");
        public static Image<Rgba32> ImageIcon5K => Image.Load<Rgba32>("Resources/osu/icon-5k.png");
        public static Image<Rgba32> ImageIcon6K => Image.Load<Rgba32>("Resources/osu/icon-6k.png");
        public static Image<Rgba32> ImageIcon7K => Image.Load<Rgba32>("Resources/osu/icon-7k.png");
        public static Image<Rgba32> ImageIcon8K => Image.Load<Rgba32>("Resources/osu/icon-8k.png");
        public static Image<Rgba32> ImageIcon9K => Image.Load<Rgba32>("Resources/osu/icon-9k.png");
        public static Image<Rgba32> ImageIconCoop => Image.Load<Rgba32>("Resources/osu/icon-coop.png");
        public static Image<Rgba32> ImageIconFadeIn => Image.Load<Rgba32>("Resources/osu/icon-fadein.png");
        public static Image<Rgba32> ImageIconFadeOut => Image.Load<Rgba32>("Resources/osu/icon-fadeout.png");

        public static Image<Rgba32> ImageIconCog => Image.Load<Rgba32>("Resources/fa/icon-cog.png");
        public static Image<Rgba32> ImageIconCogs => Image.Load<Rgba32>("Resources/fa/icon-cogs.png");
        public static Image<Rgba32> ImageIconDice => Image.Load<Rgba32>("Resources/fa/icon-dice.png");
        public static Image<Rgba32> ImageIconLemon => Image.Load<Rgba32>("Resources/fa/icon-lemon.png");
        public static Image<Rgba32> ImageIconStar => Image.Load<Rgba32>("Resources/fa/icon-star.png");
        public static Image<Rgba32> ImageIconTachometer => Image.Load<Rgba32>("Resources/fa/icon-tachometer.png");

        public static Image<Rgba32> ImageGlyphGeki => Image.Load<Rgba32>("Resources/glyphs/glyph-geki.png");
        public static Image<Rgba32> ImageGlyphKatu => Image.Load<Rgba32>("Resources/glyphs/glyph-katu.png");

        static Assets()
        {
            var collection = new FontCollection();

            ExoSemiBold = collection.Install("Resources/fonts/Exo-SemiBold.ttf").CreateFont(0);
            ExoExtraBold = collection.Install("Resources/fonts/Exo-ExtraBold.ttf").CreateFont(0);
            ExoBlack = collection.Install("Resources/fonts/Exo-Black.ttf").CreateFont(0);

            UniSansBook = collection.Install("Resources/fonts/Uni-Sans-Book.ttf").CreateFont(0);
            UniSansSemiBold = collection.Install("Resources/fonts/Uni-Sans-SemiBold.ttf").CreateFont(0);
            UniSansBold = collection.Install("Resources/fonts/Uni-Sans-Bold.ttf").CreateFont(0);
            UniSansHeavy = collection.Install("Resources/fonts/Uni-Sans-Heavy.ttf").CreateFont(0);
        }
    }
}