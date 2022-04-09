using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;

namespace Outer_World___Grimaldi_Andrea
{
    class Location
    {
        // Variabelen
        private string locationName;
        private string description;
        private int zoneLevel;

        //Player - Speler had ik hier nodig om te switchen naar een andere locatie
        Player player;

        private List<Monsters> locatieMonsters = new List<Monsters>();

        //Constructor - meegeven welke speler ik informatie uit wil halen
        public Location(string giveLocationName, string giveDescription, int giveZoneLevel, Player givePlayer)
        {

            locationName = giveLocationName;
            description = giveDescription;
            zoneLevel = giveZoneLevel;
            player = givePlayer;

            FileCompiler.OproepKlasseObject(MethodBase.GetCurrentMethod().DeclaringType, giveLocationName);
        }

        //GameScéne - om te verplaatsen / etc.
        public void GameScéne()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);

            //Leest de opgeslagen gegevens van de logger en update alles volgens de laatste save
            ContinueGameData();

            //Varibele
            bool valid = true;

            //verlaagt de gold en slaat dit op + (van string naar int)
            int goldcheck = int.Parse(FileCompiler.GetGold());
            player.GoldSpend(goldcheck);

            if (player.GetCurrentHealth() >= player.GetMaxHp())
            {
                player.SetMaxHp(player.GetCurrentHealth());
            }

            if (player.GetMana() >= player.GetMaxMana())
            {
                player.SetMaxMana(player.GetMana());
            }

