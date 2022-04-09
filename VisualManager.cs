using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using static System.Console;
using System.Reflection;

namespace Outer_World___Grimaldi_Andrea
{
    class VisualManager
    {

        //WELKOM MESSAGE
        public void Welkom(string wText, string credits, string loadingBar)
        {
            FileCompiler.OproepKlasseObject(MethodBase.GetCurrentMethod().DeclaringType, "Visual");
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            //Startbericht laten afspelen - letter per letter
            var randSleep = new Random();
            Console.SetCursorPosition(28, 10);
            for (int i = 0; i < wText.Length; i++)
            {
                Console.Write(" ");
                Console.Write(wText[i]);
                Thread.Sleep(randSleep.Next(100, 200));
            }
            Console.WriteLine();

            Console.SetCursorPosition(35, 13);

            // credits
            for (int j = 0; j < credits.Length; j++)
            {
                Console.Write(" ");
                Console.Write(credits[j]);
                Thread.Sleep(randSleep.Next(100, 200));
            }

            Console.SetCursorPosition(35, 20);
            Console.Write("Press any key to to start . . . ");
            Thread.Sleep(1000);

            Console.ReadKey();
            Console.Clear();

            Menu();

            Console.SetCursorPosition(35, 22);
            Console.WriteLine("- - - L O A D I N G - - -");
            Console.SetCursorPosition(35, 23);

            for (int k = 0; k < loadingBar.Length; k++)
            {
                Console.Write(" ");
                Console.Write(loadingBar[k]);
                Thread.Sleep(randSleep.Next(100, 200));
            }

            Thread.Sleep(1000);
            Console.SetCursorPosition(35, 22);
            Console.WriteLine("- - - C O M P L E T E - - -\n\n");
            Console.Write("Press any key to to continue . . .");
            Console.ReadKey();
            Console.Clear();
        }

        //Start menu met keuzebar ;)
        public void Menu()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            //checker
            string str;
            int strint;
            bool checker = false;
            bool valid = true;

            //Start menu - while functie
            while (valid)
            {
                //Ascii
                Console.SetCursorPosition(0, 3);
                string title = @"   ____  _    _ _______ ______ _____   __          ______  _____  _      _____  
  / __ \| |  | |__   __|  ____|  __ \  \ \        / / __ \|  __ \| |    |  __ \ 
 | |  | | |  | |  | |  | |__  | |__) |  \ \  /\  / | |  | | |__) | |    | |  | |
 | |  | | |  | |  | |  |  __| |  _  /    \ \/  \/ /| |  | |  _  /| |    | |  | |
 | |__| | |__| |  | |  | |____| | \ \     \  /\  / | |__| | | \ \| |____| |__| |
  \____/ \____/   |_|  |______|_|  \_\     \/  \/   \____/|_|  \_|______|_____/ 
                                                                                
";
                Console.Write("\n" + title);

                //choice
                if (FileCompiler.AmountRows() == 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine("2) New Game\n3) Credits\n4) Exit");
                    Console.Write("\nChoice: ");
                }
                else
                {
                    Console.WriteLine("1) Continue\n2) New Game\n3) Credits\n4) Exit");
                    Console.Write("\nChoice: ");
                }

                str = Console.ReadLine();
                checker = int.TryParse(str, out strint);

                if (str == string.Empty || checker == false)
                {
                    Console.WriteLine("\nYou've entered something wrong...");
                    Console.Write("Press ENTER to continue . . .");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    int input = Convert.ToInt32(str);
                    Console.Clear();

                    switch (input)
                    {
                        //Continue
                        case 1:
                            {
                                if (FileCompiler.AmountRows() == 0)
                                {
                                    Console.SetCursorPosition(0, 3);
                                    Console.WriteLine(title);
                                    Console.WriteLine("There's no save file found, start a New Game!");
                                    Console.WriteLine("\nPress ENTER to continue . . .");
                                    Console.ReadLine();
                                    Console.Clear();
                                }
                                else
                                {
                                    valid = false;
                                }
                                break;
                            }
                        // New Game
                        case 2:
                            {
                                FileCompiler.ClearList();
                                valid = false;
                                break;
                            }
                        //credits
                        case 3:
                            {
                                Console.SetCursorPosition(0, 3);
                                Console.WriteLine(title);
                                Console.WriteLine("This game was made by TheDre\nHope you enjoy it! ^^");
                                Console.WriteLine("\nPress ENTER to continue . . .");
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            }
                        //QUIT
                        case 4:
                            {
                                string title1 = @"(^ _ ^)/";
                                Console.SetCursorPosition(0, 3);
                                Console.WriteLine(title);
                                Console.WriteLine(title1);
                                Console.WriteLine("Thank you for playing, see you another time!");

                                //quitter
                                Environment.Exit(0);
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            }
                        //default - buiten bereik case
                        default:
                            {
                                Console.WriteLine("\nYou've entered something wrong...");
                                Console.Write("Press ENTER to continue . . .");
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            }
                    }
                }
            }
        }

    }
}

