using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace com.jeremyaburns.tools
{
	/**
	 * This class provides a means to group an arbitrary amount of files of any type together
	 * in a single file.  This is a very simple implementation as no compression or folder
	 * hierarchy is supported.
	 */
	static public class BinaryFileGroup
	{
		/**
		 * <summary>Create a group file.  Note that when the group file is expanded, all source files will
		 * be dumped in the same output folder as no folder hierarchy information is preserved.</summary>
		 * <param name="destpath">The path to the group file you want to generate.</param>
		 * <param name="sourcePaths">An array of paths to the files you want to group together.</param>
		 */
		static public void Create( string destPath, string[] sourcePaths )
		{
				if( File.Exists( destPath ) ) File.Delete( destPath );

				StreamWriter outputStream = new StreamWriter( destPath, false );
				BinaryWriter output = new BinaryWriter( outputStream.BaseStream );

				for( int i = 0 ; i < sourcePaths.Length ; ++i )
				{
					string inFileName = sourcePaths[i].Substring( sourcePaths[i].LastIndexOf( "\\" ) + 1 );
					byte[] inFile = File.ReadAllBytes( sourcePaths[i] );

					output.Write( inFileName );
					output.Write( (UInt32)(inFile.Length) );
					output.Write( inFile );
				}

				output.Close();
		}
		
		/**
		 * <summary>Expand a group file.</summary>
		 * <param name="filePath">Path to the group file you want to expand</param>
		 * <param name="outputFolderPath">Path to a folder you want to dump the output files in to.</param>
		 */
		static public void Expand( string filePath, string outputFolderPath )
		{
				if( !File.Exists( filePath ) ) return;
				if( !Directory.Exists( outputFolderPath ) ) return;

				StreamReader inFileStream = new StreamReader( filePath );
				BinaryReader inFile = new BinaryReader( inFileStream.BaseStream );

				while( inFile.BaseStream.Position < inFile.BaseStream.Length )
				{
					string fileName = inFile.ReadString();
					StreamWriter outputStream = new StreamWriter( outputFolderPath + "\\" + fileName, false );
					BinaryWriter output = new BinaryWriter( outputStream.BaseStream );
					UInt32 byteCount = inFile.ReadUInt32();
					output.Write( inFile.ReadBytes( (int)byteCount ) );
					output.Close();
				}

				inFile.Close();
		}
	}
}