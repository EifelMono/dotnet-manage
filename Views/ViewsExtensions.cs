using System;
using System.Runtime.CompilerServices;
using NStack;
using Terminal.Gui;

namespace dotnet_manage.Views
{
    public static class ViewsExtensions
    {
        public static MenuBarItem FixMenuBarItem(this MenuBarItem menuBarItem)
        {
            var length = int.MinValue;
            foreach (var item in menuBarItem.Children)
                length = Math.Max(length, item.Title.Length+ item.Help.Length+ 1);
            foreach (var item in menuBarItem.Children)
                if (item is MenuItemSplit menuItemSplit)
                    menuItemSplit.Title = new string('-', length);
            return menuBarItem;
        }
    }
}