            while (valid)
            {
                //Checkers - Moest de waarde onlogisch zijn, zet dit dan op de juiste waarde (= grenzen)
                if (player.GetCurrentHealth() <= 0)
                {
                    player.SetCurrentHealth(0);
                }
                else if (player.GetCurrentHealth() >= player.GetMaxHp())
                {
                    player.SetCurrentHealth(player.GetMaxHp());
                }

                //Mana checker
                if (player.GetMana() <= 0)
                {
                    player.SetMana(0);
                }
                else if (player.GetMana() >= player.GetMaxMana())
                {
                    player.SetMana(player.GetMaxMana());
                }

                if (player.GetGold() <= 0)
                {
                    player.SetGold(0);
                }

                FileCompiler.DataPlayerFile(player.GetPlayerName(), player.GetLevel(), player.GetGold(), player.GetCurrentHealth(), player.GetMana());

                //Area check - "HARD CODED" yes i know uwu :c
                // Normaal moest dit in een andere script zodat de gegevens makkelijker te verkrijgen waren (vermoord me aub niet andy :S)
                if (0 == player.IndexCurrentLocation())
                {
                    LocationPineCo();
                    Console.Write("\n$-------------------------------------------------------------------------$");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\n " + player.GetCurrentLocation().description);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n$-------------------------------------------------------------------------$");
                }
                else if (1 == player.IndexCurrentLocation())
                {
                    LocationBilbo();
                    Console.Write("\n$-------------------------------------------------------------------------$");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n " + player.GetCurrentLocation().description);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n$-------------------------------------------------------------------------$");
                }
                else if (2 == player.IndexCurrentLocation())
                {
                    LocationZeratros();
                    Console.Write("\n$-------------------------------------------------------------------------$");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\n " + player.GetCurrentLocation().description);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n$-------------------------------------------------------------------------$");
                }
                else if (3 == player.IndexCurrentLocation())
                {
                    TheEnd();
                    break;
                }
                else
                {
                    Console.WriteLine("\nCheck check who is this? -Stranger from the void");
                    break;
                }

                //Checker
                string str;
                int strint;
                bool checker = false;

                //choice
                Console.WriteLine("\n\t1) Explore\t\t\t2) Shop\n\n\t3) Leave Place\t\t\t4) Quit & Save");
                Console.WriteLine("\n$-------------------------------------------------------------------------$");
                Console.Write("\n\tChoice: ");

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
                                // Monsters bevechten om geld en exp
                                Explore();
                                break;
                            }
                        case 2:
                            {
                                //Shop waar de speler kan upgraden / healen etc etc
                                Shop();
                                break;
                            }
                        case 3:
                            {
                                //Kijken als de speler hoog genoeg is om te vertrekken anders terug naar huis hehe
                                LeavePlace();
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            }
                        case 4:
                            {
                                //Aflsluiten
                                QuitnSave();
                                break;
                            }
                        default:
                            {
                                Console.Clear();
                                break;
                            }

                    }
                }
            }
        }

        //return location naam
        public string GetLocationName()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return locationName;
        }

        // Exploreer locatie + vechten voor Gold & EXP
        public void Explore()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            EnterBattle();
        }

        //Battle Enter Text + Check
        private void EnterBattle()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            player.DataBorder();
            string steps = @"        ________   _______  _      ____  _____  _____ _   _  _____ 
        |  ____\ \ / /  __ \| |    / __ \|  __ \|_   _| \ | |/ ____|
        | |__   \ V /| |__) | |   | |  | | |__) | | | |  \| | |  __ 
        |  __|   > < |  ___/| |   | |  | |  _  /  | | | . ` | | |_ |
        | |____ / . \| |    | |___| |__| | | \ \ _| |_| |\  | |__| |
        |______/_/ \_\_|    |______\____/|_|  \_\_____|_| \_|\_____|
                                                             ";
            Console.WriteLine(steps);
            Console.WriteLine("$-------------------------------------------------------------------------$");

            if (0 == player.IndexCurrentLocation())
            {
                //Area code eruithalen en doorsturen
                int zone = player.GetMap()[player.IndexCurrentLocation()].GetMonsterList()[0].GetArea();
                player.GetMap()[player.IndexCurrentLocation()].SpawnMonster(zone);
            }
            else if (1 == player.IndexCurrentLocation())
            {
                int zone = player.GetMap()[player.IndexCurrentLocation() + 1].GetMonsterList()[0].GetArea();
                player.GetMap()[player.IndexCurrentLocation() + 1].SpawnMonster(zone);
            }
            else if (2 == player.IndexCurrentLocation())
            {
                int zone = player.GetMap()[player.IndexCurrentLocation() + 2].GetMonsterList()[0].GetArea();
                player.GetMap()[player.IndexCurrentLocation() + 2].SpawnMonster(zone);
            }
            else if (3 == player.IndexCurrentLocation())
            {
                int zone = player.GetMap()[player.IndexCurrentLocation() + 3].GetMonsterList()[0].GetArea();
                player.GetMap()[player.IndexCurrentLocation() + 3].SpawnMonster(zone); ;
            }
            else
            {
                Console.WriteLine("\nCheck check who is this? -Stranger from the void");
            }

            Console.ReadLine();
            Console.Clear();

        }

        //Monster Spawnen afhankelijk van welke zone de speler in is
        private void SpawnMonster(int zone)
        {
            var rdm = new Random();

            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            if (zone == 0)
            {
                int index = rdm.Next(locatieMonsters.Count);
                if (index == 0)
                {
                    string slime = @"                              ██████████          
                          ████░░░░░░░░░░████      
                        ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██    
                      ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██  
                      ██▒▒▒▒██▒▒▒▒▒▒▒▒▒▒██▒▒▒▒██  
                    ██▒▒▒▒▒▒██▒▒▒▒▒▒▒▒▒▒██▒▒▒▒▒▒██
                    ██▒▒▒▒▒▒██▒▒▒▒▒▒▒▒▒▒██▒▒▒▒▒▒██
                    ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██
                    ██▒▒▒▒▒▒▒▒██▒▒▒▒▒▒██▒▒▒▒▒▒▒▒██
                    ██▓▓▒▒▒▒▒▒▒▒██████▒▒▒▒▒▒▒▒▓▓██
                      ██▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██  
                        ██████████████████████  ";
                    Console.WriteLine("\t\t\t      >>" + locatieMonsters[index].GetMonsterName() + "<<");
                    Console.WriteLine("$-------------------------------------------------------------------------$\n");
                    Console.WriteLine(slime);
                    Console.Write("$-------------------------------------------------------------------------$\n");
                    Console.WriteLine("\t\t\t  " + locatieMonsters[index].GetMonsterName() + " has appeared!");
                    Console.Write("$-------------------------------------------------------------------------$\n");
                    Console.Write("\tPress ENTER to continue . . .");
                    Console.ReadLine();
                    Console.Clear();
                    Fighting(index);
                }
                else if (index == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t\t>>" + locatieMonsters[index].GetMonsterName() + "<<");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("$-------------------------------------------------------------------------$\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    string slime = @"                            ████████████        
                          ██▒▒▒▒▒▒▒▒▒▒▒▒██      
                        ██▒▒░░░░░░░░░░░░▒▒██    
                        ██▒▒░░░░░░░░░░░░▒▒██    
                      ██▒▒░░████░░░░████░░▒▒██  
                      ██▒▒░░████░░░░████░░▒▒██  
                    ██▒▒░░░░████░░░░████░░░░▒▒██
                    ██▒▒░░░░░░░░░░░░░░░░░░░░▒▒██
                    ██▒▒░░░░░░░░░░░░░░░░░░░░▒▒██
                      ██▒▒▒▒░░░░░░░░░░░░▒▒▒▒██  
                        ████▒▒▒▒▒▒▒▒▒▒▒▒████    
                            ████████████        ";
                    Console.WriteLine(slime);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("$-------------------------------------------------------------------------$");
                    Console.WriteLine("\t\t   " + locatieMonsters[index].GetMonsterName() + " has appeared!");
                    Console.Write("$-------------------------------------------------------------------------$\n");
                    Console.Write("\tPress ENTER to continue . . .");
                    Console.ReadLine();
                    Console.Clear();
                    Fighting(index);
                }
                else
                {
                    Console.WriteLine("\nCheck check who is this? -Stranger from the void");
                }
            }
            else if (zone == 1)
            {
                int index = rdm.Next(locatieMonsters.Count);
                if (index == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t\t>>" + locatieMonsters[index].GetMonsterName() + "<<");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("$-------------------------------------------------------------------------$\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    string bat = @"             __       __   ____       ____
               ) \     / (   )   \     /   (
              )_  \_V_/  _(   )_  \_V_/  _(
                )__   __(       )__   __(            
                   `-'             `-'";
                    Console.WriteLine(bat);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("$-------------------------------------------------------------------------$");
                    Console.WriteLine("\t\t   " + locatieMonsters[index].GetMonsterName() + " has appeared!");
                    Console.Write("$-------------------------------------------------------------------------$\n");
                    Console.Write("\tPress ENTER to continue . . .");
                    Console.ReadLine();
                    Console.Clear();
                    Fighting(index);
                }
                else
                {
                    Console.WriteLine("\nCheck check who is this? -Stranger from the void");
                }
            }
            else if (zone == 2)
            {
                int index = rdm.Next(locatieMonsters.Count);
                if (index == 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\t\t\t     >>" + locatieMonsters[index].GetMonsterName() + "<<");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("$-------------------------------------------------------------------------$\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    string skeleton = @"                                  .-.
                                 (o.o)
                                  |=|
                                 __|__
                               //.=|=.\\
                              // .=|=. \\
                              \\ .=|=. //
                               \\(_=_)//
                                (:| |:)
                                 || ||
                                 () ()
                                 || ||
                                 || ||
                                ==' '==";
                    Console.WriteLine(skeleton);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("$-------------------------------------------------------------------------$");
                    Console.WriteLine("\t\t\t " + locatieMonsters[index].GetMonsterName() + " has appeared!");
                    Console.Write("$-------------------------------------------------------------------------$\n");
                    Console.Write("\tPress ENTER to continue . . .");
                    Console.ReadLine();
                    Console.Clear();
                    Fighting(index);

                }
                else
                {
                    Console.WriteLine("\nCheck check who is this? -Stranger from the void");
                }
            }
            else if (zone == 3)
            {
                Console.WriteLine("\nCheck check who is this? -Stranger from the void?");
            }
            else
            {
                Console.WriteLine("\nCheck check who is this? -Stranger from the void");
            }

        }

        //Fighting simuleren - gevens opvragen / doorsturen en verwerken xoxo
        public void Fighting(int index)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);

            bool monsterIsAlive = true;
            string str;
            int strint;
            bool checker = false;

            //Attack & level bijhouden + berekeningen
            int levelCheck = locatieMonsters[index].CalculateLevel(1);
            int minAttack = locatieMonsters[index].GetMinDmg(levelCheck);
            int maxAttack = locatieMonsters[index].GetMaxDmg(levelCheck);

            int currentHealth = locatieMonsters[index].GetCurrentHealth(levelCheck);
            int maxHealth = locatieMonsters[index].GetMaxHealth(levelCheck);

            while (monsterIsAlive == true)
            {

                //Border tekenen van de speler
                player.DataBorder();

                Console.Write("  $-------------------------------------------------------------------------$\n");
                Console.WriteLine("\n    " + locatieMonsters[index].GetMonsterName() + " - " + locatieMonsters[index].GetDescription());
                Console.Write("\n  $-------------------------------------------------------------------------$\n");

                //Border van de monster oproepen
                MonsterData(index, levelCheck, minAttack, maxAttack, currentHealth, maxHealth);

                // als het monster dood is ==> Add exp en eindig de loop
                if (locatieMonsters[index].GetCurHp() <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n\n\tCongratulations, you killed " + locatieMonsters[index].GetMonsterName() + "!");
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    int exp = locatieMonsters[index].GetRewardExp(levelCheck);
                    int gold = locatieMonsters[index].GetRewardGold(levelCheck);

                    player.AddExp(exp);
                    player.AddGold(gold);
                    Console.WriteLine("\n\tBy slaying the monster you gained: {0} EXP & {1} Gold!!", exp, gold);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n\tPress ENTER to continue . . . ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    FileCompiler.DataPlayerFile(player.GetPlayerName(), player.GetLevel(), player.GetGold(), player.GetCurrentHealth(), player.GetMana());
                    locatieMonsters[index].SetCurrentHp(maxHealth);
                    monsterIsAlive = false;
                    break;
                }

                //Leven van het monster te groot is ==> zet dit op de max (voor het geval dat)
                if (locatieMonsters[index].GetCurHp() >= locatieMonsters[index].GetMaxHp())
                {
                    {
                        locatieMonsters[index].SetCurrentHp(locatieMonsters[index].GetMaxHp());
                    }
                }

                // Speler dood? ==> trek al het gold ervan af + reset naar locatie Menu
                if (player.GetCurrentHealth() <= 0)
                {
                    Console.WriteLine("\n\tYou've been defeated by the almighty " + locatieMonsters[index].GetMonsterName() + "!");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\tYou will bring shame to the family, monkey!");
                    CoolPrint("\n\tRESPAWNING - ALL GOLD LOST!");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    player.SetGold(0);
                    player.SetCurrentHealth(player.GetMaxHp());
                    FileCompiler.DataPlayerFile(player.GetPlayerName(), player.GetLevel(), player.GetGold(), player.GetCurrentHealth(), player.GetMana());
                    monsterIsAlive = false;
                    Console.Write("\n\tPress ENTER to continue . . .");
                    break;
                }


                Console.WriteLine("\n  1) Melee");
                Console.WriteLine("  2) Spell");
                Console.WriteLine("  3) Run Away");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Using 'Run Away' = RNG related");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n  $-------------------------------------------------------------------------$\n");
                Console.Write("\n\n  Choice: ");

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
                        //Attack - Sword
                        case 1:
                            {
                                //DMG van de speler berekenen
                                int i = player.GetWeapon()[0].DoAttackDmg();

                                //Verlaag de monster hp
                                locatieMonsters[index].SetCurrentHp(locatieMonsters[index].GetCurHp() - i);

                                currentHealth = locatieMonsters[index].GetCurHp();

                                //Border tekenen van de speler
                                player.DataBorder();

                                Console.Write("  $-------------------------------------------------------------------------$\n");
                                Console.WriteLine("\n    " + locatieMonsters[index].GetMonsterName() + " - " + locatieMonsters[index].GetDescription());
                                Console.Write("\n  $-------------------------------------------------------------------------$\n");

                                //Border van de monster oproepen
                                MonsterData(index, levelCheck, minAttack, maxAttack, currentHealth, maxHealth);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\n\n You lunge your sword at " + locatieMonsters[index].GetMonsterName() + "! - Dealing " + i + " DMG!");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("\n\b Press ENTER to continue . . . ");
                                Console.ReadLine();
                                Console.Clear();

                                if (locatieMonsters[index].GetCurHp() > 0)
                                {
                                    int damage = locatieMonsters[index].CalculateDamage(minAttack, maxAttack);
                                    player.LoseHealth(damage);
                                    FileCompiler.DataPlayerFile(player.GetPlayerName(), player.GetLevel(), player.GetGold(), player.GetCurrentHealth(), player.GetMana());
                                    Console.Clear();
                                    //Border tekenen van de speler
                                    player.DataBorder();
                                    Console.Write("  $-------------------------------------------------------------------------$\n");
                                    Console.WriteLine("\n    " + locatieMonsters[index].GetMonsterName() + " - " + locatieMonsters[index].GetDescription());
                                    Console.Write("\n  $-------------------------------------------------------------------------$\n");

                                    //Border van de monster oproepen
                                    MonsterData(index, levelCheck, minAttack, maxAttack, currentHealth, maxHealth);
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n   " + locatieMonsters[index].GetMonsterName() + " attacked you for: " + damage + " DMG!");
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("\n    Press ENTER to continue . . . ");
                                    Console.ReadLine();
                                    Console.Clear();
                                }

                                break;
                            }
                        //Spells - Wand
                        case 2:
                            {
                                //Border tekenen van de speler
                                player.DataBorder();

                                Console.Write("  $-------------------------------------------------------------------------$\n");
                                Console.WriteLine("\n    " + locatieMonsters[index].GetMonsterName() + " - " + locatieMonsters[index].GetDescription());
                                Console.Write("\n  $-------------------------------------------------------------------------$\n");

                                int priceFireBall = 10 + player.GetWeapon()[player.GetWeaponIndex()].GetWapenLevel();
                                int priceWindSlash = 6 + player.GetWeapon()[player.GetWeaponIndex()].GetWapenLevel();
                                int priceWaterBubble = 8 + player.GetWeapon()[player.GetWeaponIndex()].GetWapenLevel();

                                //Border van de monster oproepen
                                MonsterData(index, levelCheck, minAttack, maxAttack, currentHealth, maxHealth);

                                Console.WriteLine("\n  1) Fire Ball       MP: " + priceFireBall);
                                Console.WriteLine("  2) Wind Slash      MP: " + priceWindSlash);
                                Console.WriteLine("  3) Water Bubble    MP: " + priceWaterBubble);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n  4) Exit");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("\n  $-------------------------------------------------------------------------$\n");
                                Console.Write("\n\n  Choice: ");

                                //Input checker
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
                                    input = Convert.ToInt32(str);
                                    Console.Clear();

                                    switch (input)
                                    {
                                        //Fireball - AAAAAAAAAAA VUUR
                                        case 1:
                                            {
                                                if (player.GetMana() <= priceFireBall)
                                                {
                                                    //Border tekenen van de speler
                                                    player.DataBorder();

                                                    Console.Write("  $-------------------------------------------------------------------------$\n");
                                                    Console.WriteLine("\n    " + locatieMonsters[index].GetMonsterName() + " - " + locatieMonsters[index].GetDescription());
                                                    Console.Write("\n  $-------------------------------------------------------------------------$\n");

                                                    //Border van de monster oproepen
                                                    MonsterData(index, levelCheck, minAttack, maxAttack, currentHealth, maxHealth);
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("\n\tYou don't have enough mana to cast this spell!");
                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                    Console.WriteLine("\n\t Press ENTER to continue . . . ");
                                                    Console.ReadLine();
                                                    Console.Clear();
                                                }
                                                else
                                                {
                                                    player.LoseMana(priceFireBall);

                                                    //DMG van de speler bereken
                                                    player.GetWeapon()[1].SetWeaponType(0);
                                                    int i = player.GetWeapon()[1].DoAttackDmg();
                                                    //Verlaag de monster hp
                                                    locatieMonsters[index].SetCurrentHp(locatieMonsters[index].GetCurHp() - i);

                                                    currentHealth = locatieMonsters[index].GetCurHp();

                                                    //Border tekenen van de speler
                                                    player.DataBorder();

                                                    Console.Write("  $-------------------------------------------------------------------------$\n");
                                                    Console.WriteLine("\n    " + locatieMonsters[index].GetMonsterName() + " - " + locatieMonsters[index].GetDescription());
                                                    Console.Write("\n  $-------------------------------------------------------------------------$\n");

                                                    //Border van de monster oproepen
                                                    MonsterData(index, levelCheck, minAttack, maxAttack, currentHealth, maxHealth);
                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                    Console.WriteLine("\n\n\tYou raise your staff, castine FIRE BALL at " + locatieMonsters[index].GetMonsterName() + "! - Dealing " + i + " DMG!");
                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                    Console.WriteLine("\n\t Press ENTER to continue . . . ");
                                                    Console.ReadLine();
                                                    Console.Clear();

                                                    if (locatieMonsters[index].GetCurHp() > 0)
                                                    {
                                                        int damage = locatieMonsters[index].CalculateDamage(minAttack, maxAttack);
                                                        player.LoseHealth(damage);
                                                        FileCompiler.DataPlayerFile(player.GetPlayerName(), player.GetLevel(), player.GetGold(), player.GetCurrentHealth(), player.GetMana());
                                                        Console.Clear();
                                                        //Border tekenen van de speler
                                                        player.DataBorder();
                                                        Console.Write("  $-------------------------------------------------------------------------$\n");
                                                        Console.WriteLine("\n    " + locatieMonsters[index].GetMonsterName() + " - " + locatieMonsters[index].GetDescription());
                                                        Console.Write("\n  $-------------------------------------------------------------------------$\n");

                                                        //Border van de monster oproepen
                                                        MonsterData(index, levelCheck, minAttack, maxAttack, currentHealth, maxHealth);
                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                        Console.WriteLine("\n   " + locatieMonsters[index].GetMonsterName() + " attacked you for: " + damage + " DMG!");
                                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                                        Console.WriteLine("\n    Press ENTER to continue . . . ");
                                                        Console.ReadLine();
                                                        Console.Clear();
                                                    }
                                                }

                                                break;
                                            }
                                        // Wind Slash - WOOOOOOSH
                                        case 2:
                                            {
                                                if (player.GetMana() <= priceWindSlash)
                                                {
                                                    //Border tekenen van de speler
                                                    player.DataBorder();

                                                    Console.Write("  $-------------------------------------------------------------------------$\n");
                                                    Console.WriteLine("\n    " + locatieMonsters[index].GetMonsterName() + " - " + locatieMonsters[index].GetDescription());
                                                    Console.Write("\n  $-------------------------------------------------------------------------$\n");

                                                    //Border van de monster oproepen
                                                    MonsterData(index, levelCheck, minAttack, maxAttack, currentHealth, maxHealth);
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("\n\tYou don't have enough mana to cast this spell!");
                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                    Console.WriteLine("\n\t Press ENTER to continue . . . ");
                                                    Console.ReadLine();
                                                    Console.Clear();
                                                }
                                                else
                                                {
                                                    player.LoseMana(priceWindSlash);
                                                    //DMG van de speler bereken
                                                    player.GetWeapon()[1].SetWeaponType(1);
                                                    int i = player.GetWeapon()[1].DoAttackDmg();
                                                    //Verlaag de monster hp
                                                    locatieMonsters[index].SetCurrentHp(locatieMonsters[index].GetCurHp() - i);

                                                    currentHealth = locatieMonsters[index].GetCurHp();

                                                    //Border tekenen van de speler
                                                    player.DataBorder();

                                                    Console.Write("  $-------------------------------------------------------------------------$\n");
                                                    Console.WriteLine("\n    " + locatieMonsters[index].GetMonsterName() + " - " + locatieMonsters[index].GetDescription());
                                                    Console.Write("\n  $-------------------------------------------------------------------------$\n");

                                                    //Border van de monster oproepen
                                                    MonsterData(index, levelCheck, minAttack, maxAttack, currentHealth, maxHealth);
                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                    Console.WriteLine("\n\n\t You raise your staff, casting Wind slash at " + locatieMonsters[index].GetMonsterName() + "! - Dealing " + i + " DMG!");
                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                    Console.WriteLine("\n\t Press ENTER to continue . . . ");
                                                    Console.ReadLine();
                                                    Console.Clear();

                                                    if (locatieMonsters[index].GetCurHp() > 0)
                                                    {
                                                        int damage = locatieMonsters[index].CalculateDamage(minAttack, maxAttack);
                                                        player.LoseHealth(damage);
                                                        FileCompiler.DataPlayerFile(player.GetPlayerName(), player.GetLevel(), player.GetGold(), player.GetCurrentHealth(), player.GetMana());
                                                        Console.Clear();
                                                        //Border tekenen van de speler
                                                        player.DataBorder();
                                                        Console.Write("  $-------------------------------------------------------------------------$\n");
                                                        Console.WriteLine("\n    " + locatieMonsters[index].GetMonsterName() + " - " + locatieMonsters[index].GetDescription());
                                                        Console.Write("\n  $-------------------------------------------------------------------------$\n");

                                                        //Border van de monster oproepen
                                                        MonsterData(index, levelCheck, minAttack, maxAttack, currentHealth, maxHealth);
                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                        Console.WriteLine("\n   " + locatieMonsters[index].GetMonsterName() + " attacked you for: " + damage + " DMG!");
                                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                                        Console.WriteLine("\n    Press ENTER to continue . . . ");
                                                        Console.ReadLine();
                                                        Console.Clear();
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                        //Waterball - SPLASH ON YO BIGH LIKE WATER
                                        case 3:
                                            {
                                                if (player.GetMana() <= priceWaterBubble)
                                                {
                                                    //Border tekenen van de speler
                                                    player.DataBorder();

                                                    Console.Write("  $-------------------------------------------------------------------------$\n");
                                                    Console.WriteLine("\n    " + locatieMonsters[index].GetMonsterName() + " - " + locatieMonsters[index].GetDescription());
                                                    Console.Write("\n  $-------------------------------------------------------------------------$\n");

                                                    //Border van de monster oproepen
                                                    MonsterData(index, levelCheck, minAttack, maxAttack, currentHealth, maxHealth);
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("\n\tYou don't have enough mana to cast this spell!");
                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                    Console.WriteLine("\n\t Press ENTER to continue . . . ");
                                                    Console.ReadLine();
                                                    Console.Clear();
                                                }
                                                else
                                                {
                                                    player.LoseMana(priceWaterBubble);

                                                    //DMG van de speler bereken
                                                    player.GetWeapon()[1].SetWeaponType(2);
                                                    int i = player.GetWeapon()[1].DoAttackDmg();
                                                    //Verlaag de monster hp
                                                    locatieMonsters[index].SetCurrentHp(locatieMonsters[index].GetCurHp() - i);

                                                    currentHealth = locatieMonsters[index].GetCurHp();

                                                    //Border tekenen van de speler
                                                    player.DataBorder();

                                                    Console.Write("  $-------------------------------------------------------------------------$\n");
                                                    Console.WriteLine("\n    " + locatieMonsters[index].GetMonsterName() + " - " + locatieMonsters[index].GetDescription());
                                                    Console.Write("\n  $-------------------------------------------------------------------------$\n");

                                                    //Border van de monster oproepen
                                                    MonsterData(index, levelCheck, minAttack, maxAttack, currentHealth, maxHealth);
                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                    Console.WriteLine("\n\n\t You raise your staff, castine Water bubble at " + locatieMonsters[index].GetMonsterName() + "! - Dealing " + i + " DMG!");
                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                    Console.WriteLine("\n\t Press ENTER to continue . . . ");
                                                    Console.ReadLine();
                                                    Console.Clear();

                                                    if (locatieMonsters[index].GetCurHp() > 0)
                                                    {
                                                        int damage = locatieMonsters[index].CalculateDamage(minAttack, maxAttack);
                                                        player.LoseHealth(damage);
                                                        FileCompiler.DataPlayerFile(player.GetPlayerName(), player.GetLevel(), player.GetGold(), player.GetCurrentHealth(), player.GetMana());
                                                        Console.Clear();
                                                        //Border tekenen van de speler
                                                        player.DataBorder();
                                                        Console.Write("  $-------------------------------------------------------------------------$\n");
                                                        Console.WriteLine("\n    " + locatieMonsters[index].GetMonsterName() + " - " + locatieMonsters[index].GetDescription());
                                                        Console.Write("\n  $-------------------------------------------------------------------------$\n");

                                                        //Border van de monster oproepen
                                                        MonsterData(index, levelCheck, minAttack, maxAttack, currentHealth, maxHealth);
                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                        Console.WriteLine("\n   " + locatieMonsters[index].GetMonsterName() + " attacked you for: " + damage + " DMG!");
                                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                                        Console.WriteLine("\n    Press ENTER to continue . . . ");
                                                        Console.ReadLine();
                                                        Console.Clear();
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                        //Exit
                                        case 4:
                                            {
                                                break;
                                            }
                                        default:
                                            {
                                                //Border tekenen van de speler
                                                player.DataBorder();

                                                Console.Write("  $-------------------------------------------------------------------------$\n");
                                                Console.WriteLine("\n    " + locatieMonsters[index].GetMonsterName() + " - " + locatieMonsters[index].GetDescription());
                                                Console.Write("\n  $-------------------------------------------------------------------------$\n");

                                                //Border van de monster oproepen
                                                MonsterData(index, levelCheck, minAttack, maxAttack, currentHealth, maxHealth);


                                                Console.WriteLine("\nYou've entered something wrong...");
                                                Console.Write("Press ENTER to continue . . .");
                                                Console.ReadLine();
                                                Console.Clear();
                                                break;
                                            }
                                    }
                                }
                                break;
                            }
                        // RUN
                        case 3:
                            {

                                Random rnd = new Random();

                                int walkaway = 20;
                                int walkawaySucces = rnd.Next(walkaway - 20, walkaway + 20);

                                //Border tekenen van de speler
                                player.DataBorder();

                                Console.Write("  $-------------------------------------------------------------------------$\n");
                                Console.WriteLine("\n    " + locatieMonsters[index].GetMonsterName() + " - " + locatieMonsters[index].GetDescription());
                                Console.Write("\n  $-------------------------------------------------------------------------$\n");

                                //Border van de monster oproepen
                                MonsterData(index, levelCheck, minAttack, maxAttack, currentHealth, maxHealth);


                                if (walkawaySucces <= 5)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n\tNANI !!! -  BIG SUCCES, you use your SPECIAL MOVE:\n\tRAN AWAY FROM TIGHT SITUATIONS!!");
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.Write("\n  $-------------------------------------------------------------------------$\n");
                                    Console.WriteLine("\n\tPress ENTER to continue . . . ");
                                    Console.ReadLine();
                                    Console.Clear();
                                    monsterIsAlive = false;
                                    Console.WriteLine("\n\tPress ENTER to continue . . . ");

                                    break;
                                }
                                else
                                {
                                    int damage = locatieMonsters[index].CalculateDamage(minAttack, maxAttack);
                                    Console.WriteLine("\n   It sadly failed...");
                                    damage *= 2;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n   " + locatieMonsters[index].GetMonsterName() + " attacked you for: " + damage + " DMG!");
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.Write("\n  $-------------------------------------------------------------------------$\n");
                                    Console.WriteLine("\n\tPress ENTER to continue . . . ");
                                    player.LoseHealth(damage);
                                    FileCompiler.DataPlayerFile(player.GetPlayerName(), player.GetLevel(), player.GetGold(), player.GetCurrentHealth(), player.GetMana());
                                    //damage afdoen van de speler + opslaan en doorsturen naar de file

                                    Console.ReadLine();
                                    Console.Clear();
                                }

                                break;
                            }
                        //Voor het geval datje ;)
                        default:
                            {
                                //Border tekenen van de speler
                                player.DataBorder();

                                Console.Write("  $-------------------------------------------------------------------------$\n");
                                Console.WriteLine("\n    " + locatieMonsters[index].GetMonsterName() + " - " + locatieMonsters[index].GetDescription());
                                Console.Write("\n  $-------------------------------------------------------------------------$\n");

                                //Border van de monster oproepen
                                MonsterData(index, levelCheck, minAttack, maxAttack, currentHealth, maxHealth);


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

        // Return alle data van een monster
        public void MonsterData(int index, int levelCheck, int minAttack, int maxAttack, int currentHealth, int maxHealth)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);

            //Gegevens van de monster printen
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tMONSTER: {0}   HP: {1} / {2}    LVL: {3}    ATK: {4} / {5}", locatieMonsters[0].GetMonsterName(), currentHealth, maxHealth, levelCheck, minAttack, maxAttack);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  $-------------------------------------------------------------------------$\n");
        }

        //Shop functie met switch om bepaalde dingen te doen - :)
        public void Shop()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            bool valid = true;

            while (valid)
            {
                player.DataBorder();

                //standaard prijs opstellen zodat de klant per level dit kan verhogen
                int priceSword = 5 * player.GetWeapon()[player.GetWeaponIndex() - 1].GetWapenLevel();
                int priceWand = 8 * player.GetWeapon()[player.GetWeaponIndex()].GetWapenLevel();
                int priceHealth = (5 - 2) * player.GetLevel();
                int priceMp = (4 - 2) * player.GetLevel();

                String title = @" _________ ____  ____ ________       ______  ____  ____  ___  _______   
|  _   _  |_   ||   _|_   __  |    .' ____ \|_   ||   _.'   `|_   __ \  
|_/ | | \_| | |__| |   | |_ \_|    | (___ \_| | |__| |/  .-.  \| |__) | 
    | |     |  __  |   |  _| _      _.____`.  |  __  || |   | ||  ___/  
   _| |_   _| |  | |_ _| |__/ |    | \____) |_| |  | |\  `-'  _| |_     
  |_____| |____||____|________|     \______.|____||____`.___.|_____|    
                                                                       ";
                Console.WriteLine(title);
                Console.WriteLine("Aaah Welcome Adventurer!");
                Console.WriteLine("With what can I help you today?\n");
                Console.WriteLine("1) Upgrade Sword - " + priceSword + " Gold");
                Console.WriteLine("2) Upgrade Wand  - " + priceWand + " Gold");
                Console.WriteLine("3) Restore healh - " + priceHealth * player.GetLevel() + " Gold" + "\tHP Gain " + priceHealth);
                Console.WriteLine("4) Restore Mana  - " + priceMp * player.GetLevel() + " Gold" + "\tMP Gain " + priceMp);
                Console.WriteLine("\n5) Exit\n");
                Console.WriteLine("$-------------------------------------------------------------------------$");
                Console.Write("\nChoice: ");
                string str;
                int strint;
                bool checker = false;

                str = Console.ReadLine();
                checker = int.TryParse(str, out strint);

                if (str == string.Empty || checker == false)
                {
                    Console.WriteLine("\nStop being drunk okay? You entered something wrong...");
                    Console.WriteLine("Press ENTER to continue . . .");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    int input = Convert.ToInt32(str);
                    Console.Clear();

                    switch (input)
                    {
                        case 1:
                            {
                                UpgradeSword(title, priceSword);
                                break;
                            }
                        case 2:
                            {
                                UpgradeWand(title, priceWand);
                                break;
                            }
                        case 3:
                            {
                                RestoreHealth(title, priceHealth);
                                break;
                            }
                        case 4:
                            {
                                RestoreMp(title, priceMp);
                                break;
                            }
                        case 5:
                            {
                                valid = false;
                                break;
                            }
                        default:
                            {
                                player.DataBorder();
                                Console.WriteLine(title);
                                Console.WriteLine("\nAre you drunk again? You entered something wrong...");
                                Console.WriteLine("Press ENTER to continue . . .");
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            }
                    }
                }
            }
        }

        //Verhogen van de wapenlevel - XXX
        public void UpgradeSword(string title, int priceSword)
        {
            bool upgraded = false;
            int priceS = priceSword * player.GetWeapon()[player.GetWeaponIndex() - 1].GetWapenLevel();
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            player.DataBorder();
            Console.WriteLine(title);
            if (player.GetGold() >= priceS)
            {
                Console.WriteLine("So you want to upgrade your slice 'nd dicer huuh!?\nWell then here we gooooo...\n");
                //GOLD VERLAGEN
                player.GoldSpend(player.GetGold() - priceS);
                FileCompiler.DataPlayerFile(player.GetPlayerName(), player.GetLevel(), player.GetGold(), player.GetCurrentHealth(), player.GetMana());

                CoolPrint("SMACK SMAAACK SMACK - thing*");

                //Upgrade het wapen met 1 level die dan op zijn beurt de min & max dmg verhoogt van dat wapen
                player.GetWeapon()[player.GetWeaponIndex() - 1].SetWeaponLevel(1);
                Console.WriteLine("\n\nsheeee looking shiny as hell, good luck with it and come again foreh... I mean adventurer ^^");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Congrats your weapon is now level " + player.GetWeapon()[player.GetWeaponIndex() - 1].GetWapenLevel() + "!");
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (player.GetWeapon()[0].GetWapenLevel() >= 5 && upgraded == false)
                {
                    Console.WriteLine("\nWOOOW  - Now you can deal BLEED each time you hit the enemy!");
                    upgraded = true;
                }
                else if (player.GetWeapon()[0].GetWapenLevel() >= 5)
                {
                    Console.WriteLine("\nDon't forget the upgrade on your weapon - It's something special!");
                }
                else
                {
                    Console.WriteLine("\nReaching Level 5 on your weapon will upgrade its POWER!");
                }

                Console.Write("\nPress ENTER to continue . . .");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("you're....broke... GET LOSSSSTTTT!!!");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nPress ENTER to continue . . .");
                Console.ReadLine();
                Console.Clear();
            }
        }

        //Upgraden van de wand naar de volgende level
        public void UpgradeWand(string title, int priceWand)
        {
            int priceW = priceWand * player.GetWeapon()[player.GetWeaponIndex()].GetWapenLevel();
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            player.DataBorder();
            Console.WriteLine(title);
            if (player.GetGold() >= priceW)
            {
                Console.WriteLine("So you want to be liek Harry Potter E?\nWell then here we gooooo...\n");
                //GOLD VERLAGEN
                player.GoldSpend(player.GetGold() - priceW);
                FileCompiler.DataPlayerFile(player.GetPlayerName(), player.GetLevel(), player.GetGold(), player.GetCurrentHealth(), player.GetMana());

                CoolPrint("SMACK SMAAACK SMACK - thing*");

                //Upgrade het wapen met 1 level die dan op zijn beurt de min & max dmg verhoogt van dat wapen
                player.GetWeapon()[player.GetWeaponIndex()].SetWeaponLevel(1);
                Console.WriteLine("\n\noooh my gawd, looks very naice! ^^");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Congrats your wand is now level " + player.GetWeapon()[player.GetWeaponIndex()].GetWapenLevel() + "!");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nPress ENTER to continue . . .");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("you're....broke... GET LOSSSSTTTT!!!");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nPress ENTER to continue . . .");
                Console.ReadLine();
                Console.Clear();
            }
        }

        //Health restoreren + check ups
        private void RestoreHealth(string title, int priceHealth)
        {
            int restore = priceHealth;
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            player.DataBorder();
            Console.WriteLine(title);

            if (player.GetCurrentHealth() == player.GetMaxHp())
            {
                Console.WriteLine("\n$-------------------------------------------------------------------------$");
                Console.WriteLine("\nLow on health mister, well take this goooood shot of whiskey!\n\nIt will make you feeel gooood!\n");
                Console.WriteLine("\n$-------------------------------------------------------------------------$");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("wait... You already have max health, stop playing these games!\n\nGet out of here!");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n\nPress ENTER to continue . . .");
                Console.ReadLine();
                Console.Clear();
            }
            else if (player.GetGold() >= restore)
            {
                //GOLD VERLAGEN
                player.GoldSpend(player.GetGold() - restore);

                CoolPrint("CHUG CHUG CHUUUGGG - AAAAH!");
                player.GainHealth(restore);

                if (player.GetCurrentHealth() >= player.GetMaxHp())
                {
                    player.SetCurrentHealth(player.GetMaxHp());
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" -You gained " + (restore) + " HP");
                Console.ForegroundColor = ConsoleColor.Yellow;
                FileCompiler.DataPlayerFile(player.GetPlayerName(), player.GetLevel(), player.GetGold(), player.GetCurrentHealth(), player.GetMana());
                Console.Write("\nPress ENTER to continue . . .");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("you're....broke... GET LOSSSSTTTT!!!");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n\nPress ENTER to continue . . .");
                Console.ReadLine();
                Console.Clear();
            }
        }

        //Mana restoreren + check ups
        public void RestoreMp(string title, int priceMp)
        {
            int restore = priceMp;
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            player.DataBorder();
            Console.WriteLine(title);

            if (player.GetMana() == player.GetMaxMana())
            {
                Console.WriteLine("\n$-------------------------------------------------------------------------$");
                Console.WriteLine("\nLow on MAA-NA mister, well take this goooood shot this fine booty!\n\nIt will make you feeel gooood!\n");
                Console.WriteLine("\n$-------------------------------------------------------------------------$");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You already have max MAA-NA, stop playing these games!\n\nGet out of here!");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n\nPress ENTER to continue . . .");
                Console.ReadLine();
                Console.Clear();
            }
            else if (player.GetGold() >= restore)
            {
                //GOLD VERLAGEN
                player.GoldSpend(player.GetGold() - priceMp);

                CoolPrint("SLURP SLUURP SLUUUURP - REEEEE!");
                player.GainMana(restore);

                if (player.GetMana() >= player.GetMaxMana())
                {
                    player.SetMana(player.GetMaxMana());
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" -You gained " + (restore) + " MP");
                Console.ForegroundColor = ConsoleColor.Yellow;
                FileCompiler.DataPlayerFile(player.GetPlayerName(), player.GetLevel(), player.GetGold(), player.GetCurrentHealth(), player.GetMana());
                Console.Write("\nPress ENTER to continue . . .");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("you're....broke... GET LOSSSSTTTT!!!");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n\nPress ENTER to continue . . .");
                Console.ReadLine();
                Console.Clear();
            }
        }

        //Checker om te kijken als de speler hoog genoeg is zoniet stuurt de console em trg -p a i n
        public void LeavePlace()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            //Checker
            if (player.GetLevel() < player.GetMap()[player.IndexCurrentLocation() + 1].zoneLevel)
            {
                int difference = player.GetMap()[player.IndexCurrentLocation() + 1].zoneLevel - player.GetLevel();
                Console.Write("\n$-------------------------------------------------------------------------$");
                Console.Write("\n\t\t\t\tS o r r y");
                Console.WriteLine("\n$-------------------------------------------------------------------------$");
                Console.WriteLine("\nYour level isn't high enough, you need to return");
                Console.WriteLine("\nRaise your rank by " + difference + " lvl('s) to enter the next area!");
                Console.Write("\nPress ENTER to continue . . .");
            }
            else
            {
                string footstep = @" oOOO()
 /  _)
 |  (
 \__)  ()OOOo
        (_  \
         )  |
 oOOO()  (__/
 /  _)
 |  (
 \__)  ()OOOo
        (_  \
         )  |
 oOOO()  (__/
 /  _)
 |  (
 \__)  ()OOOo
        (_  \
         )  |
 oOOO()  (__/
 /  _)
 |  (
 \__)  ()OOOo
        (_  \
         )  |
