using System;
using System.IO;
using System.Linq;

namespace Esolib
{
    public static class Esolib
    {
        public static char[,] ReadFileToGrid(string filePath, int width, int height, bool printArray)
        {
            ///<Summary>Creates a 2D array from a text file
            var map = new char[height, width];
            StreamReader sr = new StreamReader(filePath);

            string line;
            int lineCount = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (lineCount <= height)
                {
                    for (int i = 0; i < width; i++)
                    {
                        //Add all chars of line to the correct positions in the map array
                        map[lineCount, i] = line[i];
                    }
                    if (lineCount < height)
                        lineCount++;
                }
            }
            sr.Close();

            if (printArray == true)
            {
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        Console.Write(string.Format("{0} ", map[i, j]));
                    }
                    Console.Write(Environment.NewLine + Environment.NewLine);
                }
            }
            return map;
        }

        public static int[] GetArraySizeFromFile(string filePath)
        {
            ///<Summary>Gets the height and width of your file
            int[] size = new int[2];
            FileInfo fi = new FileInfo(filePath);
            size[0] = File.ReadLines(fi.FullName).Count();

            var lines = File.ReadLines(filePath);
            string maximum = lines.OrderByDescending(a => a.Length).First().ToString();
            size[1] = Convert.ToInt32(maximum);

            return size;
        }

        public static int[] GetLongestAndShortest(string filePath)
        {
            ///<Summary>Gets the lenght of the shortest and longest lines
            int[] size = new int[2];
            var lines = File.ReadLines(filePath);

            string minimum = lines.OrderBy(a => a.Length).First().ToString();
            string maximum = lines.OrderByDescending(a => a.Length).First().ToString();
            size[0] = Convert.ToInt32(minimum);
            size[1] = Convert.ToInt32(maximum);

            return size;
        }
    }
}
