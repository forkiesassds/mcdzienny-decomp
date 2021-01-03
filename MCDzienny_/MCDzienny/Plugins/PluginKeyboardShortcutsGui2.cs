using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    // Token: 0x02000047 RID: 71
    public class PluginKeyboardShortcutsGui2 : UserControl, IComponentConnector, IStyleConnector
    {
        // Token: 0x040000F4 RID: 244
        private bool _contentLoaded;

        // Token: 0x040000F2 RID: 242
        internal Button applyButton;

        // Token: 0x040000F0 RID: 240
        internal GridViewColumn commandColumn;

        // Token: 0x040000EE RID: 238
        internal ListView keyboardShortcuts;

        // Token: 0x040000F3 RID: 243
        internal Button newRowButton;

        // Token: 0x040000F1 RID: 241
        internal GridViewColumn shortcutColumn;

        // Token: 0x040000EF RID: 239
        internal GridView shortcutsList;

        // Token: 0x0600018C RID: 396 RVA: 0x00009138 File Offset: 0x00007338
        public PluginKeyboardShortcutsGui2()
        {
            InitializeComponent();
        }

        // Token: 0x06000199 RID: 409 RVA: 0x000095E4 File Offset: 0x000077E4
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (_contentLoaded) return;
            _contentLoaded = true;
            var uri = new Uri("/MCDzienny_;component/plugins/keyboardshortcuts/pluginkeyboardshortcutsgui2.xaml",
                UriKind.Relative);
            Application.LoadComponent(this, uri);
        }

        // Token: 0x0600019B RID: 411 RVA: 0x00009620 File Offset: 0x00007820
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    keyboardShortcuts = (ListView) target;
                    return;
                case 2:
                    shortcutsList = (GridView) target;
                    return;
                case 3:
                    commandColumn = (GridViewColumn) target;
                    return;
                case 5:
                    shortcutColumn = (GridViewColumn) target;
                    return;
                case 8:
                    applyButton = (Button) target;
                    applyButton.Click += Button_Click_2;
                    return;
                case 9:
                    newRowButton = (Button) target;
                    newRowButton.Click += Button_Click;
                    return;
            }

            _contentLoaded = true;
        }

        // Token: 0x0600019C RID: 412 RVA: 0x000096E0 File Offset: 0x000078E0
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DebuggerNonUserCode]
        void IStyleConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 4:
                    ((TextBox) target).TextChanged += TextBox_TextChanged;
                    ((TextBox) target).Initialized += TextBox_Initialized;
                    return;
                case 5:
                    break;
                case 6:
                    ((TextBox) target).PreviewKeyDown += ShortcutTextBox_PreviewKeyDown;
                    ((TextBox) target).TextChanged += TextBox_TextChanged_1;
                    ((TextBox) target).Initialized += shortcut_Initialized;
                    return;
                case 7:
                    ((Button) target).Click += Button_Click_1;
                    break;
                default:
                    return;
            }
        }

        // Token: 0x0600018D RID: 397 RVA: 0x00009148 File Offset: 0x00007348
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            keyboardShortcuts.Items.Add(new ShortcutInfo());
        }

        // Token: 0x0600018E RID: 398 RVA: 0x00009160 File Offset: 0x00007360
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            keyboardShortcuts.Items.Remove(button.Tag);
        }

        // Token: 0x0600018F RID: 399 RVA: 0x0000918C File Offset: 0x0000738C
        private void ShortcutTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            var key = e.Key == Key.System ? e.SystemKey : e.Key;
            if (key == Key.LWin || key == Key.RWin) return;
            var stringBuilder = new StringBuilder();
            if ((e.KeyboardDevice.Modifiers & ModifierKeys.Control) != ModifierKeys.None) stringBuilder.Append("Ctrl+");
            if ((e.KeyboardDevice.Modifiers & ModifierKeys.Shift) != ModifierKeys.None) stringBuilder.Append("Shift+");
            if ((e.KeyboardDevice.Modifiers & ModifierKeys.Alt) != ModifierKeys.None) stringBuilder.Append("Alt+");
            if (key != Key.LeftShift && key != Key.RightShift && key != Key.LeftCtrl && key != Key.RightCtrl &&
                key != Key.LeftAlt && key != Key.RightAlt) stringBuilder.Append(key.ToString());
            var textBox = (TextBox) sender;
            textBox.Text = stringBuilder.ToString();
            textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        // Token: 0x06000190 RID: 400 RVA: 0x00009278 File Offset: 0x00007478
        public void RemoveEmptyShortcuts()
        {
            var list = new List<ShortcutInfo>();
            foreach (var obj in keyboardShortcuts.Items)
            {
                var shortcutInfo = (ShortcutInfo) obj;
                if (string.IsNullOrEmpty(shortcutInfo.Shortcut) || string.IsNullOrEmpty(shortcutInfo.Command))
                    list.Add(shortcutInfo);
            }

            foreach (var removeItem in list) keyboardShortcuts.Items.Remove(removeItem);
        }

        // Token: 0x06000191 RID: 401 RVA: 0x00009344 File Offset: 0x00007544
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
        }

        // Token: 0x06000192 RID: 402 RVA: 0x00009348 File Offset: 0x00007548
        private void HighlightOnError(TextBox tb)
        {
            if (!ServerProperties.ValidString(tb.Text, "%![]:.,{}~-+()?_/\\^*#@$~`\"'|=;<>& ") || tb.Text.Length > 64)
            {
                tb.Background = new SolidColorBrush(Color.FromArgb(50, 200, 0, 0));
                return;
            }

            tb.ClearValue(BackgroundProperty);
        }

        // Token: 0x06000193 RID: 403 RVA: 0x0000939C File Offset: 0x0000759C
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox) sender;
            HighlightOnError(tb);
        }

        // Token: 0x06000194 RID: 404 RVA: 0x000093B8 File Offset: 0x000075B8
        private void TextBox_Initialized(object sender, EventArgs e)
        {
            var tb = (TextBox) sender;
            HighlightOnError(tb);
        }

        // Token: 0x06000195 RID: 405 RVA: 0x000093D4 File Offset: 0x000075D4
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            try
            {
                var textBox = (TextBox) sender;
                var text = textBox.Text;
                textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                HighlightDuplicateShortcuts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " <> " + ex.StackTrace);
            }
        }

        // Token: 0x06000196 RID: 406 RVA: 0x00009438 File Offset: 0x00007638
        private void HighlightDuplicateShortcuts()
        {
            var enumerable = from ShortcutInfo si in keyboardShortcuts.Items
                where !string.IsNullOrEmpty(si.Shortcut)
                select si;
            var source = (from si in enumerable
                group si by si.Shortcut
                into g
                where g.Count() > 1
                select g).SelectMany(g => g);
            foreach (var shortcutInfo in enumerable)
            {
                var depObj = (ListBoxItem) keyboardShortcuts.ItemContainerGenerator.ContainerFromItem(shortcutInfo);
                foreach (var textBox in FindVisualChildren<TextBox>(depObj))
                    if (textBox.Name == "shortcut")
                    {
                        if (source.Contains(shortcutInfo))
                            textBox.Background = new SolidColorBrush(Color.FromArgb(50, 200, 0, 0));
                        else
                            textBox.ClearValue(BackgroundProperty);
                    }
            }
        }

        // Token: 0x06000197 RID: 407 RVA: 0x000095B8 File Offset: 0x000077B8
        public IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    var child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T) yield return (T) child;
                    foreach (var childOfChild in FindVisualChildren<T>(child)) yield return childOfChild;
                }
        }

        // Token: 0x06000198 RID: 408 RVA: 0x000095DC File Offset: 0x000077DC
        private void shortcut_Initialized(object sender, EventArgs e)
        {
            HighlightDuplicateShortcuts();
        }

        // Token: 0x0600019A RID: 410 RVA: 0x00009614 File Offset: 0x00007814
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        [DebuggerNonUserCode]
        internal Delegate _CreateDelegate(Type delegateType, string handler)
        {
            return Delegate.CreateDelegate(delegateType, this, handler);
        }
    }
}