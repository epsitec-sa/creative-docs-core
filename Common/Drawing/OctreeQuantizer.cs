/*
This file is part of CreativeDocs.

Copyright © 2003-2024, EPSITEC SA, 1400 Yverdon-les-Bains, Switzerland

CreativeDocs is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
any later version.

CreativeDocs is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/


using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Epsitec.Common.Drawing.Platform
{
    public class OctreeQuantizer
    {
        public static BitmapSource Quantize(BitmapSource source, int maxColors, int maxColorBits)
        {
            if (source.Format.BitsPerPixel != 32)
            {
                throw new System.ArgumentException(
                    "Invalid source: must be in 32-bit/pixel format"
                );
            }

            OctreeQuantizer quantizer = new OctreeQuantizer(maxColors, maxColorBits);

            return quantizer.Quantize(source);
        }

        private OctreeQuantizer(int maxColors, int maxColorBits)
        {
            if ((maxColors < 1) || (maxColors > 255) || (maxColorBits < 1) || (maxColorBits > 8))
            {
                throw new System.ArgumentException("Invalid number of colors");
            }

            this.octree = new Octree(maxColorBits);
            this.maxColors = maxColors;
            this.bitsPerPixel = maxColorBits;
        }

        private BitmapSource Quantize(BitmapSource source)
        {
            this.QuantizeUsingOctree(source);

            PixelFormat format = this.GetPixelFormat();
            BitmapPalette palette = this.GetPalette();

            return new FormatConvertedBitmap(source, format, palette, 10);
        }

        private PixelFormat GetPixelFormat()
        {
            switch (this.bitsPerPixel)
            {
                case 1:
                    return PixelFormats.Indexed1;

                case 2:
                    return PixelFormats.Indexed2;

                case 3:
                case 4:
                    return PixelFormats.Indexed4;

                case 5:
                case 6:
                case 7:
                case 8:
                    return PixelFormats.Indexed8;
            }

            throw new System.InvalidOperationException();
        }

        private void QuantizeUsingOctree(BitmapSource source)
        {
            int dx = source.PixelWidth;
            int dy = source.PixelHeight;
            int pitch = dx * 4;

            byte[] row = new byte[pitch];

            for (int y = 0; y < dy; y++)
            {
                source.CopyPixels(new System.Windows.Int32Rect(0, y, dx, 1), row, pitch, 0);

                for (int x = 0; x < dx; x++)
                {
                    this.octree.AddColor(new Color32(row, x * 4));
                }
            }
        }

        private BitmapPalette GetPalette()
        {
            return new BitmapPalette(this.octree.GeneratePaletteColors(this.maxColors));
        }

        private readonly Octree octree;
        private readonly int maxColors;
        private readonly int bitsPerPixel;
    }
}
