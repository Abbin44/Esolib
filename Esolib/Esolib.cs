using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Esolib
{
    public static class Esolib
    {
        public static char[,] ReadFileToGrid(int width, int height, string filePath, bool printArray)
        {
            ///<Summary>Creates a 2D array from a text file
            ///</Summary>
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
            ///</Summary>
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
            ///</Summary>
            int[] size = new int[2];
            var lines = File.ReadLines(filePath);

            string minimum = lines.OrderBy(a => a.Length).First().ToString();
            string maximum = lines.OrderByDescending(a => a.Length).First().ToString();
            size[0] = Convert.ToInt32(minimum);
            size[1] = Convert.ToInt32(maximum);

            return size;
        }

        public static void AddInvisibleBorder(int width, int height, string filePath)
        {
            ///<Summary>Adds an invisible border around all code to prenvent checking null values in the 2D array
            ///</Summary>
            string[] lines = new string[5];
            int lineCounter = 0;

            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead(filePath))
            using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    //Reads file and adds lines to an array
                    lines[lineCounter] = line;
                    Console.WriteLine(lines[lineCounter]); //THIS IS THE GOOD LOOKING DEBUG PRINT
                    lineCounter++;
                }

                for (int i = 0; i < lines.Length; i++)
                {
                    //Check if a line is non-existant, and make it empty in that case
                    if (lines[i] == null)
                        lines[i] = "";

                    //Check if line is less than longest line
                    if (lines[i].Length < width)
                    {
                        int diff = width - lines[i].Length;
                        char blank = ' ';

                        for (int j = 0; j < diff; j++)
                        {
                            //Add a blank space diff ammount of times to the end of the line
                            lines[i] += blank;
                        }
                    }
                    else
                        continue;
                }
            }
            //Write edited text to file
            File.WriteAllLines(filePath, lines);
        }

        public static bool IsValidFile(string filePath, string extention)
        {
            ///<Summary>Checks if the file path is pointing to a valid file type
            ///</Summary>
            if (filePath.EndsWith(extention))
                return true;
            else
                return false;
        }
    }
            
}
