using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace dotnet_manage
{
    public static class Helpers
    {
        public static string[] GetLinesFromProcess(string exeFileName, string arguments)
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = exeFileName,
                        Arguments = arguments,
                        UseShellExecute = false,
                        RedirectStandardOutput = true
                    }
                };
                process.Start();
                var lines = process.StandardOutput.ReadToEnd().Trim().Replace("\r\n", "\n").Split('\n');
                process.WaitForExit();
                return lines;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new string[0];
        }

        public static void UrlOpenWithBrowser(string url)
        {
            StartProcess("open", Directory.GetCurrentDirectory(), url);
        }

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

        public static void DirectoryOpenWithBrowserWindow(string directory)
        {
            if (directory is null)
                return;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                StartProcess("open", directory, $"\"{directory}\"");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                StartProcess("open", directory, $"\"{directory}\"");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                StartProcess("cmd.exe", directory, $"/c explorer \"{directory}\"");
        }


        public static void DirectoryOpenWithShellWindow(string directory)
        {

            if (directory is null)
                return;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                StartProcess("open", directory, $"-a Terminal.app {directory}");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                StartProcess("xterm", directory);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                StartProcess("cmd.exe", directory, $"/c start cmd.exe @cmd /k cd \"{directory}\"");
        }
    }
}
