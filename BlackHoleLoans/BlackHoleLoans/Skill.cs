using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackHoleLoans.PlayerRelated;

namespace BlackHoleLoans
{
  [Flags]
  public enum Skills { Blast, BloodShot, Leech,LaserSword }//add skill name
  public class Skill
  {
    public float skillRatio { get; set; }
    public string Name { get; set; }
    public bool isDamage;
    public bool isHealing;
    public bool isSelfDamage;
    public bool isPhysicalDamage;
    public bool isPiercing;
    public Skill(Skills skill)//add new if statement for new skill
    {
      isDamage = false;
      isHealing = false;
      isSelfDamage = false;
      isPhysicalDamage = false;
      isPiercing = false;
      if (skill.HasFlag(Skills.Blast))
      {
        skillRatio = 2.0f;
        Name = "Blast";
        isDamage = true;
        isHealing = false;
        isPhysicalDamage = false;
        isSelfDamage = false;
        isPiercing = false;
      }
      if (skill.HasFlag(Skills.BloodShot))
      {
        skillRatio = 2.0f;
        Name = "BloodShot";
        isDamage = false;
        isHealing = false;
        isPhysicalDamage = true;
        isSelfDamage = true;
        isPiercing = false;
      }
      if (skill.HasFlag(Skills.Leech))
      {
        skillRatio = 1.0f;
        Name = "Leech";
        isDamage = true;
        isHealing = true;
        isPhysicalDamage = false;
        isSelfDamage = false;
        
      }
      if (skill.HasFlag(Skills.LaserSword))
      {
          skillRatio = .75f;
          Name = "Laser Sword";
          isDamage = false;
          isHealing = false;
          isPhysicalDamage = false;
          isSelfDamage = false;
          isPiercing = true;
      }
    }
  }
}
