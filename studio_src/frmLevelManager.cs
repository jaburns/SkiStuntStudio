using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

using com.jeremyaburns.tools;

namespace SkiStuntStudio
{
	// TODO: Add copy button, launch Ski stunts button.
	public partial class frmLevelManager : Form
	{
		private int selectedLevelIndex; // Stores the index in Program.levelManager of the currently selected level.
		private string selectedFolder;


		public frmLevelManager()
		{
				InitializeComponent();
				repopulateLevelList();
		}




		private void repopulateLevelList()
		{
				levelGrid.Rows.Clear();
				for( int i = 0 ; i < Program.levelManager.LevelCount ; ++i ) {
					levelGrid.Rows.Add( Program.levelManager.GetLevelName( i ) );
				}
		}

		private void levelGrid_CellEnter( object sender, DataGridViewCellEventArgs e )
		{
				levelGrid.Rows[ e.RowIndex ].Selected = true;	// Select the whole row.

				selectedLevelIndex = e.RowIndex;
				selectedFolder = Program.levelManager.GetLevelFolder( e.RowIndex );

				if( picPreview.BackgroundImage != null ) {
					picPreview.BackgroundImage.Dispose();
					picPreview.BackgroundImage = null;
				}

				string previewFile = Program.ROOT_PATH + "\\scripts\\ski\\studio\\" + selectedFolder + "\\preview.jpg";
				if( File.Exists( previewFile ) ) {
					picPreview.BackgroundImage = Image.FromFile( previewFile );
				} else {
					picPreview.BackgroundImage = Image.FromFile( Program.ROOT_PATH + "\\artwork\\preview.jpg" );
				}
		}

		private string getUnusedLevelFolderName()
		{
				int i = 0;
				string newFolder;
				do {
					newFolder = Program.ROOT_PATH + "\\scripts\\ski\\studio\\" + i.ToString();
					i++;
				}
				while( Directory.Exists( newFolder ) );

				return newFolder;
		}


		/**
		 */
		private void btnNew_Click( object sender, EventArgs e )
		{
				string newLevelFolder = getUnusedLevelFolderName();
				string newLevelName   = "New Level";

				DialogResult r = Program.InputBox( "Ski Stunt Studio", "Enter a name for the new level:", ref newLevelName );

				if( r != DialogResult.OK ) return;

				Directory.CreateDirectory( newLevelFolder );
				SkiStuntLevel newLevel = new SkiStuntLevel( newLevelFolder );
				newLevel.LevelTitle = newLevelName;
				newLevel.Save();
				newLevel.Dispose();

				Program.levelManager.UpdateLevelList();
				repopulateLevelList();
		}

		/**
		 */
		private void btnEdit_Click( object sender, EventArgs e )
		{
				frmMain editorWindow = new frmMain( Program.levelManager.GetLevelFullPath( selectedLevelIndex ) );

				if( picPreview.BackgroundImage != null ) {
					picPreview.BackgroundImage.Dispose();
					picPreview.BackgroundImage = null;
				}

				this.Hide();
				editorWindow.ShowDialog( this );
				repopulateLevelList();
				this.Show();
		}

		

		private void btnDelete_Click(object sender, EventArgs e)
		{
				DialogResult r = MessageBox.Show( " This action cannot be undone. You can export the level and save it somewhere if you think you might want to use it again.\n\nAre you sure you want to delete this level?", "Ski Stunt Studio", MessageBoxButtons.YesNo, MessageBoxIcon.Warning );

				if( r != DialogResult.Yes ) return;

				Directory.Delete( Program.levelManager.GetLevelFullPath( selectedLevelIndex ), true );

				Program.levelManager.UpdateLevelList();
				repopulateLevelList();
		}

		private void btnExport_Click( object sender, EventArgs e )
		{
				SaveFileDialog dlg = new SaveFileDialog();
				dlg.Filter = "Ski Stunt Studio Level(*.sss)|*.sss";
				if( dlg.ShowDialog() != DialogResult.OK ) return;
				BinaryFileGroup.Create( dlg.FileName, Directory.GetFiles( Program.levelManager.GetLevelFullPath( selectedLevelIndex ) ) );
		}

		private void btnImport_Click(object sender, EventArgs e)
		{
				OpenFileDialog dlg = new OpenFileDialog();

				dlg.CheckFileExists = true;
				dlg.Filter = "Ski Stunt Studio Level(*.sss)|*.sss";

				if( dlg.ShowDialog() != DialogResult.OK ) return;

				string newLevelFolder = getUnusedLevelFolderName();

				Directory.CreateDirectory( newLevelFolder );
				BinaryFileGroup.Expand( dlg.FileName, newLevelFolder );

				Program.levelManager.UpdateLevelList();
				repopulateLevelList();
		}

		private void btnLaunchSkiStunt_Click(object sender, EventArgs e)
		{
				Process.Start( new ProcessStartInfo( Program.ROOT_PATH + "\\bin\\skiStunt.exe" ) );
		}

		private void btnOpenFolder_Click(object sender, EventArgs e)
		{
				Process.Start( new ProcessStartInfo( "explorer.exe", "\"" + Program.ROOT_PATH + "scripts\\ski\\studio\\" + selectedFolder + "\"" ) );
		}
	}
}
