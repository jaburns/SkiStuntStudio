using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SkiStuntStudio
{
	public class LevelManager
	{
		private string rootFolder;

		private List<string> levelFolders;
		private List<string> levelNames;

		public int LevelCount { get { return levelFolders.Count; } }


		public LevelManager( string rootSkiStuntPath )
		{
				this.rootFolder = rootSkiStuntPath;

				this.levelFolders = new List<string>();
				this.levelNames   = new List<string>();

				UpdateLevelList();
		}

		public string GetLevelFolder( int index )
		{
				if( index < 0 || index >= levelFolders.Count ) return "";
				return levelFolders[ index ];
		}

		public string GetLevelName( int index )
		{
				if( index < 0 || index >= levelFolders.Count ) return "";
				return levelNames[ index ];
		}

		public string GetLevelFullPath( int index )
		{
				if( index < 0 || index >= levelFolders.Count ) return "";
				return rootFolder + "\\scripts\\ski\\studio\\" + levelFolders[ index ];
		}


		public void UpdateLevelList()
		{
				levelFolders.Clear();
				levelNames.Clear();

				DirectoryInfo di = new DirectoryInfo( rootFolder + "\\scripts\\ski\\studio" );

				foreach( DirectoryInfo d in di.GetDirectories() ) 
				{
					if( File.Exists( d.FullName + "\\studioFile.txt" ) ) {
						levelFolders.Add( d.Name );
						levelNames.Add( getLevelName( d.FullName + "\\studioFile.txt" ) );
					}
				}

				writeLevelListTcl();
		}


		private string getLevelName( string studioFile )
		{
				string ret;
				StreamReader sr = new StreamReader( studioFile );
				ret = sr.ReadLine();
				sr.Close();
				sr.Dispose();
				return ret;
		}
		
		private void writeLevelListTcl()
		{
				StreamWriter listFile = new StreamWriter( rootFolder + "\\scripts\\studio.tcl" );

				listFile.WriteLine( "set ::studio_labels [list \\" ); // Some bug in Ski Stunt causes the game to crash if there
				for( int i = 0 ; i < levelNames.Count ; ++i ) { // are less than 3 levels in the list.
					listFile.WriteLine( "\"" + levelNames[ i ] + "\" \\" );
				}
				listFile.WriteLine( "]" );

				listFile.WriteLine( "set ::studio_stages [list \\" );
				for( int i = 0 ; i < levelFolders.Count ; ++i ) {
					listFile.WriteLine( "\"char_startPreview studio/" + levelFolders[ i ] + "\" \\" );
				}
				listFile.WriteLine( "]" );

				listFile.WriteLine( "proc loadStudioList {} {" );
				listFile.WriteLine( "scenario_delete" );
				listFile.WriteLine( "scenario_new $::LevelX [expr $::LevelY - 2] $::LevelSize \\" );
				listFile.WriteLine( "$::studio_labels $::studio_stages" );
				listFile.WriteLine( "if {[info var ::PrevState] == {}} {" );
				listFile.WriteLine( "scenario_select_cb 0" );
				listFile.WriteLine( "}" );
				listFile.WriteLine( "}" );

				listFile.Close();
				listFile.Dispose();
		}
	}
}
