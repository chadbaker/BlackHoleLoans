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
    public class CombatTest
    {
        private ContentManager _content;
        private SpriteBatch spriteBatch;
        private SpriteFont combatfontbig,combatfontsmall;
        private Texture2D arrowleft,arrowright;
        private int playerAttack, enemyAttack, playerDefense, enemyDefense, playerConcentration, enemyConcentration, playerHealth, enemyHealth;
        private int menuoptiony;
        private int height;
        private int width;
        private Skill playerSkillA, playerSkillB, enemySkillA, enemySkillB;
        KeyboardState prevKeyboardState, currentKeyboardState;
        public Player player { get; set; }
        public Enemy enemy { get; set; }
        private bool isPlayer;
        private Color color;
        private int pskillA;
        private int pskillB;
        private int eskillA;
        private int eskillB;
        Game1 game;

        public CombatTest(ContentManager content,Game1 g)
        {
            game = g;
            prevKeyboardState = Keyboard.GetState();
            currentKeyboardState = Keyboard.GetState();
            _content = content;
            menuoptiony = 1;
            isPlayer = true;
            height = 600;
            width = 800;
            #region initial stats
            playerAttack = 5;
            enemyAttack = 5;
            playerDefense = 5;
            enemyDefense = 5;
            playerConcentration = 5;
            enemyConcentration = 5;
            playerHealth = 10;
            enemyHealth = 10;
            pskillA = 0;
            pskillB = 1;
            eskillA = 0;
            eskillB = 1;
            #endregion
        }

        public void LoadContent()
        {
            arrowleft = _content.Load<Texture2D>("CombatTest/arrowleft");
            arrowright = _content.Load<Texture2D>("CombatTest/arrowright");
            combatfontbig = _content.Load<SpriteFont>("Combat/combatfontbig");
            combatfontsmall = _content.Load<SpriteFont>("Combat/combatfontsmall");
        }

        public void Update(GameTime gameTime)
        {
            prevKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            #region up and down movement
            if (prevKeyboardState.IsKeyUp(Keys.Up) && currentKeyboardState.IsKeyDown(Keys.Up))
            {
                if (menuoptiony != 1)
                {
                    menuoptiony--;
                }
            }
            if (prevKeyboardState.IsKeyUp(Keys.Down) && currentKeyboardState.IsKeyDown(Keys.Down))
            {
                if (menuoptiony != 6)
                {
                    menuoptiony++;
                }
            }
            #endregion
            #region incrementing
            if (prevKeyboardState.IsKeyUp(Keys.Left) && currentKeyboardState.IsKeyDown(Keys.Left))
            {
                if (menuoptiony == 1)
                {
                    if (playerAttack > 0)
                    {
                        playerAttack--;
                    }
                }
                if (menuoptiony == 2)
                {
                    if (playerDefense > 0)
                    {
                        playerDefense--;
                    }
                }
                if (menuoptiony == 3)
                {
                    if (playerConcentration > 0)
                    {
                        playerConcentration--;
                    }
                }
                if (menuoptiony == 4)
                {
                    if (playerHealth > 4)
                    {
                        playerHealth -= 5;
                    }
                }
                if (menuoptiony == 5)
                {
                    if (pskillA > 0)
                    {
                        pskillA--;
                    }
                    else
                    {
                        pskillA = 2;
                    }
                }
                if (menuoptiony == 6)
                {
                    if (pskillB > 0)
                    {
                        pskillB--;
                    }
                    else
                    {
                        pskillB = 2;
                    }
                }
            }
            if (prevKeyboardState.IsKeyUp(Keys.Right) && currentKeyboardState.IsKeyDown(Keys.Right))
            {
                if (menuoptiony == 1)
                {
                    playerAttack++;
                }
                if (menuoptiony == 2)
                {
                    playerDefense++;
                }
                if (menuoptiony == 3)
                {
                    playerConcentration++;
                }
                if (menuoptiony == 4)
                {
                    playerHealth += 5;
                }
                if (menuoptiony == 5)
                {
                    if (pskillA < 2)
                    {
                        pskillA++;
                    }
                    else
                    {
                        pskillA = 0;
                    }
                }
                if (menuoptiony == 6)
                {
                    if (pskillB < 2)
                    {
                        pskillB++;
                    }
                    else
                    {
                        pskillB = 0;
                    }
                }
            }
            #endregion
            #region advancing
            if (prevKeyboardState.IsKeyUp(Keys.Enter) && currentKeyboardState.IsKeyDown(Keys.Enter))
            {
                if (isPlayer)
                {
                    playerSkillA = new Skill((Skill.Skills)Skill.Skills.ToObject(typeof(Skill.Skills), pskillA));
                    playerSkillB = new Skill((Skill.Skills)Skill.Skills.ToObject(typeof(Skill.Skills), pskillB));
                    game.player = new Player(playerAttack, playerDefense, playerConcentration, playerHealth, "Player", playerSkillA, playerSkillB);
                    isPlayer = false;
                }
                else
                {
                    enemySkillA = new Skill((Skill.Skills)Skill.Skills.ToObject(typeof(Skill.Skills), eskillA));
                    enemySkillB = new Skill((Skill.Skills)Skill.Skills.ToObject(typeof(Skill.Skills), eskillB));
                    game.enemy = new Enemy(enemyAttack, enemyDefense,enemyConcentration, enemyHealth, "Dummy", enemySkillA, enemySkillB);
                    game.combat = new Combat(_content, height,width, game, game.player, game.enemy);
                    game.combat.SetSpriteBatch(spriteBatch);
                    game.combat.LoadContent();
                    game.runCombat = true;
                }
            }
            #endregion
        }

        public void Draw()
        {
            String skillName;
            color = Color.White;
            #region player or enemy
            if (isPlayer)
            {
                Vector2 textCenter = combatfontbig.MeasureString("Player") * .5f;
                spriteBatch.DrawString(combatfontbig, "Player", new Vector2(width / 2 - textCenter.X, 2 * height / 15 - textCenter.Y), Color.White);
                spriteBatch.DrawString(combatfontsmall, playerAttack+"", new Vector2(4*width/8,4*height/15), Color.White);
                spriteBatch.DrawString(combatfontsmall, playerDefense + "", new Vector2(4 * width / 8, 6 * height / 15), Color.White);
                spriteBatch.DrawString(combatfontsmall, playerConcentration + "", new Vector2(4 * width / 8, 8 * height / 15), Color.White);
                spriteBatch.DrawString(combatfontsmall, playerHealth+ "", new Vector2(4 * width / 8, 10 * height / 15), Color.White);
                spriteBatch.DrawString(combatfontsmall, Skill.Skills.GetName(typeof(Skill.Skills),pskillA), new Vector2(4 * width / 8, 12 * height / 15), Color.White);
                spriteBatch.DrawString(combatfontsmall, Skill.Skills.GetName(typeof(Skill.Skills),pskillB), new Vector2(4 * width / 8, 14 * height / 15), Color.White);
            }
            else
            {
                Vector2 textCenter = combatfontbig.MeasureString("Enemy") * .5f;
                spriteBatch.DrawString(combatfontbig, "Enemy", new Vector2(width / 2 - textCenter.X, 2 * height / 15 - textCenter.Y), Color.White);
                spriteBatch.DrawString(combatfontsmall, enemyAttack+"", new Vector2(4*width/8,4*height/15), Color.White);
                spriteBatch.DrawString(combatfontsmall, playerDefense + "", new Vector2(4 * width / 8, 6 * height / 15), Color.White);
                spriteBatch.DrawString(combatfontsmall, playerConcentration + "", new Vector2(4 * width / 8, 8 * height / 15), Color.White);
                spriteBatch.DrawString(combatfontsmall, playerHealth+ "", new Vector2(4 * width / 8, 10 * height / 15), Color.White);
                spriteBatch.DrawString(combatfontsmall, Skill.Skills.GetName(typeof(Skill.Skills),eskillA), new Vector2(4 * width / 8, 12 * height / 15), Color.White);
                spriteBatch.DrawString(combatfontsmall, Skill.Skills.GetName(typeof(Skill.Skills),eskillB), new Vector2(4 * width / 8, 14 * height / 15), Color.White);
            }
            #endregion
            #region arrows
            for (int i = 1; i < 7; i++)
            {
                spriteBatch.Draw(arrowleft, new Rectangle(2 * width / 8, (2+2*i) * height / 15, arrowleft.Width, arrowleft.Height), Color.White);
                spriteBatch.Draw(arrowright, new Rectangle(6 * width / 8, (2+2*i) * height / 15, arrowright.Width, arrowright.Height), Color.White);
            }
            #endregion
            #region stat options
            if (menuoptiony != 1)
            {
                spriteBatch.DrawString(combatfontsmall, "Attack", new Vector2(width / 8, 4 * height / 15), Color.White);
            }
            else
            {
                spriteBatch.DrawString(combatfontsmall, "Attack", new Vector2(width / 8, 4 * height / 15), Color.Yellow);
            }
            if (menuoptiony != 2)
            {
                spriteBatch.DrawString(combatfontsmall, "Defense", new Vector2(width / 8, 6 * height / 15), Color.White);
            }
            else
            {
                spriteBatch.DrawString(combatfontsmall, "Defense", new Vector2(width / 8, 6 * height / 15), Color.Yellow);
            }
            if (menuoptiony != 3)
            {
                spriteBatch.DrawString(combatfontsmall, "Concen.", new Vector2(width / 8, 8 * height / 15), Color.White);
            }
            else
            {
                spriteBatch.DrawString(combatfontsmall, "Concen.", new Vector2(width / 8, 8 * height / 15), Color.Yellow);
            }
            if (menuoptiony != 4)
            {
                spriteBatch.DrawString(combatfontsmall, "Health", new Vector2(width / 8, 10 * height / 15), Color.White);
            }
            else
            {
                spriteBatch.DrawString(combatfontsmall, "Health", new Vector2(width / 8, 10 * height / 15), Color.Yellow);
            }
            if (menuoptiony != 5)
            {
                spriteBatch.DrawString(combatfontsmall, "SkillA", new Vector2(width / 8, 12 * height / 15), Color.White);
            }
            else
            {
                spriteBatch.DrawString(combatfontsmall, "SkillA", new Vector2(width / 8, 12 * height / 15), Color.Yellow);
            }
            if (menuoptiony != 6)
            {
                spriteBatch.DrawString(combatfontsmall, "SkillB", new Vector2(width / 8, 14 * height / 15), Color.White);
            }
            else
            {
                spriteBatch.DrawString(combatfontsmall, "SkillB", new Vector2(width / 8, 14 * height / 15), Color.Yellow);
            }
            #endregion
            
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
