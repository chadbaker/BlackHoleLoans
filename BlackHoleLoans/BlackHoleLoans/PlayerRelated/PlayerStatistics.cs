using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackHoleLoans.PlayerRelated;

namespace BlackHoleLoans
{
  public class PlayerStatistics
  {
    //int attack, defence, concentration;
    //int health, level, experience;
    private int attack, defence, concentration, health, level, experience;
    public int TotalHealth { get; set; }

    public int Attack
    {
      get { return attack; }
    }
    public int Defence
    {
      get { return defence; }
    }

    public int Concentration
    {
      get { return concentration; }
    }

    public int Health
    {
      get { return health; }
    }

    public bool isDead()
    {
      if (health <= 0)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public PlayerStatistics(int atk, int def, int con, int h)
    {
      attack = atk;
      defence = def;
      concentration = con;
      health = h;
      TotalHealth = health;
      level = 1;
      experience = 0;
    }

    public PlayerStatistics(int atk, int def, int con)
    {
      attack = atk;
      defence = def;
      concentration = con;
      health = 100;
      TotalHealth = health;
      level = 1;
      experience = 0;
    }

    public void SubtractHealth(int dmg)
    {
      health -= dmg;
      if (health < 0)
      {
        health = 0;
      }
    }

    public void addHealth(int h)
    {
      health += h;
      if (health > TotalHealth)
      {
        health = TotalHealth;
      }
    }


    /// <summary>
    /// Increments the current experience. If current experience + gained experience
    /// passes the required exp to level up, increments lvl and adds the leftover exp
    /// to 0
    /// </summary>
    /// <param name="exp"></param>
    public void GainExperience(int exp) { }

    private void LevelUp(int leftOverEXP)
    {
      attack++;
      defence++;
      concentration++;
      health += 10;
      level++;
      experience = leftOverEXP;
    }

    public void FullHeal()
    {
      health = TotalHealth;
    }
  }
}
