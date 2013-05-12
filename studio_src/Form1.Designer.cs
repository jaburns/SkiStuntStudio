namespace SkiStuntStudio
{
	partial class frmTextureSelect
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
			this.picShowTex = new System.Windows.Forms.PictureBox();
			this.radColor = new System.Windows.Forms.RadioButton();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.radTexture = new System.Windows.Forms.RadioButton();
			this.btnImport = new System.Windows.Forms.Button();
			this.btnExport = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnLeft = new System.Windows.Forms.Button();
			this.btnRight = new System.Windows.Forms.Button();
			this.btnSelectColor = new System.Windows.Forms.Button();
			this.txtOffsetY = new System.Windows.Forms.TextBox();
			this.txtOffsetX = new System.Windows.Forms.TextBox();
			this.txtScaleX = new System.Windows.Forms.TextBox();
			this.txtScaleY = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.picShowTex)).BeginInit();
			this.SuspendLayout();
			// 
			// picShowTex
			// 
			this.picShowTex.BackColor = System.Drawing.Color.White;
			this.picShowTex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.picShowTex.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.picShowTex.Location = new System.Drawing.Point(3, 5);
			this.picShowTex.Name = "picShowTex";
			this.picShowTex.Size = new System.Drawing.Size(256, 256);
			this.picShowTex.TabIndex = 0;
			this.picShowTex.TabStop = false;
			// 
			// radColor
			// 
			this.radColor.AutoSize = true;
			this.radColor.Location = new System.Drawing.Point(9, 271);
			this.radColor.Name = "radColor";
			this.radColor.Size = new System.Drawing.Size(49, 17);
			this.radColor.TabIndex = 1;
			this.radColor.TabStop = true;
			this.radColor.Text = "Color";
			this.radColor.UseVisualStyleBackColor = true;
			this.radColor.CheckedChanged += new System.EventHandler(this.radColor_CheckedChanged);
			// 
			// colorDialog
			// 
			this.colorDialog.AnyColor = true;
			this.colorDialog.Color = System.Drawing.Color.White;
			this.colorDialog.SolidColorOnly = true;
			// 
			// radTexture
			// 
			this.radTexture.AutoSize = true;
			this.radTexture.Location = new System.Drawing.Point(9, 310);
			this.radTexture.Name = "radTexture";
			this.radTexture.Size = new System.Drawing.Size(61, 17);
			this.radTexture.TabIndex = 2;
			this.radTexture.TabStop = true;
			this.radTexture.Text = "Texture";
			this.radTexture.UseVisualStyleBackColor = true;
			this.radTexture.CheckedChanged += new System.EventHandler(this.radTexture_CheckedChanged);
			// 
			// btnImport
			// 
			this.btnImport.Location = new System.Drawing.Point(171, 306);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(88, 24);
			this.btnImport.TabIndex = 3;
			this.btnImport.Text = "Import Texture";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// btnExport
			// 
			this.btnExport.Location = new System.Drawing.Point(171, 336);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(88, 24);
			this.btnExport.TabIndex = 4;
			this.btnExport.Text = "Export Texture";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(171, 393);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(88, 24);
			this.btnDelete.TabIndex = 5;
			this.btnDelete.Text = "Delete Texture";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnLeft
			// 
			this.btnLeft.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLeft.Location = new System.Drawing.Point(92, 306);
			this.btnLeft.Name = "btnLeft";
			this.btnLeft.Size = new System.Drawing.Size(24, 24);
			this.btnLeft.TabIndex = 6;
			this.btnLeft.Text = "◄";
			this.btnLeft.UseVisualStyleBackColor = true;
			this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
			// 
			// btnRight
			// 
			this.btnRight.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRight.Location = new System.Drawing.Point(132, 306);
			this.btnRight.Name = "btnRight";
			this.btnRight.Size = new System.Drawing.Size(24, 24);
			this.btnRight.TabIndex = 7;
			this.btnRight.Text = "►";
			this.btnRight.UseVisualStyleBackColor = true;
			this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
			// 
			// btnSelectColor
			// 
			this.btnSelectColor.Location = new System.Drawing.Point(92, 267);
			this.btnSelectColor.Name = "btnSelectColor";
			this.btnSelectColor.Size = new System.Drawing.Size(167, 24);
			this.btnSelectColor.TabIndex = 8;
			this.btnSelectColor.Text = "Select";
			this.btnSelectColor.UseVisualStyleBackColor = true;
			this.btnSelectColor.Click += new System.EventHandler(this.btnSelectColor_Click);
			// 
			// txtOffsetY
			// 
			this.txtOffsetY.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtOffsetY.Location = new System.Drawing.Point(92, 340);
			this.txtOffsetY.Name = "txtOffsetY";
			this.txtOffsetY.Size = new System.Drawing.Size(64, 20);
			this.txtOffsetY.TabIndex = 9;
			this.txtOffsetY.Text = "0.0";
			this.txtOffsetY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtOffsetY.TextChanged += new System.EventHandler(this.txtOffsetY_TextChanged);
			this.txtOffsetY.Leave += new System.EventHandler(this.txtBox_Leave);
			// 
			// txtOffsetX
			// 
			this.txtOffsetX.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtOffsetX.Location = new System.Drawing.Point(3, 340);
			this.txtOffsetX.Name = "txtOffsetX";
			this.txtOffsetX.Size = new System.Drawing.Size(64, 20);
			this.txtOffsetX.TabIndex = 10;
			this.txtOffsetX.Text = "0.0";
			this.txtOffsetX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtOffsetX.TextChanged += new System.EventHandler(this.txtOffsetX_TextChanged);
			this.txtOffsetX.Leave += new System.EventHandler(this.txtBox_Leave);
			// 
			// txtScaleX
			// 
			this.txtScaleX.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtScaleX.Location = new System.Drawing.Point(3, 397);
			this.txtScaleX.Name = "txtScaleX";
			this.txtScaleX.Size = new System.Drawing.Size(64, 20);
			this.txtScaleX.TabIndex = 11;
			this.txtScaleX.Text = "1.0";
			this.txtScaleX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtScaleX.TextChanged += new System.EventHandler(this.txtScaleX_TextChanged);
			this.txtScaleX.Leave += new System.EventHandler(this.txtBox_Leave);
			// 
			// txtScaleY
			// 
			this.txtScaleY.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtScaleY.Location = new System.Drawing.Point(92, 396);
			this.txtScaleY.Name = "txtScaleY";
			this.txtScaleY.Size = new System.Drawing.Size(64, 20);
			this.txtScaleY.TabIndex = 12;
			this.txtScaleY.Text = "1.0";
			this.txtScaleY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtScaleY.TextChanged += new System.EventHandler(this.txtScaleY_TextChanged);
			this.txtScaleY.Leave += new System.EventHandler(this.txtBox_Leave);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 363);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 13);
			this.label1.TabIndex = 13;
			this.label1.Text = "Offset X";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 383);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 14;
			this.label2.Text = "Scale X";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(103, 363);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(45, 13);
			this.label3.TabIndex = 15;
			this.label3.Text = "Offset Y";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(103, 382);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(44, 13);
			this.label4.TabIndex = 16;
			this.label4.Text = "Scale Y";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(9, 440);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(245, 14);
			this.label5.TabIndex = 17;
			this.label5.Text = "the level to be saved immediately.";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(20, 426);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(217, 14);
			this.label6.TabIndex = 17;
			this.label6.Text = "NOTE:  Changes made here cause";
			// 
			// frmTextureSelect
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(262, 459);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtScaleY);
			this.Controls.Add(this.txtScaleX);
			this.Controls.Add(this.txtOffsetX);
			this.Controls.Add(this.txtOffsetY);
			this.Controls.Add(this.btnSelectColor);
			this.Controls.Add(this.btnRight);
			this.Controls.Add(this.btnLeft);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnExport);
			this.Controls.Add(this.btnImport);
			this.Controls.Add(this.radTexture);
			this.Controls.Add(this.radColor);
			this.Controls.Add(this.picShowTex);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmTextureSelect";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Ski Stunt Studio - Texture Select";
			((System.ComponentModel.ISupportInitialize)(this.picShowTex)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox picShowTex;
		private System.Windows.Forms.RadioButton radColor;
		private System.Windows.Forms.ColorDialog colorDialog;
		private System.Windows.Forms.RadioButton radTexture;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnLeft;
		private System.Windows.Forms.Button btnRight;
		private System.Windows.Forms.Button btnSelectColor;
		private System.Windows.Forms.TextBox txtOffsetY;
		private System.Windows.Forms.TextBox txtOffsetX;
		private System.Windows.Forms.TextBox txtScaleX;
		private System.Windows.Forms.TextBox txtScaleY;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
	}
}