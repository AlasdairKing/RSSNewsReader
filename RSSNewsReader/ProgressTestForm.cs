using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RSSNewsReader
{
    public partial class ProgressTestForm : Form
    {
        public ProgressTestForm()
        {
            InitializeComponent();
        }

        public void AddText(string s)
        {
            this.txtProgress.Text += s + Environment.NewLine;
            this.txtProgress.SelectionStart = this.txtProgress.Text.Length - 1;
            this.txtProgress.ScrollToCaret();
        }
    }

}
