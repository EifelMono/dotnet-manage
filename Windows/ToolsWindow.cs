using System;
using Terminal.Gui;

namespace dotnet_manage.Windows
{
    public class ToolsWindow : BaseWindow
    {
        public ToolsWindow(Toplevel toplevel) : base(toplevel, "Tool's")
        {

        }

        ListView PackageIdListView;
        void PackageIdListView_SelectedChanged()
        {
        }

        protected override void AddElements()
        {
            var versionFrame = new FrameView(new Rect(0, 0, 38, 21), "Tool");
            PackageIdListView = new ListView(new Rect(0, 0, 36, 19), Tools.PackageIdItems)
            {
            };

            PackageIdListView.SelectedChanged += PackageIdListView_SelectedChanged;
            {
            }
            versionFrame.Add(PackageIdListView);

            Add(
                versionFrame);

        }

        public override void BrintThisToFront()
        {
            base.BrintThisToFront();
            SetFocus(PackageIdListView);
        }
    }
}
