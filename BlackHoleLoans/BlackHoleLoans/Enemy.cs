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
        EnemyStatistics enemyStats;
        public Enemy(int att, int def, int con)
        {
            enemyStats = new EnemyStatistics(att,def,con);

        }

        public EnemyStatistics GetEnemyStats()
        {
            return enemyStats;
        }

        public void ExecuteAI1(Player p)
        {
            int damage = enemyStats.Attack - p.GetPlayerStats().Defence;
            if (damage < 0)
            {
                damage = 1;
            }
            p.GetPlayerStats().SubtractHealth(damage);
        }
        
        public void ExecuteAI2()
        {
        }

        public void ExecuteA3()
        {
        }
    }
}
