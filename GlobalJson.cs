using System;
using System.Security.Cryptography.X509Certificates;
namespace dotnet_manage
{
    public class GlobalJson
    {
        public class GlobalJsonSdk
        {
            public string version { get; set; } = "";
        }
        public GlobalJsonSdk Sdk { get; set; } = new GlobalJsonSdk();

        public GlobalJson()
        {
        }

        public GlobalJson(string version)
        {
            Sdk.version = version;
        }
    }
}
