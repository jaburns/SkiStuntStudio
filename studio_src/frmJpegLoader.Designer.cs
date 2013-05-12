namespace SkiStuntStudio
{
	partial class frmJpegLoader
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
			this.jpegViewer = new System.Windows.Forms.PictureBox();
			this.btnImport = new System.Windows.Forms.Button();
			this.btnExport = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.lblDefault = new System.Windows.Forms.Label();
			this.btnNoImage = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.jpegViewer)).BeginInit();
			this.SuspendLayout();
			// 
			// jpegViewer
			// 
			this.jpegViewer.BackColor = System.Drawing.Color.Black;
			this.jpegViewer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.jpegViewer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.jpegViewer.Location = new System.Drawing.Point(0, 49);
			this.jpegViewer.Name = "jpegViewer";
			this.jpegViewer.Size = new System.Drawing.Size(512, 256);
			this.jpegViewer.TabIndex = 1;
			this.jpegViewer.TabStop = false;
			// 
			// btnImport
			// 
			this.btnImport.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnImport.Location = new System.Drawing.Point(12, 12);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(154, 26);
			this.btnImport.TabIndex = 2;
			this.btnImport.Text = "Import";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// btnExport
			// 
			this.btnExport.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExport.Location = new System.Drawing.Point(180, 12);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(154, 26);
			this.btnExport.TabIndex = 3;
			this.btnExport.Text = "Export";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(21, 309);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(462, 14);
			this.label1.TabIndex = 4;
			this.label1.Text = "NOTE:  Changes made here cause the level to be saved immediately.";
			// 
			// lblDefault
			// 
			this.lblDefault.AutoSize = true;
			this.lblDefault.BackColor = System.Drawing.Color.Black;
			this.lblDefault.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDefault.ForeColor = System.Drawing.Color.White;
			this.lblDefault.Location = new System.Drawing.Point(145, 158);
			this.lblDefault.Name = "lblDefault";
			this.lblDefault.Size = new System.Drawing.Size(205, 23);
			this.lblDefault.TabIndex = 5;
			this.lblDefault.Text = "[Using Default]";
			// 
			// btnNoImage
			// 
			this.btnNoImage.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNoImage.Location = new System.Drawing.Point(346, 12);
			this.btnNoImage.Name = "btnNoImage";
			this.btnNoImage.Size = new System.Drawing.Size(154, 26);
			this.btnNoImage.TabIndex = 6;
			this.btnNoImage.Text = "Use Default";
			this.btnNoImage.UseVisualStyleBackColor = true;
			this.btnNoImage.Click += new System.EventHandler(this.btnNoImage_Click);
			// 
			// frmJpegLoader
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(512, 329);
			this.Controls.Add(this.btnNoImage);
			this.Controls.Add(this.lblDefault);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnExport);
			this.Controls.Add(this.btnImport);
			this.Controls.Add(this.jpegViewer);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmJpegLoader";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Ski Stunt Studio - Image Loader";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmJpegLoader_FormClosed);
			((System.ComponentModel.ISupportInitialize)(this.jpegViewer)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox jpegViewer;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblDefault;
		private System.Windows.Forms.Button btnNoImage;
	}
}