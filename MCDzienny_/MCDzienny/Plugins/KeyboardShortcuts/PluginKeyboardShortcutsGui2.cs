using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using MCDzienny.Plugins.KeyboardShortcuts;

namespace MCDzienny.Plugins
{
    public partial class PluginKeyboardShortcutsGui2 : UserControl, IComponentConnector, IStyleConnector
    {
        void Button_Click(object sender, RoutedEventArgs e)
        {
            keyboardShortcuts.Items.Add(new ShortcutInfo());
        }

        void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0007: Expected O, but got Unknown
            Button val = (Button)sender;
            keyboardShortcuts.Items.Remove(val.Tag);
        }

        void ShortcutTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_0012: Invalid comparison between Unknown and I4
            //IL_001d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0015: Unknown result type (might be due to invalid IL or missing references)
            //IL_0022: Unknown result type (might be due to invalid IL or missing references)
            //IL_0023: Unknown result type (might be due to invalid IL or missing references)
            //IL_0026: Invalid comparison between Unknown and I4
            //IL_0028: Unknown result type (might be due to invalid IL or missing references)
            //IL_002b: Invalid comparison between Unknown and I4
            //IL_003a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0040: Unknown result type (might be due to invalid IL or missing references)
            //IL_0055: Unknown result type (might be due to invalid IL or missing references)
            //IL_005b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0070: Unknown result type (might be due to invalid IL or missing references)
            //IL_0076: Unknown result type (might be due to invalid IL or missing references)
            //IL_0085: Unknown result type (might be due to invalid IL or missing references)
            //IL_0088: Invalid comparison between Unknown and I4
            //IL_008a: Unknown result type (might be due to invalid IL or missing references)
            //IL_008d: Invalid comparison between Unknown and I4
            //IL_008f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0092: Invalid comparison between Unknown and I4
            //IL_00bd: Unknown result type (might be due to invalid IL or missing references)
            //IL_00c3: Expected O, but got Unknown
            //IL_00ab: Unknown result type (might be due to invalid IL or missing references)
            //IL_0094: Unknown result type (might be due to invalid IL or missing references)
            //IL_0097: Invalid comparison between Unknown and I4
            //IL_0099: Unknown result type (might be due to invalid IL or missing references)
            //IL_009c: Invalid comparison between Unknown and I4
            //IL_009e: Unknown result type (might be due to invalid IL or missing references)
            //IL_00a1: Invalid comparison between Unknown and I4
            e.Handled = true;
            Key val = (int)e.Key == 156 ? e.SystemKey : e.Key;
            if ((int)val != 70 && (int)val != 71)
            {
                StringBuilder stringBuilder = new StringBuilder();
                if (((int)e.KeyboardDevice.Modifiers & 2) != 0)
                {
                    stringBuilder.Append("Ctrl+");
                }
                if (((int)e.KeyboardDevice.Modifiers & 4) != 0)
                {
                    stringBuilder.Append("Shift+");
                }
                if (((int)e.KeyboardDevice.Modifiers & 1) != 0)
                {
                    stringBuilder.Append("Alt+");
                }
                if ((int)val != 116 && (int)val != 117 && (int)val != 118 && (int)val != 119 && (int)val != 120 && (int)val != 121)
                {
                    stringBuilder.Append(val);
                }
                TextBox val2 = (TextBox)sender;
                val2.Text = stringBuilder.ToString();
                val2.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
        }

        public void RemoveEmptyShortcuts()
        {
            var list = new List<ShortcutInfo>();
            foreach (ShortcutInfo item in keyboardShortcuts.Items)
            {
                if (string.IsNullOrEmpty(item.Shortcut) || string.IsNullOrEmpty(item.Command))
                {
                    list.Add(item);
                }
            }
            foreach (ShortcutInfo item2 in list)
            {
                keyboardShortcuts.Items.Remove(item2);
            }
        }

        void Button_Click_2(object sender, RoutedEventArgs e) {}

        void HighlightOnError(TextBox tb)
        {
            //IL_002b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0030: Unknown result type (might be due to invalid IL or missing references)
            //IL_003a: Expected O, but got Unknown
            if (!ServerProperties.ValidString(tb.Text, "%![]:.,{}~-+()?_/\\^*#@$~`\"'|=;<>& ") || tb.Text.Length > 64)
            {
                tb.Background = new SolidColorBrush(Color.FromArgb(50, 200, 0, 0));
            }
            else
            {
                tb.ClearValue(Control.BackgroundProperty);
            }
        }

        void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0007: Expected O, but got Unknown
            TextBox tb = (TextBox)sender;
            HighlightOnError(tb);
        }

        void TextBox_Initialized(object sender, EventArgs e)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0007: Expected O, but got Unknown
            TextBox tb = (TextBox)sender;
            HighlightOnError(tb);
        }

        void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0007: Expected O, but got Unknown
            //IL_003d: Unknown result type (might be due to invalid IL or missing references)
            try
            {
                TextBox val = (TextBox)sender;
                val.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                HighlightDuplicateShortcuts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " <> " + ex.StackTrace);
            }
        }

        void HighlightDuplicateShortcuts()
        {
            //IL_00c1: Unknown result type (might be due to invalid IL or missing references)
            //IL_00c7: Expected O, but got Unknown
            //IL_0107: Unknown result type (might be due to invalid IL or missing references)
            //IL_010c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0116: Expected O, but got Unknown
            IEnumerable<ShortcutInfo> enumerable = from ShortcutInfo si in keyboardShortcuts.Items
                                                   where !string.IsNullOrEmpty(si.Shortcut)
                select si;
            IEnumerable<ShortcutInfo> source = (from si in enumerable
                group si by si.Shortcut
                into g
                where g.Count() > 1
                select g).SelectMany(g => g);
            foreach (ShortcutInfo item in enumerable)
            {
                ListBoxItem depObj = (ListBoxItem)keyboardShortcuts.ItemContainerGenerator.ContainerFromItem(item);
                foreach (TextBox item2 in FindVisualChildren<TextBox>((DependencyObject)(object)depObj))
                {
                    if (item2.Name == "shortcut")
                    {
                        if (source.Contains(item))
                        {
                            item2.Background = new SolidColorBrush(Color.FromArgb(50, 200, 0, 0));
                        }
                        else
                        {
                            item2.ClearValue(Control.BackgroundProperty);
                        }
                    }
                }
            }
        }

        public IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null)
            {
                yield break;
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child != null && child is T)
                {
                    yield return (T)child;
                }
                foreach (T item in FindVisualChildren<T>(child))
                {
                    yield return item;
                }
            }
        }

        void shortcut_Initialized(object sender, EventArgs e)
        {
            HighlightDuplicateShortcuts();
        }
    }
}