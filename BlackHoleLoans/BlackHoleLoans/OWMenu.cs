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
using BlackHoleLoans.PlayerRelated;

namespace BlackHoleLoans
{
  class OWMenu
  {
    Texture2D background;
    SpriteFont font, largeFont;
    KeyboardState keystate, prevKeystate;
    Player[] party;

    public OWMenu(Player[] p)//Should pass in the Player[] party from game1
    {
      keystate = Keyboard.GetState();
      prevKeystate = keystate;
      party = p;
    }

    public void LoadContent(ContentManager content)
    {
      background = content.Load<Texture2D>("MainMenu/stars");

      font = content.Load<SpriteFont>("Fonts/MenuOptions");
      largeFont = content.Load<SpriteFont>("Fonts/PauseMenu");
    }

    public int Update(Game g)
    {
      keystate = Keyboard.GetState();

      if (keystate.IsKeyDown(Keys.R) && prevKeystate.IsKeyUp(Keys.R))
      {
        return 2;
      }

      else if (keystate.IsKeyDown(Keys.M) && prevKeystate.IsKeyUp(Keys.M))
      {
        return 0;
      }

      else if (keystate.IsKeyDown(Keys.E))
      {
        g.Exit();
      }

      prevKeystate = keystate;
      return 4;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(background, new Rectangle(0, 0, 800, 600), Color.White);
      
      spriteBatch.DrawString(largeFont,"Paused", new Vector2(325,100), Color.White);
      spriteBatch.DrawString(font, "Resume (R)", new Vector2(325, 150), Color.White);
      spriteBatch.DrawString(font, "Back to Main Menu (M)", new Vector2(325, 200), Color.White);
      spriteBatch.DrawString(font, "Exit Game (E)", new Vector2(325, 250), Color.White);

      for (int i = 0; i < 3; i++)
      {

        spriteBatch.DrawString(font, party[i].Name, new Vector2(150, 300+(i*50)), Color.White);
        spriteBatch.DrawString(font, "Health: " + party[i].GetPlayerStats().Health + "/" +
                  party[i].GetPlayerStats().TotalHealth, new Vector2(350, 300+(i*50)), Color.White);
      }
      spriteBatch.Draw(party[0].GetPlayerSprites()[1], new Vector2(300, 300), Color.White);
      spriteBatch.Draw(party[1].GetPlayerSprites()[0], new Vector2(300, 350), Color.White);
      spriteBatch.Draw(party[2].GetPlayerSprites()[0], new Vector2(300, 400), Color.White);
    }         
  }
}
