using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MCDzienny
{
    // Token: 0x020001B6 RID: 438
    public class CustomListView : ListView
    {
        // Token: 0x0400067C RID: 1660
        private readonly List<string> groups = new List<string>();

        // Token: 0x0400067D RID: 1661
        private string lastSelectedItemName = "";

        // Token: 0x170004DF RID: 1247
        // (get) Token: 0x06000C78 RID: 3192 RVA: 0x000489C0 File Offset: 0x00046BC0
        public string LastSelectedItemName
        {
            get { return lastSelectedItemName; }
        }

        // Token: 0x06000C79 RID: 3193 RVA: 0x000489C8 File Offset: 0x00046BC8
        public void AddGroup(string groupName)
        {
            if (!groups.Contains(groupName))
            {
                Groups.Add(groupName, groupName);
                groups.Add(groupName);
            }
        }

        // Token: 0x06000C7A RID: 3194 RVA: 0x000489F4 File Offset: 0x00046BF4
        public ListViewGroup GetGroup(string groupName)
        {
            return Groups[groups.IndexOf(groupName)];
        }

        // Token: 0x06000C7B RID: 3195 RVA: 0x00048A10 File Offset: 0x00046C10
        public bool GroupExists(string groupName)
        {
            return groups.Contains(groupName);
        }

        // Token: 0x06000C7C RID: 3196 RVA: 0x00048A20 File Offset: 0x00046C20
        public void ClearGroups()
        {
            Groups.Clear();
            groups.Clear();
        }

        // Token: 0x06000C7D RID: 3197 RVA: 0x00048A38 File Offset: 0x00046C38
        public void SaveCurrentState()
        {
        }

        // Token: 0x06000C7E RID: 3198 RVA: 0x00048A3C File Offset: 0x00046C3C
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            if (SelectedIndices.Count > 0) lastSelectedItemName = Items[SelectedIndices[0]].Text;
        }

        // Token: 0x06000C7F RID: 3199 RVA: 0x00048A78 File Offset: 0x00046C78
        public void RemoveAllItems()
        {
            for (var i = Items.Count - 1; i >= 0; i--) Items.RemoveAt(i);
        }
    }
}