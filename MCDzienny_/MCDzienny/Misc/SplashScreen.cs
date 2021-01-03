using System.Threading;
using System.Windows.Forms;

namespace MCDzienny.Misc
{
    // Token: 0x020001C7 RID: 455
    public partial class SplashScreen : Form
    {
        // Token: 0x06000CBC RID: 3260 RVA: 0x000499E4 File Offset: 0x00047BE4
        public SplashScreen()
        {
            AllowTransparency = true;
            InitializeComponent();
        }

        // Token: 0x06000CBD RID: 3261 RVA: 0x000499FC File Offset: 0x00047BFC
        public void FadeOut()
        {
            Thread.Sleep(100);
            for (var i = 0; i < 10; i++) Opacity -= 0.10000000149011612;
            Close();
        }
    }
}