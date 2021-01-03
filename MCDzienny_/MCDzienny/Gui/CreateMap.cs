using System;
using System.Windows.Forms;

namespace MCDzienny.GUI
{
    // Token: 0x02000154 RID: 340
    public partial class CreateMap : Form
    {
        // Token: 0x060009D6 RID: 2518 RVA: 0x00033D40 File Offset: 0x00031F40
        public CreateMap()
        {
            InitializeComponent();
            CenterToParent();
            MinimizeBox = false;
            MaximizeBox = false;
        }

        // Token: 0x060009D7 RID: 2519 RVA: 0x00033D64 File Offset: 0x00031F64
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Token: 0x060009D8 RID: 2520 RVA: 0x00033D6C File Offset: 0x00031F6C
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                Command.all.Find("newlvl").Use(null,
                    string.Concat(mapName.Text, " ", mapX.Text, " ", mapY.Text, " ", mapZ.Text, " ",
                        mapGenerator.Text.ToLower()));
                Command.all.Find("load").Use(null, mapName.Text);
                Hide();
                MessageBox.Show("The map was created and loaded.");
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }

            Close();
        }
    }
}