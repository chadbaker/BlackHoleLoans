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
    class Combat
    {
        #region global variables
        private Game1 maingame;
        private Queue<string> messageQueue;
        private SpriteBatch spriteBatch;
        private ContentManager _content;
        private int _height, _width,menuoption;
        private int lastPlayerHealth,lastEnemyHealth;
        private Texture2D combatmenubase,cursor,dummyplayertexture,dummyenemytexture,healthbar;
        private SpriteFont combatfontbig,combatfontsmall;
        private Player dummyplayer;
        private Enemy dummyenemy;
        KeyboardState prevKeyboardState, currentKeyboardState;
        private static readonly TimeSpan menuinterval = TimeSpan.FromMilliseconds(100);
        private static readonly TimeSpan messageinterval = TimeSpan.FromMilliseconds(1500);
        private TimeSpan lastMenuChoiceTime,lastMessageTime;
        private bool executeMenuLogic;
        #endregion
        public enum MenuOption {Fight=1,Run=2,Attack=3,SkillA=4,SkillB=5}

        public Combat(ContentManager content,int height, int width,Game1 game)
        {
            _content = content;
            _height = height;
            _width = width;
            maingame = game;
            menuoption = (int)MenuOption.Fight;
            prevKeyboardState = Keyboard.GetState();
            currentKeyboardState = Keyboard.GetState();
            dummyplayer = new Player(5,5,5,"Player",new Skill(Skill.Skills.Fire),new Skill(Skill.Skills.Heal));
            dummyenemy = new Enemy(8,1,0,"Dummy",1);
            messageQueue = new Queue<string>();

        }

        public void LoadContent()
        {
            #region textures
            combatmenubase = _content.Load<Texture2D>("Combat/combatmenubase");
            cursor = _content.Load<Texture2D>("Combat/cursor");
            dummyplayertexture = _content.Load<Texture2D>("Combat/dummyplayertexture");
            dummyenemytexture = _content.Load<Texture2D>("Combat/dummyenemytexture");
            healthbar = _content.Load<Texture2D>("Combat/healthbar");
            #endregion 
            #region fonts
            combatfontbig = _content.Load<SpriteFont>("Combat/combatfontbig");
            combatfontsmall = _content.Load<SpriteFont>("Combat/combatfontsmall");
            #endregion
        }

        public void Update(GameTime gameTime)
        {
            prevKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            #region combat menu logic
            if (executeMenuLogic)
            {
                if (prevKeyboardState.IsKeyUp(Keys.Left) &&
                    currentKeyboardState.IsKeyDown(Keys.Left) &&
                    menuoption == (int)MenuOption.Run)
                {
                    menuoption = (int)MenuOption.Fight; 
                }
                else if (prevKeyboardState.IsKeyUp(Keys.Right) &&
                    currentKeyboardState.IsKeyDown(Keys.Right) &&
                    menuoption == (int)MenuOption.Fight)
                {
                    menuoption = (int)MenuOption.Run; ;
                }
                if (menuoption == (int)MenuOption.Fight &&
                    prevKeyboardState.IsKeyUp(Keys.Z) &&
                    currentKeyboardState.IsKeyDown(Keys.Z))
                {
                    lastMenuChoiceTime = gameTime.TotalGameTime;
                    menuoption = (int)MenuOption.Attack; ;
                }
                if (menuoption == (int)MenuOption.Run &&
                    prevKeyboardState.IsKeyUp(Keys.Z) &&
                    currentKeyboardState.IsKeyDown(Keys.Z))
                {
                    AddMessage("Player ran away!");
                }
                if (menuoption == (int)MenuOption.Attack &&
                    prevKeyboardState.IsKeyUp(Keys.Z) &&
                    currentKeyboardState.IsKeyDown(Keys.Z))
                {
                    if (lastMenuChoiceTime + menuinterval < gameTime.TotalGameTime)
                    {
                        StartTurn(gameTime,MenuOption.Attack);
                        menuoption = (int)MenuOption.Fight;
                    }
                }

                if (menuoption == (int)MenuOption.SkillA &&
                    prevKeyboardState.IsKeyUp(Keys.Z) &&
                    currentKeyboardState.IsKeyDown(Keys.Z))
                {
                    if (lastMenuChoiceTime + menuinterval < gameTime.TotalGameTime)
                    {
                        StartTurn(gameTime,MenuOption.SkillA);
                        menuoption = (int)MenuOption.Fight;
                    }
                }

                if (menuoption == (int)MenuOption.SkillB &&
                    prevKeyboardState.IsKeyUp(Keys.Z) &&
                    currentKeyboardState.IsKeyDown(Keys.Z))
                {
                    if (lastMenuChoiceTime + menuinterval < gameTime.TotalGameTime)
                    {
                        StartTurn(gameTime, MenuOption.SkillB);
                        menuoption = (int)MenuOption.Fight;
                    }
                }

                if ((menuoption == (int)MenuOption.Attack ||
                    menuoption == (int)MenuOption.SkillA ||
                    menuoption == (int)MenuOption.SkillB) &&
                    prevKeyboardState.IsKeyUp(Keys.X) &&
                    currentKeyboardState.IsKeyDown(Keys.X))
                {
                    menuoption = (int)MenuOption.Fight;
                }

                if(menuoption==(int)MenuOption.Attack&&
                    prevKeyboardState.IsKeyUp(Keys.Down)&&
                    currentKeyboardState.IsKeyDown(Keys.Down))
                {
                    lastMenuChoiceTime = gameTime.TotalGameTime;
                    menuoption = (int)MenuOption.SkillA;
                }

                if(menuoption==(int)MenuOption.SkillA&&
                    prevKeyboardState.IsKeyUp(Keys.Down)&&
                    currentKeyboardState.IsKeyDown(Keys.Down))
                {
                    if (lastMenuChoiceTime + menuinterval < gameTime.TotalGameTime)
                    {
                        menuoption = (int)MenuOption.SkillB;
                    }
                }

                if (menuoption == (int)MenuOption.SkillA &&
                    prevKeyboardState.IsKeyUp(Keys.Up)&&
                    currentKeyboardState.IsKeyDown(Keys.Up))
                {
                    menuoption = (int)MenuOption.Attack;
                }

                if(menuoption == (int)MenuOption.SkillB&&
                    prevKeyboardState.IsKeyUp(Keys.Up)&&
                    currentKeyboardState.IsKeyDown(Keys.Up))
                {
                    menuoption = (int)MenuOption.SkillA;
                }

            }
            #endregion
        }

        public void Draw(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            spriteBatch.DrawString(combatfontsmall,mouseState.X+" , "+mouseState.Y,new Vector2(0,0),Color.White);
            DrawCombatMenu();
            DrawEntities();
            DrawMessageQueue(gameTime);
        }

        public void UnloadContent()
        {
            _content.Unload();
        }

        public void SetSpriteBatch(SpriteBatch sb)
        {
            spriteBatch = sb;
        }

        public void DrawMessage(String message)
        {
            spriteBatch.Draw(combatmenubase, new Rectangle(0,0,_width,combatmenubase.Height/4),Color.White);
            Vector2 textCenter = combatfontsmall.MeasureString(message)*.5f;
            spriteBatch.DrawString(combatfontsmall, message, new Vector2(_width/2 - textCenter.X, combatmenubase.Height / 8 - textCenter.Y), Color.White);
        }

        public void AddMessage(String m)
        {
            messageQueue.Enqueue(m);
        }

        public void StartTurn(GameTime gameTime, MenuOption menuOption)
        {
            lastMessageTime = gameTime.TotalGameTime;
            if (menuOption == MenuOption.Attack)
            {
                lastPlayerHealth = dummyplayer.GetPlayerStats().Health;
                lastEnemyHealth = dummyenemy.GetEnemyStats().Health;
                dummyplayer.ExecuteBasicAttack(dummyenemy);
                AddMessage(dummyplayer.Name+" attacked " + dummyenemy.Name+"!");
                DetermineMessage();
            }
            else if(menuOption == MenuOption.SkillA)
            {
                lastPlayerHealth = dummyplayer.GetPlayerStats().Health;
                lastEnemyHealth = dummyenemy.GetEnemyStats().Health;
                dummyplayer.ExecuteSkillA(dummyenemy,dummyplayer);
                AddMessage(dummyplayer.Name + " used " + dummyplayer.skillA.Name+"!");
                DetermineMessage();
            }

            else if(menuOption == MenuOption.SkillB)
            {
                lastPlayerHealth = dummyplayer.GetPlayerStats().Health;
                lastEnemyHealth = dummyenemy.GetEnemyStats().Health;
                dummyplayer.ExecuteSkillB(dummyenemy,dummyplayer);
                AddMessage(dummyplayer.Name + " used " + dummyplayer.skillB.Name + "!");
                DetermineMessage();
            }

            if (dummyenemy.GetEnemyStats().Health > 0)
            {
                lastPlayerHealth = dummyplayer.GetPlayerStats().Health;
                lastEnemyHealth = dummyenemy.GetEnemyStats().Health;
                dummyenemy.ExecuteAI1(dummyplayer);
                AddMessage(dummyenemy.Name + " attacked " + dummyplayer.Name + "!");
                DetermineMessage();
            }
        }

        public void DrawCombatMenu()
        {
            Vector2 textSize1 = combatfontbig.MeasureString("FIGHT");
            Vector2 textCenter1 = combatfontbig.MeasureString("FIGHT")*.5f;
            Vector2 textCenter2 = combatfontbig.MeasureString("RUN")*.5f;
            Vector2 textCenter3 = combatfontsmall.MeasureString("Attack") * .5f;
            spriteBatch.Draw(combatmenubase, new Rectangle(0, 4*_height/6,
                _width, combatmenubase.Height), 
                Color.White);
            spriteBatch.DrawString(combatfontbig, "FIGHT", new Vector2(_width/8, 5*_height/6-textCenter1.Y), Color.White);
            spriteBatch.DrawString(combatfontbig, "RUN", new Vector2(6*_width/8, 5*_height/6-textCenter2.Y), Color.White);
            if (executeMenuLogic)
            {
                if(menuoption==(int)MenuOption.Attack||menuoption==(int)MenuOption.SkillA||menuoption==(int)MenuOption.SkillB)
                {
                    spriteBatch.DrawString(combatfontsmall, "Attack", 
                        new Vector2(_width/8+textSize1.X+cursor.Width, 
                            31*_height/42-textCenter3.Y), Color.White);
                    spriteBatch.DrawString(combatfontsmall, dummyplayer.skillA.Name,
                        new Vector2(_width / 8 + textSize1.X + cursor.Width,
                            35 * _height / 42 - textCenter3.Y), Color.White);
                    spriteBatch.DrawString(combatfontsmall, dummyplayer.skillB.Name,
                        new Vector2(_width / 8 + textSize1.X + cursor.Width,
                            39 * _height / 42 - textCenter3.Y), Color.White);
                }
                if (menuoption == (int)MenuOption.Fight)
                {
                    spriteBatch.Draw(cursor, 
                        new Rectangle(_width/8-cursor.Width,
                            5*_height/6-(int)textCenter1.Y, cursor.Width, 
                            cursor.Height), 
                            Color.White);
                }
                else if (menuoption == (int)MenuOption.Run)
                {
                    spriteBatch.Draw(cursor, 
                        new Rectangle(6 * _width / 8 - cursor.Width,
                            5 * _height / 6 - (int)textCenter1.Y, cursor.Width,
                            cursor.Height),
                            Color.White);
                }
                else if (menuoption == (int)MenuOption.Attack)
                {
                     spriteBatch.Draw(cursor, 
                        new Rectangle(295, 31 * _height / 42 - (int)textCenter3.Y,
                            cursor.Width / 2, cursor.Height / 2), Color.White);
                }

                else if (menuoption == (int)MenuOption.SkillA)
                {
                    spriteBatch.Draw(cursor,
                        new Rectangle(295, 35 * _height / 42 - (int)textCenter3.Y,
                            cursor.Width / 2, cursor.Height / 2), Color.White);
                }

                else if(menuoption == (int)MenuOption.SkillB)
                {
                    spriteBatch.Draw(cursor,
                        new Rectangle(295, 39 * _height / 42 - (int)textCenter3.Y,
                            cursor.Width / 2, cursor.Height / 2), Color.White);
                }

            }
        }

        public void DrawEntities()
        {
            spriteBatch.Draw(dummyplayertexture, new Rectangle(_width/8, _height/4,
                dummyplayertexture.Width, dummyplayertexture.Height), Color.White);
            spriteBatch.Draw(dummyenemytexture,
                new Rectangle(7*_width/8-dummyenemytexture.Width, _height/4,
                dummyenemytexture.Width,dummyenemytexture.Height), Color.White);
            spriteBatch.DrawString(combatfontsmall, 
                dummyplayer.GetPlayerStats().Health + "/"
                +dummyplayer.GetPlayerStats().TotalHealth, 
                new Vector2(_width/8, _height/4+dummyplayertexture.Height+healthbar.Height), 
                Color.Red);
            //uncomment for enemy health to appear on screen
            /*spriteBatch.DrawString(combatfontsmall,
                dummyenemy.GetEnemyStats().Health + "/"+dummyenemy.GetEnemyStats().TotalHealth,
                new Vector2(7 * _width / 8 - dummyenemytexture.Width,
                    _height / 4 + dummyplayertexture.Height+healthbar.Height),
                    Color.Red);
             */
            DrawHealthBars(_width /8, 7 * _width / 8 - dummyenemytexture.Width,
                _height / 4 + dummyplayertexture.Height,
                _height / 4 + dummyplayertexture.Height);
        }

        public void DrawMessageQueue(GameTime gameTime)
        {
            if (messageQueue.Count > 0)
            {
                executeMenuLogic = false;
                if (lastMessageTime + messageinterval > gameTime.TotalGameTime)
                {
                    DrawMessage(messageQueue.Peek());
                }
                else
                {
                    messageQueue.Dequeue();
                    lastMessageTime = gameTime.TotalGameTime;
                }
            }
            else
            {
                executeMenuLogic = true;
            }
        }

        public void DrawHealthBars(int x,int x2,int y,int y2)
        {
            //player health bar
            spriteBatch.Draw(healthbar,new Rectangle(x,y,healthbar.Width,healthbar.Height),Color.Black);
            spriteBatch.Draw(healthbar, new Rectangle(x+5, y+5, healthbar.Width-10, healthbar.Height-10), Color.Gray);
            spriteBatch.Draw(healthbar, new Rectangle(x+5, y+5,
                (int)((healthbar.Width - 10) * 
                (double)dummyplayer.GetPlayerStats().Health / 
                (double)dummyplayer.GetPlayerStats().TotalHealth),
                healthbar.Height - 10), Color.Red);
            //enemy health bar
            spriteBatch.Draw(healthbar, new Rectangle(x2, y2, healthbar.Width, healthbar.Height), Color.Black);
            spriteBatch.Draw(healthbar, new Rectangle(x2 + 5, y2 + 5, healthbar.Width - 10, healthbar.Height - 10), Color.Gray);
            spriteBatch.Draw(healthbar, new Rectangle(x2 + 5, y2 + 5, (int)((healthbar.Width - 10) *
                (double)dummyenemy.GetEnemyStats().Health /
                (double)dummyenemy.GetEnemyStats().TotalHealth),
                healthbar.Height - 10), Color.Red);
        }

        public void DetermineMessage()
        {
            //if block dealing with the player
            if (lastPlayerHealth > dummyplayer.GetPlayerStats().Health &&
                dummyplayer.GetPlayerStats().Health != 0)
            {
                AddMessage(dummyplayer.Name + " took " + 
                    (lastPlayerHealth - dummyplayer.GetPlayerStats().Health)
                    + " damage!");
            }

            else if (lastPlayerHealth < dummyplayer.GetPlayerStats().Health)
            {
                AddMessage(dummyplayer.Name + " recovered " +
                    (dummyplayer.GetPlayerStats().Health-lastPlayerHealth)
                    +" health!");
            }

            else if (dummyplayer.GetPlayerStats().Health == 0)
            {
                AddMessage(dummyplayer.Name + " fainted!");
            }
            //end if block dealing with the player
            //if block dealing with the enemy
            if (lastEnemyHealth > dummyenemy.GetEnemyStats().Health &&
                dummyenemy.GetEnemyStats().Health != 0)
            {
                AddMessage(dummyenemy.Name + " took " + 
                    (lastEnemyHealth - dummyenemy.GetEnemyStats().Health) 
                    + " damage!");
            }
            else if(dummyenemy.GetEnemyStats().Health == 0 && dummyenemy.isDead==false)
            {
                AddMessage(dummyenemy.Name + " died!");
                dummyenemy.isDead = true;
            }
            //end if block dealing with enemy
            if(lastPlayerHealth == dummyplayer.GetPlayerStats().Health &&
                lastEnemyHealth==dummyenemy.GetEnemyStats().Health)
            {
                AddMessage("No effect..");
            }
        }
    }
}
