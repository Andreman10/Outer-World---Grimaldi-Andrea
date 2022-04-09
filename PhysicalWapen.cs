using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Outer_World___Grimaldi_Andrea
{
    class PhysicalWapen : Wapen
    {
        private int bleedOverTime = 2;

        //Constructor die met de gegevens van de moederklasse overschreven wordt
        public PhysicalWapen(string giveWapenName, int giveMinDmg, int giveMaxDmg, int giveLevel, int giveDmgBoostPerUpgrade, int giveBleedOverTime, Player givePlayer) : base(giveWapenName, giveMinDmg, giveMaxDmg, giveLevel, giveDmgBoostPerUpgrade, givePlayer)
        {
            this.bleedOverTime = giveBleedOverTime;
            FileCompiler.OproepKlasseObject(MethodBase.GetCurrentMethod().DeclaringType, giveWapenName);
        }

        //Override om de moederklasse over te schrijven - Base om een bepaalde functie/etc op te roepen van de moederklasse
        public override void CalculateDamage()
        {
            FileCompiler.OproepFunctieFile(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType);
            if (GetWapenLevel() >= 5)
            {
                int bleed = GetWapenLevel() + bleedOverTime;
                base.CalculateDamage();
                base.SetAttackDmg(bleed);
            }
            else
            {
                base.CalculateDamage();
            }

        }
    }
}
