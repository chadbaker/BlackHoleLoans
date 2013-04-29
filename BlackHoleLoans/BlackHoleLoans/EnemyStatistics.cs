using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackHoleLoans.PlayerRelated;

namespace BlackHoleLoans
{
  public class EnemyStatistics
  {
    //int attack, defence, concentration;
    //int health, level, experience;
    private int attack, defence, concentration, health, level;
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
    public EnemyStatistics(int atk, int def, int con, int h)
    {
      attack = atk;
      defence = def;
      concentration = con;
      health = h;
      TotalHealth = health;
      level = 1;
    }
  }
}
