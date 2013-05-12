namespace SkiStuntStudio
{
	partial class frmLevelManager
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			this.levelGrid = new System.Windows.Forms.DataGridView();
			this.colLevelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnNew = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnExport = new System.Windows.Forms.Button();
			this.btnImport = new System.Windows.Forms.Button();
			this.btnLaunchSkiStunt = new System.Windows.Forms.Button();
			this.picPreview = new System.Windows.Forms.PictureBox();
			this.btnOpenFolder = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.levelGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
			this.SuspendLayout();
			// 
			// levelGrid
			// 
			this.levelGrid.AllowUserToAddRows = false;
			this.levelGrid.AllowUserToDeleteRows = false;
			this.levelGrid.AllowUserToResizeColumns = false;
			this.levelGrid.AllowUserToResizeRows = false;
			dataGridViewCellStyle10.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.levelGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
			this.levelGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle11.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.levelGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
			this.levelGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.levelGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLevelName});
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle12.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.levelGrid.DefaultCellStyle = dataGridViewCellStyle12;
			this.levelGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.levelGrid.Location = new System.Drawing.Point(3, 5);
			this.levelGrid.MultiSelect = false;
			this.levelGrid.Name = "levelGrid";
			this.levelGrid.ReadOnly = true;
			this.levelGrid.RowHeadersVisible = false;
			this.levelGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.levelGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.levelGrid.Size = new System.Drawing.Size(178, 256);
			this.levelGrid.TabIndex = 0;
			this.levelGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.levelGrid_CellEnter);
			// 
			// colLevelName
			// 
			this.colLevelName.HeaderText = "Level Name";
			this.colLevelName.Name = "colLevelName";
			this.colLevelName.ReadOnly = true;
			this.colLevelName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.colLevelName.Width = 150;
			// 
			// btnEdit
			// 
			this.btnEdit.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnEdit.Location = new System.Drawing.Point(67, 266);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(58, 23);
			this.btnEdit.TabIndex = 1;
			this.btnEdit.Text = "Edit";
			this.btnEdit.UseVisualStyleBackColor = true;
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnNew
			// 
			this.btnNew.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNew.Location = new System.Drawing.Point(3, 266);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(58, 23);
			this.btnNew.TabIndex = 2;
			this.btnNew.Text = "New";
			this.btnNew.UseVisualStyleBackColor = true;
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDelete.Location = new System.Drawing.Point(259, 266);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(58, 23);
			this.btnDelete.TabIndex = 3;
			this.btnDelete.Text = "Delete";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnExport
			// 
			this.btnExport.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExport.Location = new System.Drawing.Point(195, 266);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(58, 23);
			this.btnExport.TabIndex = 4;
			this.btnExport.Text = "Export";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// btnImport
			// 
			this.btnImport.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnImport.Location = new System.Drawing.Point(131, 266);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(58, 23);
			this.btnImport.TabIndex = 5;
			this.btnImport.Text = "Import";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// btnLaunchSkiStunt
			// 
			this.btnLaunchSkiStunt.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLaunchSkiStunt.Location = new System.Drawing.Point(425, 266);
			this.btnLaunchSkiStunt.Name = "btnLaunchSkiStunt";
			this.btnLaunchSkiStunt.Size = new System.Drawing.Size(197, 23);
			this.btnLaunchSkiStunt.TabIndex = 6;
			this.btnLaunchSkiStunt.Text = "Launch Ski Stunt Simulator";
			this.btnLaunchSkiStunt.UseVisualStyleBackColor = true;
			this.btnLaunchSkiStunt.Click += new System.EventHandler(this.btnLaunchSkiStunt_Click);
			// 
			// picPreview
			// 
			this.picPreview.BackColor = System.Drawing.Color.Black;
			this.picPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.picPreview.Location = new System.Drawing.Point(187, 5);
			this.picPreview.Name = "picPreview";
			this.picPreview.Size = new System.Drawing.Size(435, 256);
			this.picPreview.TabIndex = 7;
			this.picPreview.TabStop = false;
			// 
			// btnOpenFolder
			// 
			this.btnOpenFolder.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOpenFolder.Location = new System.Drawing.Point(323, 266);
			this.btnOpenFolder.Name = "btnOpenFolder";
			this.btnOpenFolder.Size = new System.Drawing.Size(96, 23);
			this.btnOpenFolder.TabIndex = 8;
			this.btnOpenFolder.Text = "Open Folder";
			this.btnOpenFolder.UseVisualStyleBackColor = true;
			this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
			// 
			// frmLevelManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(625, 293);
			this.Controls.Add(this.btnOpenFolder);
			this.Controls.Add(this.picPreview);
			this.Controls.Add(this.btnLaunchSkiStunt);
			this.Controls.Add(this.btnImport);
			this.Controls.Add(this.btnExport);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnNew);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.levelGrid);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmLevelManager";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ski Stunt Studio";
			((System.ComponentModel.ISupportInitialize)(this.levelGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView levelGrid;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Button btnLaunchSkiStunt;
		private System.Windows.Forms.DataGridViewTextBoxColumn colLevelName;
		private System.Windows.Forms.PictureBox picPreview;
		private System.Windows.Forms.Button btnOpenFolder;
	}
}