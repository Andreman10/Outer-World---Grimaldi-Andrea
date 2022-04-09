using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml; //nodig om XML te kunnen gebruiken
using System.Xml.Resolvers;
using System.Reflection;

namespace Outer_World___Grimaldi_Andrea
{
    class Monsters
    {
        // Variabelen
        private string monsterName;
        private string description;
        private int id;
        private int minDmg;
        private int maxDmg;
        private int currentHealth;
        private int maxHealth;
        private int rewardGold;
        private int rewardExp;
        private int area;

        private static int i = 0;

        // XML READER - PARSER (veel breinpijn van gekregen maar eey het werkt!)
        public Monsters()
        {
            // xmlreader aanmaken en zeggen waar onze te parsen xml bestand zich bevindt
            //XmlReader onzeReader = XmlReader.Create(@"C:\Users\Andy\Desktop\xml-studenten.xml"); - ander manier: Aanmaken document
            XmlReader ourReader = XmlReader.Create("Xml-Monsters.xml"); // dit werkt als je xml file naast je executable staat (dus in de debug\... folder)

            //zorgt ervoor dat alle non content nodes geskipped worden en dus naar de volgende node OF einde van de node gaat.
            ourReader.MoveToContent();

            //Nodig om informatie te verwerken
            //Leest de eerste element en werkt daar van verder
            while (ourReader.Read())
            {

                if (ourReader.IsStartElement())
                {
                    //CHECKERRRRRR 
                    if (ourReader.ReadToFollowing("id"))
                    {
                        ourReader.Read(); //MLeest de volgende node (hier zit data in)
                        int checkId = int.Parse(ourReader.Value);
                        id = checkId;
                    }

                    #region ass
                    if (id == i)
                    {
                        if (ourReader.ReadToFollowing("name"))
                        {
                            ourReader.Read(); //MLeest de volgende node (hier zit data in)
                            monsterName = ourReader.Value;
                        }
                        if (ourReader.ReadToFollowing("description"))
                        {
                            ourReader.Read(); //MLeest de volgende node (hier zit data in)
                            description = ourReader.Value;
                        }
                        if (ourReader.ReadToFollowing("minDmg"))
                        {
                            ourReader.Read(); //MLeest de volgende node (hier zit data in)
                            int checkMinDmg = int.Parse(ourReader.Value);
                            minDmg = checkMinDmg;
                        }
                        if (ourReader.ReadToFollowing("maxDmg"))
                        {
                            ourReader.Read(); //MLeest de volgende node (hier zit data in)
                            int checkMaxDmg = int.Parse(ourReader.Value);
                            maxDmg = checkMaxDmg;
                        }
                        if (ourReader.ReadToFollowing("currentHealth"))
                        {
                            ourReader.Read(); //MLeest de volgende node (hier zit data in)
                            int checkCurrentHealth = int.Parse(ourReader.Value);
                            currentHealth = checkCurrentHealth;
                        }
                        if (ourReader.ReadToFollowing("maxHealth"))
                        {
                            ourReader.Read(); //MLeest de volgende node (hier zit data in)
                            int checkMaxHealth = int.Parse(ourReader.Value);
                            maxHealth = checkMaxHealth;
                        }
                        if (ourReader.ReadToFollowing("rewardGold"))
                        {
                            ourReader.Read(); //MLeest de volgende node (hier zit data in)
                            int rewGold = int.Parse(ourReader.Value);
                            rewardGold = rewGold;
                        }
                        if (ourReader.ReadToFollowing("rewardExp"))
                        {
                            ourReader.Read(); //MLeest de volgende node (hier zit data in)
                            int checkRewardExp = int.Parse(ourReader.Value);
                            rewardExp = checkRewardExp;
                        }
                        if (ourReader.ReadToFollowing("area"))
                        {
                            ourReader.Read(); //MLeest de volgende node (hier zit data in)
                            int checkArea = int.Parse(ourReader.Value);
                            area = checkArea;
                        }
                        i++;
                        return;
                        #endregion
                    }
                }
            }
            FileCompiler.OproepKlasseObject(MethodBase.GetCurrentMethod().DeclaringType, monsterName);
            return;
        }

        //Getters
        //Return Area
        public int GetArea()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return area;
        }

        //Returned monsternaam
        public string GetMonsterName()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return monsterName;
        }

        public string GetDescription()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return description;
        }

        public int GetMinDmg(int level)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            minDmg += level;
            return minDmg / 2;
        }

        public int GetMaxDmg(int level)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            maxDmg += level;
            return maxDmg / 2;
        }

        //Bereken de currenthealth
        public int GetCurrentHealth(int level)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            currentHealth += level * 2;
            return currentHealth;
        }

        //Current hp terugsturen zonder berekening
        public int GetCurHp()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return currentHealth;
        }

        public int GetMaxHp()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return maxHealth;
        }

        public int GetMaxHealth(int level)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            maxHealth += level * 2;
            return maxHealth;
        }

        public int GetRewardGold(int level)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            rewardGold += level;
            return rewardGold;
        }

        public int GetRewardExp(int level)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            rewardGold += level;
            return rewardExp;
        }

        //Level berekenen afhankelijk van de zone
        public int CalculateLevel(int multiplier)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            Random rnd = new Random();
            return rnd.Next(area * multiplier + 1, area * multiplier + 5);
        }

        // damage van het monster berekenen
        public int CalculateDamage(int minDmg, int maxDmg)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            Random rnd = new Random();
            int monsterAttack = rnd.Next(minDmg, maxDmg);
            return monsterAttack;
        }

        //Setter
        public void SetCurrentHp(int max)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            currentHealth = max;
        }

    }
}

