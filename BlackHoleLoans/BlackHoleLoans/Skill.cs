using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackHoleLoans
{
    public class Skill
    {
        [Flags]
        public enum Skills {Fire, Ice, Heal }
        public float skillRatio { get; set; }
        public string Name { get; set; }
        public bool isDamage;
        public bool isHealing;
        public Skill(Skills skill)
        {
            isDamage = false;
            isHealing = false;

            if (skill.HasFlag(Skills.Fire))
            {
                skillRatio = 2.0f;
                Name = "Fire";
                isDamage = true;
                isHealing = false;
            }
            if (skill.HasFlag(Skills.Ice))
            {
                skillRatio = 1.5f;
                Name = "Ice";
                isDamage = true;
                isHealing = false;
            }
            if(skill.HasFlag(Skills.Heal))
            {
                skillRatio = 1.0f;
                Name = "Heal";
                isDamage = false;
                isHealing = true;
            }
        }
    }
}
