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
  public class Combat
  {
    #region global variables
    private Game1 maingame;
    public Queue<string> messageQueue;//change back to private
    private SpriteBatch spriteBatch;
    private ContentManager _content;
    private int _height, _width, menuoption;
    private int selectedPlayerAction;
    private int highestMenuOption, lowestMenuOption, menuLayer;
    private Texture2D combatmenubase, cursor, dummyplayertexture, dummyenemytexture, healthbar, gray, messagebase;
    private SpriteFont combatfontbig, combatfontsmall, verysmallfont;
    private SoundEffect menuselect,menuconfirm,menuback,enemydeath,winsound,losesound;
    private Song battletheme;
    private Player selectedPlayer, enemySelectedPlayer;
    private Enemy selectedEnemy;
    private Player[] thePlayers;
    private Enemy[] theEnemies;
    private Random random;
    KeyboardState prevKeyboardState, currentKeyboardState;
    private static readonly TimeSpan menuinterval = TimeSpan.FromMilliseconds(100);
    private static readonly TimeSpan messageinterval = TimeSpan.FromMilliseconds(1500);
    private TimeSpan lastMenuChoiceTime, lastMessageTime;
    private bool executeMenuLogic;
    private Skill chosenEnemySkill;
    public int onlyDoOnce = 0;
    public bool ranAway;
    private Texture2D[] backgrounds;
    #endregion
    public enum MenuOption { Fight = 1, Run = 2, Attack = 3, SkillA = 4, SkillB = 5 }

    public Combat(ContentManager content, int height, int width, Game1 game, Player[] p, Enemy[] e)
    {
      _content = content;
      _height = height;
      _width = width;
      maingame = game;
      random = new Random();
      highestMenuOption = 1;
      lowestMenuOption = 3;
      menuLayer = 0;
      menuoption = (int)MenuOption.Fight;
      prevKeyboardState = Keyboard.GetState();
      currentKeyboardState = Keyboard.GetState();
      thePlayers = p;
      theEnemies = e;
      messageQueue = new Queue<string>();
      AddMessage("Player Turn");
      ranAway = false;
      backgrounds = new Texture2D[3];
      MediaPlayer.IsRepeating = true;
      MediaPlayer.Volume = 0.25f;
    }

    public bool IsDone()
    {
      if (AllPlayersAreFainted() || AllEnemiesAreDead())
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public void LoadContent()
    {
      #region textures
      combatmenubase = _content.Load<Texture2D>("Combat/combatmenubase");
      cursor = _content.Load<Texture2D>("Combat/cursor");
      dummyplayertexture = _content.Load<Texture2D>("Combat/dummyplayertexture");
      dummyenemytexture = _content.Load<Texture2D>("Combat/dummyenemytexture");
      healthbar = _content.Load<Texture2D>("Combat/healthbar");
      gray = _content.Load<Texture2D>("Combat/gray");
      messagebase = _content.Load<Texture2D>("Combat/messagebase");

      backgrounds[0] = _content.Load<Texture2D>("CombatBackgrounds/CombatBackgroundBrown");
      backgrounds[1] = _content.Load<Texture2D>("CombatBackgrounds/CombatBackgroundYellow");
      backgrounds[2] = _content.Load<Texture2D>("CombatBackgrounds/CombatBackgroundRed");
      #endregion
      #region fonts
      combatfontbig = _content.Load<SpriteFont>("Combat/combatfontbig");
      combatfontsmall = _content.Load<SpriteFont>("Combat/combatfontsmall");
      verysmallfont = _content.Load<SpriteFont>("Combat/verysmallfont");
      #endregion
      #region sounds
      menuselect = _content.Load<SoundEffect>("Combat/menuselect");
      menuconfirm = _content.Load<SoundEffect>("Combat/menuconfirm");
      menuback = _content.Load<SoundEffect>("Combat/menuback");
      battletheme = _content.Load<Song>("Combat/battletheme");
      enemydeath = _content.Load<SoundEffect>("Combat/enemydeath");
      winsound = _content.Load<SoundEffect>("Combat/winsound");
      losesound = _content.Load<SoundEffect>("Combat/losesound");
      MediaPlayer.Play(battletheme);
      #endregion
    }

    public void Update(GameTime gameTime)
    {
      prevKeyboardState = currentKeyboardState;
      currentKeyboardState = Keyboard.GetState();
      #region combat menu logic
      if (executeMenuLogic && !IsDone())
      {
        UpdateMenuOption();
        if (menuLayer == 0)
        {
          LayerZeroAction(gameTime);
        }
        if (menuLayer == 1)
        {
          LayerOneAction(gameTime);
        }
        if (menuLayer == 2)
        {
          LayerTwoAction(gameTime);
        }
        if (menuLayer == 3)
        {
          LayerThreeAction(gameTime);
        }
      }
      if (AllPlayersHaveGone())
      {
        ChangeTurns(gameTime);
      }
      #endregion
      PlayMenuSounds();
    }

    private void PlayMenuSounds()
    {
        if (IsKeyClicked(Keys.Z))
        {
            menuconfirm.Play();
        }
        if (IsKeyClicked(Keys.X))
        {
            menuback.Play();
        }
    }

    private bool AllPlayersHaveGone()
    {
      for (int i = 0; i < thePlayers.Length; i++)
      {
        if (!thePlayers[i].hasGone)
        {
          return false;
        }
      }
      return true;
    }

    private bool AllPlayersAreFainted()
    {
      for (int i = 0; i < thePlayers.Length; i++)
      {
        if (!thePlayers[i].isFainted)
        {
          return false;
        }
      }
      return true;
    }

    private bool AllEnemiesAreDead()
    {
      for (int i = 0; i < theEnemies.Length; i++)
      {
        if (!theEnemies[i].isDead)
        {
          return false;
        }
      }
      return true;
    }

    private void ChangeTurns(GameTime gameTime)
    {
      if (!WonFight() && !LostFight())
      {
        StartEnemyTurn(gameTime);
        AddMessage("Player Turn");
      }
      else if (WonFight())
      {
        if (onlyDoOnce == 0)
        {
          MediaPlayer.Stop();
          winsound.Play();
          AddMessage("You have defeated the enemy!");
          onlyDoOnce++;
        }
      }

      else if (LostFight())
      {
        if (onlyDoOnce == 0)
        {
          MediaPlayer.Stop();
          losesound.Play();
          AddMessage("You have been defeated!");
          onlyDoOnce++;
        }

      }
      for (int i = 0; i < thePlayers.Length; i++)
      {
        if(!thePlayers[i].isFainted)
          thePlayers[i].hasGone = false;
      }
    }

    private void UpdateMenuOption()
    {
      if (IsKeyClicked(Keys.Down) && menuoption != lowestMenuOption)
      {
        menuoption++;
        menuselect.Play();
      }
      if (IsKeyClicked(Keys.Up) && menuoption != highestMenuOption)
      {
        menuoption--;
        menuselect.Play();
      }
    }

    private void LayerZeroAction(GameTime gameTime)
    {
      if (menuoption == 1 && IsKeyClicked(Keys.Z) &&
          thePlayers[0].hasGone == false && !thePlayers[0].isFainted)
      {
        selectedPlayer = thePlayers[0];
        //thePlayers[0].hasGone = true;
        lastMenuChoiceTime = gameTime.TotalGameTime;
        ChangeLayers(1);
      }
      if (menuoption == 2 && IsKeyClicked(Keys.Z) &&
          thePlayers[1].hasGone == false && !thePlayers[1].isFainted)
      {
        selectedPlayer = thePlayers[1];
        //thePlayers[1].hasGone = true;
        lastMenuChoiceTime = gameTime.TotalGameTime;
        ChangeLayers(1);
      }
      if (menuoption == 3 && IsKeyClicked(Keys.Z) &&
          thePlayers[2].hasGone == false && !thePlayers[2].isFainted)
      {
        selectedPlayer = thePlayers[2];
        lastMenuChoiceTime = gameTime.TotalGameTime;
        ChangeLayers(1);
      }
    }

    private void LayerThreeAction(GameTime gameTime)
    {
      if (menuoption == 1 &&
          IsKeyClicked(Keys.Z))
      {
        if (lastMenuChoiceTime + menuinterval < gameTime.TotalGameTime)
        {
          selectedEnemy = theEnemies[0];
          ExecutePlayerAction(selectedPlayerAction, gameTime);
          selectedPlayer.hasGone = true;
        }
      }

      if (menuoption == 2 &&
          IsKeyClicked(Keys.Z))
      {
        if (lastMenuChoiceTime + menuinterval < gameTime.TotalGameTime)
        {
          selectedEnemy = theEnemies[1];
          ExecutePlayerAction(selectedPlayerAction, gameTime);
          selectedPlayer.hasGone = true;
        }
      }

      if (menuoption == 3 &&
          IsKeyClicked(Keys.Z))
      {
        if (lastMenuChoiceTime + menuinterval < gameTime.TotalGameTime)
        {
          selectedEnemy = theEnemies[2];
          ExecutePlayerAction(selectedPlayerAction, gameTime);
          selectedPlayer.hasGone = true;
        }
      }
      if (IsKeyClicked(Keys.X))
      {
        if (lastMenuChoiceTime + menuinterval < gameTime.TotalGameTime)
        {
          ChangeLayers(2);
        }
      }
    }

    private void LayerTwoAction(GameTime gameTime)
    {
      if (menuoption == 1 &&
          IsKeyClicked(Keys.Z))
      {
        if (lastMenuChoiceTime + menuinterval < gameTime.TotalGameTime)
        {
          lastMenuChoiceTime = gameTime.TotalGameTime;
          selectedPlayerAction = 1;
          ChangeLayers(3);
        }
      }

      if (menuoption == 2 &&
          IsKeyClicked(Keys.Z))
      {
        if (lastMenuChoiceTime + menuinterval < gameTime.TotalGameTime)
        {
          lastMenuChoiceTime = gameTime.TotalGameTime;
          selectedPlayerAction = 2;
          ChangeLayers(3);
        }
      }

      if (menuoption == 3 &&
          IsKeyClicked(Keys.Z))
      {
        if (lastMenuChoiceTime + menuinterval < gameTime.TotalGameTime)
        {
          lastMenuChoiceTime = gameTime.TotalGameTime;
          selectedPlayerAction = 3;
          ChangeLayers(3);
        }
      }
      if (IsKeyClicked(Keys.X))
      {
        if (lastMenuChoiceTime + menuinterval < gameTime.TotalGameTime)
        {
          ChangeLayers(1);
        }
      }
    }

    private void LayerOneAction(GameTime gameTime)
    {
      if (menuoption == 1 &&
          IsKeyClicked(Keys.Z))
      {
        if (lastMenuChoiceTime + menuinterval < gameTime.TotalGameTime)
        {
          lastMenuChoiceTime = gameTime.TotalGameTime;
          ChangeLayers(2);
        }
      }
      if (menuoption == 2 &&
          IsKeyClicked(Keys.Z))
      {
        if (lastMenuChoiceTime + menuinterval < gameTime.TotalGameTime)//here
        {
          
          Random random = new Random();
          if ((random.Next(0, 100) + 1) <= 20)
          {
            MediaPlayer.Stop();
            AddMessage("Player ran away!");
            ranAway = true;
          }
          else
            AddMessage("You failed to run away!");

          selectedPlayer.hasGone = true;
          ChangeLayers(0);
        }
        lastMessageTime = gameTime.TotalGameTime;
      }
      if (IsKeyClicked(Keys.X))
      {
        if (lastMenuChoiceTime + menuinterval < gameTime.TotalGameTime)
        {
          ChangeLayers(0);
          //selectedPlayer.hasGone = false;
        }
      }
    }

    private void ChangeLayers(int layer)
    {
      menuoption = 1;
      if (layer == 1)
      {
        lowestMenuOption = 2;
      }
      else if (layer == 2 || layer == 0)
      {
        lowestMenuOption = 3;
      }
      else if (layer == 3)
      {
        lowestMenuOption = theEnemies.Length;
      }
      menuLayer = layer;
    }

    public void Draw(GameTime gameTime, int whichBG)
    {
      DrawBackground(whichBG);
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
      spriteBatch.Draw(messagebase, new Rectangle(0, 0, _width, messagebase.Height), Color.White);
      Vector2 textCenter = combatfontsmall.MeasureString(message) * .5f;
      spriteBatch.DrawString(combatfontsmall, message, new Vector2(_width / 2 - textCenter.X, messagebase.Height / 2 - textCenter.Y), Color.White);
    }

    public void AddMessage(String m)
    {
      messageQueue.Enqueue(m);
    }

    private void StartEnemyTurn(GameTime gameTime)
    {
      AddMessage("Enemy Turn");
      for (int i = 0; i < theEnemies.Length; i++)
      {
        if (!theEnemies[i].isDead)
        {
          SelectPlayer();
          ExecuteEnemyAction(theEnemies[i], gameTime);
        }
      }
    }

    private void ExecuteEnemyAction(Enemy e, GameTime gameTime)
    {
      lastMessageTime = gameTime.TotalGameTime;
      SetHealthReferencePoint();
      if (e.GetEnemyStats().Health > 0)
      {
        SetHealthReferencePoint();
        if (e.whichAi == 1)
        {
          e.ExecuteAI1(enemySelectedPlayer);
          AddMessage(e.Name + " attacked " + enemySelectedPlayer.Name + "!");
        }
        else if (e.whichAi == 2)
        {
          chosenEnemySkill = e.ExecuteAI2(enemySelectedPlayer, e);
          if (chosenEnemySkill != null)
          {
            AddMessage(e.Name + " cast " + e.skillA.Name + "!");
          }
          else
          {
            AddMessage(e.Name + " attacked " + enemySelectedPlayer.Name + "!");
          }
        }
        else if (e.whichAi == 3)
        {
          chosenEnemySkill = e.ExecuteAI3(enemySelectedPlayer, e);
          AddMessage(e.Name + " cast " + chosenEnemySkill.Name + "!");
        }
        DeterminePlayerMessages(enemySelectedPlayer);
        DetermineOtherMessages(enemySelectedPlayer);
      }
    }

    //used to select a player for enemies to attack
    private void SelectPlayer()
    {
      enemySelectedPlayer = thePlayers[random.Next(0, 3)];
      
      while (enemySelectedPlayer.isFainted && !LostFight())
      {
        enemySelectedPlayer = thePlayers[random.Next(0, 3)];
      }
    }

    private void ExecutePlayerAction(int option, GameTime gameTime)
    {
      SetHealthReferencePoint();
      lastMessageTime = gameTime.TotalGameTime;
      if (option == 1)
      {
        selectedPlayer.ExecuteBasicAttack(selectedEnemy);
        AddMessage(selectedPlayer.Name + " attacked " + selectedEnemy.Name + "!");
      }
      else if (option == 2)
      {
        selectedPlayer.ExecuteSkillA(selectedEnemy, selectedPlayer);
        AddMessage(selectedPlayer.Name + " used " + selectedPlayer.skillA.Name + "!");
      }
      else if (option == 3)
      {
        selectedPlayer.ExecuteSkillB(selectedEnemy, selectedPlayer);
        AddMessage(selectedPlayer.Name + " used " + selectedPlayer.skillB.Name + "!");
      }
      DetermineEnemyMessages();
      DetermineOtherMessages(selectedPlayer);
      ChangeLayers(0);
    }

    private void SetHealthReferencePoint()
    {
      for (int i = 0; i < thePlayers.Length; i++)
      {
        thePlayers[i].lastPlayerHealth = thePlayers[i].GetPlayerStats().Health;
      }
      for (int i = 0; i < theEnemies.Length; i++)
      {
        theEnemies[i].lastEnemyHealth = theEnemies[i].GetEnemyStats().Health;
      }
    }

    public void DrawCombatMenu()
    {
      Vector2 textSize1 = combatfontsmall.MeasureString("FIGHT");
      Vector2 textCenter1 = combatfontsmall.MeasureString("FIGHT") * .5f;
      Vector2 textCenter2 = combatfontsmall.MeasureString("RUN") * .5f;
      Vector2 textCenter3 = combatfontsmall.MeasureString("Attack") * .5f;
      DrawMainMenu(textCenter1, textCenter2, textSize1);
      if (menuLayer == 1 || menuLayer == 0)
      {
        DrawLayerOne();
      }
      if (menuLayer == 2)
      {
        DrawLayerTwo(textSize1, textCenter3);
      }
      if (menuLayer == 3)
      {
        DrawLayerThree();
      }
      if (executeMenuLogic)
      {
        if (menuLayer != 0)
        {
          DrawMenuCursor(textCenter1, textCenter3);
        }
        else
        {
          DrawPlayerCursor();
        }
      }
    }

    private void DrawPlayerCursor()
    {
      if (menuoption == 1)
      {
        spriteBatch.Draw(cursor,
            new Rectangle(_width / 8 - 55,
                47 * _height / 64, cursor.Width, cursor.Height + 10),
                Color.White);
      }
      else if (menuoption == 2)
      {
        spriteBatch.Draw(cursor,
            new Rectangle(_width / 8 - 55,
                52 * _height / 64, cursor.Width, cursor.Height + 10),
                Color.White);
      }
      else if (menuoption == 3)
      {
        spriteBatch.Draw(cursor,
           new Rectangle(_width / 8 - 55, 57 * _height / 64,
               cursor.Width, cursor.Height + 10), Color.White);
      }
    }

    private void DrawMenuCursor(Vector2 textCenter1, Vector2 textCenter3)
    {
      if (menuoption == 1)
      {
        spriteBatch.Draw(cursor,
            new Rectangle(5 * _width / 8 - 10,
                12 * _height / 16, cursor.Width - 110, cursor.Height),
                Color.White);
      }
      else if (menuoption == 2)
      {
        spriteBatch.Draw(cursor,
            new Rectangle(5 * _width / 8 - 10,
                13 * _height / 16, cursor.Width - 110, cursor.Height),
                Color.White);
      }
      else if (menuoption == 3)
      {
        spriteBatch.Draw(cursor,
           new Rectangle(5 * _width / 8 - 10, 14 * _height / 16,
               cursor.Width - 110, cursor.Height), Color.White);
      }
    }

    private void DrawLayerThree()
    {
      for (int i = 0; i < theEnemies.Length; i++)
      {
        spriteBatch.DrawString(combatfontsmall, theEnemies[i].Name,
            new Vector2(5 * _width / 8, (12 + i) * _height / 16), Color.White);
      }
    }

    private void DrawLayerTwo(Vector2 textSize1, Vector2 textCenter3)
    {
      spriteBatch.DrawString(combatfontsmall, "Attack",
          new Vector2(5 * _width / 8, 12 * _height / 16), Color.White);
      spriteBatch.DrawString(combatfontsmall, selectedPlayer.skillA.Name,
          new Vector2(5 * _width / 8, 13 * _height / 16), Color.White);
      spriteBatch.DrawString(combatfontsmall, selectedPlayer.skillB.Name,
          new Vector2(5 * _width / 8, 14 * _height / 16), Color.White);
    }

    private void DrawMainMenu(Vector2 textCenter1, Vector2 textCenter2, Vector2 textSize1)
    {
      spriteBatch.Draw(combatmenubase, new Rectangle(0, 4 * _height / 6,
          _width, combatmenubase.Height),
          Color.White);
      for (int i = 0; i < thePlayers.Length; i++)
      {
        DrawHealthBars((2 * _width / 8)-30, 3 * _height / 4 + (i * 50), thePlayers[i]);
        spriteBatch.DrawString(combatfontsmall,
            thePlayers[i].GetPlayerStats().Health + "/"
            + thePlayers[i].GetPlayerStats().TotalHealth,
            new Vector2((2 * _width / 8)-30 + healthbar.Width + 10, 3 * _height / 4 + (i * 50) - 3), Color.White);
        spriteBatch.DrawString(combatfontsmall, thePlayers[i].Name, new Vector2(_width / 16, 3 * _height / 4 + (i * 50) - 7), StatusColor(thePlayers[i]));
      }
      GrayOutPlayers();
    }

    private void GrayOutPlayers()
    {
      if (thePlayers[0].hasGone || thePlayers[0].isFainted)
      {
        spriteBatch.Draw(gray,
            new Rectangle(_width / 8 - 55,
                47 * _height / 64, cursor.Width, cursor.Height + 10),
                Color.Gray * .5f);
      }
      if (thePlayers[1].hasGone || thePlayers[1].isFainted)
      {
        spriteBatch.Draw(gray,
            new Rectangle(_width / 8 - 55,
                52 * _height / 64, cursor.Width, cursor.Height + 10),
                Color.Gray * .5f);
      }
      if (thePlayers[2].hasGone || thePlayers[2].isFainted)
      {
        spriteBatch.Draw(gray,
           new Rectangle(_width / 8 - 55, 57 * _height / 64,
               cursor.Width, cursor.Height + 10), Color.Gray * .5f);
      }
    }

    private void DrawLayerOne()
    {
      spriteBatch.DrawString(combatfontsmall, "Fight",
          new Vector2(5 * _width / 8, 12 * _height / 16), Color.White);
      spriteBatch.DrawString(combatfontsmall, "Run",
          new Vector2(5 * _width / 8, 13 * _height / 16), Color.White);
    }

    public void DrawEntities()
    {
      DrawPlayers();
      DrawEnemys();
      /* DrawHealthBars(_width /8, 7 * _width / 8 - dummyenemytexture.Width,
           _height / 4 + dummyplayertexture.Height,
           _height / 4 + dummyplayertexture.Height);*/
    }

    private void DrawEnemys()
    {
      for (int i = 0; i < theEnemies.Length; i++)
      {
        if (!theEnemies[i].isDead)
        {
          spriteBatch.Draw(theEnemies[i].enemySprite,
              new Rectangle(7 * _width / 8 - theEnemies[i].enemySprite.Width, (i + 1) * _height / 6,
              theEnemies[i].enemySprite.Width, theEnemies[i].enemySprite.Height), Color.White);
        }
      }
    }
    //change to edit how players look
    private void DrawPlayers()
    {
      for (int i = 0; i < thePlayers.Length; i++)
      {
        spriteBatch.Draw(thePlayers[i].GetPlayerSprites()[1], 
          new Rectangle(_width / 8, (i + 1) * _height / 6,
            dummyplayertexture.Width, dummyplayertexture.Height), Color.White);
      }
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

    public void DrawHealthBars(int x, int y, Player p)
    {
      //player health bar
      spriteBatch.Draw(healthbar, new Rectangle(x, y, healthbar.Width, healthbar.Height), Color.Black);
      spriteBatch.Draw(healthbar, new Rectangle(x + 5, y + 5, healthbar.Width - 10, healthbar.Height - 10), Color.Gray);
      spriteBatch.Draw(healthbar, new Rectangle(x + 5, y + 5,
          (int)((healthbar.Width - 10) *
          ((double)p.GetPlayerStats().Health /
          (double)p.GetPlayerStats().TotalHealth)),
          healthbar.Height - 10), Color.Red);
    }

    private void DetermineOtherMessages(Player p)
    {
      if (p.lastPlayerHealth == p.GetPlayerStats().Health &&
          selectedEnemy.lastEnemyHealth == selectedEnemy.GetEnemyStats().Health)
      {
        AddMessage("No effect..");
      }
    }

    private void DetermineEnemyMessages()
    {
      if (selectedEnemy.lastEnemyHealth > selectedEnemy.GetEnemyStats().Health &&
          selectedEnemy.GetEnemyStats().Health != 0)
      {
        AddMessage(selectedEnemy.Name + " took " +
            (selectedEnemy.lastEnemyHealth - selectedEnemy.GetEnemyStats().Health)
            + " damage!");
      }
      else if (selectedEnemy.lastEnemyHealth < selectedEnemy.GetEnemyStats().Health)
      {
        AddMessage(selectedEnemy.Name + " recovered " +
            (selectedEnemy.GetEnemyStats().Health - selectedEnemy.lastEnemyHealth)
            + " health!");
      }
      else if (selectedEnemy.GetEnemyStats().Health == 0 && selectedEnemy.isDead == false)
      {
        AddMessage(selectedEnemy.Name + " died!");
        enemydeath.Play();
        selectedEnemy.isDead = true;
      }
    }

    private void DeterminePlayerMessages(Player p)
    {
      if (p.lastPlayerHealth > p.GetPlayerStats().Health &&
          p.GetPlayerStats().Health != 0)
      {
        AddMessage(p.Name + " took " +
            (p.lastPlayerHealth - p.GetPlayerStats().Health)
            + " damage!");
      }

      else if (p.lastPlayerHealth < p.GetPlayerStats().Health)
      {
        AddMessage(p.Name + " recovered " +
            (p.GetPlayerStats().Health - p.lastPlayerHealth)
            + " health!");
      }

      else if (p.GetPlayerStats().Health <= 0 && p.isFainted == false)
      {
        AddMessage(p.Name + " fainted!");
        p.isFainted = true;
      }
    }

    public bool IsKeyClicked(Keys key)
    {
      return prevKeyboardState.IsKeyUp(key) &&
          currentKeyboardState.IsKeyDown(key);
    }

    public bool WonFight()
    {
      return AllEnemiesAreDead();
    }

    public bool LostFight()
    {
      return AllPlayersAreFainted();
    }

    private Color StatusColor(Player player)
    {
      if (player.isFainted == true)
        return Color.Black;
      else if (((double)(player.GetPlayerStats().Health) / (double)(player.GetPlayerStats().TotalHealth)) <= .25)
        return Color.Red;
      else
        return Color.White;
    }

    private void DrawBackground(int whichBG)
    {
      if (whichBG == 7)
        spriteBatch.Draw(backgrounds[0], Vector2.Zero, Color.White);//change

      else if (whichBG == 6)
        spriteBatch.Draw(backgrounds[1], Vector2.Zero, Color.White);

      else if(whichBG == 8)
        spriteBatch.Draw(backgrounds[2], Vector2.Zero, Color.White);
    }

    public bool RanAway()
    {
      return ranAway;
    }

  }
}
