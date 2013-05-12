using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace SkiStuntStudio
{
	public partial class frmTextureSelect : Form
	{
		private SkiStuntLevel level;
		private LevelPolygon  polygon;

		private int curTexIndex;

		public frmTextureSelect( SkiStuntLevel level, LevelPolygon polygon )
		{
				InitializeComponent();

				this.level   = level;
				this.polygon = polygon;

				if( polygon.TextureID < 0 ) {
					curTexIndex = 0;
					radColor.Select();
				} else {
					curTexIndex = polygon.TextureID;
					radTexture.Select();
				}				

				resetTextboxValues();
				redrawPreviewBox();
		}

		private void setMode( bool textureMode )
		{
				btnDelete.Enabled = textureMode;
				btnExport.Enabled = textureMode;
				btnImport.Enabled = textureMode;
				btnLeft  .Enabled = textureMode;
				btnRight .Enabled = textureMode;

				btnSelectColor.Enabled = !textureMode;

				if( textureMode )
				{
					updateScrollButtonsEnabled();
					if( level.TextureList.TextureCount == 0 && !importTexture() ) radColor.Select();
				}

				redrawPreviewBox();
		}

		private void updateScrollButtonsEnabled()
		{
				btnLeft.Enabled  = curTexIndex > 0;
				btnRight.Enabled = curTexIndex < level.TextureList.TextureCount-1;
		}

		private bool importTexture()
		{
				OpenFileDialog dlg = new OpenFileDialog();

				dlg.CheckFileExists = true;
				dlg.Filter = "JPEG Files(*.jpeg,*.jpg)|*.JPG;*.JPEG";

				if( dlg.ShowDialog() != DialogResult.OK ) return false;

				level.TextureList.ImportTexture( dlg.FileName );

				curTexIndex = level.TextureList.TextureCount - 1;
				redrawPreviewBox();

				return true;
		}

		private void redrawPreviewBox()//AndUpdateLevelData
		{
				if( radTexture.Checked ) {
					picShowTex.BackgroundImage = level.TextureList.GetTextureImage( curTexIndex );
					polygon.TextureID = curTexIndex;
				} else {
					picShowTex.BackgroundImage = null;
					picShowTex.BackColor = polygon.Color;
					polygon.TextureID = -1;
				}
				level.Save();
		}

		private void radColor_CheckedChanged( object sender, EventArgs e )
		{
				setMode( false );	// Enter color mode
		}

		private void radTexture_CheckedChanged( object sender, EventArgs e )
		{
				setMode( true );	// Enter texture mode
		}

		private void btnSelectColor_Click( object sender, EventArgs e )
		{
				colorDialog.Color = picShowTex.BackColor;
				colorDialog.ShowDialog();
				polygon.Color = colorDialog.Color;
				redrawPreviewBox();
		}

		private void btnImport_Click(object sender, EventArgs e)
		{
				importTexture();
				updateScrollButtonsEnabled();
		}

		private void btnExport_Click(object sender, EventArgs e)
		{	
				SaveFileDialog dlg = new SaveFileDialog();

				dlg.Filter = "JPEG Image File(*.jpeg,*.jpg)|*.JPG;*.JPEG";

				if( dlg.ShowDialog() != DialogResult.OK ) return;

				File.Copy( level.TextureList.GetTexturePath( curTexIndex ), dlg.FileName );
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
				picShowTex.BackgroundImage = null;

				level.TextureList.DeleteTexture( curTexIndex );
				if( curTexIndex >= level.TextureList.TextureCount ) curTexIndex = level.TextureList.TextureCount - 1;
				if( curTexIndex < 0 ) radColor.Select();

				redrawPreviewBox();
				updateScrollButtonsEnabled();
		}

		private void btnLeft_Click(object sender, EventArgs e)
		{
				curTexIndex--;
				updateScrollButtonsEnabled();
				redrawPreviewBox();
		}

		private void btnRight_Click(object sender, EventArgs e)
		{
				curTexIndex++;
				updateScrollButtonsEnabled();
				redrawPreviewBox();
		}


		private void txtOffsetX_TextChanged( object sender, EventArgs e )
		{
				float.TryParse( txtOffsetX.Text, out polygon.TextureOffsetX );
		}
		private void txtOffsetY_TextChanged( object sender, EventArgs e )
		{
				float.TryParse( txtOffsetY.Text, out polygon.TextureOffsetY );
		}
		private void txtScaleX_TextChanged( object sender, EventArgs e )
		{
				float.TryParse( txtScaleX.Text, out polygon.TextureScaleX );
		}
		private void txtScaleY_TextChanged( object sender, EventArgs e )
		{
				float.TryParse( txtScaleY.Text, out polygon.TextureScaleY );
		}

		private void resetTextboxValues()
		{
				txtOffsetX.Text = polygon.TextureOffsetX.ToString();
				txtOffsetY.Text = polygon.TextureOffsetY.ToString();
				txtScaleX.Text = polygon.TextureScaleX.ToString();
				txtScaleY.Text = polygon.TextureScaleY.ToString();
		}

		private void txtBox_Leave( object sender, EventArgs e )
		{
				resetTextboxValues();
		}
	}
}
