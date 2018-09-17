using System;
namespace dotnet_manage
{
    public class Tool
    {
        public string PackageId { get; set; }

        public string Version { get; set; }

        public string Command { get; set; }

        public string Directory { get; set; }

        public override string ToString()
               => $"{PackageId} {Version}  {Command} {Directory}";

    }
}
