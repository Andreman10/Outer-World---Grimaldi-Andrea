using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Reflection;

namespace Outer_World___Grimaldi_Andrea
{
    class Wapen
    {
        //Variabelen
        private string wapenName;
        private int minDmg;
        private int maxDmg;

        // Wapens upgraden om meer schade te doen
        private int wapenLevel;
        private int dmgBoostPerUpgrade;

        private int playerAttack;

        //Player - Speler had ik hier nodig om berekeningen te doen en door te sturen.
        Player player;

        //Constructor
        public Wapen(string giveWapenName, int giveMinDmg, int giveMaxDmg, int giveLevel, int giveDmgBoostPerUpgrade, Player givePlayer)
        {
            FileCompiler.OproepKlasseObject(MethodBase.GetCurrentMethod().DeclaringType, giveWapenName);
            wapenName = giveWapenName;
            minDmg = giveMinDmg;
            maxDmg = giveMaxDmg;
            wapenLevel = giveLevel;
            dmgBoostPerUpgrade = giveDmgBoostPerUpgrade;
            player = givePlayer;
        }

        //GETTERS
        //Return WapenNaam
        public string GetName()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return wapenName;
        }

        public int GetMinDmg()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return minDmg;
        }

        public int GetMaxDmg()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return maxDmg;
        }

        public int GetWapenLevel()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return wapenLevel;

        }

        //Bereken de FLAT DAMAGE
        public int GetWapenDamage()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            Random rnd = new Random();
            minDmg = minDmg + (dmgBoostPerUpgrade * (wapenLevel - 1));
            maxDmg = maxDmg + (dmgBoostPerUpgrade * (wapenLevel - 1));
            int flatDmg = rnd.Next(minDmg, maxDmg);
            return flatDmg;
        }

        public int GetAttackDmg()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return playerAttack;
        }

        //SETTERS
        //Wapenupgraden
        public void SetWeaponLevel(int newLevel)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            wapenLevel += newLevel;
        }

        // berekening damage (moest er bleed bijgerekend worden of iets soortsgelijks)
        public void SetAttackDmg(int bleed)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            playerAttack += bleed;
            GetAttackDmg();
        }

        //Spelen attack en wapen attack opstellen
        public virtual void CalculateDamage()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            playerAttack = player.GetCurrentAttack() + GetWapenDamage();
        }

        //Roep de functie damage calculate op en geef de attack hiervan trg
        public int DoAttackDmg()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            CalculateDamage();
            return playerAttack;
        }

        //Functie die overschreven wordt - magical weapon
        public virtual void SetWeaponType(int setTo)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            int variable = setTo;
        }

    }
}
