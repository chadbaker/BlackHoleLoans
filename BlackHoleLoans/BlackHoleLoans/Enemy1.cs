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
    class Enemy1
    {
        EnemyStatistics enemyStats;
        public Enemy1(int att, int def, int con)
        {
            enemyStats = new EnemyStatistics(att,def,con);

        }

        public EnemyStatistics GetEnemyStats()
        {
            return enemyStats;
        }
    }
}
