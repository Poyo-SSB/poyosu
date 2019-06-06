namespace poyosu.Configuration
{
    public class ArgumentParser
    {
        public Arguments Parse(string[] args)
        {
            var arguments = new Arguments();

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].ToLowerInvariant() == "-c")
                {
                    arguments.Config = args[i + 1];
                }
                if (args[i].ToLowerInvariant() == "-o")
                {
                    arguments.Output = args[i + 1];
                }
            }

            return arguments;
        }
    }
}
