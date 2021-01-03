using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace MCDzienny.Gui
{
    // Token: 0x02000307 RID: 775
    public partial class UpdateWindow : Form
    {
        // Token: 0x0600165D RID: 5725 RVA: 0x000861DC File Offset: 0x000843DC
        public UpdateWindow()
        {
            InitializeComponent();
        }

        // Token: 0x0600165E RID: 5726 RVA: 0x000861EC File Offset: 0x000843EC
        private void UpdateWindow_Load(object sender, EventArgs e)
        {
            UpdLoadProp("properties/update.properties");
            var webClient = new WebClient();
            webClient.DownloadFile("http://www.mclawl.tk/revs.txt", "text/revs.txt");
            listRevisions.Items.Clear();
            var fileInfo = new FileInfo("text/revs.txt");
            var streamReader = fileInfo.OpenText();
            if (File.Exists("text/revs.txt"))
                while (!streamReader.EndOfStream)
                    listRevisions.Items.Add(streamReader.ReadLine());
            streamReader.Close();
            streamReader.Dispose();
            fileInfo.Delete();
            webClient.Dispose();
        }

        // Token: 0x0600165F RID: 5727 RVA: 0x00086284 File Offset: 0x00084484
        public void UpdSave(string givenPath)
        {
            var streamWriter = new StreamWriter(File.Create(givenPath));
            streamWriter.WriteLine("#This file manages the update process");
            streamWriter.WriteLine("#Toggle AutoUpdate to true for the server to automatically update");
            streamWriter.WriteLine("#Notify notifies players in-game of impending restart");
            streamWriter.WriteLine(
                "#Restart Countdown is how long in seconds the server will count before restarting and updating");
            streamWriter.WriteLine();
            streamWriter.WriteLine("autoupdate= " + chkAutoUpdate.Checked);
            streamWriter.WriteLine("notify = " + chkNotify.Checked);
            streamWriter.WriteLine("restartcountdown = " + txtCountdown.Text);
            streamWriter.Flush();
            streamWriter.Close();
            streamWriter.Dispose();
            Close();
        }

        // Token: 0x06001660 RID: 5728 RVA: 0x00086348 File Offset: 0x00084548
        public void UpdLoadProp(string givenPath)
        {
            if (File.Exists(givenPath))
            {
                var array = File.ReadAllLines(givenPath);
                foreach (var text in array)
                    if (text != "" && text[0] != '#')
                    {
                        var text2 = text.Split('=')[0].Trim();
                        var text3 = text.Split('=')[1].Trim();
                        string a;
                        if ((a = text2.ToLower()) != null)
                        {
                            if (!(a == "autoupdate"))
                            {
                                if (!(a == "notify"))
                                {
                                    if (a == "restartcountdown") txtCountdown.Text = text3;
                                }
                                else
                                {
                                    chkNotify.Checked = text3.ToLower() == "true";
                                }
                            }
                            else
                            {
                                chkAutoUpdate.Checked = text3.ToLower() == "true";
                            }
                        }
                    }
            }
        }

        // Token: 0x06001661 RID: 5729 RVA: 0x0008646C File Offset: 0x0008466C
        private void button1_Click(object sender, EventArgs e)
        {
            var s = txtCountdown.Text.Trim();
            double num;
            var flag = double.TryParse(s, out num);
            if (!flag || txtCountdown.Text == "")
            {
                MessageBox.Show("You must enter a number for the countdown");
                return;
            }

            UpdSave("properties/update.properties");
            Server.autoupdate = chkAutoUpdate.Checked;
        }

        // Token: 0x06001662 RID: 5730 RVA: 0x000864D4 File Offset: 0x000846D4
        private void cmdDiscard_Click(object sender, EventArgs e)
        {
            UpdLoadProp("properties/update.properties");
            Close();
        }

        // Token: 0x06001663 RID: 5731 RVA: 0x000864E8 File Offset: 0x000846E8
        private void listRevisions_SelectedValueChanged(object sender, EventArgs e)
        {
            Server.selectedrevision = listRevisions.SelectedItem.ToString();
        }
    }
}