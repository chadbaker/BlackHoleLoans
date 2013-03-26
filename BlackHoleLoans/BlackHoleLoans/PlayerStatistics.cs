using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackHoleLoans
{
    class PlayerStatistics
    {
        //int attack, defence, concentration;
        //int health, level, experience;
        private int attack, defence, concentration, health, level, experience;

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

        public PlayerStatistics(int atk, int def, int con)
        {
            attack = atk;
            defence = def;
            concentration = con;
            health = 100;
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

    }
}