namespace Podcatcher
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
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
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
            this.mnuItems = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAudio = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAudioPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAudioStop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAudioPause = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAudioForwards = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAudioRewind = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAudioBack = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAudioIncreasevolume = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAudioDecreasevolume = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpManual = new System.Windows.Forms.ToolStripMenuItem();
            this.openFeedURlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.staMain = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrCheckForNewItems = new System.Windows.Forms.Timer(this.components);
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlSubMain = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.lstItems = new System.Windows.Forms.ListBox();
            this.lblitems = new System.Windows.Forms.Label();
            this.wmp = new AxWMPLib.AxWindowsMediaPlayer();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lstFeeds = new System.Windows.Forms.ListBox();
            this.lblFeeds = new System.Windows.Forms.Label();
            this.tspMain = new System.Windows.Forms.ToolStrip();
            this.btnBack = new System.Windows.Forms.ToolStripButton();
            this.btnRewind = new System.Windows.Forms.ToolStripButton();
            this.btnPlay = new System.Windows.Forms.ToolStripButton();
            this.btnPause = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.btnForward = new System.Windows.Forms.ToolStripButton();
            this.menuStripMain.SuspendLayout();
            this.staMain.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlSubMain.SuspendLayout();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wmp)).BeginInit();
            this.pnlLeft.SuspendLayout();
            this.tspMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStripMain.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuView,
            this.mnuFeeds,
            this.mnuItems,
            this.mnuAudio,
            this.mnuHelp});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(910, 65);
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
            this.mnuFile.Size = new System.Drawing.Size(108, 61);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileImport
            // 
            this.mnuFileImport.Name = "mnuFileImport";
            this.mnuFileImport.Size = new System.Drawing.Size(295, 66);
            this.mnuFileImport.Text = "&Import";
            this.mnuFileImport.Click += new System.EventHandler(this.mnuFileImport_Click);
            // 
            // mnuFileExport
            // 
            this.mnuFileExport.Name = "mnuFileExport";
            this.mnuFileExport.Size = new System.Drawing.Size(295, 66);
            this.mnuFileExport.Text = "&Export";
            this.mnuFileExport.Click += new System.EventHandler(this.mnuFileExport_Click);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(295, 66);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deletedItemsToolStripMenuItem});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(133, 61);
            this.mnuView.Text = "&View";
            // 
            // deletedItemsToolStripMenuItem
            // 
            this.deletedItemsToolStripMenuItem.Name = "deletedItemsToolStripMenuItem";
            this.deletedItemsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.deletedItemsToolStripMenuItem.Size = new System.Drawing.Size(573, 66);
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
            this.mnuFeeds.Size = new System.Drawing.Size(207, 61);
            this.mnuFeeds.Text = "&Podcasts";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(519, 66);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // mnuFeedsNext
            // 
            this.mnuFeedsNext.Name = "mnuFeedsNext";
            this.mnuFeedsNext.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.mnuFeedsNext.Size = new System.Drawing.Size(519, 66);
            this.mnuFeedsNext.Text = "Next";
            this.mnuFeedsNext.Click += new System.EventHandler(this.nextToolStripMenuItem_Click);
            // 
            // mnuFeedsPrevious
            // 
            this.mnuFeedsPrevious.Name = "mnuFeedsPrevious";
            this.mnuFeedsPrevious.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.mnuFeedsPrevious.Size = new System.Drawing.Size(519, 66);
            this.mnuFeedsPrevious.Text = "Previous";
            this.mnuFeedsPrevious.Click += new System.EventHandler(this.previousToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(516, 6);
            // 
            // mnuWebsiteAdd
            // 
            this.mnuWebsiteAdd.Name = "mnuWebsiteAdd";
            this.mnuWebsiteAdd.Size = new System.Drawing.Size(519, 66);
            this.mnuWebsiteAdd.Text = "Add feed";
            this.mnuWebsiteAdd.Click += new System.EventHandler(this.addFeedToolStripMenuItem_Click);
            // 
            // renameFeedToolStripMenuItem
            // 
            this.renameFeedToolStripMenuItem.Enabled = false;
            this.renameFeedToolStripMenuItem.Name = "renameFeedToolStripMenuItem";
            this.renameFeedToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.renameFeedToolStripMenuItem.Size = new System.Drawing.Size(519, 66);
            this.renameFeedToolStripMenuItem.Text = "&Rename feed";
            this.renameFeedToolStripMenuItem.Click += new System.EventHandler(this.renameFeedToolStripMenuItem_Click);
            // 
            // deleteFeedToolStripMenuItem
            // 
            this.deleteFeedToolStripMenuItem.Enabled = false;
            this.deleteFeedToolStripMenuItem.Name = "deleteFeedToolStripMenuItem";
            this.deleteFeedToolStripMenuItem.Size = new System.Drawing.Size(519, 66);
            this.deleteFeedToolStripMenuItem.Text = "Delete feed";
            this.deleteFeedToolStripMenuItem.Click += new System.EventHandler(this.deleteFeedToolStripMenuItem_Click);
            // 
            // openFeedWebsiteToolStripMenuItem
            // 
            this.openFeedWebsiteToolStripMenuItem.Name = "openFeedWebsiteToolStripMenuItem";
            this.openFeedWebsiteToolStripMenuItem.Size = new System.Drawing.Size(519, 66);
            this.openFeedWebsiteToolStripMenuItem.Text = "Open feed website";
            this.openFeedWebsiteToolStripMenuItem.Click += new System.EventHandler(this.openFeedWebsiteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(516, 6);
            // 
            // mnuFeedsDeleteallwebsitefeeds
            // 
            this.mnuFeedsDeleteallwebsitefeeds.Name = "mnuFeedsDeleteallwebsitefeeds";
            this.mnuFeedsDeleteallwebsitefeeds.Size = new System.Drawing.Size(519, 66);
            this.mnuFeedsDeleteallwebsitefeeds.Text = "Delete all feeds";
            this.mnuFeedsDeleteallwebsitefeeds.Click += new System.EventHandler(this.mnuWebsiteDeleteallwebsitefeeds_Click);
            // 
            // mnuItems
            // 
            this.mnuItems.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.deleteAllToolStripMenuItem});
            this.mnuItems.Name = "mnuItems";
            this.mnuItems.Size = new System.Drawing.Size(146, 61);
            this.mnuItems.Text = "&Items";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(347, 66);
            this.deleteToolStripMenuItem.Text = "&Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // deleteAllToolStripMenuItem
            // 
            this.deleteAllToolStripMenuItem.Name = "deleteAllToolStripMenuItem";
            this.deleteAllToolStripMenuItem.Size = new System.Drawing.Size(347, 66);
            this.deleteAllToolStripMenuItem.Text = "Delete All";
            this.deleteAllToolStripMenuItem.Click += new System.EventHandler(this.deleteAllToolStripMenuItem_Click_1);
            // 
            // mnuAudio
            // 
            this.mnuAudio.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAudioPlay,
            this.mnuAudioStop,
            this.mnuAudioPause,
            this.mnuAudioForwards,
            this.mnuAudioRewind,
            this.mnuAudioBack,
            this.mnuAudioIncreasevolume,
            this.mnuAudioDecreasevolume});
            this.mnuAudio.Name = "mnuAudio";
            this.mnuAudio.Size = new System.Drawing.Size(156, 61);
            this.mnuAudio.Text = "&Audio";
            // 
            // mnuAudioPlay
            // 
            this.mnuAudioPlay.Enabled = false;
            this.mnuAudioPlay.Name = "mnuAudioPlay";
            this.mnuAudioPlay.Size = new System.Drawing.Size(489, 66);
            this.mnuAudioPlay.Text = "Play";
            this.mnuAudioPlay.Click += new System.EventHandler(this.MnuAudioPlay_Click);
            // 
            // mnuAudioStop
            // 
            this.mnuAudioStop.Enabled = false;
            this.mnuAudioStop.Name = "mnuAudioStop";
            this.mnuAudioStop.Size = new System.Drawing.Size(489, 66);
            this.mnuAudioStop.Text = "Stop";
            this.mnuAudioStop.Click += new System.EventHandler(this.MnuAudioStop_Click);
            // 
            // mnuAudioPause
            // 
            this.mnuAudioPause.Enabled = false;
            this.mnuAudioPause.Name = "mnuAudioPause";
            this.mnuAudioPause.Size = new System.Drawing.Size(489, 66);
            this.mnuAudioPause.Text = "Pause";
            this.mnuAudioPause.Click += new System.EventHandler(this.MnuAudioPause_Click);
            // 
            // mnuAudioForwards
            // 
            this.mnuAudioForwards.Enabled = false;
            this.mnuAudioForwards.Name = "mnuAudioForwards";
            this.mnuAudioForwards.Size = new System.Drawing.Size(489, 66);
            this.mnuAudioForwards.Text = "Skip Forwards";
            this.mnuAudioForwards.Click += new System.EventHandler(this.MnuAudioForwards_Click);
            // 
            // mnuAudioRewind
            // 
            this.mnuAudioRewind.Enabled = false;
            this.mnuAudioRewind.Name = "mnuAudioRewind";
            this.mnuAudioRewind.Size = new System.Drawing.Size(489, 66);
            this.mnuAudioRewind.Text = "Skip Backwards";
            this.mnuAudioRewind.Click += new System.EventHandler(this.MnuAudioRewind_Click);
            // 
            // mnuAudioBack
            // 
            this.mnuAudioBack.Enabled = false;
            this.mnuAudioBack.Name = "mnuAudioBack";
            this.mnuAudioBack.Size = new System.Drawing.Size(489, 66);
            this.mnuAudioBack.Text = "Back";
            this.mnuAudioBack.Click += new System.EventHandler(this.MnuAudioBack_Click);
            // 
            // mnuAudioIncreasevolume
            // 
            this.mnuAudioIncreasevolume.Name = "mnuAudioIncreasevolume";
            this.mnuAudioIncreasevolume.Size = new System.Drawing.Size(489, 66);
            this.mnuAudioIncreasevolume.Text = "Increase Volume";
            this.mnuAudioIncreasevolume.Click += new System.EventHandler(this.MnuAudioIncreasevolume_Click);
            // 
            // mnuAudioDecreasevolume
            // 
            this.mnuAudioDecreasevolume.Name = "mnuAudioDecreasevolume";
            this.mnuAudioDecreasevolume.Size = new System.Drawing.Size(489, 66);
            this.mnuAudioDecreasevolume.Text = "Decrease Volume";
            this.mnuAudioDecreasevolume.Click += new System.EventHandler(this.MnuAudioDecreasevolume_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpManual,
            this.openFeedURlToolStripMenuItem,
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(132, 61);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpManual
            // 
            this.mnuHelpManual.Name = "mnuHelpManual";
            this.mnuHelpManual.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.mnuHelpManual.Size = new System.Drawing.Size(452, 66);
            this.mnuHelpManual.Text = "&Manual";
            this.mnuHelpManual.Click += new System.EventHandler(this.mnuHelpManual_Click);
            // 
            // openFeedURlToolStripMenuItem
            // 
            this.openFeedURlToolStripMenuItem.Name = "openFeedURlToolStripMenuItem";
            this.openFeedURlToolStripMenuItem.Size = new System.Drawing.Size(452, 66);
            this.openFeedURlToolStripMenuItem.Text = "Open feed URL";
            this.openFeedURlToolStripMenuItem.Click += new System.EventHandler(this.openFeedURlToolStripMenuItem_Click);
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(452, 66);
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
            this.lblStatus.Size = new System.Drawing.Size(895, 12);
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
            this.pnlMain.Controls.Add(this.pnlSubMain);
            this.pnlMain.Controls.Add(this.tspMain);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 65);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(910, 634);
            this.pnlMain.TabIndex = 2;
            // 
            // pnlSubMain
            // 
            this.pnlSubMain.Controls.Add(this.pnlRight);
            this.pnlSubMain.Controls.Add(this.pnlLeft);
            this.pnlSubMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSubMain.Location = new System.Drawing.Point(0, 92);
            this.pnlSubMain.Name = "pnlSubMain";
            this.pnlSubMain.Size = new System.Drawing.Size(910, 542);
            this.pnlSubMain.TabIndex = 11;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.lstItems);
            this.pnlRight.Controls.Add(this.lblitems);
            this.pnlRight.Controls.Add(this.wmp);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(422, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(488, 542);
            this.pnlRight.TabIndex = 1;
            // 
            // lstItems
            // 
            this.lstItems.AccessibleName = "";
            this.lstItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstItems.FormattingEnabled = true;
            this.lstItems.IntegralHeight = false;
            this.lstItems.ItemHeight = 57;
            this.lstItems.Location = new System.Drawing.Point(0, 57);
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(488, 485);
            this.lstItems.TabIndex = 9;
            this.lstItems.Click += new System.EventHandler(this.lstItems_Click);
            this.lstItems.SelectedIndexChanged += new System.EventHandler(this.LstItems_SelectedIndexChanged_1);
            this.lstItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstItems_KeyDown);
            this.lstItems.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstItems_KeyUp);
            // 
            // lblitems
            // 
            this.lblitems.AutoSize = true;
            this.lblitems.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblitems.Location = new System.Drawing.Point(0, 0);
            this.lblitems.Name = "lblitems";
            this.lblitems.Size = new System.Drawing.Size(155, 57);
            this.lblitems.TabIndex = 8;
            this.lblitems.Text = "&Results";
            // 
            // wmp
            // 
            this.wmp.Enabled = true;
            this.wmp.Location = new System.Drawing.Point(-123, 134);
            this.wmp.Name = "wmp";
            this.wmp.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmp.OcxState")));
            this.wmp.Size = new System.Drawing.Size(183, 144);
            this.wmp.TabIndex = 3;
            this.wmp.TabStop = false;
            this.wmp.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.Wmp_PlayStateChange);
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.lstFeeds);
            this.pnlLeft.Controls.Add(this.lblFeeds);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(422, 542);
            this.pnlLeft.TabIndex = 0;
            // 
            // lstFeeds
            // 
            this.lstFeeds.AccessibleName = "";
            this.lstFeeds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFeeds.FormattingEnabled = true;
            this.lstFeeds.IntegralHeight = false;
            this.lstFeeds.ItemHeight = 57;
            this.lstFeeds.Location = new System.Drawing.Point(0, 57);
            this.lstFeeds.Name = "lstFeeds";
            this.lstFeeds.Size = new System.Drawing.Size(422, 485);
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
            this.lblFeeds.Size = new System.Drawing.Size(179, 57);
            this.lblFeeds.TabIndex = 4;
            this.lblFeeds.Text = "Feed &list";
            // 
            // tspMain
            // 
            this.tspMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tspMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBack,
            this.btnRewind,
            this.btnPlay,
            this.btnPause,
            this.btnStop,
            this.btnForward});
            this.tspMain.Location = new System.Drawing.Point(0, 0);
            this.tspMain.Name = "tspMain";
            this.tspMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspMain.Size = new System.Drawing.Size(910, 92);
            this.tspMain.TabIndex = 2;
            // 
            // btnBack
            // 
            this.btnBack.Enabled = false;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Image = global::Podcatcher.Properties.Resources.player_start;
            this.btnBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(101, 86);
            this.btnBack.Text = "Back";
            this.btnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnRewind
            // 
            this.btnRewind.Enabled = false;
            this.btnRewind.Font = new System.Drawing.Font("Segoe UI", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRewind.Image = global::Podcatcher.Properties.Resources.player_rew;
            this.btnRewind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRewind.Name = "btnRewind";
            this.btnRewind.Size = new System.Drawing.Size(145, 86);
            this.btnRewind.Text = "Rewind";
            this.btnRewind.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRewind.Click += new System.EventHandler(this.BtnRewind_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Enabled = false;
            this.btnPlay.Font = new System.Drawing.Font("Segoe UI", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.Image = global::Podcatcher.Properties.Resources.player_play;
            this.btnPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(93, 86);
            this.btnPlay.Text = "Play";
            this.btnPlay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Font = new System.Drawing.Font("Segoe UI", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.Image = global::Podcatcher.Properties.Resources.player_pause;
            this.btnPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(121, 86);
            this.btnPause.Text = "Pause";
            this.btnPause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPause.Click += new System.EventHandler(this.BtnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Image = global::Podcatcher.Properties.Resources.player_stop;
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(102, 86);
            this.btnStop.Text = "Stop";
            this.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // btnForward
            // 
            this.btnForward.Enabled = false;
            this.btnForward.Font = new System.Drawing.Font("Segoe UI", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForward.Image = global::Podcatcher.Properties.Resources.player_fwd;
            this.btnForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(160, 86);
            this.btnForward.Text = "Forward";
            this.btnForward.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnForward.Click += new System.EventHandler(this.BtnForward_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(23F, 57F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 721);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.staMain);
            this.Controls.Add(this.menuStripMain);
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "frmMain";
            this.Text = "Podcatcher";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.staMain.ResumeLayout(false);
            this.staMain.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlSubMain.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wmp)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.tspMain.ResumeLayout(false);
            this.tspMain.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem mnuItems;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
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
        private System.Windows.Forms.ToolStrip tspMain;
        private System.Windows.Forms.ToolStripButton btnBack;
        private System.Windows.Forms.ToolStripButton btnRewind;
        private System.Windows.Forms.ToolStripButton btnPause;
        private System.Windows.Forms.Panel pnlSubMain;
        private System.Windows.Forms.ToolStripButton btnPlay;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.ToolStripButton btnForward;
        private AxWMPLib.AxWindowsMediaPlayer wmp;
        private System.Windows.Forms.ToolStripMenuItem mnuAudio;
        private System.Windows.Forms.ToolStripMenuItem mnuAudioPlay;
        private System.Windows.Forms.ToolStripMenuItem mnuAudioStop;
        private System.Windows.Forms.ToolStripMenuItem mnuAudioPause;
        private System.Windows.Forms.ToolStripMenuItem mnuAudioForwards;
        private System.Windows.Forms.ToolStripMenuItem mnuAudioRewind;
        private System.Windows.Forms.ToolStripMenuItem mnuAudioIncreasevolume;
        private System.Windows.Forms.ToolStripMenuItem mnuAudioDecreasevolume;
        private System.Windows.Forms.ToolStripMenuItem mnuAudioBack;
    }
}

