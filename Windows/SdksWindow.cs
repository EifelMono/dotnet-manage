using System;
using System.Linq;
using Terminal.Gui;
namespace dotnet_manage.Windows
{
    public class SdksWindow : BaseWindow
    {
        public SdksWindow(Toplevel toplevel) : base(toplevel, "Sdk's")
        {

        }

        ListView VersionListView;
        void VersionListView_SelectedChanged()
        {
        }

        protected override void AddElements()
        {
            var versionFrame = new FrameView(new Rect(0, 0, 38, 21), "Version's");
            VersionListView = new ListView(new Rect(0, 0, 36, 19), Sdks.VersionItems)
            {
            };

            VersionListView.SelectedChanged += VersionListView_SelectedChanged;
            {
            }
            versionFrame.Add(VersionListView);

            Add(
                versionFrame,
                new Button(50, 12, "Set Version 2.0")
                {
                    Clicked = () =>
                    {
                        Sdks.GlobalJsonWrite(Sdks.Items.FirstOrDefault(sdk => sdk.Version.StartsWith("2.2", StringComparison.Ordinal)).Version);
                    }
                },
                new Button(50, 13, "Browser current sdk")
                {
                    Clicked = () =>
                    {
                        Sdks.SdkDirectoryOpenWithBrowserWindow(Sdks.Current);
                    }
                },
                new Button(50, 14, "Shell current sdk")
                {
                    Clicked = () =>
                    {
                        Sdks.SdkDirectoryOpenWithShellWindow(Sdks.Current);
                    }
                }
                );
        }

        public override void BrintThisToFront()
        {
            base.BrintThisToFront();
            SetFocus(VersionListView);
        }
    }
}
