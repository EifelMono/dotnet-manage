using System;
using System.Linq;
using Terminal.Gui;
using System.Net.NetworkInformation;
using System.Dynamic;
using dotnet_manage.Windows;
using System.Reflection;
using dotnet_manage.Views;
using Microsoft.Extensions.PlatformAbstractions;

namespace dotnet_manage
{
    class Program
    {
        static SdksWindow SdksWindow;
        static ToolsWindow ToolsWindow;
        static Toplevel Toplevel;

        public static void Main(string[] args)
        {
            Application.Init();
            Toplevel = Application.Top;

            SdksWindow = new SdksWindow(Toplevel);
            ToolsWindow = new ToolsWindow(Toplevel);

            // Creates a menubar, the item "New" has a help menu.
            var menu = new MenuBar(new MenuBarItem[]
            {
                new MenuBarItem ("_Manage", new MenuItem []
                {
                    new MenuItem ("_Sdk's", "", SdksWindow.BrintThisToFront),
                    new MenuItem ("_Tool's", "",  ToolsWindow.BrintThisToFront),
                    new MenuItemSplit(),
                    new MenuItem ("_Quit", "", () => { if (Quit ()) Toplevel.Running = false; })
                }).FixMenuBarItem(),
                new MenuBarItem ("_Help", new MenuItem []
                {
                    new MenuItem ("_About", "", About),
                    new MenuItemSplit(),
                    new MenuItem ("_GitHub", "", () => Helpers.UrlOpenWithBrowser("https://github.com/EifelMono/dotnet-manage")),
                    new MenuItem ("_NuGet", "", () => Helpers.UrlOpenWithBrowser("https://www.nuget.org/packages/dotnet-manage")),
                }).FixMenuBarItem(),
            });
            Toplevel.Add(menu);

            SdksWindow.BrintThisToFront();

            Application.Run();
        }

        static bool Quit()
        {
            return true;
        }

        static void About()
        {
            var dialog = new Dialog("About", 40, 10, new Button("Ok", is_default: true)
            {
                Clicked = () => { Application.RequestStop(); }
            });

            var version = PlatformServices.Default.Application.ApplicationVersion;
            dialog.Add(
                new Label($"dotnet-manage {version}")
                {
                    X = Pos.Center(),
                    Y = 1
                },
                new Label("(c) by eifelmono 2018")
                {
                    X = Pos.Center(),
                    Y = 3
                }
            );
            Application.Run(dialog);
        }
    }
}
