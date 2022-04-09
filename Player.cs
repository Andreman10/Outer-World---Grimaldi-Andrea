using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Outer_World___Grimaldi_Andrea
{
    class Player
    {
        // Variabelen
        private string playerName;
        private int currentHealth;
        private int maxHealth = 25;
        private int gold = 20;
        private int level = 1;
        private int attack = 2;
        private int exp = 0;
        private int expNeeded = 5;
        private int currentMana;
        private int maxMana = 10;

        //Variabele + List
        private Location currentLocation;
        List<Location> map = new List<Location>();
        List<Wapen> backpack = new List<Wapen>();

        //Tijdens de start zet de maxhealth in de currenthealth
        public Player()
        {
            currentHealth = maxHealth;
            currentMana = maxMana;
            FileCompiler.OproepKlasseObject(MethodBase.GetCurrentMethod().DeclaringType, "Adventurer");
        }

        //Date verzamelen van de speler
        public void PlayerData(string location)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            //Checker
            string name;
            string input;
            bool valid = true;

            while (valid)
            {
                //ASCI Art
                Console.SetCursorPosition(0, 3);
                string title = @"   ____  _    _ _______ ______ _____   __          ______  _____  _      _____  
  / __ \| |  | |__   __|  ____|  __ \  \ \        / / __ \|  __ \| |    |  __ \ 
 | |  | | |  | |  | |  | |__  | |__) |  \ \  /\  / | |  | | |__) | |    | |  | |
 | |  | | |  | |  | |  |  __| |  _  /    \ \/  \/ /| |  | |  _  /| |    | |  | |
 | |__| | |__| |  | |  | |____| | \ \     \  /\  / | |__| | | \ \| |____| |__| |
  \____/ \____/   |_|  |______|_|  \_\     \/  \/   \____/|_|  \_|______|_____/ 
                                                                                
";
                Console.WriteLine("\n" + title);
                //if (string.IsNullOrEmpty(playerName))

                //Checker Nieuwe speler of returning speler
                if (FileCompiler.AmountRows() == 0)
                {
                    Console.WriteLine("\nBefore you set foot on a dangerous adventure, please give a cool name for your character!");
                    Console.Write("\nCharacter Name: ");
                    input = Console.ReadLine();

                    //Ingave is leeg of een nummer
                    if (input == string.Empty || input.All(char.IsDigit))
                    {
                        Console.WriteLine("\n-------------\nDon't forget to give your character a cool name, try again!");
                        Console.Write("Press any key to to continue . . .");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        name = input;
                        playerName = char.ToUpper(name[0]) + name.Substring(1);

                        //Date invullen in logging
                        FileCompiler.DataPlayerFile(playerName, level, gold, currentHealth, currentMana);

                        Console.Clear();
                        Console.SetCursorPosition(0, 3);
                        Console.WriteLine("\n" + title);

                        Console.WriteLine("Welkom '" + playerName + "', quite a name to give yourself!\n");
                        Console.WriteLine("We will be sending you on your adventure now.\nHave fun and GOOD LUCK!");
                        Console.Write("\nPress any key to to continue . . .");
                        Console.ReadKey();
                        Console.Clear();

                        Console.SetCursorPosition(0, 3);
                        Console.WriteLine("\n" + title);
                        Console.WriteLine("Your name is {0}. Killed during an accident and reborn in another world." +
                        "\nYou can feel a big light shining on your big forehead, making you realise\nyou've been brought back to life!\n", FileCompiler.GetName());
                        Console.WriteLine("Upon waking up from the light, you can feel tree's around you and\nsense some danger from the noises in the area...\n\nThis is your story.");
                        Console.Write("\nPress any key to to continue . . .");
                        Console.ReadKey();
                        Console.Clear();

                        Console.WriteLine("$----------------------------$");
                        Console.WriteLine("          Stranger");
                        Console.WriteLine("$----------------------------$");
                        Console.SetCursorPosition(0, 3);
                        string titletwo = @"░░░████▌█████▌█░████████▐▀██▀
░▄█████░█████▌░█░▀██████▌█▄▄▀▄
░▌███▌█░▐███▌▌░░▄▄░▌█▌███▐███░▀
▐░▐██░░▄▄▐▀█░░░▐▄█▀▌█▐███▐█
░░███░▌▄█▌░░▀░░▀██░░▀██████▌
░░░▀█▌▀██▀░▄░░░░░░░░░███▐███
░░░░██▌░░░░░░░░░░░░░▐███████▌
░░░░███░░░░░▀█▀░░░░░▐██▐███▀▌
░░░░▌█▌█▄░░░░░░░░░▄▄████▀░▀
░░░░░░█▀██▄▄▄░▄▄▀▀▒█▀█░";

                        Console.WriteLine(titletwo);
                        Console.WriteLine("\n$--------------------------------------------$");
                        Console.WriteLine("Hello " + FileCompiler.GetName() + ", welcome to {0}!", location);
                        Console.WriteLine("You're so cute, therefore take these items!");
                        Console.WriteLine("\nGood luck on your adventure!");
                        Console.WriteLine("$---------------------------------------------$");
                        Console.Write("\nPress any key to to continue . . .");
                        Console.ReadKey();
                        Console.Clear();

                        string sword = @"              />
 (           //-------------------------------------(
(*)OXOXOXOXO(*>======================================\
 (           \\---------------------------------------)
              \>";
                        string staff = @"(`-._o_.-')
 `--.|.--'
   ( | )
    (|)
    (|)
    '|`;";
                        Console.WriteLine("\n$------------------------------------------------------------------$");
                        Console.WriteLine("                              Gift");
                        Console.WriteLine("$------------------------------------------------------------------$");
                        Console.WriteLine(sword);
                        Console.WriteLine(staff);
                        Console.WriteLine("\n$------------------------------------------------------------------$");
                        Console.WriteLine("You received: \n\n- Wooden Sword\n- Basic Wand");
                        Console.WriteLine("\n$------------------------------------------------------------------$");

                        Console.Write("\nPress any key to to continue . . .");
                        Console.ReadKey();
                        Console.Clear();

                        valid = false;
                    }
                }
                else
                {
                    //Verder spelen
                    Console.WriteLine("Welcome Back " + FileCompiler.GetName() + "!");
                    Console.WriteLine("You may be cute but you will not have the luxury - RESET LOCATION & WEAPON LEVELS MUAHAH");
                    Console.WriteLine("You forget this was A HARDCOREEE GAME MUAHAHAH");
                    Console.Write("\nPress any key to to continue your adventure. . .");
                    Console.ReadKey();
                    Console.Clear();
                    valid = false;
                }
            }
        }

        //Getters
        public string GetPlayerName()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return playerName;
        }

        public int GetCurrentHealth()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return currentHealth;
        }

        //Maximum hp returnen
        public int GetMaxHp()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return maxHealth;
        }

        public int GetCurrentAttack()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return attack;
        }

        public int GetLevel()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return level;
        }

        public int GetGold()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return gold;
        }

        public int GetMana()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return currentMana;
        }

        public int GetMaxMana()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return maxMana;
        }

        // Checkt huidige locatie en return de index ervan
        //adhv van een foreach om elke locatie in de map e checken
        public int IndexCurrentLocation()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            int i = 0;

            foreach (Location l in map)
            {
                if (l == currentLocation)
                {
                    return i;
                }
                i++;
            }

            return i;
        }

        //Returned de huidige locatie
        public Location GetCurrentLocation()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return currentLocation;
        }

        //Returned de wapenindex
        public int GetWeaponIndex()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            int i = -1;
            foreach (Wapen w in backpack)
            {
                i++;
            }
            return i;
        }

        //returned de lijst van de mappen
        public List<Location> GetMap()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return map;
        }

        // returned de lijst van wapen
        public List<Wapen> GetWeapon()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return backpack;
        }

        // Wanneer men gold uitgeeft, verlaag huidige gold
        //Setters - verander waarde
        public void GoldSpend(int goldLoss)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            gold = goldLoss;
        }

        public void SetMaxHp(int health)
        {
            maxHealth = health;
        }

        public void SetCurrentHealth(int health)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            currentHealth = health;
        }

        public void SetGold(int goldSet)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            gold = goldSet;
        }

        //leven krijgen van shop
        public void GainHealth(int gain)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            currentHealth += gain;

            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }
        }

        //leven verliezen van monster
        public void LoseHealth(int lose)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            currentHealth -= lose;
        }

        //same as health
        public void GainMana(int gain)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            currentMana += gain;

            if (currentMana >= maxMana)
            {
                currentMana = maxMana;
            }

        }

        public void SetMaxMana(int number)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            maxMana = number;
        }

        public void SetMana(int mana)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            currentMana = mana;
        }

        public void LoseMana(int loseAmount)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            currentMana -= loseAmount;
        }

        public void SetLevel(int newLevel)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            level = newLevel;
        }

        public void SetAttack(int newAttack)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            attack = newAttack;
        }

        //Border tekenen met de stats van de speler + update naar de file
        public void DataBorder()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\tHP: {0} / {1}  MP: {2} / {3}    LVL: {4}    BASIC ATK: {5}  GOLD: {6}", FileCompiler.GetCurrentHealth(), maxHealth, currentMana, maxMana, FileCompiler.GetLevel(), attack, FileCompiler.GetGold());
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        //Exp toevoegen na een battle (Checken als je kan levelen of niet)
        public void AddExp(int points)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            if (exp + points >= expNeeded)
            {
                LevelUp();
            }
            else
            {
                exp += points;
            }

        }

        //Gold toevoegen nadat de monster verslagen is
        public void AddGold(int points)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            gold += points;
        }

        //Functie om te levelen en resetten
        public void LevelUp()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            level += 1;
            attack += 2;
            maxHealth += 5;
            maxMana += 5;
            currentHealth = maxHealth;
            currentMana = maxMana;
            expNeeded = 5 * level;
            exp = 0;

            Console.WriteLine("\n\tCongrats you also Lvld Up!\n\tLVL NOW: " + level + "\n\tATK NOW: " + attack + "\n\t+5 HP - Full Health\n\t+5 MP - Full Mana\n\tExp Needed for Next Level: " + expNeeded + " Exp!");
            FileCompiler.DataPlayerFile(playerName, level, gold, currentHealth, currentMana);

        }

        //Voegt een locatie toe aan de list + als er nog geen locatie is dan wordt dit de huidige locatie 
        public void AddLocation(Location nameLocation)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            map.Add(nameLocation);

            if (currentLocation == null)
            {
                currentLocation = nameLocation;
            }

        }

        //Neemt de huidige lcoatie en telt daar 1 bij op
        public void SendToNextLocation()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            currentLocation = map[IndexCurrentLocation() + 1];
        }

        //Wapen toevoegen aan een lijst
        public void AddWeapon(Wapen wapen)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            backpack.Add(wapen);
        }

    }
}
