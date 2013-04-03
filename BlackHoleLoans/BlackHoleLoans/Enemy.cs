using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace BlackHoleLoans
{
    public class Enemy
    {
        public string Name { get;set; }
        EnemyStatistics enemyStats;
        public bool isDead { get; set; }
        public int whichAi { get; set; }
        public Skill skillA { get; set; }
        public Skill skillB { get; set; }
        Random random;
        //creates an enemy without skills
        public Enemy(int att, int def, int con,int health, string n)
        {
            enemyStats = new EnemyStatistics(att,def,con,health);
            Name = n;
            whichAi = 1;
            isDead = false;
        }
        //creates an enemy with 1 skill
        public Enemy(int att, int def, int con,int health, string n,Skill a)
        {
            enemyStats = new EnemyStatistics(att, def, con,health);
            Name = n;
            skillA = a;
            random = new Random();
            whichAi = 2;
            isDead = false;
        }
        //creates an enemy with 2 skills
        public Enemy(int att, int def, int con,int health, string n, Skill a,Skill b)
        {
            enemyStats = new EnemyStatistics(att, def, con,health);
            Name = n;
            skillA = a;
            skillB = b;
            random = new Random();
            whichAi = 3;
            isDead = false;
        }

        public EnemyStatistics GetEnemyStats()
        {
            return enemyStats;
        }

        public void ExecuteAI1(Player p,Enemy e)
        {
            int damage = enemyStats.Attack - p.GetPlayerStats().Defence;
            if (damage < 0)
            {
                damage = 1;
            }
            p.GetPlayerStats().SubtractHealth(damage);
        }
        
        public Skill ExecuteAI2(Player p,Enemy e)
        {
            int randomNumber = random.Next(1,100);
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
                    if (damage < 0)
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

        public Skill ExecuteAI3(Player p,Enemy e)
        {
            Skill chosenSkill;
            int randomNumber = random.Next(1,100);

            if(randomNumber > 50)
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
    }
}
