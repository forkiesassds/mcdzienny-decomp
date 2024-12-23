using System.Windows.Controls;
using System.Windows.Markup;

namespace MCDzienny.Gui.Wpf
{
    public partial class ColorChat : UserControl, IComponentConnector
    {
        public RichTextBox ColorChatBox 
        { 
            get { return colorChat; } 
            set { colorChat = value; } 
        }
    }
}