namespace SkiStuntStudio
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
			this.EditorBox = new System.Windows.Forms.PictureBox();
			this.RenderTimer = new System.Windows.Forms.Timer(this.components);
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.importGNDFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.returnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.goalTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hintTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.backgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.customPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.demoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.playerStartsWithJetpackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.launchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.skiStuntSimulatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.radCrate = new System.Windows.Forms.RadioButton();
			this.radBigRock = new System.Windows.Forms.RadioButton();
			this.radTree = new System.Windows.Forms.RadioButton();
			this.radBigBall = new System.Windows.Forms.RadioButton();
			this.radSnowmanHead = new System.Windows.Forms.RadioButton();
			this.radGiantSpBoard = new System.Windows.Forms.RadioButton();
			this.radSeeSaw = new System.Windows.Forms.RadioButton();
			this.radJetpackCrate = new System.Windows.Forms.RadioButton();
			this.radEdit = new System.Windows.Forms.RadioButton();
			this.button1 = new System.Windows.Forms.Button();
			this.radNopackCrate = new System.Windows.Forms.RadioButton();
			this.mnuObject = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuObject_AlwaysActive = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuObject_Asleep = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuObject_Anchored = new System.Windows.Forms.ToolStripMenuItem();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.lblTitle = new System.Windows.Forms.Label();
			this.mnuEndzone = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuEndzone_Unconcious = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEndzone_Alive = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEndzone_Ground = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEndzone_Skis = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuPolygon = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.selectColorTextureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.solidAtAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.polygonBFmenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.polygonSBmenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.polygonBTFmnuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.polygonSTBmenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.frictionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.frictionToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
			this.bouncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bounceToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
			this.iToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.solidityToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
			((System.ComponentModel.ISupportInitialize)(this.EditorBox)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.mnuObject.SuspendLayout();
			this.mnuEndzone.SuspendLayout();
			this.mnuPolygon.SuspendLayout();
			this.SuspendLayout();
			// 
			// EditorBox
			// 
			this.EditorBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.EditorBox.BackColor = System.Drawing.Color.Black;
			this.EditorBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.EditorBox.Location = new System.Drawing.Point(0, 72);
			this.EditorBox.Name = "EditorBox";
			this.EditorBox.Size = new System.Drawing.Size(834, 493);
			this.EditorBox.TabIndex = 0;
			this.EditorBox.TabStop = false;
			this.EditorBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EditorBox_MouseMove);
			this.EditorBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EditorBox_MouseDown);
			this.EditorBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.EditorBox_MouseUp);
			// 
			// RenderTimer
			// 
			this.RenderTimer.Enabled = true;
			this.RenderTimer.Interval = 30;
			this.RenderTimer.Tick += new System.EventHandler(this.RenderTimer_Tick);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.launchToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.menuStrip1.Size = new System.Drawing.Size(834, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveLevelToolStripMenuItem,
            this.toolStripMenuItem2,
            this.importGNDFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.returnToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// saveLevelToolStripMenuItem
			// 
			this.saveLevelToolStripMenuItem.Name = "saveLevelToolStripMenuItem";
			this.saveLevelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveLevelToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.saveLevelToolStripMenuItem.Text = "&Save Level";
			this.saveLevelToolStripMenuItem.Click += new System.EventHandler(this.saveLevelToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(204, 6);
			// 
			// importGNDFileToolStripMenuItem
			// 
			this.importGNDFileToolStripMenuItem.Name = "importGNDFileToolStripMenuItem";
			this.importGNDFileToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.importGNDFileToolStripMenuItem.Text = "Import GND File...";
			this.importGNDFileToolStripMenuItem.Click += new System.EventHandler(this.importGNDFileToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(204, 6);
			// 
			// returnToolStripMenuItem
			// 
			this.returnToolStripMenuItem.Name = "returnToolStripMenuItem";
			this.returnToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.returnToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.returnToolStripMenuItem.Text = "Return to Level List";
			this.returnToolStripMenuItem.Click += new System.EventHandler(this.returnToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripMenuItem3,
            this.goalTextToolStripMenuItem,
            this.hintTextToolStripMenuItem,
            this.toolStripMenuItem1,
            this.backgroundToolStripMenuItem,
            this.customPreviewToolStripMenuItem,
            this.demoToolStripMenuItem,
            this.playerStartsWithJetpackToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.undoToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.undoToolStripMenuItem.Text = "&Undo";
			this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
			// 
			// redoToolStripMenuItem
			// 
			this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
			this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.redoToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.redoToolStripMenuItem.Text = "Redo";
			this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(166, 6);
			// 
			// goalTextToolStripMenuItem
			// 
			this.goalTextToolStripMenuItem.Name = "goalTextToolStripMenuItem";
			this.goalTextToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.goalTextToolStripMenuItem.Text = "Goal Text";
			this.goalTextToolStripMenuItem.Click += new System.EventHandler(this.goalTextToolStripMenuItem_Click);
			// 
			// hintTextToolStripMenuItem
			// 
			this.hintTextToolStripMenuItem.Name = "hintTextToolStripMenuItem";
			this.hintTextToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.hintTextToolStripMenuItem.Text = "Hint Text";
			this.hintTextToolStripMenuItem.Click += new System.EventHandler(this.hintTextToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(166, 6);
			// 
			// backgroundToolStripMenuItem
			// 
			this.backgroundToolStripMenuItem.Name = "backgroundToolStripMenuItem";
			this.backgroundToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.backgroundToolStripMenuItem.Text = "Custom &Background";
			this.backgroundToolStripMenuItem.Click += new System.EventHandler(this.backgroundToolStripMenuItem_Click);
			// 
			// customPreviewToolStripMenuItem
			// 
			this.customPreviewToolStripMenuItem.Name = "customPreviewToolStripMenuItem";
			this.customPreviewToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.customPreviewToolStripMenuItem.Text = "Custom &Preview";
			this.customPreviewToolStripMenuItem.Click += new System.EventHandler(this.customPreviewToolStripMenuItem_Click);
			// 
			// demoToolStripMenuItem
			// 
			this.demoToolStripMenuItem.Name = "demoToolStripMenuItem";
			this.demoToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.demoToolStripMenuItem.Text = "Specify Demo";
			// 
			// playerStartsWithJetpackToolStripMenuItem
			// 
			this.playerStartsWithJetpackToolStripMenuItem.Checked = true;
			this.playerStartsWithJetpackToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.playerStartsWithJetpackToolStripMenuItem.Name = "playerStartsWithJetpackToolStripMenuItem";
			this.playerStartsWithJetpackToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.playerStartsWithJetpackToolStripMenuItem.Text = "Start with Jetpack";
			this.playerStartsWithJetpackToolStripMenuItem.Click += new System.EventHandler(this.playerStartsWithJetpackToolStripMenuItem_Click);
			// 
			// launchToolStripMenuItem
			// 
			this.launchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.skiStuntSimulatorToolStripMenuItem});
			this.launchToolStripMenuItem.Name = "launchToolStripMenuItem";
			this.launchToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
			this.launchToolStripMenuItem.Text = "Launch";
			// 
			// skiStuntSimulatorToolStripMenuItem
			// 
			this.skiStuntSimulatorToolStripMenuItem.Name = "skiStuntSimulatorToolStripMenuItem";
			this.skiStuntSimulatorToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.skiStuntSimulatorToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
			this.skiStuntSimulatorToolStripMenuItem.Text = "Ski Stunt Simulator";
			this.skiStuntSimulatorToolStripMenuItem.Click += new System.EventHandler(this.skiStuntSimulatorToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.aboutToolStripMenuItem.Text = "About";
			// 
			// radCrate
			// 
			this.radCrate.Appearance = System.Windows.Forms.Appearance.Button;
			this.radCrate.AutoSize = true;
			this.radCrate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.radCrate.Location = new System.Drawing.Point(65, 29);
			this.radCrate.MinimumSize = new System.Drawing.Size(40, 40);
			this.radCrate.Name = "radCrate";
			this.radCrate.Size = new System.Drawing.Size(40, 40);
			this.radCrate.TabIndex = 12;
			this.radCrate.TabStop = true;
			this.radCrate.UseVisualStyleBackColor = true;
			// 
			// radBigRock
			// 
			this.radBigRock.Appearance = System.Windows.Forms.Appearance.Button;
			this.radBigRock.AutoSize = true;
			this.radBigRock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.radBigRock.Location = new System.Drawing.Point(111, 29);
			this.radBigRock.MinimumSize = new System.Drawing.Size(40, 40);
			this.radBigRock.Name = "radBigRock";
			this.radBigRock.Size = new System.Drawing.Size(40, 40);
			this.radBigRock.TabIndex = 13;
			this.radBigRock.TabStop = true;
			this.radBigRock.UseVisualStyleBackColor = true;
			// 
			// radTree
			// 
			this.radTree.Appearance = System.Windows.Forms.Appearance.Button;
			this.radTree.AutoSize = true;
			this.radTree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.radTree.Location = new System.Drawing.Point(157, 29);
			this.radTree.MinimumSize = new System.Drawing.Size(40, 40);
			this.radTree.Name = "radTree";
			this.radTree.Size = new System.Drawing.Size(40, 40);
			this.radTree.TabIndex = 14;
			this.radTree.TabStop = true;
			this.radTree.UseVisualStyleBackColor = true;
			// 
			// radBigBall
			// 
			this.radBigBall.Appearance = System.Windows.Forms.Appearance.Button;
			this.radBigBall.AutoSize = true;
			this.radBigBall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.radBigBall.Location = new System.Drawing.Point(203, 29);
			this.radBigBall.MinimumSize = new System.Drawing.Size(40, 40);
			this.radBigBall.Name = "radBigBall";
			this.radBigBall.Size = new System.Drawing.Size(40, 40);
			this.radBigBall.TabIndex = 15;
			this.radBigBall.TabStop = true;
			this.radBigBall.UseVisualStyleBackColor = true;
			// 
			// radSnowmanHead
			// 
			this.radSnowmanHead.Appearance = System.Windows.Forms.Appearance.Button;
			this.radSnowmanHead.AutoSize = true;
			this.radSnowmanHead.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.radSnowmanHead.Location = new System.Drawing.Point(249, 29);
			this.radSnowmanHead.MinimumSize = new System.Drawing.Size(40, 40);
			this.radSnowmanHead.Name = "radSnowmanHead";
			this.radSnowmanHead.Size = new System.Drawing.Size(40, 40);
			this.radSnowmanHead.TabIndex = 16;
			this.radSnowmanHead.TabStop = true;
			this.radSnowmanHead.UseVisualStyleBackColor = true;
			// 
			// radGiantSpBoard
			// 
			this.radGiantSpBoard.Appearance = System.Windows.Forms.Appearance.Button;
			this.radGiantSpBoard.AutoSize = true;
			this.radGiantSpBoard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.radGiantSpBoard.Location = new System.Drawing.Point(295, 29);
			this.radGiantSpBoard.MinimumSize = new System.Drawing.Size(40, 40);
			this.radGiantSpBoard.Name = "radGiantSpBoard";
			this.radGiantSpBoard.Size = new System.Drawing.Size(40, 40);
			this.radGiantSpBoard.TabIndex = 17;
			this.radGiantSpBoard.TabStop = true;
			this.radGiantSpBoard.UseVisualStyleBackColor = true;
			// 
			// radSeeSaw
			// 
			this.radSeeSaw.Appearance = System.Windows.Forms.Appearance.Button;
			this.radSeeSaw.AutoSize = true;
			this.radSeeSaw.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.radSeeSaw.Location = new System.Drawing.Point(341, 29);
			this.radSeeSaw.MinimumSize = new System.Drawing.Size(40, 40);
			this.radSeeSaw.Name = "radSeeSaw";
			this.radSeeSaw.Size = new System.Drawing.Size(40, 40);
			this.radSeeSaw.TabIndex = 18;
			this.radSeeSaw.TabStop = true;
			this.radSeeSaw.UseVisualStyleBackColor = true;
			// 
			// radJetpackCrate
			// 
			this.radJetpackCrate.Appearance = System.Windows.Forms.Appearance.Button;
			this.radJetpackCrate.AutoSize = true;
			this.radJetpackCrate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.radJetpackCrate.Location = new System.Drawing.Point(387, 29);
			this.radJetpackCrate.MinimumSize = new System.Drawing.Size(40, 40);
			this.radJetpackCrate.Name = "radJetpackCrate";
			this.radJetpackCrate.Size = new System.Drawing.Size(40, 40);
			this.radJetpackCrate.TabIndex = 19;
			this.radJetpackCrate.TabStop = true;
			this.radJetpackCrate.UseVisualStyleBackColor = true;
			// 
			// radEdit
			// 
			this.radEdit.Appearance = System.Windows.Forms.Appearance.Button;
			this.radEdit.AutoSize = true;
			this.radEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.radEdit.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radEdit.Location = new System.Drawing.Point(4, 29);
			this.radEdit.MinimumSize = new System.Drawing.Size(40, 40);
			this.radEdit.Name = "radEdit";
			this.radEdit.Size = new System.Drawing.Size(42, 40);
			this.radEdit.TabIndex = 20;
			this.radEdit.TabStop = true;
			this.radEdit.Text = "Cam";
			this.radEdit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radEdit.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Enabled = false;
			this.button1.Location = new System.Drawing.Point(52, 26);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(5, 46);
			this.button1.TabIndex = 21;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// radNopackCrate
			// 
			this.radNopackCrate.Appearance = System.Windows.Forms.Appearance.Button;
			this.radNopackCrate.AutoSize = true;
			this.radNopackCrate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.radNopackCrate.Location = new System.Drawing.Point(433, 29);
			this.radNopackCrate.MinimumSize = new System.Drawing.Size(40, 40);
			this.radNopackCrate.Name = "radNopackCrate";
			this.radNopackCrate.Size = new System.Drawing.Size(40, 40);
			this.radNopackCrate.TabIndex = 22;
			this.radNopackCrate.TabStop = true;
			this.radNopackCrate.UseVisualStyleBackColor = true;
			// 
			// mnuObject
			// 
			this.mnuObject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuObject_AlwaysActive,
            this.mnuObject_Asleep,
            this.mnuObject_Anchored});
			this.mnuObject.Name = "contextMenuStrip1";
			this.mnuObject.Size = new System.Drawing.Size(142, 70);
			// 
			// mnuObject_AlwaysActive
			// 
			this.mnuObject_AlwaysActive.Name = "mnuObject_AlwaysActive";
			this.mnuObject_AlwaysActive.Size = new System.Drawing.Size(141, 22);
			this.mnuObject_AlwaysActive.Text = "Always Active";
			this.mnuObject_AlwaysActive.Click += new System.EventHandler(this.mnuObject_AlwaysActive_Click);
			// 
			// mnuObject_Asleep
			// 
			this.mnuObject_Asleep.Name = "mnuObject_Asleep";
			this.mnuObject_Asleep.Size = new System.Drawing.Size(141, 22);
			this.mnuObject_Asleep.Text = "Asleep";
			this.mnuObject_Asleep.Click += new System.EventHandler(this.mnuObject_Asleep_Click);
			// 
			// mnuObject_Anchored
			// 
			this.mnuObject_Anchored.Name = "mnuObject_Anchored";
			this.mnuObject_Anchored.Size = new System.Drawing.Size(141, 22);
			this.mnuObject_Anchored.Text = "Anchored";
			this.mnuObject_Anchored.Click += new System.EventHandler(this.mnuObject_Anchored_Click);
			// 
			// txtTitle
			// 
			this.txtTitle.BackColor = System.Drawing.Color.Black;
			this.txtTitle.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTitle.ForeColor = System.Drawing.Color.Lime;
			this.txtTitle.Location = new System.Drawing.Point(522, 29);
			this.txtTitle.MaxLength = 18;
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(141, 20);
			this.txtTitle.TabIndex = 23;
			this.txtTitle.Text = "xxx";
			this.txtTitle.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.Location = new System.Drawing.Point(479, 32);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(42, 14);
			this.lblTitle.TabIndex = 24;
			this.lblTitle.Text = "Title";
			// 
			// mnuEndzone
			// 
			this.mnuEndzone.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEndzone_Unconcious,
            this.mnuEndzone_Alive,
            this.mnuEndzone_Ground,
            this.mnuEndzone_Skis});
			this.mnuEndzone.Name = "mnuEndzone";
			this.mnuEndzone.Size = new System.Drawing.Size(166, 92);
			// 
			// mnuEndzone_Unconcious
			// 
			this.mnuEndzone_Unconcious.Name = "mnuEndzone_Unconcious";
			this.mnuEndzone_Unconcious.Size = new System.Drawing.Size(165, 22);
			this.mnuEndzone_Unconcious.Text = "Unconcious is OK";
			this.mnuEndzone_Unconcious.Click += new System.EventHandler(this.mnuEndzone_Unconcious_Click);
			// 
			// mnuEndzone_Alive
			// 
			this.mnuEndzone_Alive.Name = "mnuEndzone_Alive";
			this.mnuEndzone_Alive.Size = new System.Drawing.Size(165, 22);
			this.mnuEndzone_Alive.Text = "Must Only Be Alive";
			this.mnuEndzone_Alive.Click += new System.EventHandler(this.mnuEndzone_Alive_Click);
			// 
			// mnuEndzone_Ground
			// 
			this.mnuEndzone_Ground.Name = "mnuEndzone_Ground";
			this.mnuEndzone_Ground.Size = new System.Drawing.Size(165, 22);
			this.mnuEndzone_Ground.Text = "Must Be on Ground";
			this.mnuEndzone_Ground.Click += new System.EventHandler(this.mnuEndzone_Ground_Click);
			// 
			// mnuEndzone_Skis
			// 
			this.mnuEndzone_Skis.Name = "mnuEndzone_Skis";
			this.mnuEndzone_Skis.Size = new System.Drawing.Size(165, 22);
			this.mnuEndzone_Skis.Text = "Must Be on Skis";
			this.mnuEndzone_Skis.Click += new System.EventHandler(this.mnuEndzone_Skis_Click);
			// 
			// mnuPolygon
			// 
			this.mnuPolygon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectColorTextureToolStripMenuItem,
            this.toolStripSeparator4,
            this.solidAtAllToolStripMenuItem,
            this.toolStripSeparator2,
            this.polygonBFmenuItem,
            this.polygonSBmenuItem,
            this.polygonBTFmnuItem,
            this.polygonSTBmenuItem,
            this.toolStripSeparator3,
            this.frictionToolStripMenuItem,
            this.frictionToolStripTextBox,
            this.bouncToolStripMenuItem,
            this.bounceToolStripTextBox,
            this.iToolStripMenuItem,
            this.solidityToolStripTextBox});
			this.mnuPolygon.Name = "mnuPolygon";
			this.mnuPolygon.Size = new System.Drawing.Size(177, 286);
			// 
			// selectColorTextureToolStripMenuItem
			// 
			this.selectColorTextureToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.selectColorTextureToolStripMenuItem.Name = "selectColorTextureToolStripMenuItem";
			this.selectColorTextureToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.selectColorTextureToolStripMenuItem.Text = "Modify Color/Texture";
			this.selectColorTextureToolStripMenuItem.Click += new System.EventHandler(this.selectColorTextureToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(173, 6);
			// 
			// solidAtAllToolStripMenuItem
			// 
			this.solidAtAllToolStripMenuItem.Checked = true;
			this.solidAtAllToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.solidAtAllToolStripMenuItem.Name = "solidAtAllToolStripMenuItem";
			this.solidAtAllToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.solidAtAllToolStripMenuItem.Text = "Interacts With Skier";
			this.solidAtAllToolStripMenuItem.Click += new System.EventHandler(this.solidAtAllToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(173, 6);
			// 
			// polygonBFmenuItem
			// 
			this.polygonBFmenuItem.Name = "polygonBFmenuItem";
			this.polygonBFmenuItem.Size = new System.Drawing.Size(176, 22);
			this.polygonBFmenuItem.Text = "Bring Forward";
			this.polygonBFmenuItem.Click += new System.EventHandler(this.polygonBFmenuItem_Click);
			// 
			// polygonSBmenuItem
			// 
			this.polygonSBmenuItem.Name = "polygonSBmenuItem";
			this.polygonSBmenuItem.Size = new System.Drawing.Size(176, 22);
			this.polygonSBmenuItem.Text = "Send Backward";
			this.polygonSBmenuItem.Click += new System.EventHandler(this.polygonSBmenuItem_Click);
			// 
			// polygonBTFmnuItem
			// 
			this.polygonBTFmnuItem.Name = "polygonBTFmnuItem";
			this.polygonBTFmnuItem.Size = new System.Drawing.Size(176, 22);
			this.polygonBTFmnuItem.Text = "Bring To Front";
			this.polygonBTFmnuItem.Click += new System.EventHandler(this.polygonBTFmnuItem_Click);
			// 
			// polygonSTBmenuItem
			// 
			this.polygonSTBmenuItem.Name = "polygonSTBmenuItem";
			this.polygonSTBmenuItem.Size = new System.Drawing.Size(176, 22);
			this.polygonSTBmenuItem.Text = "Send To Back";
			this.polygonSTBmenuItem.Click += new System.EventHandler(this.polygonSTBmenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(173, 6);
			// 
			// frictionToolStripMenuItem
			// 
			this.frictionToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
			this.frictionToolStripMenuItem.Enabled = false;
			this.frictionToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.frictionToolStripMenuItem.Name = "frictionToolStripMenuItem";
			this.frictionToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.frictionToolStripMenuItem.Text = "Friction:";
			// 
			// frictionToolStripTextBox
			// 
			this.frictionToolStripTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.frictionToolStripTextBox.MaxLength = 5;
			this.frictionToolStripTextBox.Name = "frictionToolStripTextBox";
			this.frictionToolStripTextBox.Size = new System.Drawing.Size(100, 20);
			this.frictionToolStripTextBox.Text = "1";
			this.frictionToolStripTextBox.Leave += new System.EventHandler(this.polygonTextBox_Leave);
			this.frictionToolStripTextBox.TextChanged += new System.EventHandler(this.frictionToolStripTextBox_TextChanged);
			// 
			// bouncToolStripMenuItem
			// 
			this.bouncToolStripMenuItem.Enabled = false;
			this.bouncToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bouncToolStripMenuItem.Name = "bouncToolStripMenuItem";
			this.bouncToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.bouncToolStripMenuItem.Text = "Bounce:";
			// 
			// bounceToolStripTextBox
			// 
			this.bounceToolStripTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bounceToolStripTextBox.MaxLength = 5;
			this.bounceToolStripTextBox.Name = "bounceToolStripTextBox";
			this.bounceToolStripTextBox.Size = new System.Drawing.Size(100, 20);
			this.bounceToolStripTextBox.Text = "1";
			this.bounceToolStripTextBox.Leave += new System.EventHandler(this.polygonTextBox_Leave);
			this.bounceToolStripTextBox.TextChanged += new System.EventHandler(this.bounceToolStripTextBox_TextChanged);
			// 
			// iToolStripMenuItem
			// 
			this.iToolStripMenuItem.Enabled = false;
			this.iToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.iToolStripMenuItem.Name = "iToolStripMenuItem";
			this.iToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.iToolStripMenuItem.Text = "Solidity:";
			// 
			// solidityToolStripTextBox
			// 
			this.solidityToolStripTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.solidityToolStripTextBox.MaxLength = 5;
			this.solidityToolStripTextBox.Name = "solidityToolStripTextBox";
			this.solidityToolStripTextBox.Size = new System.Drawing.Size(100, 20);
			this.solidityToolStripTextBox.Text = "1";
			this.solidityToolStripTextBox.Leave += new System.EventHandler(this.polygonTextBox_Leave);
			this.solidityToolStripTextBox.TextChanged += new System.EventHandler(this.solidityToolStripTextBox_TextChanged);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(834, 565);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.radNopackCrate);
			this.Controls.Add(this.txtTitle);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.radEdit);
			this.Controls.Add(this.radJetpackCrate);
			this.Controls.Add(this.radSeeSaw);
			this.Controls.Add(this.radGiantSpBoard);
			this.Controls.Add(this.radSnowmanHead);
			this.Controls.Add(this.radBigBall);
			this.Controls.Add(this.radTree);
			this.Controls.Add(this.radBigRock);
			this.Controls.Add(this.radCrate);
			this.Controls.Add(this.EditorBox);
			this.Controls.Add(this.menuStrip1);
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(700, 200);
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ski Stunt Studio - Level Editor";
			this.Deactivate += new System.EventHandler(this.frmMain_Deactivate);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
			this.Resize += new System.EventHandler(this.frmMain_Resize);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.EditorBox)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.mnuObject.ResumeLayout(false);
			this.mnuEndzone.ResumeLayout(false);
			this.mnuPolygon.ResumeLayout(false);
			this.mnuPolygon.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox EditorBox;
		private System.Windows.Forms.Timer RenderTimer;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveLevelToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem backgroundToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem demoToolStripMenuItem;
		private System.Windows.Forms.RadioButton radCrate;
		private System.Windows.Forms.RadioButton radBigRock;
		private System.Windows.Forms.RadioButton radTree;
		private System.Windows.Forms.RadioButton radBigBall;
		private System.Windows.Forms.RadioButton radSnowmanHead;
		private System.Windows.Forms.RadioButton radGiantSpBoard;
		private System.Windows.Forms.RadioButton radSeeSaw;
		private System.Windows.Forms.RadioButton radJetpackCrate;
		private System.Windows.Forms.RadioButton radEdit;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ToolStripMenuItem playerStartsWithJetpackToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem launchToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem skiStuntSimulatorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem customPreviewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importGNDFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.RadioButton radNopackCrate;
		private System.Windows.Forms.ContextMenuStrip mnuObject;
		private System.Windows.Forms.ToolStripMenuItem mnuObject_AlwaysActive;
		private System.Windows.Forms.ToolStripMenuItem mnuObject_Asleep;
		private System.Windows.Forms.ToolStripMenuItem mnuObject_Anchored;
		private System.Windows.Forms.TextBox txtTitle;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.ContextMenuStrip mnuEndzone;
		private System.Windows.Forms.ToolStripMenuItem mnuEndzone_Unconcious;
		private System.Windows.Forms.ToolStripMenuItem mnuEndzone_Alive;
		private System.Windows.Forms.ToolStripMenuItem mnuEndzone_Ground;
		private System.Windows.Forms.ToolStripMenuItem mnuEndzone_Skis;
		private System.Windows.Forms.ToolStripMenuItem returnToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip mnuPolygon;
		private System.Windows.Forms.ToolStripMenuItem selectColorTextureToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem frictionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem bouncToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem iToolStripMenuItem;
		private System.Windows.Forms.ToolStripTextBox frictionToolStripTextBox;
		private System.Windows.Forms.ToolStripTextBox bounceToolStripTextBox;
		private System.Windows.Forms.ToolStripTextBox solidityToolStripTextBox;
		private System.Windows.Forms.ToolStripMenuItem goalTextToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem hintTextToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem solidAtAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem polygonBTFmnuItem;
		private System.Windows.Forms.ToolStripMenuItem polygonSTBmenuItem;
		private System.Windows.Forms.ToolStripMenuItem polygonBFmenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem polygonSBmenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
	}
}