jgs      (__/";

                Console.SetCursorPosition(0, 1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tMOVING TO NEXT AREA");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(footstep);
                Console.Write("\nPress ENTER to continue . . .");
                //Move to next location
                player.SendToNextLocation();
            }
        }

        //Game afsluiten en saven
        public void QuitnSave()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            FileCompiler.DataPlayerFile(player.GetPlayerName(), player.GetLevel(), player.GetGold(), player.GetCurrentHealth(), player.GetMana());
            Console.SetCursorPosition(0, 3);
            string title = @"   ____  _    _ _______ ______ _____   __          ______  _____  _      _____  
  / __ \| |  | |__   __|  ____|  __ \  \ \        / / __ \|  __ \| |    |  __ \ 
 | |  | | |  | |  | |  | |__  | |__) |  \ \  /\  / | |  | | |__) | |    | |  | |
 | |  | | |  | |  | |  |  __| |  _  /    \ \/  \/ /| |  | |  _  /| |    | |  | |
 | |__| | |__| |  | |  | |____| | \ \     \  /\  / | |__| | | \ \| |____| |__| |
  \____/ \____/   |_|  |______|_|  \_\     \/  \/   \____/|_|  \_|______|_____/ 
                                                                                
";
            string title1 = @"(^ _ ^)/";
            Console.SetCursorPosition(0, 3);
            Console.WriteLine(title);
            Console.WriteLine(title1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Thank you for playing, see you another time!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //quitter
            Environment.Exit(0);
            Console.ReadLine();
            Console.Clear();
        }

        //Locaties Afprinten - ASCII ART
        public void LocationPineCo()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            Console.ForegroundColor = ConsoleColor.Green;
            string tree = @"             ^  ^  ^   ^      ___I_      ^  ^   ^  ^  ^   ^  ^
            /|\/|\/|\ /|\    /\-_--\    /|\/|\ /|\/|\/|\ /|\/|\
            /|\/|\/|\ /|\   /  \_-__\   /|\/|\ /|\/|\/|\ /|\/|\
            /|\/|\/|\ /|\   |[]| [] |   /|\/|\ /|\/|\/|\ /|\/|\";

            Console.SetCursorPosition(0, 3);
            Console.WriteLine(tree);
            Console.ForegroundColor = ConsoleColor.Yellow;

            //functie border
            player.DataBorder();
        }

        public void LocationBilbo()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            Console.ForegroundColor = ConsoleColor.White;
            string city = @"~         ~~          __
       _T      .,,.    ~--~ ^^
 ^^   // \                    ~
      ][O]    ^^      ,-~ ~
   /''-I_I         _II____
