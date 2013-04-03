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
    public class Player
    {
        PlayerStatistics playerStats;
        Texture2D[] playerDirMovement;
        public string Name { get; set; }
        public Skill skillA{get;set;}
        public Skill skillB{get;set;}
        public bool isFainted { get; set; }
        //creates a player with skills
        public Player(int atk, int def, int con, int health,string n,Skill a, Skill b)//also pass in array of textures (player dir movement) & an int (for which class
        //the player is?) - don't need anything for the race
        {
            playerStats = new PlayerStatistics(atk, def, con,health);
            Name = n;
            skillA = a;
            skillB = b;
            isFainted = false;
        }
      
        public PlayerStatistics GetPlayerStats()
        {
            return playerStats;
        }

        public int ExecuteBasicAttack(Enemy e)
        {
            int damage = playerStats.Attack - e.GetEnemyStats().Defence;
            if (damage <= 0)
            {
                damage = 1;
            }
            e.GetEnemyStats().SubtractHealth(damage);
            return damage;
        }

        public void ExecuteSkillA(Enemy e,Player p)
        {
            if (skillA.isDamage)
            {
                int damage = (int)(playerStats.Concentration * skillA.skillRatio) - e.GetEnemyStats().Defence;
                if (damage < 0)
                {
                    damage = 1;
                }
                e.GetEnemyStats().SubtractHealth(damage);
            }
            else if (skillA.isHealing)
            {
                int health = (int)(playerStats.Concentration * skillA.skillRatio);
                p.GetPlayerStats().addHealth(health);
            }
        }

        public void ExecuteSkillB(Enemy e,Player p)
        {
            if (skillB.isDamage)
            {
                int damage = (int)(playerStats.Concentration * skillB.skillRatio) - e.GetEnemyStats().Defence;
                if (damage < 0)
                {
                    damage = 1;
                }
                e.GetEnemyStats().SubtractHealth(damage);
            }
            else if (skillB.isHealing)
            {
                int health = (int)(playerStats.Concentration * skillB.skillRatio);
                p.GetPlayerStats().addHealth(health);
            }
        }
    }
}