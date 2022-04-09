using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Outer_World___Grimaldi_Andrea
{
    class FileCompiler
    {
        //Variabelen
        //Static = Alles makkelijk over te schrijven zonder teveel extra code te schrijven
        private static string playerFile;
        private static string opgeroepenFuncties;
        private static string klasseobjecten;

        private static int functieIndex;

        //list
        private List<string> OperatorPlayerFile = new List<string>();

        // List bijhouden waar info opgeslagen wordt
        private static List<string> tempList = new List<string>();
        private static List<string> functieFile = new List<string>();
        private static List<string> klasseList = new List<string>();

        // Link(locatie) naar je eigen files - files aanmaken
        DirectoryInfo currentDir = new DirectoryInfo(".");

        //Aanmaak FileCompiler
        public FileCompiler()
        {
            playerFile = currentDir.FullName + "\\playerFile.txt"; // naam voor het pad - fullname = volledige path
            opgeroepenFuncties = currentDir.FullName + "\\opgeroepenFuncties.txt"; // fullname = volledige path
            klasseobjecten = currentDir.FullName + "\\klasseObjecten.txt";

            //SAVE UP CHECK & FILL
            string[] filer = File.ReadAllLines(playerFile);
            foreach (string s in filer)
            {
                OperatorPlayerFile.Add(s);
            }

            functieFile.Add("Startup FileCompiler in de klasse: " + this.GetType().Name + ".");
            functieFile.Add(DateTime.Now.ToString());
            functieFile.Add(" ");
            functieIndex++;

            //List overschrijven naar de tekst
            File.WriteAllLines(playerFile, OperatorPlayerFile);
            File.WriteAllLines(opgeroepenFuncties, functieFile); // eerst de string dan de list (aka file)

            //Schrijven in de file van klasse objecten
            OproepKlasseObject(MethodBase.GetCurrentMethod().DeclaringType, "Compiler");
        }

        //FunctieFile bijhouden
        public static void OproepFunctieFile(MethodBase geefFunctieNaam, Type geefKlasseNaam)
        {
            functieFile.Add("Functie " + functieIndex + " - " + geefFunctieNaam.Name + " van de klasse " + geefKlasseNaam + ".");
            functieFile.Add(DateTime.Now.ToString()); //slaat de tijd van oproep op
            functieFile.Add(" ");
            functieIndex++;

            File.WriteAllLines(opgeroepenFuncties, functieFile);
        }

        //Aangemaakt objecten bijhouden bijhouden
        public static void OproepKlasseObject(Type geefKlasseNaam, string naam)
        {
            klasseList.Add("Er is een object aangemaakt van de klasse " + geefKlasseNaam);
            klasseList.Add("Object noemt: " + naam);
            klasseList.Add(" ");

            File.WriteAllLines(klasseobjecten, klasseList);
        }

        //Datafile bijhouden (+ functiefile bijhouden)
        public static void DataPlayerFile(string giveName, int giveLevel, int giveGold, int giveCurrentHealth, int giveCurrentMana)
        {
            OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            tempList.Clear();

            //Informatie opslaan
            tempList.Add(giveName);
            tempList.Add(giveLevel.ToString());
            tempList.Add(giveGold.ToString());
            tempList.Add(giveCurrentHealth.ToString());
            tempList.Add(giveCurrentMana.ToString());
            tempList.Add(" ");

            File.WriteAllLines(playerFile, tempList);
            File.WriteAllLines(opgeroepenFuncties, functieFile);
        }

        //Getters - Informatie uit de list halen 
        //adhv van een index
        public static string GetName()
        {
            OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return File.ReadAllLines(playerFile).GetValue(0).ToString();
        }

        public static string GetLevel()
        {
            OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return File.ReadAllLines(playerFile).GetValue(1).ToString();
        }

        public static string GetGold()
        {
            OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return File.ReadAllLines(playerFile).GetValue(2).ToString();
        }

        public static string GetCurrentHealth()
        {
            OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return File.ReadAllLines(playerFile).GetValue(3).ToString();
        }

        public static string GetCurrentMana()
        {
            OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return File.ReadAllLines(playerFile).GetValue(4).ToString();
        }

        //Lijst leegmaken om nieuwe gegevens in te vullen
        public static void ClearList()
        {
            OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            tempList.Clear();
            File.WriteAllLines(playerFile, tempList);
        }

        //Checker voor de file - checkt hoeveel rijen er zijn (file leeg is of niet - zo ja = nieuwe game optie / etc.)
        public static int AmountRows()
        {
            OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return File.ReadAllLines(playerFile).Length;
        }
    }
}
