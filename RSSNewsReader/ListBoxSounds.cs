using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSSNewsReader
{
    /// <summary>
    /// Provides ways to add accessible sounds to listboxes.
    /// Requires Microsoft.VisualBasic references.
    /// </summary>
    class ListBoxSounds
    {
        public static void AddSounds(System.Windows.Forms.Form f)
        {
            foreach (System.Windows.Forms.Control c in f.Controls)
            {
                if (c.GetType().ToString() == "ListBox")
                {
                    c.KeyUp += new System.Windows.Forms.KeyEventHandler(c_KeyUp);
                }
            }
        }

        static void c_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Up)
            {
                if (((System.Windows.Forms.ListBox)sender).SelectedIndex == 0)
                {
                    Microsoft.VisualBasic.Interaction.Beep();
                }
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.Down)
            {
                System.Windows.Forms.ListBox lb = (System.Windows.Forms.ListBox)sender;
                if (lb.SelectedIndex == lb.Items.Count - 1 || lb.Items.Count == 0)
                {
                    Microsoft.VisualBasic.Interaction.Beep();
                }
            }
        }
    }
}
