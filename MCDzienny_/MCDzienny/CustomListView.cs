using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MCDzienny
{
    public class CustomListView : ListView
    {
        readonly List<string> groups = new List<string>();

        string lastSelectedItemName = "";

        public string LastSelectedItemName { get { return lastSelectedItemName; } }

        public void AddGroup(string groupName)
        {
            if (!groups.Contains(groupName))
            {
                Groups.Add(groupName, groupName);
                groups.Add(groupName);
            }
        }

        public ListViewGroup GetGroup(string groupName)
        {
            return Groups[groups.IndexOf(groupName)];
        }

        public bool GroupExists(string groupName)
        {
            return groups.Contains(groupName);
        }

        public void ClearGroups()
        {
            Groups.Clear();
            groups.Clear();
        }

        public void SaveCurrentState() {}

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            if (SelectedIndices.Count > 0)
            {
                lastSelectedItemName = Items[SelectedIndices[0]].Text;
            }
        }

        public void RemoveAllItems()
        {
            for (int num = Items.Count - 1; num >= 0; num--)
            {
                Items.RemoveAt(num);
            }
        }
    }
}