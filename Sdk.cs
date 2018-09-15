using System;
namespace dotnet_manage
{
    public class Sdk
    {
        public string Version { get; set; }

        public string Directory { get; set; }

        public override string ToString()
           => $"{Version} {Directory}";

    }
}
