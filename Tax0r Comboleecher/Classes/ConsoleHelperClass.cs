using System;
using Console = Colorful.Console;

namespace Tax0r_Comboleecher.Classes
{
    class ConsoleHelperClass
    {

        public void SetConsoleTitle(string title)
        {
            Console.Title = title;
        }

        public string GetConsoleTitle()
        {
            return Console.Title;
        }
    }
}
