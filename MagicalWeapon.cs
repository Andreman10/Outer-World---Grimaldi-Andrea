using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Outer_World___Grimaldi_Andrea
{
    class MagicalWeapon : Wapen
    {
        //Variabelen
        private int damageOverTime;
        private int damageTime;

        private int typeDamage;

        // Constructor met overerving
        public MagicalWeapon(string giveWapenName, int giveMinDmg, int giveMaxDmg, int giveLevel, int giveDamageOverTime, int giveDamagetime, int giveDmgBoostPerUpgrade, Player givePlayer) : base(giveWapenName, giveMinDmg, giveMaxDmg, giveLevel, giveDmgBoostPerUpgrade, givePlayer)
        {
            damageOverTime = giveDamageOverTime;
            damageTime = giveDamagetime;
            FileCompiler.OproepKlasseObject(MethodBase.GetCurrentMethod().DeclaringType, giveWapenName);
        }

        //Overriden van de moederfunctie
        public override void CalculateDamage()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);

            //Fireball
            if (typeDamage == 0)
            {
                damageOverTime = 2;
                damageTime = 3;

                int elemental = GetWapenLevel() + (damageOverTime * damageTime);
                base.CalculateDamage();
                base.SetAttackDmg(elemental);
            }
            //Wind Slash
            else if (typeDamage == 1)
            {
                damageOverTime = 1;
                damageTime = 2;

                int elemental = GetWapenLevel() + (damageOverTime * damageTime);
                base.CalculateDamage();
                base.SetAttackDmg(elemental);
            }
            //Water ball
            else if (typeDamage == 2)
            {
                damageOverTime = 2;
                damageTime = 2;

                int elemental = GetWapenLevel() + (damageOverTime * damageTime);
                base.CalculateDamage();
                base.SetAttackDmg(elemental);
            }
            //Voor het geval dat
            else
            {
                Console.WriteLine("Something went wrong - I'm going to return you!");
            }

                base.CalculateDamage();

        }
        //Gettter
        public int GetTypeDamage()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            return typeDamage;
        }

        //Setter -  overschreven functie die van de moederklasse
        public override void SetWeaponType(int setTo)
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            typeDamage = setTo;

        }

    }
}
