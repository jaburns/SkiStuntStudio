using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SkiStuntStudio
{
	/**
	 * This class is used to manage the texture files in a level folder.  Each texture file is in JPEG
	 * format and follows a numerically incrementing naming convention.  This class gets a list of all
	 * textures currently residing in the level and manages deleting and adding new textures.  It also
	 * keep a Brush object for each texture so the editor can paint them (poorly) on the polygons.
	 */
	public class TextureList : IDisposable
	{
		public delegate void TextureEventHandler( object sender, int textureIndex );

		public event TextureEventHandler TextureDelete; // The args object is the index of the texture that was deleted.
												        // LevelPolygons added to the level that this class is pointed to must
												        // must listen to this so they can update their texture references.
		private string levelFolder;
		private int    textureCount;

		private List<Brush> brushes;
		private List<Image> images;

		public string LevelFolder { get { return levelFolder; } }

		public int TextureCount { get { return textureCount; } }

		
		public TextureList( string levelFolder )
		{
				this.levelFolder = levelFolder;

				countTextures();
				regenerateBrushes();
		}


		private string generateTexturePath( int index )
		{
				return levelFolder + "\\" + SkiStuntLevel.TEXTURE_NAME_PREFIX + index.ToString() + SkiStuntLevel.TEXTURE_NAME_SUFFIX ;
		}

	
		public string GetTexturePath( int index )
		{
				if( index < 0 || index >= textureCount ) return "";
				return generateTexturePath( index );
		}

		public Brush GetTextureBrush( int index )
		{
				if( index < 0 || index >= textureCount ) return null;
				return brushes[ index ];
		}

		public Image GetTextureImage( int index )
		{
				if( index < 0 || index >= textureCount ) return null;
				return images[ index ];
		}

		public void ImportTexture( string path )
		{
				if( !File.Exists( path ) ) return;
				File.Copy( path, generateTexturePath( textureCount ) );

				images.Add( Image.FromFile( generateTexturePath( textureCount ) ) );
				brushes.Add( new TextureBrush( images[ textureCount ] ) );

				textureCount++;
		}

		public void DeleteTexture( int index )
		{
				if( index < 0 || index >= textureCount ) return;

				disposeBrushes();

				File.Delete( generateTexturePath( index ) );
				for( int i = index+1 ; i < textureCount ; ++i ) {
					File.Move( generateTexturePath( i ), generateTexturePath( i-1 ) );
				}
				textureCount--;

				regenerateBrushes();

				if( TextureDelete != null ) TextureDelete( this, index );
		}	

		private void disposeBrushes()
		{
				foreach( Image i in images  ) i.Dispose();
				foreach( Brush b in brushes ) b.Dispose();
				
				images  = null;
				brushes = null;
		}

		private void regenerateBrushes()
		{
				brushes = new List<Brush>();
				images  = new List<Image>();

				int i = 0;
				while( File.Exists( generateTexturePath( i ) ) ) {
					images.Add( Image.FromFile( generateTexturePath( i ) ) );
					brushes.Add( new TextureBrush( images[i] ) );
					i++;
				}				
		}

		private void countTextures()
		{
				textureCount = 0;
				int i = 0;
				while( File.Exists( generateTexturePath( i++ ) ) ) textureCount++;
		}

		public void Dispose()
		{
				disposeBrushes();
		}
	}
}
