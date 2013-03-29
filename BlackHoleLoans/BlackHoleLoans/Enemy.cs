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
    class Enemy
    {
        public string Name { get;set; }
        EnemyStatistics enemyStats;
        public Enemy(int att, int def, int con, string n)
        {
            enemyStats = new EnemyStatistics(att,def,con);
            Name = n;
        }

        public EnemyStatistics GetEnemyStats()
        {
            return enemyStats;
        }

        public int ExecuteAI1(Player p)
        {
            int damage = enemyStats.Attack - p.GetPlayerStats().Defence;
            if (damage < 0)
            {
                damage = 1;
            }
            p.GetPlayerStats().SubtractHealth(damage);
            return damage;
        }
        
        public void ExecuteAI2()
        {
        }

        public void ExecuteA3()
        {
        }
    }
}
