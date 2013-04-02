using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BlackHoleLoans
{
    class CombatTest
    {
        private ContentManager _content;
        private SpriteBatch spriteBatch;
        private SpriteFont combatfontbig;
        private Texture2D arrowleft,arrowright;
        private int playerAttack, enemyAttack, playerDefense, enemyDefense, playerConcentration, enemyConcentration, playerHealth, enemyHealth;
        private int menuoptiony;
        private int menuoptionx;
        private int height;
        private int width;
        private Skill playerSkillA, playerSkillB, enemySkillA, enemySkillB;
        KeyboardState prevKeyboardState, currentKeyboardState;
        public Player player { get; set; }
        public Enemy enemy { get; set; }
        private bool isPlayer;

        public CombatTest()
        {
            prevKeyboardState = Keyboard.GetState();
            currentKeyboardState = Keyboard.GetState();
            menuoptiony = 1;
            menuoptionx = 1;
            isPlayer = true;
            height = 600;
            width = 800;
        }

        public void LoadContent()
        {
            arrowleft = _content.Load<Texture2D>("CombatTest/arrowleft");
            arrowright = _content.Load<Texture2D>("CombatTest/arrowright");
            combatfontbig = _content.Load<SpriteFont>("Combat/combatfontbig");
        }

        public void Update()
        {
            prevKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            #region up and down movement
            #endregion
            #region incrementing
            #endregion
        }

        public void Draw()
        {
            if (isPlayer)
            {
            }
        }

        public void UnloadContent()
        {
            _content.Unload();
        }

        public void SetSpritebatch(SpriteBatch sb)
        {
            spriteBatch = sb;
        }
    }
}
