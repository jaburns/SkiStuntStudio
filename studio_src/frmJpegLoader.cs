using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;

using com.jeremyaburns.math;
using com.jeremyaburns.tools;

namespace SkiStuntStudio
{
	//TODO: Allow clearing imported value and reverting to default.
	public partial class frmJpegLoader : Form
	{
		private string imagePath;
		private int    imageWidth;
		private int    imageHeight;

		private bool noImage;

		private SkiStuntLevel level;

		public frmJpegLoader( string imagePath, int width, int height, SkiStuntLevel level )
		{
				InitializeComponent();

				this.imagePath   = imagePath;
				this.imageWidth  = width;
				this.imageHeight = height;
				this.level = level;	// We only really need a reference to the active level so we can call .save() when an image is deleted/added.

				noImage = !File.Exists( imagePath );

				lblDefault.Visible =  noImage;
				btnExport.Enabled  = !noImage;
				btnNoImage.Enabled = !noImage;
				
				jpegViewer.BackgroundImage = noImage ? null : new Bitmap( imagePath );
		}


		private void btnExport_Click(object sender, EventArgs e)
		{
				if( noImage ) return;	// Just in case.  Really this button shouldn't be enabled if there's no image.

				SaveFileDialog dlg = new SaveFileDialog();

				dlg.Filter = "JPEG Image File(*.jpeg,*.jpg)|*.JPG;*.JPEG";

				if( dlg.ShowDialog() != DialogResult.OK ) return;

				File.Copy( imagePath, dlg.FileName );
		}


		private void btnImport_Click(object sender, EventArgs e)
		{
				OpenFileDialog dlg = new OpenFileDialog();

				dlg.CheckFileExists = true;
				dlg.Filter = "Image Files(*.jpeg,*.jpg,*.bmp,*.png,*.gif)|*.JPG;*.JPEG;*.BMP;*.PNG;*.GIF";

				if( dlg.ShowDialog() != DialogResult.OK ) return;

				if( !noImage )
				{
					DialogResult stillGoAheadResult =
					MessageBox.Show( "The current background image will be replaced. This action can not be undone. If you " +
									 "want to hold on to it you should export it before importing " +
									 "a new one.\n\nContinue with import?", "Import Image", MessageBoxButtons.YesNo );

					if( stillGoAheadResult != DialogResult.Yes ) return;
				}

				Bitmap   original = (Bitmap)Image.FromFile( dlg.FileName );
				Bitmap   resized  = new Bitmap( imageWidth, imageHeight );
				Graphics gResized = Graphics.FromImage( resized );

				if( !noImage )
				{
					jpegViewer.BackgroundImage.Dispose();
					jpegViewer.BackgroundImage = null;
				}
				
				gResized.DrawImage( original, 0, 0, imageWidth, imageHeight );

				ImageTools.SaveJpeg( imagePath, resized, 100 );
				
				jpegViewer.BackgroundImage = resized;
				lblDefault.Visible = false;
				btnExport.Enabled  = true;
				btnNoImage.Enabled = true;
				noImage = false;

				gResized.Dispose();
				original.Dispose();

				level.Save();
		}

		private void frmJpegLoader_FormClosed(object sender, FormClosedEventArgs e)
		{
				if( !noImage ) jpegViewer.BackgroundImage.Dispose();
		}

		private void btnNoImage_Click(object sender, EventArgs e)
		{
				if( noImage ) return;

				DialogResult stillGoAheadResult =
				MessageBox.Show( "The current background image will be deleted. This action can not be undone." +
								 "\n\nAre you sure you want to revert to the default image?", "Use Default Image", MessageBoxButtons.YesNo );

				if( stillGoAheadResult != DialogResult.Yes ) return;

				jpegViewer.BackgroundImage.Dispose();
				jpegViewer.BackgroundImage = null;

				File.Delete( imagePath );

				lblDefault.Visible = true;
				btnExport.Enabled  = false;
				btnNoImage.Enabled = false;
				noImage = true;

				level.Save();
		}
	}
}
