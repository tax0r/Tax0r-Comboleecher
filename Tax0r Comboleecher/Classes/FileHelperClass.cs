using System;
using System.IO;
using System.Linq;
using System.Drawing;
using Console = Colorful.Console;

namespace Tax0r_Comboleecher.Classes
{
    class FileHelperClass
    {

        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void saveToFile(string[] list, int amount)
        {
            string fileName = RandomString(5) + "(" + amount + ")";
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                File.WriteAllLines(desktopPath + @"\" + fileName + ".txt", list);
                Console.WriteLine("[Success!]: Saved to: '" + desktopPath + @"\" + fileName + ".txt'", Color.LightGreen);
            }
            catch(Exception e)
            {
                Console.WriteLine("[MISTAKE!]: " + e.Message, Color.Red);
            }
        }

        public string[] readUrlsFromFile(string filePath)
        {
            try
            {
                return File.ReadAllLines(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("[MISTAKE!]: " + e.Message, Color.Red);
                return null;
            }
        }
    }
}