__/_  /   \ ______/ ''   /'\_,__
  | II--'''' \,--:--..,_/,.-{ },
; '/__\,.--';|   |[] .-.| O{ _ }
:' |  | []  -|   ''--:.;[,.'\,/
'  |[]|,.--'' '',   ''-,.    |
  ..    ..-''    ;       ''. '";


            Console.SetCursorPosition(0, 3);
            Console.WriteLine(city);

            //functie border
            player.DataBorder();
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        public void LocationZeratros()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            Console.ForegroundColor = ConsoleColor.Red;
            string zeratros = @"               (                                (
                )           )        (                   )
              (                       )      )            .---.
          )              (     .-""-.       (        (   /     \
         ( .-""-.  (      )   / _  _ \        )       )  |() ()|
          / _  _ \   )        |(_\/_)|  .---.   (        (_ 0 _)
          |(_)(_)|  ( .---.   (_ /\ _) /     \    .-""-.  |xxx|
          (_ /\ _)   /     \   |v==v|  |<\ />|   / _  _ \ '---'
           |wwww|    |(\ /)|(  '-..-'  (_ A _)   |/_)(_\|
           '-..-'    (_ o _)  )  .---.  |===|    (_ /\ _)
                      |===|  (  /     \ '---'     |mmmm|
                  jgs '---'     |{\ /}|           '-..-'
                                (_ V _)
                                 | |/|";


            Console.WriteLine(zeratros);

            //functie border
            player.DataBorder();
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        public void TheEnd()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            String end = @"_________          _______    _______  _        ______  
\__   __/|\     /|(  ____ \  (  ____ \( (    /|(  __  \ 
   ) (   | )   ( || (    \/  | (    \/|  \  ( || (  \  )
   | |   | (___) || (__      | (__    |   \ | || |   ) |
   | |   |  ___  ||  __)     |  __)   | (\ \) || |   | |
   | |   | (   ) || (        | (      | | \   || |   ) |
   | |   | )   ( || (____/\  | (____/\| )  \  || (__/  )
   )_(   |/     \|(_______/  (_______/|/    )_)(______/ 
                                                       ";
            Console.WriteLine(end);

            //functie border
            player.DataBorder();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Congratulations on finishing the demo!");
            Console.Write("\nPress ENTER to continue . . .");
            QuitnSave();

        }

        //Leest de gegevens van de opgeslagen save en update alle gegevens
        public void ContinueGameData()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            int cHp = int.Parse(FileCompiler.GetCurrentHealth());
            int lvl = int.Parse(FileCompiler.GetLevel());
            int mana = int.Parse(FileCompiler.GetCurrentMana());

            player.SetCurrentHealth(cHp);
            player.SetMaxHp(player.GetMaxHp() + (lvl * 5) - 5);
            player.SetAttack(player.GetCurrentAttack() + (lvl * 2) - 2);
            player.SetLevel(lvl);
            player.SetMana(mana);
            player.SetMaxMana(player.GetMaxMana() + (lvl * 5) - 5);
        }

        // Printfunctie
        public void CoolPrint(string wText)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            var randSleep = new Random();
            for (int i = 0; i < wText.Length; i++)
            {
                Console.Write(" ");
                Console.Write(wText[i]);
                Thread.Sleep(randSleep.Next(25, 50));
            }
            Thread.Sleep(1000);
        }

        //Monster toevoegen aan een bepaalde locatie - Setter
        public void AddMonster(Monsters monster)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            locatieMonsters.Add(monster);
        }

        //Returned de list
        public List<Monsters> GetMonsterList()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return locatieMonsters;
        }
    }
}
