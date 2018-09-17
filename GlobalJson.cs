using System;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace dotnet_manage
{
    public class GlobalJson
    {
        public class GlobalJsonSdk
        {
            [JsonProperty("version")]
            public string Version { get; set; } = "";
        }
        [JsonProperty("sdk")]
        public GlobalJsonSdk Sdk { get; set; } = new GlobalJsonSdk();

        public GlobalJson()
        {
        }

        public GlobalJson(string version)
        {
            Sdk.Version = version;
        }
    }
}
