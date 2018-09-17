using System;
using Terminal.Gui;
using System.Dynamic;
namespace dotnet_manage.Windows
{
    public class BaseWindow : Window
    {
        protected Toplevel Toplevel;

        public BaseWindow(Toplevel toplevel, string title) : base(new Rect(0, 1, toplevel.Frame.Width, toplevel.Frame.Height - 1), title)
        {
            Toplevel = toplevel;
#pragma warning disable RECS0021 // Warns about calls to virtual member functions occuring in the constructor
            AddElements();
#pragma warning restore RECS0021 // Warns about calls to virtual member functions occuring in the constructor
        }

        protected virtual void AddElements()
        {

        }

        public virtual void BrintThisToFront()
        {
            for (var index = Toplevel.Subviews.Count - 1; index > 0; index--)
            {
                if (Toplevel.Subviews[index] is var baseView)
                    Toplevel.Remove(baseView);
            }
            Toplevel.Add(this);
            Toplevel.Redraw(Toplevel.Bounds);
        }
    }
}
