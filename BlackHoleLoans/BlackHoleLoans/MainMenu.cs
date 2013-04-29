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
  class MainMenu
  {
    private SpriteBatch spriteBatch;
    private ContentManager _content;
    private Texture2D spaceship, stars, BHLlogo;

    KeyboardState prevKeyboardState, currentKeyboardState;
    SpriteFont smallFont, bigFont;//Fonts used to display text on the screen

    private int _height, _width; //Screen resolution
    private int numOptions, lowestPossibleY;//Number of menu options and the lowest Y value to draw the cursor
    private int cursorX, cursorY;
    int volume, menuCursorLocation;
    int menuScreen;//Used to determine which menu screen the user is currently on

    const int MAINMENU=1, CONTROLS=2, CREDITS=3;

    public MainMenu(ContentManager content, int width, int height)
    {
      _content = content;
      _width = width;
      _height = height;

      prevKeyboardState = Keyboard.GetState();
      currentKeyboardState = Keyboard.GetState();

      cursorX = 200;
      cursorY = 200;
      menuCursorLocation = 1;
      volume = 25;
      menuScreen = 1;
      numOptions = 4;
      lowestPossibleY = 425;
    }

    public void LoadContent()
    {
      //Loads all the images needed for the main menu
      spaceship = _content.Load<Texture2D>("MainMenu/spaceship");
      stars = _content.Load<Texture2D>("MainMenu/stars");
      BHLlogo = _content.Load<Texture2D>("MainMenu/BHL4");
      smallFont = _content.Load<SpriteFont>("Fonts/MenuOptions");
      bigFont = _content.Load<SpriteFont>("Fonts/MenuTitles");
    }

    public int Update()
    {
      prevKeyboardState = currentKeyboardState;
      currentKeyboardState = Keyboard.GetState();

      /*
       *These two conditionals change the number of options depending on which
       *screen the user is on. The first screen (main menu) has fewer options than
       *then options screen
       */
      if (menuScreen == MAINMENU)
      {
        numOptions = 4;
        lowestPossibleY = 425;
      }

      return interactWithMainMenu();
    }

    protected int interactWithMainMenu()
    {
      //Console.WriteLine(menuCursorLocation + " " + menuScreen);

      #region Entering a menu options

      //If enter was pressed..
      if (prevKeyboardState.IsKeyDown(Keys.B) && currentKeyboardState.IsKeyUp(Keys.B))
      {
        if (menuScreen == CONTROLS || menuScreen == CREDITS)
          menuScreen = MAINMENU;
      }

      if (prevKeyboardState.IsKeyDown(Keys.Enter) && currentKeyboardState.IsKeyUp(Keys.Enter))
      {
        if ((menuCursorLocation == 1 && menuScreen == MAINMENU))
          return 1;

        else if (menuCursorLocation == 2)
        {
          menuCursorLocation = 1;
          menuScreen = CONTROLS;
          cursorY = 200;
        }

        else if (menuCursorLocation == 3)
        {
          menuCursorLocation = 1;
          menuScreen = CREDITS;
          cursorY = 200;
        }

        else if (menuCursorLocation == 4)
          return -1;
      }

      #endregion

      #region Move cursor in the menus

      else if (menuScreen==MAINMENU && prevKeyboardState.IsKeyUp(Keys.Down) && currentKeyboardState.IsKeyDown(Keys.Down))
      {
        if (menuCursorLocation != numOptions)
        {
          cursorY += 75;
          menuCursorLocation++;
        }
        else
        {
          cursorY = 200;
          menuCursorLocation = 1;
        }

      }
      else if (menuScreen == MAINMENU && prevKeyboardState.IsKeyUp(Keys.Up) && currentKeyboardState.IsKeyDown(Keys.Up))
      {
        if (menuCursorLocation != 1)
        {
          cursorY -= 75;
          menuCursorLocation--;
        }
        else
        {
          cursorY = lowestPossibleY;
          menuCursorLocation = numOptions;
        }
      }
      #endregion

      return 0;

    }

    public void Draw()
    {
      //Console.WriteLine(menuScreen);

      //Draws the background
      spriteBatch.Draw(stars, new Rectangle(0, 0, _width, _height), Color.White);
      //Draws the BHL logo
      spriteBatch.Draw(BHLlogo, new Vector2(200, 20), null,
                    Color.White, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);
      //Calls the Draw methods depending on the chosen option
      if (menuScreen == MAINMENU)
        drawMainMenu();
      else if (menuScreen == CONTROLS)
        drawControls();
      else if (menuScreen == CREDITS)
        drawCredits();


    }


    protected void drawMainMenu()
    {
      spriteBatch.DrawString(bigFont, "Main Menu", new Vector2(200, 150), Color.White);
      spriteBatch.DrawString(smallFont, "Start Game", new Vector2(350, 220), Color.White);
      spriteBatch.DrawString(smallFont, "Controls", new Vector2(350, 295), Color.White);
      spriteBatch.DrawString(smallFont, "Credits", new Vector2(350, 370), Color.White);
      spriteBatch.DrawString(smallFont, "Exit", new Vector2(350, 445), Color.White);

      spriteBatch.Draw(spaceship, new Vector2(cursorX, cursorY), null,
                    Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);

    }

    protected void drawControls()
    {
      spriteBatch.DrawString(bigFont, "Controls", new Vector2(200, 150), Color.White);

      spriteBatch.DrawString(bigFont, "Please read the README.txt file", new Vector2(200,250), Color.White);
      spriteBatch.DrawString(bigFont, "for information related to controls", new Vector2(200, 350), Color.White);
      //Add in all the keys we'll be using in the game
    }

    protected void drawCredits()
    {
      spriteBatch.DrawString(bigFont, "Credits!", new Vector2(200, 150), Color.White);
      spriteBatch.DrawString(smallFont, "Created By:", new Vector2(200, 200), Color.White);

      spriteBatch.DrawString(smallFont, "Andy Tonoyan   Tyler Hall", new Vector2(250, 250), Color.White);
      spriteBatch.DrawString(smallFont, "Charles Baker    Eric Meisner", new Vector2(250, 300), Color.White);
      spriteBatch.DrawString(smallFont, "Michael Briseno", new Vector2(250, 350), Color.White);

      spriteBatch.DrawString(smallFont, "Thanks to:", new Vector2(200, 400), Color.White);
      //Enter people to thank..
    }

    public void setSpriteBatch(SpriteBatch sB)
    {
      spriteBatch = sB;
    }
  }
}
