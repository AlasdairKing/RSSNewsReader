namespace RSSNewsReader
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletedItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFeeds = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFeedsNext = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFeedsPrevious = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuWebsiteAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.renameFeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFeedWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFeedsDeleteallwebsitefeeds = new System.Windows.Forms.ToolStripMenuItem();
            this.itemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpManual = new System.Windows.Forms.ToolStripMenuItem();
            this.openFeedURlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.staMain = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrCheckForNewItems = new System.Windows.Forms.Timer(this.components);
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.lstItems = new System.Windows.Forms.ListBox();
            this.lblitems = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lstFeeds = new System.Windows.Forms.ListBox();
            this.lblFeeds = new System.Windows.Forms.Label();
            this.menuStripMain.SuspendLayout();
            this.staMain.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStripMain.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.viewToolStripMenuItem,
            this.mnuFeeds,
            this.itemsToolStripMenuItem,
            this.mnuHelp});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(910, 53);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileImport,
            this.mnuFileExport,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(85, 49);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileImport
            // 
            this.mnuFileImport.Name = "mnuFileImport";
            this.mnuFileImport.Size = new System.Drawing.Size(227, 54);
            this.mnuFileImport.Text = "&Import";
            this.mnuFileImport.Click += new System.EventHandler(this.mnuFileImport_Click);
            // 
            // mnuFileExport
            // 
            this.mnuFileExport.Name = "mnuFileExport";
            this.mnuFileExport.Size = new System.Drawing.Size(227, 54);
            this.mnuFileExport.Text = "&Export";
            this.mnuFileExport.Click += new System.EventHandler(this.mnuFileExport_Click);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(227, 54);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deletedItemsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(104, 49);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // deletedItemsToolStripMenuItem
            // 
            this.deletedItemsToolStripMenuItem.Name = "deletedItemsToolStripMenuItem";
            this.deletedItemsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.deletedItemsToolStripMenuItem.Size = new System.Drawing.Size(442, 54);
            this.deletedItemsToolStripMenuItem.Text = "&Deleted items";
            this.deletedItemsToolStripMenuItem.Click += new System.EventHandler(this.deletedItemsToolStripMenuItem_Click);
            // 
            // mnuFeeds
            // 
            this.mnuFeeds.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.mnuFeedsNext,
            this.mnuFeedsPrevious,
            this.toolStripMenuItem1,
            this.mnuWebsiteAdd,
            this.renameFeedToolStripMenuItem,
            this.deleteFeedToolStripMenuItem,
            this.openFeedWebsiteToolStripMenuItem,
            this.toolStripMenuItem2,
            this.mnuFeedsDeleteallwebsitefeeds});
            this.mnuFeeds.Name = "mnuFeeds";
            this.mnuFeeds.Size = new System.Drawing.Size(119, 49);
            this.mnuFeeds.Text = "F&eeds";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(398, 54);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // mnuFeedsNext
            // 
            this.mnuFeedsNext.Name = "mnuFeedsNext";
            this.mnuFeedsNext.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.mnuFeedsNext.Size = new System.Drawing.Size(398, 54);
            this.mnuFeedsNext.Text = "Next";
            this.mnuFeedsNext.Click += new System.EventHandler(this.nextToolStripMenuItem_Click);
            // 
            // mnuFeedsPrevious
            // 
            this.mnuFeedsPrevious.Name = "mnuFeedsPrevious";
            this.mnuFeedsPrevious.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.mnuFeedsPrevious.Size = new System.Drawing.Size(398, 54);
            this.mnuFeedsPrevious.Text = "Previous";
            this.mnuFeedsPrevious.Click += new System.EventHandler(this.previousToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(395, 6);
            // 
            // mnuWebsiteAdd
            // 
            this.mnuWebsiteAdd.Name = "mnuWebsiteAdd";
            this.mnuWebsiteAdd.Size = new System.Drawing.Size(398, 54);
            this.mnuWebsiteAdd.Text = "Add feed";
            this.mnuWebsiteAdd.Click += new System.EventHandler(this.addFeedToolStripMenuItem_Click);
            // 
            // renameFeedToolStripMenuItem
            // 
            this.renameFeedToolStripMenuItem.Enabled = false;
            this.renameFeedToolStripMenuItem.Name = "renameFeedToolStripMenuItem";
            this.renameFeedToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.renameFeedToolStripMenuItem.Size = new System.Drawing.Size(398, 54);
            this.renameFeedToolStripMenuItem.Text = "&Rename feed";
            this.renameFeedToolStripMenuItem.Click += new System.EventHandler(this.renameFeedToolStripMenuItem_Click);
            // 
            // deleteFeedToolStripMenuItem
            // 
            this.deleteFeedToolStripMenuItem.Enabled = false;
            this.deleteFeedToolStripMenuItem.Name = "deleteFeedToolStripMenuItem";
            this.deleteFeedToolStripMenuItem.Size = new System.Drawing.Size(398, 54);
            this.deleteFeedToolStripMenuItem.Text = "Delete feed";
            this.deleteFeedToolStripMenuItem.Click += new System.EventHandler(this.deleteFeedToolStripMenuItem_Click);
            // 
            // openFeedWebsiteToolStripMenuItem
            // 
            this.openFeedWebsiteToolStripMenuItem.Name = "openFeedWebsiteToolStripMenuItem";
            this.openFeedWebsiteToolStripMenuItem.Size = new System.Drawing.Size(398, 54);
            this.openFeedWebsiteToolStripMenuItem.Text = "Open feed website";
            this.openFeedWebsiteToolStripMenuItem.Click += new System.EventHandler(this.openFeedWebsiteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(395, 6);
            // 
            // mnuFeedsDeleteallwebsitefeeds
            // 
            this.mnuFeedsDeleteallwebsitefeeds.Name = "mnuFeedsDeleteallwebsitefeeds";
            this.mnuFeedsDeleteallwebsitefeeds.Size = new System.Drawing.Size(398, 54);
            this.mnuFeedsDeleteallwebsitefeeds.Text = "Delete all feeds";
            this.mnuFeedsDeleteallwebsitefeeds.Click += new System.EventHandler(this.mnuWebsiteDeleteallwebsitefeeds_Click);
            // 
            // itemsToolStripMenuItem
            // 
            this.itemsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.deleteAllToolStripMenuItem});
            this.itemsToolStripMenuItem.Name = "itemsToolStripMenuItem";
            this.itemsToolStripMenuItem.Size = new System.Drawing.Size(115, 49);
            this.itemsToolStripMenuItem.Text = "&Items";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(268, 54);
            this.deleteToolStripMenuItem.Text = "&Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // deleteAllToolStripMenuItem
            // 
            this.deleteAllToolStripMenuItem.Name = "deleteAllToolStripMenuItem";
            this.deleteAllToolStripMenuItem.Size = new System.Drawing.Size(268, 54);
            this.deleteAllToolStripMenuItem.Text = "Delete All";
            this.deleteAllToolStripMenuItem.Click += new System.EventHandler(this.deleteAllToolStripMenuItem_Click_1);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpManual,
            this.openFeedURlToolStripMenuItem,
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(103, 49);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpManual
            // 
            this.mnuHelpManual.Name = "mnuHelpManual";
            this.mnuHelpManual.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.mnuHelpManual.Size = new System.Drawing.Size(345, 54);
            this.mnuHelpManual.Text = "&Manual";
            this.mnuHelpManual.Click += new System.EventHandler(this.mnuHelpManual_Click);
            // 
            // openFeedURlToolStripMenuItem
            // 
            this.openFeedURlToolStripMenuItem.Name = "openFeedURlToolStripMenuItem";
            this.openFeedURlToolStripMenuItem.Size = new System.Drawing.Size(345, 54);
            this.openFeedURlToolStripMenuItem.Text = "Open feed URL";
            this.openFeedURlToolStripMenuItem.Click += new System.EventHandler(this.openFeedURlToolStripMenuItem_Click);
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(345, 54);
            this.mnuHelpAbout.Text = "&About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // staMain
            // 
            this.staMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.staMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.staMain.Location = new System.Drawing.Point(0, 699);
            this.staMain.Name = "staMain";
            this.staMain.Size = new System.Drawing.Size(910, 22);
            this.staMain.TabIndex = 1;
            this.staMain.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.lblStatus.Size = new System.Drawing.Size(895, 15);
            this.lblStatus.Spring = true;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tmrCheckForNewItems
            // 
            this.tmrCheckForNewItems.Enabled = true;
            this.tmrCheckForNewItems.Interval = 10000;
            this.tmrCheckForNewItems.Tick += new System.EventHandler(this.tmrCheckForNewItems_Tick);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlRight);
            this.pnlMain.Controls.Add(this.pnlLeft);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 53);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(910, 646);
            this.pnlMain.TabIndex = 2;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.lstItems);
            this.pnlRight.Controls.Add(this.lblitems);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(422, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(488, 646);
            this.pnlRight.TabIndex = 1;
            // 
            // lstItems
            // 
            this.lstItems.AccessibleName = "";
            this.lstItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstItems.FormattingEnabled = true;
            this.lstItems.IntegralHeight = false;
            this.lstItems.ItemHeight = 45;
            this.lstItems.Location = new System.Drawing.Point(0, 45);
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(488, 601);
            this.lstItems.TabIndex = 9;
            this.lstItems.Click += new System.EventHandler(this.lstItems_Click);
            this.lstItems.SelectedIndexChanged += new System.EventHandler(this.lstItems_SelectedIndexChanged_1);
            this.lstItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstItems_KeyDown);
            this.lstItems.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstItems_KeyUp);
            // 
            // lblitems
            // 
            this.lblitems.AutoSize = true;
            this.lblitems.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblitems.Location = new System.Drawing.Point(0, 0);
            this.lblitems.Name = "lblitems";
            this.lblitems.Size = new System.Drawing.Size(120, 45);
            this.lblitems.TabIndex = 8;
            this.lblitems.Text = "&Results";
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.lstFeeds);
            this.pnlLeft.Controls.Add(this.lblFeeds);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(422, 646);
            this.pnlLeft.TabIndex = 0;
            // 
            // lstFeeds
            // 
            this.lstFeeds.AccessibleName = "";
            this.lstFeeds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFeeds.FormattingEnabled = true;
            this.lstFeeds.IntegralHeight = false;
            this.lstFeeds.ItemHeight = 45;
            this.lstFeeds.Location = new System.Drawing.Point(0, 45);
            this.lstFeeds.Name = "lstFeeds";
            this.lstFeeds.Size = new System.Drawing.Size(422, 601);
            this.lstFeeds.TabIndex = 5;
            this.lstFeeds.Click += new System.EventHandler(this.lstFeeds_Click);
            this.lstFeeds.SelectedIndexChanged += new System.EventHandler(this.lstFeeds_SelectedIndexChanged);
            this.lstFeeds.DoubleClick += new System.EventHandler(this.lstFeeds_DoubleClick);
            this.lstFeeds.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstFeeds_KeyDown);
            // 
            // lblFeeds
            // 
            this.lblFeeds.AutoSize = true;
            this.lblFeeds.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFeeds.Location = new System.Drawing.Point(0, 0);
            this.lblFeeds.Name = "lblFeeds";
            this.lblFeeds.Size = new System.Drawing.Size(139, 45);
            this.lblFeeds.TabIndex = 4;
            this.lblFeeds.Text = "Feed &list";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(18F, 45F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 721);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.staMain);
            this.Controls.Add(this.menuStripMain);
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "frmMain";
            this.Text = "RSS News Reader";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.staMain.ResumeLayout(false);
            this.staMain.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpManual;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.StatusStrip staMain;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripMenuItem mnuFileImport;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExport;
        private System.Windows.Forms.ToolStripMenuItem mnuFeeds;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuFeedsNext;
        private System.Windows.Forms.ToolStripMenuItem mnuFeedsPrevious;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuWebsiteAdd;
        private System.Windows.Forms.ToolStripMenuItem deleteFeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFeedWebsiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletedItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuFeedsDeleteallwebsitefeeds;
        private System.Windows.Forms.ToolStripMenuItem renameFeedToolStripMenuItem;
        private System.Windows.Forms.Timer tmrCheckForNewItems;
        private System.Windows.Forms.ToolStripMenuItem openFeedURlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllToolStripMenuItem;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.ListBox lstItems;
        private System.Windows.Forms.Label lblitems;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.ListBox lstFeeds;
        private System.Windows.Forms.Label lblFeeds;
    }
}

