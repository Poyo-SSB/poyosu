using Newtonsoft.Json;
using poyosu.Builders;
using poyosu.Configuration;
using poyosu.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace poyosu
{
    public class Program
    {
        private static readonly string[] extensions = new[] {
            ".png",
            ".ini"
        };

        private static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        private static async Task MainAsync(string[] args)
        {
            Arguments arguments = new ArgumentParser().Parse(args);

            await Build(arguments);

            Logger.Log("Done!");

            // await Task.Delay(-1);
        }

        private static async Task Build(Arguments arguments)
        {
            var serializer = new JsonSerializer()
            {
                Formatting = Formatting.Indented
            };
            var configsReader = new JsonTextReader(new StringReader(File.ReadAllText(arguments.Config)));

            serializer.Converters.Add(new Rgba32Converter());

            Config[] configs = serializer.Deserialize<Config[]>(configsReader);

            foreach (Config config in configs)
            {
                var parameters = new Parameters();
                parameters.Populate(config);
                serializer.Populate(config.Parameters.CreateReader(), parameters);

                Logger.Log($"Building {config.Folder}...");

                await Task.WhenAll(new List<Task>
                {
                    //new CursorBuilder().Build(parameters),
                    new CursorTrailBuilder().Build(parameters),
                    //new ModBuilder().Build(parameters),
                    //new ScorebarBuilder().Build(parameters),
                    //new FollowpointBuilder().Build(parameters),
                    //new GradeBuilder().Build(parameters),
                    //new MenuButtonBuilder().Build(parameters),
                    //new SongButtonBuilder().Build(parameters),
                    //new RankingPanelBuilder().Build(parameters),
                    //new PauseButtonBuilder().Build(parameters),
                    //new HitCircleBuilder().Build(parameters),
                    //new HitCircleNumberBuilder().Build(parameters),
                    //new JudgementBuilder().Build(parameters),
                    //new ReverseArrowBuilder().Build(parameters),
                    //new ApproachCircleBuilder().Build(parameters),
                    //new FollowCircleBuilder().Build(parameters),
                    new StarsBuilder().Build(parameters),

                    new SkinIniBuilder().Build(parameters)
                }.AsParallel());

                string path = Path.Combine(arguments.Output, config.Folder);
                DirectoryUtilities.EmptyOrCreateDirectory(path);

                foreach (string file in Directory.EnumerateFiles("Build", "*.*", SearchOption.AllDirectories))
                {
                    string extension = Path.GetExtension(file);

                    if (!extensions.Contains(extension))
                    {
                        continue;
                    }

                    string name = Path.GetFileName(file);
                    string newPath = Path.Combine(path, name);

                    File.Copy(file, newPath);
                }
            }
        }
    }
}
