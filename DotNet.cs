using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace dotnet_manage
{
    public static class DotNet
    {
        #region Sdks

        #region Helpers
        static List<Sdk> _Skds = null;

        static List<Sdk> GetSdks()
        {
            var result = new List<Sdk>();

            try
            {
                var dotnet = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "dotnet",
                        Arguments = "--list-sdks",
                        UseShellExecute = false,
                        RedirectStandardOutput = true
                    }
                };
                dotnet.Start();

                var lines = dotnet.StandardOutput.ReadToEnd().Trim().Replace("\r\n", "\n").Split('\n');
                foreach (var line in lines)
                {
                    var items = line.Split('[');
                    if (items.Length == 2)
                    {
                        result.Add(new Sdk
                        {
                            Version = items[0].Trim(),
                            Directory = System.IO.Path.Combine(items[1].Replace("]", "").Trim(), items[0].Trim())
                        });
                    }
                }
                dotnet.WaitForExit();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return result;
        }
        #endregion

        public static List<Sdk> Sdks { get => _Skds ?? (_Skds = GetSdks()); }

        public static Sdk LastSdk => Sdks.Last();

        public static Sdk CurrentSdk()
        {
            return LastSdk;
        }
        #endregion

        #region Global.Json
        public static string GlobalJsonFilename = "global.json";

        public static bool GlobalJsonExist() => File.Exists(GlobalJsonFilename);

        public static void GlobalJsonRemove()
        {
            if (GlobalJsonExist())
                File.Delete(GlobalJsonFilename);
        }

        public static bool GlobalJsonWrite(string version)
        {
            try
            {
                File.WriteAllText(GlobalJsonFilename,
                            JsonConvert.SerializeObject(
                                new GlobalJson(version), Formatting.Indented));
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public static string GlobalJsonRead()
        {
            try
            {
                return GlobalJsonExist() ? JsonConvert.DeserializeObject<GlobalJson>(File.ReadAllText(GlobalJsonFilename)).Sdk.version : null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
        #endregion

        #region Actions
        static void StartProcess(string fileName, string workingDirectory, string arguments = "")
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    WorkingDirectory = workingDirectory,
                    Arguments = arguments
                }
            };
            proc.Start();
        }

        public static void SdkDirectoryOpenInBrowserWindow(Sdk sdk)
        {
            if (sdk is null)
                return;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                StartProcess("open", sdk.Directory, $"\"{sdk.Directory}\"");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                StartProcess("open", sdk.Directory, $"\"{sdk.Directory}\"");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                StartProcess("cmd.exe", sdk.Directory, $"/c explorer \"{sdk.Directory}\"");
        }

        public static void SdkDirectoryOpenInShellWindow(Sdk sdk)
        {

            if (sdk is null)
                return;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                StartProcess("open", sdk.Directory, $"-a Terminal.app {sdk.Directory}");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                StartProcess("xterm", sdk.Directory);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                StartProcess("cmd.exe", sdk.Directory, $"/c start cmd.exe @cmd /k cd \"{sdk.Directory}\"");
        }
        #endregion
    }
}
