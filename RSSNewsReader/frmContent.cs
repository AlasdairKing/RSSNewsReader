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
    public partial class frmContent : Form
    {
        public frmContent()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ParseContent(string content)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string tempHTMLFile = System.IO.Path.GetTempFileName();
            System.IO.StreamWriter sw = new System.IO.StreamWriter(tempHTMLFile, false);
            sw.Write("<html>" + content + "</html>");
            sw.Close();
            System.Xml.XmlTextReader xtr = new System.Xml.XmlTextReader(tempHTMLFile);
            bool readOK = true;
            while (readOK)
            {
                try
                {
                    readOK = xtr.Read();
                    if (xtr.NodeType == System.Xml.XmlNodeType.Element)
                    {
                        sb.Append("\r");
                    }
                    else if (xtr.NodeType == System.Xml.XmlNodeType.Text || xtr.NodeType == System.Xml.XmlNodeType.CDATA)
                    {
                        sb.Append(xtr.Value);
                    }
                }
                catch
                {
                    // We care not a fig for parsing errors in HTML! Carry on.
                }
            }
            this.rtbContent.Text = sb.ToString();
        }
    }
}
