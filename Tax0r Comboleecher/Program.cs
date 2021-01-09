using System;
using Console = Colorful.Console;
using System.Threading;
using System.Drawing;
using Tax0r_Comboleecher.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Tax0r_Comboleecher
{
    class Program
    {
        static async Task Main(string[] args)
        {
            double version = 1.0;
            int totalLinks = 0;
            int finishedLinks = 0;
            int combos = 0;

            ComboFinderClass comboFinder = new ComboFinderClass();
            FileHelperClass fileHelper = new FileHelperClass();
            ConsoleHelperClass consoleHelper = new ConsoleHelperClass();
            MathHelperClass mathHelper = new MathHelperClass();
            FilterHelperClass filterHelper = new FilterHelperClass();

            filterHelper.AddFilter(".png");
            filterHelper.AddFilter(".jpg");
            filterHelper.AddFilter(".js");
            filterHelper.AddFilter(".pdf");
            filterHelper.AddFilter(".gov");
            filterHelper.AddFilter(".css");
            filterHelper.AddFilter(".svg");
            filterHelper.AddFilter(".ttf");

            consoleHelper.SetConsoleTitle("Comboleecher v" + version + " - Tax0r 2020 | " + finishedLinks + "/" + totalLinks + " URL's - " + combos + " Combo's");

            Console.WriteAscii("TAX0R, 2020", Color.LightPink);

            Thread.Sleep(TimeSpan.FromSeconds(2));

            Console.Clear();
            Console.WriteLine("[Important!]: Here's a List of currently active Filters.", Color.LightBlue);
            foreach (string filter in filterHelper.getFilters().ToArray())
            {
                Console.WriteLine(filter, Color.LightYellow);
            }

            Thread.Sleep(TimeSpan.FromSeconds(2));

            Console.Clear();
            Console.WriteLine("[Important!]: PRESS ESC TO INTERRUPT AND FINISH INSTANTLY.", Color.LightBlue);
            Console.WriteLine("[Important!]: Please drag&drop you'r URL-list into the Application.", Color.Pink);

            string toReplace = '"'.ToString();
            string input = Console.ReadLine().Replace(toReplace, string.Empty);
            Console.WriteLine(input);
            Console.Clear();

            string[] urls = fileHelper.readUrlsFromFile(input);
            List<string> scrapedCombos = new List<string>();
            List<string> urlsDone = new List<string>();
            List<string> fails = new List<string>();

            totalLinks = mathHelper.GetAmount(urls);
            consoleHelper.SetConsoleTitle("Comboleecher v" + version + " - Tax0r 2020 | " + finishedLinks + "/" + totalLinks + " URL's - " + combos + " Combo's");

            foreach (string url in urls)
            {
                if (filterHelper.Filtered(url, fails))
                {
                    try
                    {
                        Console.WriteLine("[GOOD URL]: " + url, Color.Green);
                        
                        urlsDone.Add(url);
                        finishedLinks = mathHelper.GetAmount(urlsDone.ToArray());
                        consoleHelper.SetConsoleTitle("Comboleecher v" + version + " - Tax0r 2020 | " + finishedLinks + "/" + totalLinks + " URL's - " + combos + " Combo's");

                        string content = await comboFinder.GetUrlContent(url);

                        string[] foundCombos = comboFinder.GetCombos(content);

                        if (foundCombos.Length < 1) fails.Add(url);

                        foreach (string foundCombo in foundCombos)
                        {
                            Console.WriteLine("[NEW COMBO]: " + foundCombo + " | " + url, Color.LightGreen);
                            scrapedCombos.Add(foundCombo);
                        }

                        combos = mathHelper.GetAmount(scrapedCombos.ToArray());
                        consoleHelper.SetConsoleTitle("Comboleecher v" + version + " - Tax0r 2020 | " + finishedLinks + "/" + totalLinks + " URL's - " + combos + " Combo's");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[BAD URL]: " + url, Color.Red);
                        fails.Add(url);
                    }
                }
                else
                {
                    Console.WriteLine("[BAD URL]: " + url, Color.Red);
                    fails.Add(url);
                }
            }

            List<string> distinctCombos = scrapedCombos.Distinct().ToList();

            Console.Clear();
            Console.WriteLine("[Success!]: Combo's we're scraped successfully!", Color.White);
            Console.WriteLine("[Information]: New Combo's found: " + scrapedCombos.Count(), Color.LightBlue);
            Console.WriteLine("[Information]: Distinct Combo's found: " + distinctCombos.Count(), Color.LightPink);

            if(distinctCombos.Count() > 0) fileHelper.saveToFile(distinctCombos.ToArray(), distinctCombos.Count());

            Console.WriteLine("\npress any key to exit the process...", Color.White);
            Console.ReadKey();
        }
    }
}
