using poyosu.Configuration;
using poyosu.Utilities;
using System.IO;
using System.Threading.Tasks;

namespace poyosu.Builders
{
    public abstract class Builder
    {
        public abstract string Folder { get; }
        public abstract string Name { get; }

        public async Task Build(Parameters parameters)
        {
            Logger.Log($"Generating {this.Name}...");

            string path = Path.Combine("Build", this.Folder);

            DirectoryUtilities.EmptyOrCreateDirectory(path);

            await this.Generate(path, parameters);
        }

        public abstract Task Generate(string path, Parameters parameters);
    }
}
