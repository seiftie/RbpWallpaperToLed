using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RbpWallpaperToLed.Service.Handlers
{
    public static class ImageHandlers
    {
        public static void Process(string path)
        {
            Bitmap bmp = new Bitmap(path);
            Dictionary<Color, int> topColors = GetTopColors(bmp);
            bool result = TransmissionHandlers.Send();
            if (result)
            {
                // TODO: Display success notification in system tray
                // EXTRA: Create GUI application and send data to GUI
            }
            else
            {
                // TODO: Display error notification in system tray
                // EXTRA: Create GUI application and send data to GUI
            }
        }

        /// <summary>
        /// Returns a Dictionary with the most available colors in a bitmap image. 
        /// </summary>
        /// <param name="bitmap">Bitmap of the image which needs to be analysed.</param>
        /// <param name="amount">Amount of colors it needs to return.</param>
        /// <returns>Dictionary including the colors with their percentual amount in the dictionary.</returns>
        private static Dictionary<Color, int> GetTopColors(Bitmap bitmap, int amount = 3)
        {
            Dictionary<Color, int> colors = new Dictionary<Color, int>();
            int totalCount = 0;
            int percentualCount = 0;

            using (Bitmap bmp = bitmap)
            {
                var colorsWithCount = GetPixels(bmp)
                    .GroupBy(color => color)
                    .Select(grp => new
                    {
                        Color = grp.Key,
                        Count = grp.Count()
                    })
                    .OrderByDescending(x => x.Count)
                    .Take(amount);

                // get total amount of the top colors, so it can be used to get a percentual number from it in the next foreach
                foreach (var color in colorsWithCount.Select(c => c.Count))
                {
                    totalCount += color;
                }

                foreach (var colorWithCount in colorsWithCount)
                {
                    percentualCount = (colorWithCount.Count / totalCount) * 100;
                    colors.Add(colorWithCount.Color, percentualCount);
                }
            }

            return colors;
        }

        /// <summary>
        /// Loop through a bitmap image to retrieve all colors per pixel
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns>Returns IEnumerable of Color objects</returns>
        private static IEnumerable<Color> GetPixels(Bitmap bitmap)
        {
            List<Color> colors = new List<Color>();
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    colors.Add(pixel);
                }
            }

            return colors;
        }
    }
}
