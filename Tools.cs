using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace dotnet_manage
{
    public static class Tools
    {
        #region Sdks

        #region Helpers
        static List<Tool> _Items = null;

        static List<Tool> GetItems()
        {
            var result = new List<Tool>();

            try
            {
                int count = 0;
                foreach (var line in Helpers.GetLinesFromProcess("dotnet", "tool list -g"))
                {
                    count++;
                    if (count < 3)
                        continue;
                    var items = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (items.Length == 3)
                    {
                        result.Add(new Tool
                        {
                            PackageId = items[0].Trim(),
                            Version = items[1].Trim(),
                            Command = items[2].Trim(),
                            Directory = ""
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

        public static List<Tool> Items { get => _Items ?? (_Items = GetItems()); }

        public static List<string> PackageIdItems
        {
            get => (from item in Items select new string(item.PackageId)).ToList();
        }

        public static Tool GetToolByPackageId(string packageId)
            => Items.FirstOrDefault(item => item.PackageId == packageId);


        #endregion
    }
}

