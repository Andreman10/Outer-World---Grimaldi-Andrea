using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace Outer_World___Grimaldi_Andrea
{
    class Program
    {
        static void Main(string[] args)
        {
            //Titel
            Console.Title = "Lost in Outer World";

            //Kader
            Console.WindowHeight = 30;
            Console.WindowWidth = 101;

            //Aanmaak verwijzingen
            FileCompiler compiler = new FileCompiler();
            Player adventurur = new Player();
            VisualManager visual = new VisualManager();

            //Aanmaak locaties
            Location pineco = new Location("Pine-Co Village", "\t\t\tYour starting Village.\n\n\tPine-Co Village is surrounded by beautifull tree's!", 0, adventurur);
            Location bilbo = new Location("Bilbo City", "\t\tCity where the monkey's are gathered", 5, adventurur);
            Location zeratora = new Location("Zeratora", "\t\tThe Undead City", 10, adventurur);
            Location theEnd = new Location("The End", "The End of the Demo\n\n\tThank you for Playing!", 15, adventurur);

            //Invullen Locatie in de lijst
            adventurur.AddLocation(pineco);
            adventurur.AddLocation(bilbo);
            adventurur.AddLocation(zeratora);
            adventurur.AddLocation(theEnd);

            //Aanmaak Wapens - overerving
            // Polymorfisme - NIET VERGETEN AAA
            Wapen sword = new PhysicalWapen("Woorden Sword", 4, 6, 1, 2, 0, adventurur);
            Wapen wand = new MagicalWeapon("Basic Wand", 8, 10, 1, 2, 3, 1, adventurur);

            // Voorbeeld Typecasting
            //((dochterklasse)object).funtie(); - printfunctie ofs

            //Invullen wapens in de lijst
            adventurur.AddWeapon(sword);
            adventurur.AddWeapon(wand);

            //Aanmaak Monsters om informatie mee in te vullen - XML
            Monsters p0 = new Monsters();
            Monsters p1 = new Monsters();
            Monsters b0 = new Monsters();
            Monsters z0 = new Monsters();

            //List voor alle monsters
            pineco.AddMonster(p0);
            pineco.AddMonster(p1);
            bilbo.AddMonster(b0);
            zeratora.AddMonster(z0);

            //Kleurveranderen
            Console.ForegroundColor = ConsoleColor.Yellow;

            //Startup
            visual.Welkom(" Lost In Outer World", "Made By TheDre", "■■■■■■■■■■■■");

            //Data speler verzamelen
            adventurur.PlayerData(pineco.GetLocationName());

            //Scene afspelen waar de speler kan interageren met de wereld (aka locatie)
            //Normaal gezien zou dit onder een andere klasse vallen zodat deze makkelijker te onderscheiden is van elkaar (niet Mr Clean dus :p)
            pineco.GameScéne();

        }
    }
}
