﻿using System;
using System.Collections.Generic;
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
        private SpriteBatch spriteBatch;
        private ContentManager _content;
        private int _height, _width,menuoption;
        private Texture2D combatmenubase,cursor,dummyplayertexture,dummyenemytexture;
        private SpriteFont combatfontbig,combatfontsmall;
        private Player dummyplayer;
        private Enemy1 dummyenemy;
        KeyboardState prevKeyboardState, currentKeyboardState;
        #endregion
        public Combat(ContentManager content,int height, int width,Game1 game)
        {
            _content = content;
            _height = height;
            _width = width;
            maingame = game;
            menuoption = 1;
            prevKeyboardState = Keyboard.GetState();
            currentKeyboardState = Keyboard.GetState();
            dummyplayer = new Player(5,5,5);
            dummyenemy = new Enemy1(5,1,0);
        }

        public void LoadContent()
        {
            #region textures
            combatmenubase = _content.Load<Texture2D>("Combat/combatmenubase");
            cursor = _content.Load<Texture2D>("Combat/cursor");
            dummyplayertexture = _content.Load<Texture2D>("Combat/dummyplayertexture");
            dummyenemytexture = _content.Load<Texture2D>("Combat/dummyenemytexture");
            #endregion 
            #region fonts
            combatfontbig = _content.Load<SpriteFont>("Combat/combatfontbig");
            combatfontsmall = _content.Load<SpriteFont>("Combat/combatfontsmall");
            #endregion
        }

        public void Update()
        {
            prevKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            if(prevKeyboardState.IsKeyUp(Keys.Left)&&
                currentKeyboardState.IsKeyDown(Keys.Left)&&menuoption==2)
            {
                menuoption = 1;
            }
            else if(prevKeyboardState.IsKeyUp(Keys.Right)&&currentKeyboardState.IsKeyDown(Keys.Right)&&menuoption==1)
            {
                menuoption = 2;
            }
            if(menuoption==1&&prevKeyboardState.IsKeyUp(Keys.Z)&&currentKeyboardState.IsKeyDown(Keys.Z))
            {
                dummyenemy.GetEnemyStats().SubtractHealth(dummyplayer.GetPlayerStats().Attack-dummyenemy.GetEnemyStats().Defence);
                if(dummyenemy.GetEnemyStats().Health==0)
                {
                    maingame.Exit();
                }
            }
            if (menuoption == 2 && prevKeyboardState.IsKeyUp(Keys.Z) && currentKeyboardState.IsKeyDown(Keys.Z))
            {
                maingame.Exit();
            }
        }

        public void Draw()
        {
            spriteBatch.Draw(combatmenubase, new Rectangle(0, 400, _width, 200), Color.White);
            spriteBatch.Draw(dummyplayertexture,new Rectangle(100,150,64,64),Color.White);
            spriteBatch.Draw(dummyenemytexture, new Rectangle(500,150,64,64), Color.White);
            spriteBatch.DrawString(combatfontsmall,dummyplayer.GetPlayerStats().Health+"", new Vector2(110, 234), Color.Red);
            spriteBatch.DrawString(combatfontsmall, dummyenemy.GetEnemyStats().Health + "", new Vector2(510, 234), Color.Red);
            spriteBatch.DrawString(combatfontbig, "FIGHT", new Vector2(100, 460), Color.White);
            spriteBatch.DrawString(combatfontbig, "RUN", new Vector2(500, 460), Color.White);
            if (menuoption == 1)
            {
                spriteBatch.Draw(cursor, new Rectangle(36,460,64,64),Color.White);
            }
            else if(menuoption == 2)
            {
                spriteBatch.Draw(cursor, new Rectangle(436, 460, 64, 64), Color.White);
            }
        }
        public void SetSpriteBatch(SpriteBatch sb)
        {
            spriteBatch = sb;
        }

    }
}