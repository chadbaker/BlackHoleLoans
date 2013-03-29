using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackHoleLoans
{
    class Skill
    {
        [Flags]
        public enum Skills {Fire, Ice }
        public float skillRatio { get; set; }
        public string Name { get; set; }

        public Skill(Skills skill)
        {
            if (skill.HasFlag(Skills.Fire))
            {
                skillRatio = 2.0f;
                Name = "Fire";
            }
            if (skill.HasFlag(Skills.Ice))
            {
                skillRatio = 1.5f;
                Name = "Ice";
            }
        }
    }
}
