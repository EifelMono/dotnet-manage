using System;
using McMaster.Extensions.CommandLineUtils;

namespace dotnet_manage
{
    class Program
    {
        public static int Main(string[] args)
            => CommandLineApplication.Execute<Program>(args);

        [Option(Description = "The subject")]
        public string Subject { get; }

        [Option(Description = "Use the version")]
        public string Use { get; }

        [Option(Description = "Clear the version")]
        public string Clear { get; }

        private void OnExecute()
        {
            var subject = Subject ?? "world";
            Console.WriteLine($"Hello {subject}!");


            foreach (var sdk in DotNet.Sdks)
                Console.WriteLine($"{sdk.Version} {sdk.Directory}");

            DotNet.SdkDirectoryOpenInBrowserWindow(DotNet.LastSdk);

            DotNet.SdkDirectoryOpenInShellWindow(DotNet.LastSdk);

            Console.ReadLine();
        }
    }
}
