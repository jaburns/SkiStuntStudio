using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace com.jeremyaburns.tools
{
	static public class ImageTools
	{
		/**
		 * <summary>
		 * Save a JPEG file from an Image object at a specified quality.
		 * </summary>
		 * <param name="path">The path to the JPEG file to save.</param>
		 * <param name="img">An image object representing the image to save as a JPEG file.</param>
		 * <param name="quality">An integer value between 0 and 100 specifying the quality.</param>
		 */
		static public void SaveJpeg( string path, Image img, int quality )
        {
				if( quality <   0 ) quality =   0;
				if( quality > 100 ) quality = 100;

				ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
				int jpegEncoderIndex = 0;

				for( int i = 0 ; i < codecs.Length ; i++ ) {
					if( codecs[i].MimeType == "image/jpeg" ) {
						jpegEncoderIndex = i;
						break;
					}
				}

				EncoderParameters encoderParams = new EncoderParameters(1);
				encoderParams.Param[0] = new EncoderParameter( System.Drawing.Imaging.Encoder.Quality, quality );

				img.Save( path, codecs[ jpegEncoderIndex ], encoderParams );
        } 

		/**
		 * <summary>
		 * Create a six-digit hex number string of the form RRGGBB which represents a specified color.  (i.e. as you would use in HTML)
		 * </summary>
		 */
		static public string HexColorStringRGB( Color color )
		{
				return color.R.ToString( "X2" ) + color.G.ToString( "X2" ) + color.B.ToString( "X2" );
		}
	}
}
