using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace dotnet_manage
{
    public static class Sdks
    {
        #region Sdks

        #region Helpers
        static List<Sdk> _Items = null;

        static List<Sdk> GetItems()
        {
            var result = new List<Sdk>();

            try
            {
                foreach (var line in Helpers.GetLinesFromProcess("dotnet", "--list-sdks"))
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return result;
        }
        #endregion

        public static List<Sdk> Items { get => _Items ?? (_Items = GetItems()); }

        public static List<string> VersionItems
        {
            get => (from item in Items select new string(item.Version)).ToList();
        }

        public static Sdk GetSdkByVersion(string version)
          => Items.FirstOrDefault(item => item.Version == version);


        public static Sdk Last => Items.Last();

        public static Sdk Current
        {
            get
            {
                try
                {
                    if (GlobalJsonExist())
                    {
                        var version = GlobalJsonRead();
                        if (!string.IsNullOrEmpty(version))
                        {
                            var currentSdk = Items.FirstOrDefault(sdk => sdk.Version == version);
                            return new Sdk
                            {
                                Version = version,
                                Directory = currentSdk == null ? "No sdk directory found" : currentSdk.Directory
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                return Last;
            }
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
                return GlobalJsonExist() ? JsonConvert.DeserializeObject<GlobalJson>(File.ReadAllText(GlobalJsonFilename)).Sdk.Version : null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
        #endregion

        #region Actions

        public static void SdkDirectoryOpenWithBrowserWindow(Sdk sdk)
            => Helpers.DirectoryOpenWithBrowserWindow(sdk.Directory);

        public static void SdkDirectoryOpenWithShellWindow(Sdk sdk)
            => Helpers.DirectoryOpenWithShellWindow(sdk.Directory);

        #endregion
    }
}
