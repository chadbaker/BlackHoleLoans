using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using BlackHoleLoans.PlayerRelated;

namespace BlackHoleLoans
{
  public class Enemy : Entity
  {
    protected int[] path // The Enemy will move in the directions indicated by the ints in order and loop back.
    {                    // Closed loops are suggested, but the program does not enforce this!
      get;
      set;
    }

    protected int path_index = 0;

    public string Name { get; set; }
    EnemyStatistics enemyStats;
    public bool isDead { get; set; }
    public int whichAi { get; set; }
    public Skill skillA { get; set; }
    public Skill skillB { get; set; }
    Random random;
    public int lastEnemyHealth { get; set; }
    public Texture2D enemySprite { get; set; }
    public bool startCombat = false;
    int paralyzedCounter = 0;

    public Enemy(OverWorld ow, Tile t, int[] newpath, string name)
      : base(ow, t)
    {
      path_index = 0;
      path = newpath;
      Name = name;
    }

    public Enemy(OverWorld ow, Tile t, int f, int[] newpath, string name)
      : base(ow, t, f)
    {
      path_index = 0;
      path = newpath;
      Name = name;
    }



    //chad

   public Enemy(int att, int def, int con, int health, string n, Texture2D eS)
    {
      enemyStats = new EnemyStatistics(att, def, con, health);
      Name = n;
      whichAi = 1;
      isDead = false;
      enemySprite = eS;
    }
    //creates an enemy with 1 skill
   public Enemy(int att, int def, int con, int health, string n, Texture2D eS, Skill a)
    {
      enemyStats = new EnemyStatistics(att, def, con, health);
      Name = n;
      skillA = a;
      random = new Random();
      whichAi = 2;
      isDead = false;
      enemySprite = eS;
    }
    //creates an enemy with 2 skills
   public Enemy(int att, int def, int con, int health, string n, Texture2D eS, Skill a, Skill b)
    {
      enemyStats = new EnemyStatistics(att, def, con, health);
      Name = n;
      skillA = a;
      skillB = b;
      random = new Random();
      whichAi = 3;
      isDead = false;
      enemySprite = eS;
    }

    //end chad


    public override void OnCollision()
    {
      //run combat encounter
      Console.Write("Entered combat with entity by collision!\n");
      startCombat = true;
    }

    public override void OnInteract()
    {
      //run combat encounter
      Console.Write("Entered combat with entity by interaction!\n");
      //
      startCombat = true;
    }

    public override bool StartCombat()
    {
      return startCombat;
    }

    public override void OnUpdate()
    {
      if (paralyzedCounter > 0)
      {
        paralyzedCounter--;
        return;
      }

      if (MoveAdjacent(path[path_index]))
      {
        path_index++;
        if (path_index >= path.Length)
        {
          path_index = 0;
        }
      }
    }

    public override bool IsEnemy()
    {
      return true;
    }

    public void StopCombat()
    {
      startCombat = false;
    }

    //chad

    public EnemyStatistics GetEnemyStats()
    {
      return enemyStats;
    }

    public void ExecuteAI1(Player p)
    {
      int damage = enemyStats.Attack - p.GetPlayerStats().Defence;
      if (damage <= 0)
      {
        damage = 1;
      }
      p.GetPlayerStats().SubtractHealth(damage);
    }

    public Skill ExecuteAI2(Player p, Enemy e)
    {
      int randomNumber = random.Next(1, 100);
      Skill chosenSkill;
      if (randomNumber > 50)
      {
        chosenSkill = null;
        int damage = enemyStats.Attack - p.GetPlayerStats().Defence;
        if (damage < 0)
        {
          damage = 1;
        }
        p.GetPlayerStats().SubtractHealth(damage);
      }
      else
      {
        chosenSkill = skillA;
        if (skillA.isDamage)
        {
          int damage = (int)(enemyStats.Concentration * skillA.skillRatio) - p.GetPlayerStats().Defence;
          if (damage <= 0)
          {
            damage = 1;
          }
          p.GetPlayerStats().SubtractHealth(damage);
        }
        else if (skillA.isHealing)
        {
          int health = (int)(enemyStats.Concentration * skillA.skillRatio);
          e.GetEnemyStats().addHealth(health);
        }
      }
      return chosenSkill;
    }

    public Skill ExecuteAI3(Player p, Enemy e)
    {
      Skill chosenSkill;
      int randomNumber = random.Next(1, 100);

      if (randomNumber > 50)
      {
        chosenSkill = skillA;
      }
      else
      {
        chosenSkill = skillB;
      }

      if (chosenSkill.isDamage)
      {
        int damage = (int)(enemyStats.Concentration * chosenSkill.skillRatio) - p.GetPlayerStats().Defence;
        if (damage <= 0)
        {
          damage = 1;
        }
        p.GetPlayerStats().SubtractHealth(damage);
      }
      else if (chosenSkill.isHealing)
      {
        int health = (int)(enemyStats.Concentration * chosenSkill.skillRatio);
        e.GetEnemyStats().addHealth(health);
        Console.WriteLine(health);
      }
      return chosenSkill;
    }

    public bool IsAlive()
    {
      return enemyStats.Health != 0;
    }

    public void SetEnemySprites()
    {
      Texture2D[] tmp = new Texture2D[EntityAvatar.Count];
      tmp = EntityAvatar.ToArray();
      enemySprite = tmp[EntityAvatar.Count-1];
    }

    public Texture2D EnemySprite()
    {
      return enemySprite;
    }

    public void ParalyzeEnemy()
    {
      paralyzedCounter = 10;
    }
  }
}
